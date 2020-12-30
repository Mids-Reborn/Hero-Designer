using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mrbBase.Base.Data_Classes;
using mrbBase.Base.IO_Classes;
using mrbBase.Base.Master_Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Zstandard.Net;

namespace mrbBase
{
    public static class DatabaseAPI
    {
        public const int HeroAccolades = 3257;
        public const int VillainAccolades = 3258;
        public const int TempPowers = 3259;

        public const string MainDbName = "Mids' Hero Designer Database MK II";

        private const string RecipeName = "Mids' Hero Designer Recipe Database";

        private const string SalvageName = "Mids' Hero Designer Salvage Database";

        private const string EnhancementDbName = "Mids' Hero Designer Enhancement Database";
        private static IDictionary<string, int> AttribMod = new Dictionary<string, int>();

        private static readonly IDictionary<string, int> Classes = new Dictionary<string, int>();

        public static IDatabase Database
            => Base.Data_Classes.Database.Instance;

        private static void ClearLookups()
        {
            AttribMod.Clear();
            Classes.Clear();
        }

        public static void ExportAttribMods()
        {
            string path = $"{Application.StartupPath}\\Data\\Export\\attribModTables.json";
            string path2 = $"{Application.StartupPath}\\Data\\Export\\attribMod.json";
            string path3 = $"{Application.StartupPath}\\Data\\Export\\attribModOrdered.json";
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            File.WriteAllText(path, JsonConvert.SerializeObject(Database.AttribMods, serializerSettings));
            File.WriteAllText(path2, JsonConvert.SerializeObject(AttribMod, serializerSettings));
            Dictionary<string, int> ordered = AttribMod.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
            File.WriteAllText(path3, JsonConvert.SerializeObject(ordered, serializerSettings));
        }

        public static void UpdateModifiersDict(Modifiers.ModifierTable[] mList)
        {
            AttribMod.Clear();
            for (int i = 0; i < mList.Length; i++)
            {
                AttribMod.Add(mList[i].ID, i);
            }
        }

        public static int NidFromUidAttribMod(string uID)
        {
            if (string.IsNullOrEmpty(uID))
            {
                return -1;
            }

            if (AttribMod.ContainsKey(uID))
            {
                return AttribMod[uID];
            }

            for (var index = 0; index <= Database.AttribMods.Modifier.Length - 1; ++index)
            {
                if (!string.Equals(uID, Database.AttribMods.Modifier[index].ID, StringComparison.OrdinalIgnoreCase))
                    continue;
                AttribMod.Add(uID, index);
                return index;
            }

            return -1;
        }

        public static int NidFromUidClass(string uidClass)
        {
            if (string.IsNullOrEmpty(uidClass))
                return -1;
            if (Classes.ContainsKey(uidClass))
                return Classes[uidClass];
            var index = Database.Classes.TryFindIndex(cls =>
                string.Equals(uidClass, cls.ClassName, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
                Classes.Add(uidClass, index);
            return index;
        }

        public static string UidFromNidClass(int nIDClass)
        {
            return nIDClass < 0 || nIDClass > Database.Classes.Length ? string.Empty : Database.Classes[nIDClass].ClassName;
        }

        public static int NidFromUidOrigin(string uidOrigin, int nIDClass)
        {
            if (nIDClass < 0)
                return -1;
            return Database.Classes[nIDClass].Origin
                .TryFindIndex(o => string.Equals(o, uidOrigin, StringComparison.OrdinalIgnoreCase));
        }

        private static void FillGroupArray()
        {
            Database.PowersetGroups = new Dictionary<string, PowersetGroup>();
            foreach (var powerset in Database.Powersets)
            {
                if (string.IsNullOrEmpty(powerset.GroupName))
                    continue;
                if (!Database.PowersetGroups.TryGetValue(powerset.GroupName, out var powersetGroup))
                {
                    powersetGroup = new PowersetGroup(powerset.GroupName);
                    Database.PowersetGroups.Add(powerset.GroupName, powersetGroup);
                }

                powersetGroup.Powersets.Add(powerset.FullName, powerset);
                powerset.SetGroup(powersetGroup);
            }
        }

        public static int NidFromUidPowerset(string uidPowerset)
        {
            return GetPowersetByName(uidPowerset)?.nID ?? -1;
        }

        public static int NidFromStaticIndexPower(int sidPower)
        {
            if (sidPower < 0)
                return -1;
            return Database.Power.TryFindIndex(p => p.StaticIndex == sidPower);
        }

        public static int NidFromUidPower(string name)
        {
            return GetPowerByFullName(name)?.PowerIndex ?? -1;
        }

        public static int NidFromUidEntity(string uidEntity)
        {
            return Database.Entities.TryFindIndex(
                se => string.Equals(se.UID, uidEntity, StringComparison.OrdinalIgnoreCase));
        }

        private static int[] NidSets(PowersetGroup group, int nIDClass, Enums.ePowerSetType nType) // clsI12Lookup.vb
        {
            if ((nType == Enums.ePowerSetType.Inherent || nType == Enums.ePowerSetType.Pool) && nIDClass > -1 &&
                !Database.Classes[nIDClass].Playable)
                return Array.Empty<int>();


            var powersetArray = Database.Powersets;
            if (group != null) powersetArray = group.Powersets.Select(powerset => powerset.Value).ToArray();

            var intList = new List<int>();
            var checkType = nType != Enums.ePowerSetType.None;
            var checkClass = nIDClass > -1;
            foreach (var powerset in powersetArray)
            {
                var isOk = !checkType || powerset.SetType == nType;
                if (checkClass & isOk)
                {
                    if ((powerset.SetType == Enums.ePowerSetType.Primary ||
                         powerset.SetType == Enums.ePowerSetType.Secondary) &&
                        (powerset.nArchetype != nIDClass) & (powerset.nArchetype > -1))
                        isOk = false;
                    if (powerset.Powers.Length > 0 && isOk && powerset.SetType != Enums.ePowerSetType.Inherent &&
                        powerset.SetType != Enums.ePowerSetType.Accolade && powerset.SetType != Enums.ePowerSetType.Temp &&
                        !powerset.Powers[0].Requires.ClassOk(nIDClass))
                        isOk = false;
                }

                if (isOk)
                    intList.Add(powerset.nID);
            }

            return intList.ToArray();
        }

        public static int[] NidSets(string uidGroup, string uidClass, Enums.ePowerSetType nType)
        {
            return NidSets(Database.PowersetGroups.ContainsKey(uidGroup) ? Database.PowersetGroups[uidGroup] : null,
                NidFromUidClass(uidClass), nType);
        }

        public static int[] NidPowers(int nIDPowerset, int nIDClass = -1)
        {
            if (nIDPowerset < 0 || nIDPowerset > Database.Powersets.Length - 1)
            {
                //return Enumerable.Range(0, Database.Powersets.Length).ToArray();
                var array = new int[Database.Power.Length];
                for (var index = 0; index < Database.Power.Length; ++index)
                    array[index] = index;
                return array;
            }

            var powerset = Database.Powersets[nIDPowerset];
            return powerset.Powers.FindIndexes(pow => pow.Requires.ClassOk(nIDClass)).Select(idx => powerset.Power[idx])
                .ToArray();
        }

        public static int[] NidPowers(string uidPowerset, string uidClass = "")
        {
            return NidPowers(NidFromUidPowerset(uidPowerset), NidFromUidClass(uidClass));
        }

        public static string[] UidPowers(string uidPowerset, string uidClass = "")
        {
            if (!string.IsNullOrEmpty(uidPowerset))
                return Database.Power
                    .Where(pow =>
                        string.Equals(pow.FullSetName, uidPowerset, StringComparison.OrdinalIgnoreCase) &&
                        pow.Requires.ClassOk(uidClass)).Select(pow => pow.FullName).ToArray();
            var array = new string[Database.Power.Length];
            for (var index = 0; index < Database.Power.Length; ++index)
                array[index] = Database.Power[index].FullName;
            return array;
        }

        private static int[] NidPowersAtLevel(int iLevel, int nIDPowerset)
        {
            return nIDPowerset < 0
                ? Array.Empty<int>()
                : Database.Powersets[nIDPowerset].Powers.Where(pow => pow.Level - 1 == iLevel).Select(pow => pow.PowerIndex)
                    .ToArray();
        }

        public static int[] NidPowersAtLevelBranch(int iLevel, int nIDPowerset)
        {
            if (nIDPowerset < 0)
                return Array.Empty<int>();
            if (Database.Powersets[nIDPowerset].nIDTrunkSet < 0)
                return NidPowersAtLevel(iLevel, nIDPowerset);

            var powerset1 = NidPowersAtLevel(iLevel, nIDPowerset);
            var powerset2 = NidPowersAtLevel(iLevel, Database.Powersets[nIDPowerset].nIDTrunkSet);
            return powerset2.Concat(powerset1).ToArray();
        }

        public static void SaveJsonDatabase(ISerialize serializer, bool msgOnCompletion = true)
        {
            //var jsonSerializer = new JsonSerializer();

            var zipContent = new MemoryStream();
            var archive = new ZipArchive(zipContent, ZipArchiveMode.Create);
            AddZipFileEntry("Database.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database)), archive);
            AddZipFileEntry("Archetypes.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Classes)), archive);
            AddZipFileEntry("AttribMods.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.AttribMods)), archive);
            AddZipFileEntry("Enhancement.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Enhancements)), archive);
            AddZipFileEntry("EnhancementClasses.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.EnhancementClasses)), archive);
            AddZipFileEntry("Entities.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Entities)), archive);
            AddZipFileEntry("Levels.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Levels)), archive);
            AddZipFileEntry("Powers.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Power)), archive);
            AddZipFileEntry("PowerSets.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Powersets)), archive);
            AddZipFileEntry("PowerSetGroups.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.PowersetGroups)), archive);
            AddZipFileEntry("Recipes.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Recipes)), archive);
            AddZipFileEntry("Salvage.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Salvage)), archive);
            archive.Dispose();
            File.WriteAllBytes(Path.Combine(Application.StartupPath, @"Data\Mids.zip"), zipContent.ToArray());

            if (msgOnCompletion)
            {
                MessageBox.Show("Export completed.");
            }
        }

        public static void SaveJsonDatabaseProgress(ISerialize serializer, IntPtr frmProgressHandle, IWin32Window parent, bool msgOnCompletion = false)
        {
            //var jsonSerializer = new JsonSerializer();

            Form prg = (Form)Control.FromHandle(frmProgressHandle);
            if (prg == null)
            {
                SaveJsonDatabase(serializer, msgOnCompletion);
                return;
            }

            //prg.BeginInvoke(new Action(() => prg.ShowDialog()));

            var zipContent = new MemoryStream();
            prg.Text = "|0|Creating Zip archive...";
            var archive = new ZipArchive(zipContent, ZipArchiveMode.Create);

            prg.Text = "|8|Exporting Main database...";
            AddZipFileEntry("Database.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database)), archive);

            prg.Text = "|15|Exporting Archetypes...";
            AddZipFileEntry("Archetypes.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Classes)), archive);

            prg.Text = "|23|Exporting AttribMods database...";
            AddZipFileEntry("AttribMods.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.AttribMods)), archive);

            prg.Text = "|31|Exporting Enhancements database...";
            AddZipFileEntry("Enhancement.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Enhancements)), archive);

            prg.Text = "|38|Exporting Enhancement Classes...";
            AddZipFileEntry("EnhancementClasses.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.EnhancementClasses)), archive);

            prg.Text = "|46|Exporting Entities database...";
            AddZipFileEntry("Entities.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Entities)), archive);

            prg.Text = "|54|Exporting Levels database...";
            AddZipFileEntry("Levels.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Levels)), archive);

            prg.Text = "|62|Exporting Powers...";
            AddZipFileEntry("Powers.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Power)), archive);

            prg.Text = "|69|Exporting Powersets...";
            AddZipFileEntry("PowerSets.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Powersets)), archive);

            prg.Text = "|77|Exporting Powersets groups...";
            AddZipFileEntry("PowerSetGroups.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.PowersetGroups)), archive);

            prg.Text = "|85|Exporting Recipes database...";
            AddZipFileEntry("Recipes.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Recipes)), archive);

            prg.Text = "|92|Exporting Salvage database...";
            AddZipFileEntry("Salvage.json", Encoding.UTF8.GetBytes(serializer.Serialize(Database.Salvage)), archive);
            archive.Dispose();

            prg.Text = "|99|Writing Zip archive to disk...";
            File.WriteAllBytes(Path.Combine(Application.StartupPath, @"Data\Mids.zip"), zipContent.ToArray());

            prg.Text = "|100|";

            if (msgOnCompletion)
            {
                MessageBox.Show("Export completed.");
            }
        }

        private static void AddZipFileEntry(string fileName, byte[] fileContent, ZipArchive archive)
        {
            var entry = archive.CreateEntry(fileName);
            using (var stream = entry.Open())
            {
                stream.Write(fileContent, 0, fileContent.Length);
            }
        }

        public static string[] UidMutexAll()
        {
            var items = Database.Power.SelectMany(pow => pow.GroupMembership).Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            items.Sort();
            return items.ToArray();
        }

        public static IPowerset? GetPowersetByName(string iName)
        {
            var strArray = iName.Split('.');
            if (strArray.Length < 2)
                return null;

            if (strArray.Length > 2)
                iName = $"{strArray[0]}.{strArray[1]}";
            var key = strArray[0];
            if (!Database.PowersetGroups.ContainsKey(key))
                return null;
            var powersetGroup = Database.PowersetGroups[key];
            return powersetGroup.Powersets.ContainsKey(iName) ? powersetGroup.Powersets[iName] : null;
        }

        public static IPowerset GetPowersetByName(string iName, string iArchetype)
        {
            var idx = GetArchetypeByName(iArchetype).Idx;
            foreach (var powerset1 in Database.Powersets)
            {
                if (idx != powerset1.nArchetype && powerset1.nArchetype != -1 ||
                    !string.Equals(iName, powerset1.DisplayName, StringComparison.OrdinalIgnoreCase))
                    continue;
                if (powerset1.SetType != Enums.ePowerSetType.Ancillary)
                    return powerset1;
                if (powerset1.Power.Length > 0 && powerset1.Powers[0].Requires.ClassOk(idx))
                    return powerset1;
            }

            return null;
        }

        public static IPowerset? GetPowersetByIndex(int idx)
        {
            try
            {
                return Database.Powersets[idx];
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Pine
        public static IPowerset GetPowersetByName(string iName, Enums.ePowerSetType iSet)
        {
            return Database.Powersets.FirstOrDefault(powerset =>
                iSet == powerset.SetType && string.Equals(iName, powerset.DisplayName, StringComparison.OrdinalIgnoreCase));
        }

        public static IPowerset GetPowersetByID(string iName, Enums.ePowerSetType iSet)
        {
            return Database.Powersets.FirstOrDefault(ps =>
                iSet == ps.SetType && string.Equals(iName, ps.SetName, StringComparison.OrdinalIgnoreCase));
        }

        public static IPowerset GetInherentPowerset()
        {
            return Database.Powersets.FirstOrDefault(ps => ps.SetType == Enums.ePowerSetType.Inherent);
        }

        public static Archetype GetArchetypeByName(string iArchetype)
        {
            return Database.Classes.FirstOrDefault(cls =>
                string.Equals(iArchetype, cls.DisplayName, StringComparison.OrdinalIgnoreCase));
        }

        public static int GetOriginByName(Archetype archetype, string iOrigin)
        {
            for (var index = 0; index < archetype.Origin.Length; ++index)
                if (string.Equals(iOrigin, archetype.Origin[index], StringComparison.OrdinalIgnoreCase))
                    return index;
            return 0;
        }

        public static int GetPowerIndexByDisplayName(string iName, int iArchetype)
        {
            return Array.IndexOf(Database.Power, Database.Power.Where(p =>
                p.DisplayName == iName &&
                (p.PowerSetID <= -1 || Database.Powersets[p.PowerSetID].nArchetype == iArchetype ||
                 Database.Powersets[p.PowerSetID].nArchetype == -1)
            ));
        }

        public static IPower? GetPowerByDisplayName(string iName, int iArchetype)
        {
            var idx = Array.IndexOf(Database.Power, Database.Power.Where(p =>
                p.DisplayName == iName &&
                (p.PowerSetID <= -1 || Database.Powersets[p.PowerSetID].nArchetype == iArchetype ||
                 Database.Powersets[p.PowerSetID].nArchetype == -1)
            ));

            return idx > -1 ? Database.Power[idx] : null;
        }

        public static IPower? GetPowerByFullName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            var powersetByName = GetPowersetByName(name);

            return powersetByName?.Powers.FirstOrDefault(power2 =>
                string.Equals(power2.FullName, name, StringComparison.OrdinalIgnoreCase));
        }

        public static string[] GetPowersetNames(int iAT, Enums.ePowerSetType iSet)
        {
            var stringList = new List<string>();
            if (iSet != Enums.ePowerSetType.Pool && iSet != Enums.ePowerSetType.Inherent)
            {
                var numArray = iSet switch
                {
                    Enums.ePowerSetType.Primary => Database.Classes[iAT].Primary,
                    Enums.ePowerSetType.Secondary => Database.Classes[iAT].Secondary,
                    Enums.ePowerSetType.Ancillary => Database.Classes[iAT].Ancillary,
                    _ => Array.Empty<int>()
                };

                stringList.AddRange(numArray.Select(index => Database.Powersets[index].DisplayName));
            }
            else
            {
                stringList.AddRange(from powerset in Database.Powersets
                    where powerset.SetType == iSet
                    select powerset.DisplayName);
            }

            stringList.Sort();
            return stringList.Count > 0 ? stringList.ToArray() : new[] {"No " + Enum.GetName(iSet.GetType(), iSet)};
        }

        private static int[] GetPowersetIndexesByGroup(PowersetGroup group)
        {
            return group.Powersets.Select(powerset => powerset.Value.nID).ToArray();
        }

        public static int[] GetPowersetIndexesByGroupName(string name)
        {
            return !string.IsNullOrEmpty(name) && Database.PowersetGroups.ContainsKey(name)
                ? GetPowersetIndexesByGroup(Database.PowersetGroups[name])
                : new int[1];
        }

        public static IPowerset[] GetPowersetIndexes(Archetype at, Enums.ePowerSetType iSet)
        {
            return GetPowersetIndexes(at.Idx, iSet);
        }

        public static IPowerset[] GetPowersetIndexes(int iAT, Enums.ePowerSetType iSet)
        {
            var powersetList = new List<IPowerset>();
            if ((iSet != Enums.ePowerSetType.Pool) & (iSet != Enums.ePowerSetType.Inherent))
            {
                foreach (var ps in Database.Powersets)
                    if ((ps.nArchetype == iAT) & (ps.SetType == iSet))
                        powersetList.Add(ps);
                    else if ((iSet == Enums.ePowerSetType.Ancillary) & (ps.SetType == iSet) && ps.ClassOk(iAT))
                        powersetList.Add(ps);
            }
            else
            {
                for (var index = 0; index <= Database.Powersets.Length - 1; ++index)
                    if (Database.Powersets[index].SetType == iSet)
                        powersetList.Add(Database.Powersets[index]);
            }

            powersetList.Sort();
            return powersetList.ToArray();
        }

        public static int ToDisplayIndex(IPowerset iPowerset, IPowerset[] iIndexes)
        {
            for (var index = 0; index <= iIndexes.Length - 1; ++index)
                if (iIndexes[index].nID == iPowerset.nID)
                    return index;
            return iIndexes.Length > 0 ? 0 : -1;
        }

        // Zed -- imported from Hero Viewer
        public static bool EnhIsATO(int enhIdx)
        {
            if (enhIdx == -1) return false;

            var enhData = Database.Enhancements[enhIdx];
            if (enhData.nIDSet == -1) return false;

            var enhSetData = Database.EnhancementSets[enhData.nIDSet];

            return enhSetData.SetType == Enums.eSetType.Arachnos ||
                   enhSetData.SetType == Enums.eSetType.Blaster ||
                   enhSetData.SetType == Enums.eSetType.Brute ||
                   enhSetData.SetType == Enums.eSetType.Controller ||
                   enhSetData.SetType == Enums.eSetType.Corruptor ||
                   enhSetData.SetType == Enums.eSetType.Defender ||
                   enhSetData.SetType == Enums.eSetType.Dominator ||
                   enhSetData.SetType == Enums.eSetType.Kheldian ||
                   enhSetData.SetType == Enums.eSetType.Mastermind ||
                   enhSetData.SetType == Enums.eSetType.Scrapper ||
                   enhSetData.SetType == Enums.eSetType.Sentinel ||
                   enhSetData.SetType == Enums.eSetType.Stalker ||
                   enhSetData.SetType == Enums.eSetType.Tanker;
        }

        public static bool EnhIsWinterEventE(int enhIdx)
        {
            if (enhIdx == -1) return false;

            var enhData = Database.Enhancements[enhIdx];
            if (enhData.nIDSet == -1) return false;

            var enhSetData = Database.EnhancementSets[enhData.nIDSet];

            return enhSetData.DisplayName.IndexOf("Avalanche", StringComparison.OrdinalIgnoreCase) > -1 ||
                   enhSetData.DisplayName.IndexOf("Blistering Cold", StringComparison.OrdinalIgnoreCase) > -1 ||
                   enhSetData.DisplayName.IndexOf("Entomb", StringComparison.OrdinalIgnoreCase) > -1 ||
                   enhSetData.DisplayName.IndexOf("Frozen Blast", StringComparison.OrdinalIgnoreCase) > -1 ||
                   enhSetData.DisplayName.IndexOf("Winter's Bite", StringComparison.OrdinalIgnoreCase) > -1;
        }

        public static bool EnhIsMovieE(int enhIdx)
        {
            if (enhIdx == -1) return false;

            var enhData = Database.Enhancements[enhIdx];
            if (enhData.nIDSet == -1) return false;

            var enhSetData = Database.EnhancementSets[enhData.nIDSet];

            return enhSetData.DisplayName.IndexOf("Overwhelming Force", StringComparison.OrdinalIgnoreCase) > -1;
        }

        public static bool EnhIsIO(int enhIdx)
        {
            if (enhIdx == -1) return false;

            var enhData = Database.Enhancements[enhIdx];

            return (enhData.TypeID == Enums.eType.InventO || enhData.TypeID == Enums.eType.SetO) &&
                   !EnhIsNaturallyAttuned(enhIdx);
        }

        // Enh for which a catalyst can be used on OR has been used on already
        // All ATOs, Winter Event sets, all IOs but regular (white grade) ones
        public static bool EnhCanReceiveCatalyst(int enhIdx)
        {
            if (enhIdx == -1) return false;

            var enhData = Database.Enhancements[enhIdx];

            return EnhIsATO(enhIdx) || EnhIsWinterEventE(enhIdx) || enhData.TypeID == Enums.eType.SetO;
        }

        // Purple grade IOs + Superior ATOs + Superior Winter Event enhancements
        public static bool EnhIsSuperior(int enhIdx)
        {
            if (enhIdx == -1) return false;

            var enhData = Database.Enhancements[enhIdx];
            if (enhData.RecipeIDX == -1) return false;

            var enhRecipe = Database.Recipes[enhData.RecipeIDX];

            return enhRecipe.Rarity == Recipe.RecipeRarity.UltraRare;
        }

        // Purple grade IOs only
        public static bool EnhIsSuperiorIO(int enhIdx)
        {
            if (enhIdx == -1) return false;

            var enhData = Database.Enhancements[enhIdx];
            if (enhData.RecipeIDX == -1) return false;

            var enhRecipe = Database.Recipes[enhData.RecipeIDX];

            return enhRecipe.Rarity == Recipe.RecipeRarity.UltraRare && !EnhIsATO(enhIdx) && !EnhIsWinterEventE(enhIdx);
        }

        public static bool EnhIsNaturallyAttuned(int enhIdx)
        {
            if (enhIdx == -1) return false;

            return EnhIsATO(enhIdx) || EnhIsWinterEventE(enhIdx) || EnhIsMovieE(enhIdx);
        }

        // Enh + catalyst = Superior Enh
        // Basic ATOs, Basic Winter Event sets or purple grade sets
        // For Reference: Attuned_Overwhelming_Force_A
        public static bool CanCatalystUpgradeSuperior(int enhIdx)
        {
            if (enhIdx == -1) return false;

            var enhData = Database.Enhancements[enhIdx];
            Recipe.RecipeRarity enhRarity;
            if (enhData.RecipeIDX == -1) enhRarity = Recipe.RecipeRarity.Common;

            var enhRecipe = Database.Recipes[enhData.RecipeIDX];
            enhRarity = enhRecipe.Rarity;

            return EnhIsATO(enhIdx) || EnhIsWinterEventE(enhIdx) || enhRarity == Recipe.RecipeRarity.Rare &&
                enhData.LongName.IndexOf("Superior", StringComparison.OrdinalIgnoreCase) == -1;
        }

        private static string[] GetPurpleSetsEnhUIDList()
        {
            return Database.Enhancements.Where(e =>
                e.nIDSet > -1 &&
                e.RecipeIDX > -1 &&
                Database.Recipes[e.RecipeIDX].Rarity == Recipe.RecipeRarity.UltraRare &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Arachnos &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Blaster &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Brute &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Controller &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Corruptor &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Defender &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Dominator &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Kheldian &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Mastermind &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Scrapper &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Sentinel &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Stalker &&
                Database.EnhancementSets[e.nIDSet].SetType != Enums.eSetType.Tanker
            ).Select(e => e.UID).ToArray();
        }

        private static string[] GetATOSetsEnhUIDList()
        {
            return Database.Enhancements.Where(e =>
                e.nIDSet > -1 &&
                (
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Arachnos ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Blaster ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Brute ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Controller ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Corruptor ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Defender ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Dominator ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Kheldian ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Mastermind ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Scrapper ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Sentinel ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Stalker ||
                    Database.EnhancementSets[e.nIDSet].SetType == Enums.eSetType.Tanker
                )
            ).Select(e => e.UID).ToArray();
        }

        private static string[] GetWinterEventEnhUIDList()
        {
            return Database.Enhancements.Where(e =>
                e.nIDSet > -1 &&
                (
                    e.UID.IndexOf("Avalanche", StringComparison.OrdinalIgnoreCase) > -1 ||
                    e.UID.IndexOf("Blistering_Cold", StringComparison.OrdinalIgnoreCase) > -1 ||
                    e.UID.IndexOf("Entomb", StringComparison.OrdinalIgnoreCase) > -1 ||
                    e.UID.IndexOf("Frozen_Blast", StringComparison.OrdinalIgnoreCase) > -1 ||
                    e.UID.IndexOf("Winters_Bite", StringComparison.OrdinalIgnoreCase) > -1
                )
            ).Select(e => e.UID).ToArray();
        }

        private static string[] GetMovieEnhUIDList()
        {
            return Database.Enhancements.Where(e =>
                e.nIDSet > -1 && e.UID.IndexOf("Overwhelming_Force", StringComparison.OrdinalIgnoreCase) > -1
            ).Select(e => e.UID).ToArray();
        }

        public static string GetEnhancementBaseUIDName(string iName)
        {
            var purpleSetsEnh = GetPurpleSetsEnhUIDList();
            var ATOSetsEnh = GetATOSetsEnhUIDList();
            var WinterEventEnh = GetWinterEventEnhUIDList();
            var MovieEnh = GetMovieEnhUIDList();

            // Purple IOs
            if (purpleSetsEnh.Any(e => e.Contains(iName.Replace("Superior_Attuned_", string.Empty))))
                return iName.Replace("Superior_Attuned_", "Crafted_");

            if (ATOSetsEnh.Any(e => e.Contains(iName)) || WinterEventEnh.Any(e => e.Contains(iName)) ||
                MovieEnh.Any(e => e.Contains(iName))) return iName;

            // IOs + SpecialOs
            return iName
                .Replace("Synthetic_", string.Empty)
                .Replace("Attuned_", "Crafted_")
                .Replace("Science_", "Magic_")
                .Replace("Mutation_", "Magic_")
                .Replace("Natural__", "Magic_")
                .Replace("Mutation_", "Magic_");
        }

        public static bool EnhHasCatalyst(string iName)
        {
            var purpleSetsEnh = GetPurpleSetsEnhUIDList();
            var ATOSetsEnh = GetATOSetsEnhUIDList();
            var WinterEventEnh = GetWinterEventEnhUIDList();
            var MovieEnh = GetMovieEnhUIDList();

            // Purple IOs
            if (purpleSetsEnh.Any(e => e.Contains(iName.Replace("Superior_Attuned_", string.Empty))))
                return iName.IndexOf("Superior_Attuned_", StringComparison.OrdinalIgnoreCase) > -1;

            if (!ATOSetsEnh.Any(e => e.Contains(iName)) && !WinterEventEnh.Any(e => e.Contains(iName)) &&
                !MovieEnh.Any(e => e.Contains(iName)))
                return iName.IndexOf("Superior_", StringComparison.OrdinalIgnoreCase) > -1;

            return iName.IndexOf("Attuned_", StringComparison.OrdinalIgnoreCase) > -1;
        }

        public static int GetEnhancementByUIDName(string iName)
        {
            return Database.Enhancements.TryFindIndex(enh => enh.UID.Contains(iName));
        }

        public static int GetEnhancementByName(string iName)
        {
            return Database.Enhancements.TryFindIndex(enh =>
                string.Equals(enh.ShortName, iName, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(enh.Name, iName, StringComparison.OrdinalIgnoreCase));
        }

        public static int GetEnhancementByName(string iName, Enums.eType iType)
        {
            return Database.Enhancements.TryFindIndex(enh =>
                enh.TypeID == iType && string.Equals(enh.ShortName, iName, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(enh.Name, iName, StringComparison.OrdinalIgnoreCase));
        }

        public static int GetEnhancementByName(string iName, string iSet)
        {
            foreach (var enhancementSet in Database.EnhancementSets)
            {
                if (!string.Equals(enhancementSet.ShortName, iSet, StringComparison.OrdinalIgnoreCase))
                    continue;
                foreach (var enhancement in enhancementSet.Enhancements)
                    if (string.Equals(Database.Enhancements[enhancementSet.Enhancements[enhancement]].ShortName, iName,
                        StringComparison.OrdinalIgnoreCase))
                        return enhancementSet.Enhancements[enhancement];
            }

            return -1;
        }

        public static int FindEnhancement(string setName, string enhName, int iType, int fallBack)
        {
            for (var index = 0; index < Database.Enhancements.Length; ++index)
            {
                if (Database.Enhancements[index].TypeID != (Enums.eType) iType || !string.Equals(
                    Database.Enhancements[index].ShortName, enhName,
                    StringComparison.OrdinalIgnoreCase))
                    continue;
                int num;
                if (Database.Enhancements[index].TypeID != Enums.eType.SetO)
                    num = index;
                else if (Database.EnhancementSets[Database.Enhancements[index].nIDSet].DisplayName
                    .Equals(setName, StringComparison.OrdinalIgnoreCase))
                    num = index;
                else
                    continue;
                return num;
            }

            if ((fallBack > -1) & (fallBack < Database.Enhancements.Length))
                return fallBack;
            return -1;
        }

        //Pine
        private static int GetRecipeIdxByName(string iName)
        {
            for (var index = 0; index <= Database.Recipes.Length - 1; ++index)
                if (string.Equals(Database.Recipes[index].InternalName, iName, StringComparison.OrdinalIgnoreCase))
                    return index;
            return -1;
        }

        public static Recipe GetRecipeByName(string iName)
        {
            return Database.Recipes.FirstOrDefault(recipe =>
                string.Equals(recipe.InternalName, iName, StringComparison.OrdinalIgnoreCase));
        }

        public static string[] UidReferencingPowerFix(string uidPower, string uidNew = "")
        {
            var array = new string[0];
            for (var index1 = 0; index1 <= Database.Power.Length - 1; ++index1)
            {
                if (Database.Power[index1].Requires.ReferencesPower(uidPower, uidNew))
                {
                    Array.Resize(ref array, array.Length + 1);
                    array[array.Length - 1] = Database.Power[index1].FullName + " (Requirement)";
                }

                for (var index2 = 0; index2 <= Database.Power[index1].Effects.Length - 1; ++index2)
                {
                    if (Database.Power[index1].Effects[index2].Summon != uidPower)
                        continue;
                    Database.Power[index1].Effects[index2].Summon = uidNew;
                    Array.Resize(ref array, array.Length + 1);
                    array[array.Length - 1] = Database.Power[index1].FullName + " (GrantPower)";
                }
            }

            return array;
        }

        public static int NidFromStaticIndexEnh(int sidEnh)
        {
            int num;
            if (sidEnh < 0)
            {
                num = -1;
            }
            else
            {
                for (var index = 0; index < Database.Enhancements.Length; ++index)
                    if (Database.Enhancements[index].StaticIndex == sidEnh)
                        return index;
                num = -1;
            }

            return num;
        }

        public static int NidFromUidioSet(string uidSet)
        {
            for (var index = 0; index < Database.EnhancementSets.Count; ++index)
                if (string.Equals(Database.EnhancementSets[index].Uid, uidSet, StringComparison.OrdinalIgnoreCase))
                    return index;
            return -1;
        }

        public static int NidFromUidRecipe(string uidRecipe, ref int subIndex)
        {
            var isSub = (subIndex > -1) & uidRecipe.Contains("_");
            subIndex = -1;
            var uid = isSub ? uidRecipe.Substring(0, uidRecipe.LastIndexOf("_", StringComparison.Ordinal)) : uidRecipe;
            for (var recipeIdx = 0; recipeIdx < Database.Recipes.Length; ++recipeIdx)
            {
                if (!string.Equals(Database.Recipes[recipeIdx].InternalName, uid, StringComparison.OrdinalIgnoreCase))
                    continue;
                if (!isSub)
                    return recipeIdx;
                var startIndex = uidRecipe.LastIndexOf("_", StringComparison.Ordinal) + 1;
                if (startIndex < 0 || startIndex > uidRecipe.Length - 1)
                    return -1;
                uid = uidRecipe.Substring(startIndex);
                for (var index2 = 0; index2 < Database.Recipes[recipeIdx].Item.Length; ++index2)
                {
                    if (Database.Recipes[recipeIdx].Item[index2].Level != startIndex)
                        continue;
                    subIndex = index2;
                    return recipeIdx;
                }
            }

            return -1;
        }

        public static int NidFromUidEnh(string uidEnh)
        {
            for (var index = 0; index < Database.Enhancements.Length; ++index)
                if (string.Equals(Database.Enhancements[index].UID, uidEnh, StringComparison.OrdinalIgnoreCase))
                    return index;
            return -1;
        }

        public static int NidFromUidEnhExtended(string uidEnh)
        {
            if (!uidEnh.StartsWith("BOOSTS", true, CultureInfo.CurrentCulture))
                return NidFromUidEnh(uidEnh);
            for (var index = 0; index < Database.Enhancements.Length; ++index)
                if (string.Equals("BOOSTS." + Database.Enhancements[index].UID + "." + Database.Enhancements[index].UID,
                    uidEnh, StringComparison.OrdinalIgnoreCase))
                    return index;
            return -1;
        }

        #region ImportCode (Unused ATM)

        private static readonly List<string> DataFile = new List<string>
        {
            $"{Application.StartupPath}\\Data\\Database\\Archetypes.mhd",
            $"{Application.StartupPath}\\Data\\Database\\AttribMods.mhd",
            $"{Application.StartupPath}\\Data\\Database\\Enhancments.mhd",
            $"{Application.StartupPath}\\Data\\Database\\EnhancmentClasses.mhd",
            $"{Application.StartupPath}\\Data\\Database\\EnhancmentSets.mhd",
            $"{Application.StartupPath}\\Data\\Database\\Entities.mhd",
            $"{Application.StartupPath}\\Data\\Database\\Levels.mhd",
            $"{Application.StartupPath}\\Data\\Database\\Maths.mhd",
            $"{Application.StartupPath}\\Data\\Database\\Metadata.mhd",
            $"{Application.StartupPath}\\Data\\Database\\Origins.mhd",
            $"{Application.StartupPath}\\Data\\Database\\Powersets.mhd",
            $"{Application.StartupPath}\\Data\\Database\\PowersetGroups.mhd",
            $"{Application.StartupPath}\\Data\\Database\\Powers.mhd",
            $"{Application.StartupPath}\\Data\\Database\\Recipes.mhd",
            $"{Application.StartupPath}\\Data\\Database\\Salvage.mhd",
            $"{Application.StartupPath}\\Data\\Database\\SetTypes.mhd",
        };

        private static void SaveNewDatabase(object obj, int dbFile)
        {
            var dInfo = new FileInfo(DataFile[dbFile]).Directory?.FullName;
            if (dInfo != null)
            {
                Directory.CreateDirectory(dInfo);
            }
            using var fs = new FileStream(DataFile[dbFile], FileMode.OpenOrCreate);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            var json = JsonConvert.SerializeObject(obj, settings);
            var input = Encoding.Unicode.GetBytes(json);
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(mStream, CompressionMode.Compress) { CompressionLevel = 11 };
            compressionStream.Write(input, 0, input.Length);
            compressionStream.Close();
            var compressed = mStream.ToArray();
            fs.Write(compressed, 0, compressed.Length);
            fs.Close();
        }
        public static void MergeDatabaseFile()
        {
            LoadClasses(0);
            LoadEnhancements(2);
            LoadEnhancementClasses(3);
            LoadEnhancementSets(4);
            LoadEntities(5);
            LoadPowersets(10);
            LoadPowersetGroups(11);
            LoadPowers(12);
            LoadRecipeData(13);
            LoadSalvageData(14);
        }

        public static void LoadClasses(int dbFile)
        {
            using var fs = new FileStream(DataFile[dbFile], FileMode.Open);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(fs, CompressionMode.Decompress);
            compressionStream.CopyTo(mStream);
            var output = Encoding.Unicode.GetString(mStream.ToArray());
            Database.Classes = JsonConvert.DeserializeObject<Archetype[]>(output, settings);
        }

        public static void LoadEnhancements(int dbFile)
        {
            using var fs = new FileStream(DataFile[dbFile], FileMode.Open);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = {
                    new AbstractConverter<Enhancement, IEnhancement>()
                }
            };
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(fs, CompressionMode.Decompress);
            compressionStream.CopyTo(mStream);
            var output = Encoding.Unicode.GetString(mStream.ToArray());
            Database.Enhancements = JsonConvert.DeserializeObject<IEnhancement[]>(output, settings);
        }

        public static void LoadEnhancementClasses(int dbFile)
        {
            using var fs = new FileStream(DataFile[dbFile], FileMode.Open);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = {
                    new AbstractConverter<Enhancement, IEnhancement>()
                }
            };
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(fs, CompressionMode.Decompress);
            compressionStream.CopyTo(mStream);
            var output = Encoding.Unicode.GetString(mStream.ToArray());
            Database.EnhancementClasses = JsonConvert.DeserializeObject<Enums.sEnhClass[]>(output, settings);
        }

        public static void LoadEnhancementSets(int dbFile)
        {
            using var fs = new FileStream(DataFile[dbFile], FileMode.Open);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(fs, CompressionMode.Decompress);
            compressionStream.CopyTo(mStream);
            var output = Encoding.Unicode.GetString(mStream.ToArray());
            Database.EnhancementSets = JsonConvert.DeserializeObject<EnhancementSetCollection>(output);
        }

        public static void LoadEntities(int dbFile)
        {
            using var fs = new FileStream(DataFile[dbFile], FileMode.Open);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = {
                    new AbstractConverter<Enhancement, IEnhancement>()
                }
            };
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(fs, CompressionMode.Decompress);
            compressionStream.CopyTo(mStream);
            var output = Encoding.Unicode.GetString(mStream.ToArray());
            Database.Entities = JsonConvert.DeserializeObject<SummonedEntity[]>(output, settings);
        }

        public static void LoadPowersets(int dbFile)
        {
            using var fs = new FileStream(DataFile[dbFile], FileMode.Open);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(fs, CompressionMode.Decompress);
            compressionStream.CopyTo(mStream);
            var output = Encoding.Unicode.GetString(mStream.ToArray());
            Database.Powersets = JsonConvert.DeserializeObject<IPowerset[]>(output, settings);
        }

        public static void LoadPowersetGroups(int dbFile)
        {
            using var fs = new FileStream(DataFile[dbFile], FileMode.Open);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = {
                    new AbstractConverter<Powerset, IPowerset>()
                }
            };
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(fs, CompressionMode.Decompress);
            compressionStream.CopyTo(mStream);
            var output = Encoding.Unicode.GetString(mStream.ToArray());
            var powersetGroups = JsonConvert.DeserializeObject<Dictionary<string, PowersetGroup>>(output, settings);
            Database.PowersetGroups = powersetGroups;
        }

        public static void LoadPowers(int dbFile)
        {
            using var fs = new FileStream(DataFile[dbFile], FileMode.Open);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = {
                    new AbstractConverter<Power, IPower>()
                }
            };
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(fs, CompressionMode.Decompress);
            compressionStream.CopyTo(mStream);
            var output = Encoding.Unicode.GetString(mStream.ToArray());
            Database.Power = JsonConvert.DeserializeObject<IPower[]>(output, settings);
        }

        public static void LoadRecipeData(int dbFile)
        {
            using var fs = new FileStream(DataFile[dbFile], FileMode.Open);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(fs, CompressionMode.Decompress);
            compressionStream.CopyTo(mStream);
            var output = Encoding.Unicode.GetString(mStream.ToArray());
            Database.Recipes = JsonConvert.DeserializeObject<Recipe[]>(output, settings);
        }

        public static void LoadSalvageData(int dbFile)
        {
            using var fs = new FileStream(DataFile[dbFile], FileMode.Open);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            using var mStream = new MemoryStream();
            using var compressionStream = new ZstandardStream(fs, CompressionMode.Decompress);
            compressionStream.CopyTo(mStream);
            var output = Encoding.Unicode.GetString(mStream.ToArray());
            Database.Salvage = JsonConvert.DeserializeObject<Salvage[]>(output, settings);
        }
        #endregion

        public static void SaveMainDatabase(ISerialize serializer)
        {
            //MergeDatabaseFile();
            //Task.Delay(1500);
            var path = Files.SelectDataFileSave(Files.MxdbFileDB);

            FileStream fileStream;
            BinaryWriter writer;
            try
            {
                fileStream = new FileStream(path, FileMode.Create);
                writer = new BinaryWriter(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Main db save failed: " + ex.Message);
                return;
            }

            try
            {
                writer.Write(MainDbName);
                writer.Write(Database.Version);
                writer.Write(-1);
                writer.Write(Database.Date.ToBinary());
                writer.Write(Database.Issue);
                writer.Write("BEGIN:ARCHETYPES");
                Database.ArchetypeVersion.StoreTo(writer);
                Console.WriteLine(Database.ArchetypeVersion);
                writer.Write(Database.Classes.Length - 1);
                for (var index = 0; index <= Database.Classes.Length - 1; ++index)
                    Database.Classes[index].StoreTo(ref writer);
                writer.Write("BEGIN:POWERSETS");
                Database.PowersetVersion.StoreTo(writer);
                writer.Write(Database.Powersets.Length - 1);
                for (var index = 0; index <= Database.Powersets.Length - 1; ++index)
                    Database.Powersets[index].StoreTo(ref writer);
                writer.Write("BEGIN:POWERS");
                Database.PowerVersion.StoreTo(writer);
                Database.PowerLevelVersion.StoreTo(writer);
                Database.PowerEffectVersion.StoreTo(writer);
                Database.IOAssignmentVersion.StoreTo(writer);
                writer.Write(Database.Power.Length - 1);
                for (var index = 0; index <= Database.Power.Length - 1; ++index) Database.Power[index].StoreTo(ref writer);
                writer.Write("BEGIN:SUMMONS");
                Database.StoreEntities(writer);
                writer.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                writer.Close();
                fileStream.Close();
            }
        }

        private static void saveEnts()
        {
            var serialized = JsonConvert.SerializeObject(Database.Entities, Formatting.Indented);
            File.WriteAllText($@"{Application.StartupPath}\\data\\Ents.json", serialized);
        }
        public static bool LoadMainDatabase()
        {
            ClearLookups();
            var path = Files.SelectDataFileLoad(Files.MxdbFileDB);
            FileStream fileStream;
            BinaryReader reader;
            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(fileStream);
            }
            catch
            {
                return false;
            }

            try
            {
                if (reader.ReadString() != "Mids' Hero Designer Database MK II")
                    MessageBox.Show("Expected MHD header, got something else!", "Eeeeee!");
                Database.Version = reader.ReadSingle();
                var year = reader.ReadInt32();
                if (year > 0)
                {
                    var month = reader.ReadInt32();
                    var day = reader.ReadInt32();
                    Database.Date = new DateTime(year, month, day);
                }
                else
                {
                    Database.Date = DateTime.FromBinary(reader.ReadInt64());
                }

                Database.Issue = reader.ReadInt32();
                if (reader.ReadString() != "BEGIN:ARCHETYPES")
                {
                    MessageBox.Show("Expected Archetype Data, got something else!", "Eeeeee!");
                    reader.Close();
                    fileStream.Close();
                    return false;
                }

                Database.ArchetypeVersion.Load(reader);
                Database.Classes = new Archetype[reader.ReadInt32() + 1];
                for (var index = 0; index < Database.Classes.Length; ++index)
                    Database.Classes[index] = new Archetype(reader)
                    {
                        Idx = index
                    };
                if (reader.ReadString() != "BEGIN:POWERSETS")
                {
                    MessageBox.Show("Expected Powerset Data, got something else!", "Eeeeee!");
                    reader.Close();
                    fileStream.Close();
                    return false;
                }

                Database.PowersetVersion.Load(reader);
                var num3 = 0;
                Database.Powersets = new IPowerset[reader.ReadInt32() + 1];
                for (var index = 0; index < Database.Powersets.Length; ++index)
                {
                    Database.Powersets[index] = new Powerset(reader)
                    {
                        nID = index
                    };
                    ++num3;
                    if (num3 <= 10)
                        continue;
                    num3 = 0;
                    Application.DoEvents();
                }

                if (reader.ReadString() != "BEGIN:POWERS")
                {
                    MessageBox.Show("Expected Power Data, got something else!", "Eeeeee!");
                    reader.Close();
                    fileStream.Close();
                    return false;
                }

                Database.PowerVersion.Load(reader);
                Database.PowerLevelVersion.Load(reader);
                Database.PowerEffectVersion.Load(reader);
                Database.IOAssignmentVersion.Load(reader);
                Database.Power = new IPower[reader.ReadInt32() + 1];
                for (var index = 0; index <= Database.Power.Length - 1; ++index)
                {
                    Database.Power[index] = new Power(reader);
                    ++num3;
                    if (num3 <= 50)
                        continue;
                    num3 = 0;
                    Application.DoEvents();
                }

                if (reader.ReadString() != "BEGIN:SUMMONS")
                {
                    MessageBox.Show("Expected Summon Data, got something else!", "Eeeeee!");
                    reader.Close();
                    fileStream.Close();
                    return false;
                }

                Database.LoadEntities(reader);
                reader.Close();
                fileStream.Close();
            }
            catch
            {
                reader.Close();
                fileStream.Close();
                return false;
            }

            return true;
        }

        public static void LoadDatabaseVersion()
        {
            var target = Files.SelectDataFileLoad("I12.mhd");
            Database.Version = GetDatabaseVersion(target);
        }

        private static float GetDatabaseVersion(string fp)
        {
            var fName = Debugger.IsAttached ? Files.SearchUp("Data", fp) : fp;
            var num1 = -1f;
            float num2;
            if (!File.Exists(fName))
            {
                num2 = num1;
            }
            else
            {
                using (var fileStream = new FileStream(fName, FileMode.Open, FileAccess.Read))
                {
                    using (var binaryReader = new BinaryReader(fileStream))
                    {
                        try
                        {
                            if (binaryReader.ReadString() != "Mids' Hero Designer Database MK II")
                                MessageBox.Show("Expected MHD header, got something else!");
                            num1 = binaryReader.ReadSingle();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                            num1 = -1f;
                        }

                        binaryReader.Close();
                    }

                    fileStream.Close();
                }

                num2 = num1;
            }

            return num2;
        }

        public static bool LoadEffectIdsDatabase()
        {
            var path = Files.SelectDataFileLoad(Files.MxdbFileEffectIds);
            Database.EffectIds.Clear();
            FileStream fileStream;
            BinaryReader reader;
            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(fileStream);
            }
            catch
            {
                return false;
            }

            try
            {
                var efIdCount = reader.ReadInt32();
                for (var index = 0; index < efIdCount; index++)
                {
                    Database.EffectIds.Add(reader.ReadString());
                }
                reader.Close();
                fileStream.Close();
            }
            catch
            {
                reader.Close();
                fileStream.Close();
                return false;
            }
            return true;
        }

        public static void SaveEffectIdsDatabase()
        {
            var path = Files.SelectDataFileLoad(Files.MxdbFileEffectIds);
            FileStream fileStream;
            BinaryWriter writer;
            try
            {
                fileStream = new FileStream(path, FileMode.Create);
                writer = new BinaryWriter(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Failed to save the EffectIds DB: " + ex.Message);
                return;
            }

            try
            {
                writer.Write(Database.EffectIds.Count);
                foreach (var effectId in Database.EffectIds)
                {
                    writer.Write(effectId);
                }
                writer.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                writer.Close();
                fileStream.Close();
            }
        }

        public static bool LoadLevelsDatabase()
        {
            var path = Files.SelectDataFileLoad("Levels.mhd");
            Database.Levels = new LevelMap[0];
            StreamReader iStream;
            try
            {
                iStream = new StreamReader(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}", "Error!");
                return false;
            }

            var strArray = FileIO.IOGrab(iStream);
            while (strArray[0] != "Level")
                strArray = FileIO.IOGrab(iStream);
            Database.Levels = new LevelMap[50];
            for (var index = 0; index < 50; ++index)
                Database.Levels[index] = new LevelMap(FileIO.IOGrab(iStream));
            var intList = new List<int> {0};
            for (var index = 0; index <= Database.Levels.Length - 1; ++index)
                if (Database.Levels[index].Powers > 0)
                    intList.Add(index);
            Database.Levels_MainPowers = intList.ToArray();
            iStream.Close();
            return true;
        }

        public static void LoadOrigins()
        {
            var path = Files.SelectDataFileLoad("Origins.mhd");
            Database.Origins = new List<Origin>();
            StreamReader streamReader;
            try
            {
                streamReader = new StreamReader(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}", "Error!");
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(FileIO.IOSeekReturn(streamReader, "Version:")))
                    throw new EndOfStreamException("Unable to load Enhancement Class data, version header not found!");
                if (!FileIO.IOSeek(streamReader, "Origin"))
                    throw new EndOfStreamException("Unable to load Origin data, section header not found!");
                string[] strArray;
                do
                {
                    strArray = FileIO.IOGrab(streamReader);
                    if (strArray[0] != "End")
                        Database.Origins.Add(new Origin(strArray[0], strArray[1], strArray[2]));
                    else
                        break;
                } while (strArray[0] != "End");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                streamReader.Close();
                return;
            }

            streamReader.Close();
        }

        public static int GetOriginIDByName(string iOrigin)
        {
            for (var index = 0; index <= Database.Origins.Count - 1; ++index)
                if (string.Equals(iOrigin, Database.Origins[index].Name, StringComparison.OrdinalIgnoreCase))
                    return index;
            return 0;
        }

        public static int IsSpecialEnh(int enhID)
        {
            for (var index = 0;
                index < Database.EnhancementSets[Database.Enhancements[enhID].nIDSet].Enhancements.Length;
                ++index)
                if (enhID == Database.EnhancementSets[Database.Enhancements[enhID].nIDSet].Enhancements[index] &&
                    Database.EnhancementSets[Database.Enhancements[enhID].nIDSet].SpecialBonus[index].Index.Length > 0)
                    return index;
            return -1;
        }

        public static string GetEnhancementNameShortWSet(int iEnh)
        {
            string str;
            if (iEnh < 0 || iEnh > Database.Enhancements.Length - 1)
                str = string.Empty;
            else
                str = Database.Enhancements[iEnh].TypeID switch
                {
                    Enums.eType.Normal => Database.Enhancements[iEnh].ShortName,
                    Enums.eType.SpecialO => Database.Enhancements[iEnh].ShortName,
                    Enums.eType.InventO => "Invention: " + Database.Enhancements[iEnh].ShortName,
                    Enums.eType.SetO => Database.EnhancementSets[Database.Enhancements[iEnh].nIDSet].DisplayName + ": " +
                                        Database.Enhancements[iEnh].ShortName,
                    _ => string.Empty
                };
            return str;
        }

        public static int GetFirstValidEnhancement(int iClass)
        {
            for (var index1 = 0; index1 <= Database.Enhancements.Length - 1; ++index1)
            for (var index2 = 0; index2 <= Database.Enhancements[index1].ClassID.Length - 1; ++index2)
                if (Database.EnhancementClasses[Database.Enhancements[index1].ClassID[index2]].ID == iClass)
                    return index1;
            return -1;
        }

        public static void GuessRecipes()
        {
            foreach (var enhancement in Database.Enhancements)
            {
                if (enhancement.TypeID != Enums.eType.InventO && enhancement.TypeID != Enums.eType.SetO)
                    continue;
                var recipeIdxByName = GetRecipeIdxByName(enhancement.UID);
                if (recipeIdxByName <= -1)
                    continue;
                enhancement.RecipeIDX = recipeIdxByName;
                enhancement.RecipeName = Database.Recipes[recipeIdxByName].InternalName;
            }
        }

        public static void AssignRecipeSalvageIDs()
        {
            var numArray = new int[7];
            var strArray = new string[7];
            foreach (var recipe in Database.Recipes)
            foreach (var recipeEntry in recipe.Item)
                for (var index1 = 0; index1 <= recipeEntry.Salvage.Length - 1; ++index1)
                    if (recipeEntry.Salvage[index1] == strArray[index1])
                    {
                        recipeEntry.SalvageIdx[index1] = numArray[index1];
                    }
                    else
                    {
                        recipeEntry.SalvageIdx[index1] = -1;
                        var a = recipeEntry.Salvage[index1];
                        for (var index2 = 0; index2 <= Database.Salvage.Length - 1; ++index2)
                        {
                            if (!string.Equals(a, Database.Salvage[index2].InternalName,
                                StringComparison.OrdinalIgnoreCase))
                                continue;
                            recipeEntry.SalvageIdx[index1] = index2;
                            numArray[index1] = index2;
                            strArray[index1] = recipeEntry.Salvage[index1];
                            break;
                        }
                    }
        }

        public static void AssignRecipeIDs()
        {
            foreach (var recipe in Database.Recipes)
            {
                recipe.Enhancement = string.Empty;
                recipe.EnhIdx = -1;
            }

            for (var index1 = 0; index1 <= Database.Enhancements.Length - 1; ++index1)
            {
                if (string.IsNullOrEmpty(Database.Enhancements[index1].RecipeName))
                    continue;
                Database.Enhancements[index1].RecipeIDX = -1;
                var recipeName = Database.Enhancements[index1].RecipeName;
                for (var index2 = 0; index2 <= Database.Recipes.Length - 1; ++index2)
                {
                    if (!recipeName.Equals(Database.Recipes[index2].InternalName, StringComparison.OrdinalIgnoreCase))
                        continue;
                    Database.Recipes[index2].Enhancement = Database.Enhancements[index1].UID;
                    Database.Recipes[index2].EnhIdx = index1;
                    Database.Enhancements[index1].RecipeIDX = index2;
                    break;
                }
            }
        }

        public static void LoadRecipes()
        {
            var path = Files.SelectDataFileLoad("Recipe.mhd");
            Database.Recipes = new Recipe[0];
            FileStream fileStream;
            BinaryReader reader;
            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nRecipe database couldn't be loaded.");
                return;
            }

            if (reader.ReadString() == "Mids' Hero Designer Recipe Database")
            {
                Database.RecipeSource1 = reader.ReadString();
                Database.RecipeSource2 = reader.ReadString();
                Database.RecipeRevisionDate = DateTime.FromBinary(reader.ReadInt64());
                var num = 0;
                Database.Recipes = new Recipe[reader.ReadInt32() + 1];
                for (var index = 0; index < Database.Recipes.Length; ++index)
                {
                    Database.Recipes[index] = new Recipe(reader);
                    ++num;
                    if (num <= 100)
                        continue;
                    num = 0;
                    Application.DoEvents();
                }
            }
            else
            {
                MessageBox.Show("Recipe Database header wasn't found, file may be corrupt!");
                reader.Close();
                fileStream.Close();
            }
        }

        private static void SaveRecipesRaw(ISerialize serializer, string fn, string name)
        {
            var toSerialize = new
            {
                name,
                Database.RecipeSource1,
                Database.RecipeSource2,
                Database.RecipeRevisionDate,
                Database.Recipes
            };
            ConfigData.SaveRawMhd(serializer, toSerialize, fn, null);
        }

        public static void SaveRecipes(ISerialize serializer)
        {
            var path = Files.SelectDataFileSave("Recipe.mhd");
            SaveRecipesRaw(serializer, path, RecipeName);
            FileStream fileStream;
            BinaryWriter writer;
            try
            {
                fileStream = new FileStream(path, FileMode.Create);
                writer = new BinaryWriter(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                return;
            }

            try
            {
                writer.Write(RecipeName);
                writer.Write(Database.RecipeSource1);
                writer.Write(Database.RecipeSource2);
                writer.Write(Database.RecipeRevisionDate.ToBinary());
                writer.Write(Database.Recipes.Length - 1);
                for (var index = 0; index <= Database.Recipes.Length - 1; ++index)
                    Database.Recipes[index].StoreTo(writer);
                writer.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                writer.Close();
                fileStream.Close();
            }
        }

        public static void LoadSalvage()
        {
            var path = Files.SelectDataFileLoad("Salvage.mhd");
            Database.Salvage = new Salvage[0];
            FileStream fileStream;
            BinaryReader reader;
            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nSalvage database couldn't be loaded.");
                return;
            }

            try
            {
                if (reader.ReadString() != "Mids' Hero Designer Salvage Database")
                {
                    MessageBox.Show("Salvage Database header wasn't found, file may be corrupt!");
                    reader.Close();
                    fileStream.Close();
                }
                else
                {
                    Database.Salvage = new Salvage[reader.ReadInt32() + 1];
                    for (var index = 0; index < Database.Salvage.Length; ++index)
                        Database.Salvage[index] = new Salvage(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Salvage Database file isn't how it should be (" + ex.Message + ")\nNo salvage loaded.");
                Database.Salvage = new Salvage[0];
                reader.Close();
                fileStream.Close();
            }
        }

        private static void SaveSalvageRaw(ISerialize serializer, string fn, string name)
        {
            var toSerialize = new
            {
                name,
                Database.Salvage
            };
            ConfigData.SaveRawMhd(serializer, toSerialize, fn, null);
        }

        public static void SaveSalvage(ISerialize serializer)
        {
            var path = Files.SelectDataFileSave("Salvage.mhd");
            SaveSalvageRaw(serializer, path, SalvageName);
            FileStream fileStream;
            BinaryWriter writer;
            try
            {
                fileStream = new FileStream(path, FileMode.Create);
                writer = new BinaryWriter(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                return;
            }

            try
            {
                writer.Write(SalvageName);
                writer.Write(Database.Salvage.Length - 1);
                for (var index = 0; index <= Database.Salvage.Length - 1; ++index)
                    Database.Salvage[index].StoreTo(writer);
                writer.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                writer.Close();
                fileStream.Close();
            }
        }

        private static void SaveEnhancementDbRaw(ISerialize serializer, string filename, string name)
        {
            var toSerialize = new
            {
                name,
                Database.VersionEnhDb,
                Database.Enhancements,
                Database.EnhancementSets
            };
            ConfigData.SaveRawMhd(serializer, toSerialize, filename, null);
        }

        public static void SaveEnhancementDb(ISerialize serializer)
        {
            var path = Files.SelectDataFileSave(Files.MxdbFileEnhDB);
            SaveEnhancementDbRaw(serializer, path, EnhancementDbName);
            using var fileStream = new FileStream(path, FileMode.Create);
            using var writer = new BinaryWriter(fileStream, Encoding.UTF8);
            try
            {
                writer.Write(EnhancementDbName);
                writer.Write(Database.VersionEnhDb);
                writer.Write(Database.Enhancements.Length - 1);

                for (var index = 0; index <= Database.Enhancements.Length - 1; ++index)
                    Database.Enhancements[index].StoreTo(writer);

                writer.Write(Database.EnhancementSets.Count - 1);
                for (var index = 0; index <= Database.EnhancementSets.Count - 1; ++index)
                    Database.EnhancementSets[index].StoreTo(writer);

                writer.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\n\nTrace: {ex.StackTrace}");
                writer.Close();
                fileStream.Close();
            }
        }

        /*public static void SaveEnhancementDb(ISerialize serializer)
    {
        string path = Files.SelectDataFileSave(Files.MxdbFileEnhDB);
        //SaveEnhancementDbRaw(serializer, path, EnhancementDbName);
        FileStream fileStream;
        BinaryWriter writer;
        try
        {
            fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            writer = new BinaryWriter(fileStream);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
            return;
        }
        try
        {
            writer.Write(EnhancementDbName);
            writer.Write(Database.VersionEnhDb);
            writer.Write(Database.Enhancements.Length - 1);
            for (int index = 0; index <= Database.Enhancements.Length - 1; ++index)
                Database.Enhancements[index].StoreTo(writer);
            writer.Write(Database.EnhancementSets.Count - 1);
            for (int index = 0; index <= Database.EnhancementSets.Count - 1; ++index)
                Database.EnhancementSets[index].StoreTo(writer);
            writer.Close();
            fileStream.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
            writer.Close();
            fileStream.Close();
        }
    }*/

        public static void LoadEnhancementDb()
        {
            var path = Files.SelectDataFileLoad("EnhDB.mhd");
            Database.Enhancements = new IEnhancement[0];
            FileStream fileStream;
            BinaryReader reader;
            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nNo Enhancements have been loaded.", "EnhDB Load Failed");
                return;
            }

            try
            {
                if (reader.ReadString() != "Mids' Hero Designer Enhancement Database")
                {
                    MessageBox.Show("Enhancement Database header wasn't found, file may be corrupt!", "Meep!");
                    reader.Close();
                    fileStream.Close();
                }
                else
                {
                    reader.ReadSingle();
                    var versionEnhDb = Database.VersionEnhDb;
                    var num1 = 0;
                    Database.Enhancements = new IEnhancement[reader.ReadInt32() + 1];
                    for (var index = 0; index < Database.Enhancements.Length; ++index)
                    {
                        Database.Enhancements[index] = new Enhancement(reader);
                        ++num1;
                        if (num1 <= 5)
                            continue;
                        num1 = 0;
                        Application.DoEvents();
                    }

                    Database.EnhancementSets = new EnhancementSetCollection();
                    var num2 = reader.ReadInt32() + 1;
                    for (var index = 0; index < num2; ++index)
                    {
                        Database.EnhancementSets.Add(new EnhancementSet(reader));
                        ++num1;
                        if (num1 <= 5)
                            continue;
                        num1 = 0;
                        Application.DoEvents();
                    }

                    reader.Close();
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Enhancement Database file isn't how it should be (" + ex.Message +
                    ")\nNo Enhancements have been loaded.", "Huh...");
                Database.Enhancements = new IEnhancement[0];
                reader.Close();
                fileStream.Close();
            }
        }

        public static bool LoadEnhancementClasses()
        {
            using (var streamReader = new StreamReader(Files.SelectDataFileLoad(Files.MxdbFileEClasses)))
            {
                Database.EnhancementClasses = new Enums.sEnhClass[0];
                try
                {
                    if (string.IsNullOrEmpty(FileIO.IOSeekReturn(streamReader, "Version:")))
                        throw new EndOfStreamException("Unable to load Enhancement Class data, version header not found!");
                    if (!FileIO.IOSeek(streamReader, "Index"))
                        throw new EndOfStreamException("Unable to load Enhancement Class data, section header not found!");
                    var enhancementClasses = Database.EnhancementClasses;
                    string[] strArray;
                    do
                    {
                        strArray = FileIO.IOGrab(streamReader);
                        if (strArray[0] != "End")
                        {
                            Array.Resize(ref enhancementClasses, enhancementClasses.Length + 1);
                            enhancementClasses[enhancementClasses.Length - 1].ID = int.Parse(strArray[0]);
                            enhancementClasses[enhancementClasses.Length - 1].Name = strArray[1];
                            enhancementClasses[enhancementClasses.Length - 1].ShortName = strArray[2];
                            enhancementClasses[enhancementClasses.Length - 1].ClassID = strArray[3];
                            enhancementClasses[enhancementClasses.Length - 1].Desc = strArray[4];
                        }
                        else
                        {
                            break;
                        }
                    } while (strArray[0] != "End");

                    Database.EnhancementClasses = enhancementClasses;
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                    streamReader.Close();
                    return false;
                }

                streamReader.Close();
            }

            return true;
        }

        public static void LoadSetTypeStrings()
        {
            var path = Files.SelectDataFileLoad(Files.MxdbFileSetTypes);
            Database.SetTypeStringLong = new string[0];
            Database.SetTypeStringShort = new string[0];
            StreamReader streamReader;
            try
            {
                streamReader = new StreamReader(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(FileIO.IOSeekReturn(streamReader, "Version:")))
                    throw new EndOfStreamException("Unable to load SetType data, version header not found!");
                if (!FileIO.IOSeek(streamReader, "SetID"))
                    throw new EndOfStreamException("Unable to load SetType data, section header not found!");
                var setTypeStringLong = Database.SetTypeStringLong;
                var setTypeStringShort = Database.SetTypeStringShort;
                string[] strArray;
                do
                {
                    strArray = FileIO.IOGrab(streamReader);
                    if (strArray[0] != "End")
                    {
                        Array.Resize(ref setTypeStringLong, setTypeStringLong.Length + 1);
                        Array.Resize(ref setTypeStringShort, setTypeStringShort.Length + 1);
                        setTypeStringShort[setTypeStringShort.Length - 1] = strArray[1];
                        setTypeStringLong[setTypeStringLong.Length - 1] = strArray[2];
                    }
                    else
                    {
                        break;
                    }
                } while (strArray[0] != "End");

                Database.SetTypeStringLong = setTypeStringLong;
                Database.SetTypeStringShort = setTypeStringShort;
                Database.EnhGradeStringLong = new string[4];
                Database.EnhGradeStringShort = new string[4];
                Database.EnhGradeStringLong[0] = "None";
                Database.EnhGradeStringLong[1] = "Training Origin";
                Database.EnhGradeStringLong[2] = "Dual Origin";
                Database.EnhGradeStringLong[3] = "Single Origin";
                Database.EnhGradeStringShort[0] = "None";
                Database.EnhGradeStringShort[1] = "TO";
                Database.EnhGradeStringShort[2] = "DO";
                Database.EnhGradeStringShort[3] = "SO";
                Database.SpecialEnhStringLong = new string[5];
                Database.SpecialEnhStringShort = new string[5];
                Database.SpecialEnhStringLong[0] = "None";
                Database.SpecialEnhStringLong[1] = "Hamidon Origin";
                Database.SpecialEnhStringLong[2] = "Hydra Origin";
                Database.SpecialEnhStringLong[3] = "Titan Origin";
                Database.SpecialEnhStringLong[4] = "Yin's Talisman";
                Database.SpecialEnhStringShort[0] = "None";
                Database.SpecialEnhStringShort[1] = "HO";
                Database.SpecialEnhStringShort[2] = "TnO";
                Database.SpecialEnhStringShort[3] = "HyO";
                Database.SpecialEnhStringShort[4] = "YinO";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                streamReader.Close();
                return;
            }

            streamReader.Close();
        }

        public static bool LoadMaths()
        {
            var path = Files.SelectDataFileLoad(Files.MxdbFileMaths);
            StreamReader streamReader;
            try
            {
                streamReader = new StreamReader(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                return false;
            }

            try
            {
                if (string.IsNullOrEmpty(FileIO.IOSeekReturn(streamReader, "Version:")))
                    throw new EndOfStreamException("Unable to load Enhancement Maths data, version header not found!");
                if (!FileIO.IOSeek(streamReader, "EDRT"))
                    throw new EndOfStreamException("Unable to load Maths data, section header not found!");
                Database.MultED = new float[4][];
                for (var index = 0; index < 4; ++index)
                    Database.MultED[index] = new float[3];
                for (var index1 = 0; index1 <= 2; ++index1)
                {
                    var strArray = FileIO.IOGrab(streamReader);
                    for (var index2 = 0; index2 < 4; ++index2)
                        Database.MultED[index2][index1] = float.Parse(strArray[index2 + 1]);
                }

                if (!FileIO.IOSeek(streamReader, "EGE"))
                    throw new EndOfStreamException("Unable to load Maths data, section header not found!");
                Database.MultTO = new float[1][];
                Database.MultDO = new float[1][];
                Database.MultSO = new float[1][];
                Database.MultHO = new float[1][];
                var strArray1 = FileIO.IOGrab(streamReader);
                Database.MultTO[0] = new float[4];
                for (var index = 0; index < 4; ++index)
                    Database.MultTO[0][index] = float.Parse(strArray1[index + 1]);
                var strArray2 = FileIO.IOGrab(streamReader);
                Database.MultDO[0] = new float[4];
                for (var index = 0; index < 4; ++index)
                    Database.MultDO[0][index] = float.Parse(strArray2[index + 1]);
                var strArray3 = FileIO.IOGrab(streamReader);
                Database.MultSO[0] = new float[4];
                for (var index = 0; index < 4; ++index)
                    Database.MultSO[0][index] = float.Parse(strArray3[index + 1]);
                var strArray4 = FileIO.IOGrab(streamReader);
                Database.MultHO[0] = new float[4];
                for (var index = 0; index < 4; ++index)
                    Database.MultHO[0][index] = float.Parse(strArray4[index + 1]);
                if (!FileIO.IOSeek(streamReader, "LBIOE"))
                    throw new EndOfStreamException("Unable to load Maths data, section header not found!");
                Database.MultIO = new float[53][];
                for (var index1 = 0; index1 < 53; ++index1)
                {
                    var strArray5 = FileIO.IOGrab(streamReader);
                    Database.MultIO[index1] = new float[4];
                    for (var index2 = 0; index2 < 4; ++index2)
                        Database.MultIO[index1][index2] = float.Parse(strArray5[index2 + 1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}\r\nTrace: {ex.StackTrace}");
                streamReader.Close();
                return false;
            }

            streamReader.Close();
            return true;
        }

        public static void AssignSetBonusIndexes()
        {
            foreach (var enhancementSet in Database.EnhancementSets)
            {
                foreach (var bonu in enhancementSet.Bonus)
                    for (var index = 0; index < bonu.Index.Length; ++index)
                        bonu.Index[index] = NidFromUidPower(bonu.Name[index]);
                foreach (var specialBonu in enhancementSet.SpecialBonus)
                    for (var index = 0; index <= specialBonu.Index.Length - 1; ++index)
                        specialBonu.Index[index] = NidFromUidPower(specialBonu.Name[index]);
            }
        }

        public static float GetModifier(IEffect iEffect)
        {
            var iClass = 0;
            var iLevel = MidsContext.MathLevelBase;
            var effPower = iEffect.GetPower();
            if (effPower == null)
            {
                if (iEffect.Enhancement == null)
                    return 1f;
            }
            else
            {
                iClass = string.IsNullOrEmpty(effPower.ForcedClass)
                    ? iEffect.Absorbed_Class_nID <= -1 ? MidsContext.Archetype.Idx : iEffect.Absorbed_Class_nID
                    : NidFromUidClass(effPower.ForcedClass);
            }

            if (MidsContext.MathLevelExemp > -1 && MidsContext.MathLevelExemp < MidsContext.MathLevelBase)
                iLevel = MidsContext.MathLevelExemp;
            return GetModifier(iClass, iEffect.nModifierTable, iLevel);
        }

        private static float GetModifier(int iClass, int iTable, int iLevel)

        {
            float num;
            if (iClass < 0)
            {
                num = 0.0f;
            }
            else if (iTable < 0)
            {
                num = 0.0f;
            }
            else if (iLevel < 0)
            {
                num = 0.0f;
            }
            else if (iClass > Database.Classes.Length - 1)
            {
                num = 0.0f;
            }
            else
            {
                iClass = Database.Classes[iClass].Column;
                num = iClass >= 0
                    ? iTable <= Database.AttribMods.Modifier.Length - 1
                        ? iLevel <= Database.AttribMods.Modifier[iTable].Table.Length - 1
                            ? iClass <= Database.AttribMods.Modifier[iTable].Table[iLevel].Length - 1
                                ? Database.AttribMods.Modifier[iTable].Table[iLevel][iClass]
                                : 0.0f
                            : 0.0f
                        : 0.0f
                    : 0.0f;
            }

            return num;
        }

        public static void MatchAllIDs(IMessager iFrm = null)
        {
            UpdateMessage(iFrm, "Matching Group IDs...");
            FillGroupArray();
            UpdateMessage(iFrm, "Matching Class IDs...");
            MatchArchetypeIDs();
            UpdateMessage(iFrm, "Matching Powerset IDs...");
            MatchPowersetIDs();
            UpdateMessage(iFrm, "Matching Power IDs...");
            MatchPowerIDs();
            UpdateMessage(iFrm, "Propagating Group IDs...");
            SetPowersetsFromGroups();
            UpdateMessage(iFrm, "Matching Enhancement IDs...");
            MatchEnhancementIDs();
            UpdateMessage(iFrm, "Matching Modifier IDs...");
            MatchModifierIDs();
            UpdateMessage(iFrm, "Matching Entity IDs...");
            MatchSummonIDs();
        }

        private static void UpdateMessage(IMessager iFrm, string iMsg)

        {
            iFrm?.SetMessage(iMsg);
        }

        private static void MatchArchetypeIDs()

        {
            for (var index = 0; index <= Database.Classes.Length - 1; ++index)
            {
                Database.Classes[index].Idx = index;
                Array.Sort(Database.Classes[index].Origin);
                Database.Classes[index].Primary = new int[0];
                Database.Classes[index].Secondary = new int[0];
                Database.Classes[index].Ancillary = new int[0];
            }
        }

        private static void MatchPowersetIDs()

        {
            for (var index1 = 0; index1 <= Database.Powersets.Length - 1; ++index1)
            {
                var powerset = Database.Powersets[index1];
                powerset.nID = index1;
                powerset.nArchetype = NidFromUidClass(powerset.ATClass);
                powerset.nIDTrunkSet = string.IsNullOrEmpty(powerset.UIDTrunkSet)
                    ? -1
                    : NidFromUidPowerset(powerset.UIDTrunkSet);
                powerset.nIDLinkSecondary = string.IsNullOrEmpty(powerset.UIDLinkSecondary)
                    ? -1
                    : NidFromUidPowerset(powerset.UIDLinkSecondary);
                if (powerset.UIDMutexSets.Length > 0)
                {
                    powerset.nIDMutexSets = new int[powerset.UIDMutexSets.Length];
                    for (var index2 = 0; index2 < powerset.UIDMutexSets.Length; ++index2)
                        powerset.nIDMutexSets[index2] = NidFromUidPowerset(powerset.UIDMutexSets[index2]);
                }

                powerset.Power = new int[0];
                powerset.Powers = new IPower[0];
            }
        }

        private static void MatchPowerIDs()
        {
            Database.MutexList = UidMutexAll();
            for (var index = 0; index < Database.Power.Length; ++index)
            {
                var power1 = Database.Power[index];
                if (string.IsNullOrEmpty(power1.FullName))
                    power1.FullName = "Orphan." + power1.DisplayName.Replace(" ", "_");
                power1.PowerIndex = index;
                power1.PowerSetID = NidFromUidPowerset(power1.FullSetName);
                if (power1.PowerSetID <= -1)
                    continue;
                var ps = power1.GetPowerSet();
                var length = ps.Powers.Length;
                power1.PowerSetIndex = length;
                var power2 = ps.Power;
                Array.Resize(ref power2, length + 1);
                ps.Power = power2;
                var powers = ps.Powers;
                Array.Resize(ref powers, length + 1);
                ps.Powers = powers;
                ps.Power[length] = index;
                ps.Powers[length] = power1;
            }

            foreach (var power in Database.Power)
            {
                var flag = false;
                if (power.GetPowerSet().SetType == Enums.ePowerSetType.SetBonus)
                {
                    flag = power.PowerName.Contains("Slow");
                    if (flag)
                    {
                        power.BuffMode = Enums.eBuffMode.Debuff;
                        foreach (var index in power.Effects)
                            index.buffMode = Enums.eBuffMode.Debuff;
                    }
                }

                foreach (var effect in power.Effects)
                {
                    if (flag)
                        effect.buffMode = Enums.eBuffMode.Debuff;
                    switch (effect.EffectType)
                    {
                        case Enums.eEffectType.GrantPower:
                            effect.nSummon = NidFromUidPower(effect.Summon);
                            power.HasGrantPowerEffect = true;
                            break;
                        case Enums.eEffectType.EntCreate:
                            effect.nSummon = NidFromUidEntity(effect.Summon);
                            break;
                        case Enums.eEffectType.PowerRedirect:
                            effect.nSummon = NidFromUidPower(effect.Override);
                            power.HasPowerOverrideEffect = true;
                            break;
                    }
                }

                power.NGroupMembership = new int[power.GroupMembership.Length];
                for (var index1 = 0; index1 < power.GroupMembership.Length; ++index1)
                for (var index2 = 0; index2 < Database.MutexList.Length; ++index2)
                {
                    if (!string.Equals(Database.MutexList[index2], power.GroupMembership[index1],
                        StringComparison.OrdinalIgnoreCase))
                        continue;
                    power.NGroupMembership[index1] = index2;
                    break;
                }

                power.NIDSubPower = new int[power.UIDSubPower.Length];
                for (var index = 0; index < power.UIDSubPower.Length; ++index)
                    power.NIDSubPower[index] = NidFromUidPower(power.UIDSubPower[index]);
                MatchRequirementId(power);
            }
        }

        private static void MatchRequirementId(IPower power)

        {
            if (power.Requires.ClassName.Length > 0)
            {
                power.Requires.NClassName = new int[power.Requires.ClassName.Length];
                for (var index = 0; index < power.Requires.ClassName.Length; ++index)
                    power.Requires.NClassName[index] = NidFromUidClass(power.Requires.ClassName[index]);
            }

            if (power.Requires.ClassNameNot.Length > 0)
            {
                power.Requires.NClassNameNot = new int[power.Requires.ClassNameNot.Length];
                for (var index = 0; index < power.Requires.ClassNameNot.Length; ++index)
                    power.Requires.NClassNameNot[index] = NidFromUidClass(power.Requires.ClassNameNot[index]);
            }

            if (power.Requires.PowerID.Length > 0)
            {
                power.Requires.NPowerID = new int[power.Requires.PowerID.Length][];
                for (var index1 = 0; index1 < power.Requires.PowerID.Length; ++index1)
                {
                    power.Requires.NPowerID[index1] = new int[power.Requires.PowerID[index1].Length];
                    for (var index2 = 0; index2 < power.Requires.PowerID[index1].Length; ++index2)
                        power.Requires.NPowerID[index1][index2] =
                            !string.IsNullOrEmpty(power.Requires.PowerID[index1][index2])
                                ? NidFromUidPower(power.Requires.PowerID[index1][index2])
                                : -1;
                }
            }

            if (power.Requires.PowerIDNot.Length <= 0)
                return;
            power.Requires.NPowerIDNot = new int[power.Requires.PowerIDNot.Length][];
            for (var index1 = 0; index1 < power.Requires.PowerIDNot.Length; ++index1)
            {
                power.Requires.NPowerIDNot[index1] = new int[power.Requires.PowerIDNot[index1].Length];
                for (var index2 = 0; index2 < power.Requires.PowerIDNot[index1].Length; ++index2)
                    power.Requires.NPowerIDNot[index1][index2] =
                        !string.IsNullOrEmpty(power.Requires.PowerIDNot[index1][index2])
                            ? NidFromUidPower(power.Requires.PowerIDNot[index1][index2])
                            : -1;
            }
        }

        private static void SetPowersetsFromGroups()
        {
            for (var index1 = 0; index1 < Database.Classes.Length; ++index1)
            {
                var archetype = Database.Classes[index1];
                var intList1 = new List<int>();
                var intList2 = new List<int>();
                var intList3 = new List<int>();
                for (var index2 = 0; index2 < Database.Powersets.Length; ++index2)
                {
                    var powerset = Database.Powersets[index2];
                    if (powerset.Powers.Length > 0)
                        powerset.Powers[0].SortOverride = true;
                    if (string.Equals(powerset.GroupName, archetype.PrimaryGroup, StringComparison.OrdinalIgnoreCase))
                    {
                        intList1.Add(index2);
                        if (powerset.nArchetype < 0)
                            powerset.nArchetype = index1;
                    }

                    if (string.Equals(powerset.GroupName, archetype.SecondaryGroup, StringComparison.OrdinalIgnoreCase))
                    {
                        intList2.Add(index2);
                        if (powerset.nArchetype < 0)
                            powerset.nArchetype = index1;
                    }

                    if (string.Equals(powerset.GroupName, archetype.EpicGroup, StringComparison.OrdinalIgnoreCase) &&
                        (powerset.nArchetype == index1 || powerset.Powers.Length > 0 &&
                            powerset.Powers[0].Requires.ClassOk(archetype.ClassName)))
                        intList3.Add(index2);
                }

                archetype.Primary = intList1.ToArray();
                archetype.Secondary = intList2.ToArray();
                archetype.Ancillary = intList3.ToArray();
            }
        }


        public static void MatchEnhancementIDs()
        {
            for (var index1 = 0; index1 <= Database.Power.Length - 1; ++index1)
            {
                var intList = new List<int>();
                for (var index2 = 0; index2 <= Database.Power[index1].BoostsAllowed.Length - 1; ++index2)
                {
                    var index3 = EnhancementClassIdFromName(Database.Power[index1].BoostsAllowed[index2]);
                    if (index3 > -1)
                        intList.Add(Database.EnhancementClasses[index3].ID);
                }

                if (Database.Power[index1].Enhancements != null)
                {
                    //do nothing
                }
                else
                {
                    Database.Power[index1].Enhancements = intList.ToArray();
                }
            }

            for (var index = 0; index <= Database.EnhancementSets.Count - 1; ++index)
                Database.EnhancementSets[index].Enhancements = new int[0];
            var flag = false;
            var str = string.Empty;
            for (var index1 = 0; index1 <= Database.Enhancements.Length - 1; ++index1)
            {
                var enhancement = Database.Enhancements[index1];
                if (enhancement.TypeID != Enums.eType.SetO || string.IsNullOrEmpty(enhancement.UIDSet))
                    continue;
                var index2 = NidFromUidioSet(enhancement.UIDSet);
                if (index2 > -1)
                {
                    enhancement.nIDSet = index2;
                    Array.Resize(ref Database.EnhancementSets[index2].Enhancements,
                        Database.EnhancementSets[index2].Enhancements.Length + 1);
                    Database.EnhancementSets[index2]
                        .Enhancements[Database.EnhancementSets[index2].Enhancements.Length - 1] = index1;
                }
                else
                {
                    str = str + enhancement.UIDSet + enhancement.Name + "\n";
                    flag = true;
                }
            }

            if (!flag)
                return;
            MessageBox.Show(
                "One or more enhancements had difficulty being matched to their invention set. You should check the database for misplaced Invention Set enhancements.\n" +
                str, "Mismatch Detected");
        }

        private static int EnhancementClassIdFromName(string iName)
        {
            int num;
            if (string.IsNullOrEmpty(iName))
            {
                num = -1;
            }
            else
            {
                for (var index = 0; index <= Database.EnhancementClasses.Length - 1; ++index)
                    if (string.Equals(Database.EnhancementClasses[index].ClassID, iName,
                        StringComparison.OrdinalIgnoreCase))
                        return index;
                num = -1;
            }

            return num;
        }

        private static void MatchModifierIDs()
        {
            foreach (var power in Database.Power)
            {
                foreach (var effect in power.Effects)
                {
                    effect.nModifierTable = NidFromUidAttribMod(effect.ModifierTable);
                }
            }
        }

        public static void MatchSummonIDs()
        {
            SummonedEntity.MatchSummonIDs(NidFromUidClass, NidFromUidPowerset, NidFromUidPower);
        }

        public static void AssignStaticIndexValues(ISerialize serializer, bool save)
        {
            var lastStaticPowerIdx = -2;
            for (var index = 0; index <= Database.Power.Length - 1; ++index)
                if (Database.Power[index].StaticIndex > -1 && Database.Power[index].StaticIndex > lastStaticPowerIdx)
                    lastStaticPowerIdx = Database.Power[index].StaticIndex;
            if (lastStaticPowerIdx < -1)
                lastStaticPowerIdx = -1;
            for (var index = 0; index <= Database.Power.Length - 1; ++index)
            {
                if (Database.Power[index].StaticIndex >= 0)
                    continue;
                ++lastStaticPowerIdx;
                Database.Power[index].StaticIndex = lastStaticPowerIdx;
            }

            var lastStaticEnhIdx = -2;
            for (var index = 1; index <= Database.Enhancements.Length - 1; ++index)
                if (Database.Enhancements[index].StaticIndex > -1 &&
                    Database.Enhancements[index].StaticIndex > lastStaticEnhIdx)
                    lastStaticEnhIdx = Database.Enhancements[index].StaticIndex;
            if (lastStaticEnhIdx < -1)
                lastStaticEnhIdx = -1;
            for (var index = 1; index <= Database.Enhancements.Length - 1; ++index)
            {
                if (Database.Enhancements[index].StaticIndex >= 1)
                    continue;
                ++lastStaticEnhIdx;
                Database.Enhancements[index].StaticIndex = lastStaticEnhIdx;
            }

            if (!save)
                return;
            SaveMainDatabase(serializer);
            SaveEnhancementDb(serializer);
        }
    }

    public class AbstractConverter<TReal, TAbstract> : JsonConverter where TReal : TAbstract
    {
        public override Boolean CanConvert(Type objectType)
            => objectType == typeof(TAbstract);

        public override Object ReadJson(JsonReader reader, Type type, Object value, JsonSerializer jser)
            => jser.Deserialize<TReal>(reader);

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer jser)
            => jser.Serialize(writer, value);
    }
}