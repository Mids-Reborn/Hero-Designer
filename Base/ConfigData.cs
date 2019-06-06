﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

// Token: 0x0200001A RID: 26
public class ConfigData
{

    // (get) Token: 0x060004B8 RID: 1208 RVA: 0x0001A3A0 File Offset: 0x000185A0
    internal static ConfigData Current
    {
        get
        {
            ConfigData configData;
            if ((configData = ConfigData._current) == null)
            {
                configData = (ConfigData._current = new ConfigData(Files.SelectConfigFileLoad()));
            }
            return configData;
        }
    }


    // (get) Token: 0x060004B9 RID: 1209 RVA: 0x0001A3D8 File Offset: 0x000185D8
    // (set) Token: 0x060004BA RID: 1210 RVA: 0x0001A3F5 File Offset: 0x000185F5
    public bool PrintHistoryIOLevels
    {
        get
        {
            return this.I9.PrintIOLevels;
        }
        set
        {
            this.I9.PrintIOLevels = value;
        }
    }


    private ConfigData(string iFilename = "")
    {
        this.DamageMath.Calculate = ConfigData.EDamageMath.Average;
        this.DamageMath.ReturnValue = ConfigData.EDamageReturn.Numeric;
        this.Inc.PvE = true;
        this.I9.DefaultIOLevel = 49;
        this.I9.DisplayIOLevels = true;
        this.I9.CalculateEnahncementFX = true;
        this.I9.CalculateSetBonusFX = true;
        this.I9.PrintIOLevels = true;
        this.I9.ExportIOLevels = false;
        this.I9.ExportCompress = true;
        this.I9.ExportDataChunk = true;
        this.I9.ExportStripEnh = false;
        this.I9.ExportStripSetNames = false;
        this.I9.ExportExtraSep = false;
        this.UpdatePath = "http://repo.cohtitan.com/mids_updates/";
        this.DefaultSaveFolder = OS.GetDefaultSaveFolder();
        this.RtFont.SetDefault();
        this.Tips = new Tips();
        this.Export = new ExportConfig();
        this.CompOverride = new Enums.CompOverride[0];
        this.RTF = new RTF();
        if (!string.IsNullOrEmpty(iFilename))
        {
            try
            {
                this.Load(iFilename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        this.RelocateSaveFolder(false);
        try
        {
            this.LoadOverrides();
        }
        catch (Exception ex2)
        {
            MessageBox.Show(ex2.Message);
        }
    }


    private void Load(string iFilename)
    {
        using (FileStream fileStream = new FileStream(iFilename, FileMode.Open, FileAccess.Read))
        {
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                string text = reader.ReadString();
                string a;
                if ((a = text) != null)
                {
                    float num;
                    if (!(a == "Mids' Hero Designer Config"))
                    {
                        if (!(a == "Mids' Hero Designer Config V2"))
                        {
                            goto IL_A15;
                        }
                        num = reader.ReadSingle();
                    }
                    else
                    {
                        num = 0.9f;
                    }
                    this.NoToolTips = reader.ReadBoolean();
                    this.BaseAcc = reader.ReadSingle();
                    reader.ReadSingle();
                    reader.ReadSingle();
                    reader.ReadSingle();
                    reader.ReadSingle();
                    reader.ReadSingle();
                    this.CalcEnhLevel = (Enums.eEnhRelative)reader.ReadInt32();
                    this.CalcEnhOrigin = (Enums.eEnhGrade)reader.ReadInt32();
                    this.ExempHigh = reader.ReadInt32();
                    this.ExempLow = reader.ReadInt32();
                    this.Inc.PvE = reader.ReadBoolean();
                    reader.ReadBoolean();
                    this.DamageMath.Calculate = (ConfigData.EDamageMath)reader.ReadInt32();
                    reader.ReadSingle();
                    if (num < 1.24f)
                    {
                        if (reader.ReadBoolean())
                        {
                        }
                    }
                    else
                    {
                        reader.ReadInt32();
                    }
                    this.DamageMath.ReturnValue = (ConfigData.EDamageReturn)reader.ReadInt32();
                    this.DataDamageGraph = reader.ReadBoolean();
                    this.DataDamageGraphPercentageOnly = reader.ReadBoolean();
                    this.DataGraphType = (Enums.eDDGraph)reader.ReadInt32();
                    this.ExportScheme = reader.ReadInt32();
                    this.ExportTarget = reader.ReadInt32();
                    if (num >= 1.24f)
                    {
                        this.ExportBonusTotals = reader.ReadBoolean();
                        this.ExportBonusList = reader.ReadBoolean();
                    }
                    this._hideOriginEnhancements = reader.ReadBoolean();
                    this.ShowVillainColours = reader.ReadBoolean();
                    this.CheckForUpdates = reader.ReadBoolean();
                    this.Columns = reader.ReadInt32();
                    this.LastSize.Width = reader.ReadInt32();
                    this.LastSize.Height = reader.ReadInt32();
                    this.DvState = (Enums.eVisibleSize)reader.ReadInt32();
                    this.StatGraphStyle = (Enums.GraphStyle)reader.ReadInt32();
                    if (num >= 1f)
                    {
                        this.FreshInstall = reader.ReadBoolean();
                    }
                    if (num >= 1.1f)
                    {
                        this.ForceLevel = reader.ReadInt32();
                    }
                    if (num >= 1.2f)
                    {
                        this.I9.DefaultIOLevel = reader.ReadInt32();
                        if (this.I9.DefaultIOLevel > 49)
                        {
                            this.I9.DefaultIOLevel = 49;
                        }
                        this.I9.DisplayIOLevels = reader.ReadBoolean();
                        this.I9.CalculateEnahncementFX = reader.ReadBoolean();
                        this.I9.CalculateSetBonusFX = reader.ReadBoolean();
                        this.I9.ExportIOLevels = reader.ReadBoolean();
                        this.I9.PrintIOLevels = reader.ReadBoolean();
                        this.I9.ExportCompress = reader.ReadBoolean();
                        this.I9.ExportDataChunk = reader.ReadBoolean();
                        this.I9.ExportStripEnh = reader.ReadBoolean();
                        this.I9.ExportStripSetNames = reader.ReadBoolean();
                        this.I9.ExportExtraSep = reader.ReadBoolean();
                        this.PrintInColour = reader.ReadBoolean();
                        this._printScheme = reader.ReadInt32();
                    }
                    if (num >= 1.21f)
                    {
                        this.RtFont.PairedBase = reader.ReadSingle();
                        this.RtFont.PairedBold = reader.ReadBoolean();
                        this.RtFont.RTFBase = reader.ReadInt32();
                        this.RtFont.RTFBold = reader.ReadBoolean();
                        this.RtFont.ColorBackgroundHero = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorBackgroundVillain = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorEnhancement = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorFaded = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorInvention = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorInventionInv = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorText = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorWarning = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorPlName = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorPlSpecial = ConfigData.ReadRGB(reader);
                    }
                    if (num >= 1.22f)
                    {
                        this.ShowSlotLevels = reader.ReadBoolean();
                        this.LoadLastFileOnStart = reader.ReadBoolean();
                        this.LastFileName = reader.ReadString();
                        this.RtFont.ColorPowerAvailable = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorPowerTaken = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorPowerTakenDark = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorPowerDisabled = ConfigData.ReadRGB(reader);
                        this.RtFont.ColorPowerHighlight = ConfigData.ReadRGB(reader);
                    }
                    if (num >= 1.23f)
                    {
                        this.Tips = new Tips(reader);
                        this.DefaultSaveFolder = reader.ReadString();
                    }
                    if (num >= 1.24f)
                    {
                        this.EnhanceVisibility = reader.ReadBoolean();
                        reader.ReadBoolean();
                        this.BuildMode = (Enums.dmModes)reader.ReadInt32();
                        this.BuildOption = (Enums.dmItem)reader.ReadInt32();
                        this.UpdatePath = reader.ReadString();
                        if (string.IsNullOrEmpty(this.UpdatePath))
                        {
                            this.UpdatePath = "http://repo.cohtitan.com/mids_updates/";
                        }
                    }
                    if (num >= 1.25f)
                    {
                        this.ShowEnhRel = reader.ReadBoolean();
                        this.ShowRelSymbols = reader.ReadBoolean();
                        this.ShowPopup = reader.ReadBoolean();
                        if (num >= 1.32f)
                        {
                            this.ShowAlphaPopup = reader.ReadBoolean();
                        }
                        this.PopupRecipes = reader.ReadBoolean();
                        this.ShoppingListIncludesRecipes = reader.ReadBoolean();
                        this.PrintProfile = (ConfigData.PrintOptionProfile)reader.ReadInt32();
                        this.PrintHistory = reader.ReadBoolean();
                        this.LastPrinter = reader.ReadString();
                        this.PrintProfileEnh = reader.ReadBoolean();
                        this.DesaturateInherent = reader.ReadBoolean();
                        this.ReapeatOnMiddleClick = reader.ReadBoolean();
                    }
                    if (num >= 1.26f)
                    {
                        this.ExportHex = reader.ReadBoolean();
                    }
                    if (num >= 1.27f)
                    {
                        this.SpeedFormat = (Enums.eSpeedMeasure)reader.ReadInt32();
                    }
                    if (num >= 1.28f)
                    {
                        this.SaveFolderChecked = reader.ReadBoolean();
                    }
                    if (num >= 1.29f)
                    {
                        this.UseArcanaTime = reader.ReadBoolean();
                    }
                    if (num >= 1.3f)
                    {
                        this.Suppression = (Enums.eSuppress)reader.ReadInt32();
                    }
                    if (num >= 1.31f)
                    {
                        for (int index = 0; index < 19; index++)
                        {
                            this.DragDropScenarioAction[index] = reader.ReadInt16();
                        }
                    }
                    this.Export.ColorSchemes = new ExportConfig.ColorScheme[reader.ReadInt32() + 1];
                    for (int index2 = 0; index2 < this.Export.ColorSchemes.Length; index2++)
                    {
                        this.Export.ColorSchemes[index2].SchemeName = reader.ReadString();
                        this.Export.ColorSchemes[index2].Heading = ConfigData.ReadRGB(reader);
                        this.Export.ColorSchemes[index2].Level = ConfigData.ReadRGB(reader);
                        this.Export.ColorSchemes[index2].Slots = ConfigData.ReadRGB(reader);
                        this.Export.ColorSchemes[index2].Title = ConfigData.ReadRGB(reader);
                        if (num >= 1.2f)
                        {
                            this.Export.ColorSchemes[index2].IOColor = ConfigData.ReadRGB(reader);
                            this.Export.ColorSchemes[index2].SetColor = ConfigData.ReadRGB(reader);
                            this.Export.ColorSchemes[index2].HOColor = ConfigData.ReadRGB(reader);
                            this.Export.ColorSchemes[index2].Power = ConfigData.ReadRGB(reader);
                        }
                    }
                    this.Export.FormatCode = new ExportConfig.FormatCodes[reader.ReadInt32() + 1];
                    for (int index3 = 0; index3 < this.Export.FormatCode.Length; index3++)
                    {
                        this.Export.FormatCode[index3].Name = reader.ReadString();
                        this.Export.FormatCode[index3].Notes = reader.ReadString();
                        this.Export.FormatCode[index3].BoldOff = reader.ReadString();
                        this.Export.FormatCode[index3].BoldOn = reader.ReadString();
                        this.Export.FormatCode[index3].ColourOff = reader.ReadString();
                        this.Export.FormatCode[index3].ColourOn = reader.ReadString();
                        this.Export.FormatCode[index3].ItalicOff = reader.ReadString();
                        this.Export.FormatCode[index3].ItalicOn = reader.ReadString();
                        this.Export.FormatCode[index3].SizeOff = reader.ReadString();
                        this.Export.FormatCode[index3].SizeOn = reader.ReadString();
                        this.Export.FormatCode[index3].UnderlineOff = reader.ReadString();
                        this.Export.FormatCode[index3].UnderlineOn = reader.ReadString();
                        this.Export.FormatCode[index3].Space = (ExportConfig.WhiteSpace)reader.ReadInt32();
                    }
                    this.CreateDefaultSaveFolder();
                    goto IL_A44;
                }
            IL_A15:
                MessageBox.Show("Config file was missing a header! Using defaults.");
                reader.Close();
                fileStream.Close();
            }
        IL_A44:;
        }
    }


    public void CreateDefaultSaveFolder()
    {
        if (!Directory.Exists(this.DefaultSaveFolder))
        {
            this.DefaultSaveFolder = OS.GetDefaultSaveFolder();
        }
        if (!Directory.Exists(this.DefaultSaveFolder))
        {
            Directory.CreateDirectory(this.DefaultSaveFolder);
        }
    }


    private void Save(string iFilename, float version)
    {
        using (FileStream fileStream = new FileStream(iFilename, FileMode.Create))
        {
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write("Mids' Hero Designer Config V2");
                writer.Write(version);
                writer.Write(this.NoToolTips);
                writer.Write(this.BaseAcc);
                writer.Write(0f);
                writer.Write(0f);
                writer.Write(0f);
                writer.Write(0f);
                writer.Write(0f);
                writer.Write((int)this.CalcEnhLevel);
                writer.Write((int)this.CalcEnhOrigin);
                writer.Write(this.ExempHigh);
                writer.Write(this.ExempLow);
                writer.Write(this.Inc.PvE);
                writer.Write(true);
                writer.Write((int)this.DamageMath.Calculate);
                writer.Write(0f);
                writer.Write(0);
                writer.Write((int)this.DamageMath.ReturnValue);
                writer.Write(this.DataDamageGraph);
                writer.Write(this.DataDamageGraphPercentageOnly);
                writer.Write((int)this.DataGraphType);
                writer.Write(this.ExportScheme);
                writer.Write(this.ExportTarget);
                writer.Write(this.ExportBonusTotals);
                writer.Write(this.ExportBonusList);
                writer.Write(this._hideOriginEnhancements);
                writer.Write(this.ShowVillainColours);
                writer.Write(this.CheckForUpdates);
                writer.Write(this.Columns);
                writer.Write(this.LastSize.Width);
                writer.Write(this.LastSize.Height);
                writer.Write((int)this.DvState);
                writer.Write((int)this.StatGraphStyle);
                writer.Write(this.FreshInstall);
                writer.Write(this.ForceLevel);
                writer.Write(this.I9.DefaultIOLevel);
                writer.Write(this.I9.DisplayIOLevels);
                writer.Write(this.I9.CalculateEnahncementFX);
                writer.Write(this.I9.CalculateSetBonusFX);
                writer.Write(this.I9.ExportIOLevels);
                writer.Write(this.I9.PrintIOLevels);
                writer.Write(this.I9.ExportCompress);
                writer.Write(this.I9.ExportDataChunk);
                writer.Write(this.I9.ExportStripEnh);
                writer.Write(this.I9.ExportStripSetNames);
                writer.Write(this.I9.ExportExtraSep);
                writer.Write(this.PrintInColour);
                writer.Write(this._printScheme);
                writer.Write(this.RtFont.PairedBase);
                writer.Write(this.RtFont.PairedBold);
                writer.Write(this.RtFont.RTFBase);
                writer.Write(this.RtFont.RTFBold);
                ConfigData.WriteRGB(writer, this.RtFont.ColorBackgroundHero);
                ConfigData.WriteRGB(writer, this.RtFont.ColorBackgroundVillain);
                ConfigData.WriteRGB(writer, this.RtFont.ColorEnhancement);
                ConfigData.WriteRGB(writer, this.RtFont.ColorFaded);
                ConfigData.WriteRGB(writer, this.RtFont.ColorInvention);
                ConfigData.WriteRGB(writer, this.RtFont.ColorInventionInv);
                ConfigData.WriteRGB(writer, this.RtFont.ColorText);
                ConfigData.WriteRGB(writer, this.RtFont.ColorWarning);
                ConfigData.WriteRGB(writer, this.RtFont.ColorPlName);
                ConfigData.WriteRGB(writer, this.RtFont.ColorPlSpecial);
                writer.Write(this.ShowSlotLevels);
                writer.Write(this.LoadLastFileOnStart);
                writer.Write(this.LastFileName);
                ConfigData.WriteRGB(writer, this.RtFont.ColorPowerAvailable);
                ConfigData.WriteRGB(writer, this.RtFont.ColorPowerTaken);
                ConfigData.WriteRGB(writer, this.RtFont.ColorPowerTakenDark);
                ConfigData.WriteRGB(writer, this.RtFont.ColorPowerDisabled);
                ConfigData.WriteRGB(writer, this.RtFont.ColorPowerHighlight);
                this.Tips.StoreTo(writer);
                writer.Write(this.DefaultSaveFolder);
                writer.Write(this.EnhanceVisibility);
                writer.Write(false);
                writer.Write((int)this.BuildMode);
                writer.Write((int)this.BuildOption);
                writer.Write(this.UpdatePath);
                writer.Write(this.ShowEnhRel);
                writer.Write(this.ShowRelSymbols);
                writer.Write(this.ShowPopup);
                writer.Write(this.ShowAlphaPopup);
                writer.Write(this.PopupRecipes);
                writer.Write(this.ShoppingListIncludesRecipes);
                writer.Write((int)this.PrintProfile);
                writer.Write(this.PrintHistory);
                writer.Write(this.LastPrinter);
                writer.Write(this.PrintProfileEnh);
                writer.Write(this.DesaturateInherent);
                writer.Write(this.ReapeatOnMiddleClick);
                writer.Write(this.ExportHex);
                writer.Write((int)this.SpeedFormat);
                writer.Write(this.SaveFolderChecked);
                writer.Write(this.UseArcanaTime);
                writer.Write((int)this.Suppression);
                for (int index = 0; index < 19; index++)
                {
                    writer.Write(this.DragDropScenarioAction[index]);
                }
                writer.Write(this.Export.ColorSchemes.Length - 1);
                for (int index2 = 0; index2 <= this.Export.ColorSchemes.Length - 1; index2++)
                {
                    writer.Write(this.Export.ColorSchemes[index2].SchemeName);
                    ConfigData.WriteRGB(writer, this.Export.ColorSchemes[index2].Heading);
                    ConfigData.WriteRGB(writer, this.Export.ColorSchemes[index2].Level);
                    ConfigData.WriteRGB(writer, this.Export.ColorSchemes[index2].Slots);
                    ConfigData.WriteRGB(writer, this.Export.ColorSchemes[index2].Title);
                    ConfigData.WriteRGB(writer, this.Export.ColorSchemes[index2].IOColor);
                    ConfigData.WriteRGB(writer, this.Export.ColorSchemes[index2].SetColor);
                    ConfigData.WriteRGB(writer, this.Export.ColorSchemes[index2].HOColor);
                    ConfigData.WriteRGB(writer, this.Export.ColorSchemes[index2].Power);
                }
                writer.Write(this.Export.FormatCode.Length - 1);
                for (int index3 = 0; index3 <= this.Export.FormatCode.Length - 1; index3++)
                {
                    writer.Write(this.Export.FormatCode[index3].Name);
                    writer.Write(this.Export.FormatCode[index3].Notes);
                    writer.Write(this.Export.FormatCode[index3].BoldOff);
                    writer.Write(this.Export.FormatCode[index3].BoldOn);
                    writer.Write(this.Export.FormatCode[index3].ColourOff);
                    writer.Write(this.Export.FormatCode[index3].ColourOn);
                    writer.Write(this.Export.FormatCode[index3].ItalicOff);
                    writer.Write(this.Export.FormatCode[index3].ItalicOn);
                    writer.Write(this.Export.FormatCode[index3].SizeOff);
                    writer.Write(this.Export.FormatCode[index3].SizeOn);
                    writer.Write(this.Export.FormatCode[index3].UnderlineOff);
                    writer.Write(this.Export.FormatCode[index3].UnderlineOn);
                    writer.Write(Convert.ToInt32(this.Export.FormatCode[index3].Space));
                }
            }
        }
    }


    private static Color ReadRGB(BinaryReader reader)
    {
        byte red = reader.ReadByte();
        byte green = reader.ReadByte();
        byte blue = reader.ReadByte();
        return Color.FromArgb((int)red, (int)green, (int)blue);
    }


    private static void WriteRGB(BinaryWriter writer, Color iColor)
    {
        writer.Write(iColor.R);
        writer.Write(iColor.G);
        writer.Write(iColor.B);
    }


    private void RelocateSaveFolder(bool manual)
    {
        if (OS.GetDefaultSaveFolder() != this.DefaultSaveFolder & (!this.SaveFolderChecked || manual))
        {
            if (this.DefaultSaveFolder.IndexOf(OS.GetMyDocumentsPath(), StringComparison.OrdinalIgnoreCase) > -1)
            {
                this.SaveFolderChecked = true;
                return;
            }
            if (Directory.Exists(this.DefaultSaveFolder))
            {
                if (MessageBox.Show("In order for Mids' Hero/Villain Designer to be better behaved in more recent versions of Windows, the recommended Save folder has been changed to appear inside the My Documents folder.\nThe application can automatically move your save folder and its contents to 'My Documents\\Hero & Villain Builds\\'.\nThis message will not appear again.\n\nMove your Save folder now?", "Save Folder Location", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.LastFileName = string.Empty;
                    string defaultSaveFolder = this.DefaultSaveFolder;
                    this.DefaultSaveFolder = OS.GetDefaultSaveFolder();
                    if (FileIO.CopyFolder(defaultSaveFolder, this.DefaultSaveFolder))
                    {
                        MessageBox.Show("Save folder was moved!", "All Done", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Save folder couldn't be moved! Using old save folder instead.", "Whoops", MessageBoxButtons.OK);
                        this.DefaultSaveFolder = defaultSaveFolder;
                    }
                }
            }
            else
            {
                this.CreateDefaultSaveFolder();
            }
        }
        this.SaveFolderChecked = true;
    }


    public void SaveConfig()
    {
        try
        {
            this.Save(Files.SelectConfigFileSave(), 1.32f);
            this.SaveOverrides();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }


    private void LoadOverrides()
    {
        using (FileStream fileStream = new FileStream(Files.SelectDataFileLoad("Compare.mhd"), FileMode.Open, FileAccess.Read))
        {
            using (BinaryReader binaryReader = new BinaryReader(fileStream))
            {
                if (binaryReader.ReadString() != "Mids' Hero Designer Comparison Overrides")
                {
                    MessageBox.Show("Overrides file was missing a header! Not loading powerset comparison overrides.");
                }
                else
                {
                    this.CompOverride = new Enums.CompOverride[binaryReader.ReadInt32() + 1];
                    for (int index = 0; index <= this.CompOverride.Length - 1; index++)
                    {
                        this.CompOverride[index].Powerset = binaryReader.ReadString();
                        this.CompOverride[index].Power = binaryReader.ReadString();
                        this.CompOverride[index].Override = binaryReader.ReadString();
                    }
                }
            }
        }
    }


    private void SaveOverrides()
    {
        using (FileStream fileStream = new FileStream(Files.SelectDataFileLoad("Compare.mhd"), FileMode.Create))
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
            {
                binaryWriter.Write("Mids' Hero Designer Comparison Overrides");
                binaryWriter.Write(this.CompOverride.Length - 1);
                for (int index = 0; index <= this.CompOverride.Length - 1; index++)
                {
                    binaryWriter.Write(this.CompOverride[index].Powerset);
                    binaryWriter.Write(this.CompOverride[index].Power);
                    binaryWriter.Write(this.CompOverride[index].Override);
                }
            }
        }
    }


    public const string UpdatePathDefault = "http://repo.cohtitan.com/mids_updates/";


    private static ConfigData _current;


    public float BaseAcc = 0.75f;


    public Enums.eEnhGrade CalcEnhOrigin = Enums.eEnhGrade.SingleO;


    public Enums.eEnhRelative CalcEnhLevel = Enums.eEnhRelative.Even;


    public int ExempHigh = 50;


    public int TeamSize = 1;


    public int ExempLow = 50;


    public int ForceLevel = 50;


    public int ExportScheme = 1;


    public int ExportTarget = 1;


    public bool ExportBonusTotals;


    public bool ExportBonusList;


    public bool NoToolTips;


    public bool DataDamageGraph = true;


    public Enums.eDDGraph DataGraphType = Enums.eDDGraph.Both;


    public bool DataDamageGraphPercentageOnly;


    private bool _hideOriginEnhancements;


    public bool ShowVillainColours = true;


    public bool CheckForUpdates;


    public bool FreshInstall = true;


    public int Columns = 3;


    public Size LastSize = new Size(1072, 760);


    public Enums.eVisibleSize DvState;


    public Enums.GraphStyle StatGraphStyle = Enums.GraphStyle.Stacked;


    public Enums.eSuppress Suppression;


    public bool UseArcanaTime;


    public ConfigData.SDamageMath DamageMath;


    public ConfigData.IncludeExclude Inc;


    public readonly ExportConfig Export;


    public ConfigData.Si9 I9;


    public ConfigData.FontSettings RtFont;


    public Enums.CompOverride[] CompOverride = new Enums.CompOverride[0];


    public bool PrintInColour;


    private int _printScheme;


    public ConfigData.PrintOptionProfile PrintProfile = ConfigData.PrintOptionProfile.SinglePage;


    public bool PrintProfileEnh = true;


    public bool PrintHistory;


    public bool SaveFolderChecked;


    public string LastPrinter = string.Empty;


    public bool ShowSlotLevels;


    public bool ShowEnhRel;


    public bool ShowRelSymbols;


    public bool LoadLastFileOnStart = true;


    public string LastFileName = string.Empty;


    public string DefaultSaveFolder = string.Empty;


    public bool EnhanceVisibility;


    public bool DesaturateInherent = true;


    public Enums.dmModes BuildMode = Enums.dmModes.Dynamic;


    public Enums.dmItem BuildOption = Enums.dmItem.Slot;


    public string UpdatePath = "http://repo.cohtitan.com/mids_updates/";


    public Tips Tips;


    public bool ShowPopup = true;


    public bool ShowAlphaPopup = true;


    public bool PopupRecipes;


    public bool ShoppingListIncludesRecipes;


    public bool ReapeatOnMiddleClick = true;


    public bool ExportHex = true;


    public bool ExportChunkOnly;


    public readonly short[] DragDropScenarioAction = new short[]
    {
        3,
        0,
        5,
        0,
        3,
        5,
        0,
        0,
        5,
        0,
        2,
        3,
        0,
        2,
        2,
        0,
        0,
        0,
        0,
        0
    };


    public Enums.eSpeedMeasure SpeedFormat = Enums.eSpeedMeasure.MilesPerHour;


    public bool LongExport;


    public bool MasterMode;


    public bool ShrinkFrmSets;


    public readonly RTF RTF;


    public enum EDamageMath
    {

        Minimum,

        Average,

        Max
    }


    public enum EDamageReturn
    {

        Numeric,

        DPS,

        DPA
    }


    public enum PrintOptionProfile
    {

        None,

        SinglePage,

        MultiPage
    }


    public struct SDamageMath
    {

        public ConfigData.EDamageMath Calculate;


        public ConfigData.EDamageReturn ReturnValue;
    }


    public struct IncludeExclude
    {

        public bool PvE;
    }


    public struct Si9
    {

        public int DefaultIOLevel;


        public bool DisplayIOLevels;


        public bool CalculateEnahncementFX;


        public bool CalculateSetBonusFX;


        public bool PrintIOLevels;


        public bool ExportIOLevels;


        public bool ExportStripSetNames;


        public bool ExportStripEnh;


        public bool ExportDataChunk;


        public bool ExportCompress;


        public bool ExportExtraSep;
    }


    public struct FontSettings
    {

        public void Assign(ConfigData.FontSettings iFs)
        {
            this.RTFBase = iFs.RTFBase;
            this.RTFBold = iFs.RTFBold;
            this.ColorBackgroundHero = iFs.ColorBackgroundHero;
            this.ColorBackgroundVillain = iFs.ColorBackgroundVillain;
            this.ColorText = iFs.ColorText;
            this.ColorInvention = iFs.ColorInvention;
            this.ColorInventionInv = iFs.ColorInventionInv;
            this.ColorFaded = iFs.ColorFaded;
            this.ColorEnhancement = iFs.ColorEnhancement;
            this.ColorWarning = iFs.ColorWarning;
            this.ColorPlName = iFs.ColorPlName;
            this.ColorPlSpecial = iFs.ColorPlSpecial;
            this.ColorPowerAvailable = iFs.ColorPowerAvailable;
            this.ColorPowerTaken = iFs.ColorPowerTaken;
            this.ColorPowerTakenDark = iFs.ColorPowerTakenDark;
            this.ColorPowerHighlight = iFs.ColorPowerHighlight;
            this.ColorPowerDisabled = iFs.ColorPowerDisabled;
            this.PairedBold = iFs.PairedBold;
            this.PairedBase = iFs.PairedBase;
        }


        public void SetDefault()
        {
            this.RTFBase = 16;
            this.RTFBold = false;
            this.ColorBackgroundHero = Color.FromArgb(0, 0, 32);
            this.ColorBackgroundVillain = Color.FromArgb(32, 0, 0);
            this.ColorEnhancement = Color.FromArgb(0, 255, 0);
            this.ColorFaded = Color.FromArgb(192, 192, 192);
            this.ColorInvention = Color.FromArgb(0, 255, 255);
            this.ColorInventionInv = Color.FromArgb(0, 0, 128);
            this.ColorText = Color.FromArgb(255, 255, 255);
            this.ColorWarning = Color.FromArgb(255, 0, 0);
            this.ColorPlName = Color.FromArgb(192, 192, 255);
            this.ColorPlSpecial = Color.FromArgb(128, 128, 255);
            this.ColorPowerAvailable = Color.Yellow;
            this.ColorPowerTaken = Color.Lime;
            this.ColorPowerTakenDark = Color.FromArgb(0, 192, 0);
            this.ColorPowerHighlight = Color.FromArgb(64, 64, 96);
            this.ColorPowerDisabled = Color.FromArgb(128, 128, 192);
            this.PairedBase = 8.25f;
            this.PairedBold = false;
        }


        public int RTFBase;


        public bool RTFBold;


        public Color ColorBackgroundHero;


        public Color ColorBackgroundVillain;


        public Color ColorText;


        public Color ColorInvention;


        public Color ColorInventionInv;


        public Color ColorFaded;


        public Color ColorEnhancement;


        public Color ColorWarning;


        public Color ColorPlName;


        public Color ColorPlSpecial;


        public Color ColorPowerAvailable;


        public Color ColorPowerTaken;


        public Color ColorPowerTakenDark;


        public Color ColorPowerHighlight;


        public Color ColorPowerDisabled;


        public bool PairedBold;


        public float PairedBase;
    }


    public struct ExternalUris
    {

        public const string VersionFile = "version.xml";


        public const string EmailAddress = "midsteam@cohtitan.com";


        public const string BugReport = "http://www.honourableunited.org.uk/mhdreport.php";


        public const string DataLinkDownload = "http://www.cohplanner.com/mids/download.php";


        public const string Forums = "http://www.cohtitan.com/forum/";


        public const string Titan = "http://www.cohtitan.com/";


        public const string Planner = "http://www.cohplanner.com/";


        public const string Donate = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=8057167";


        public const string UpdateExe = "MHDLoader.exe";
    }
}
