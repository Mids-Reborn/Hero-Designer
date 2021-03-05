using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Mids_Reborn.Forms.OptionsMenuItems.DbEditor;
using Mids_Reborn.My;
using mrbBase;
using mrbBase.Base.Display;

namespace Mids_Reborn.Forms
{
    public partial class frmSetListingPvP : Form
    {
        public frmSetListingPvP()
        {
            Load += frmSetListingPvP_Load;
            InitializeComponent();
            Name = nameof(frmSetListingPvP);
            var componentResourceManager = new ComponentResourceManager(typeof(frmSetListingPvP));
            Icon = Resources.reborn;
        }

        private void AddListItem(int Index)
        {
            var items = new string[6];
            var enhancementSet = DatabaseAPI.Database.EnhancementSets[Index];
            items[0] = enhancementSet.DisplayName + " (" + enhancementSet.ShortName + ")";
            items[1] = Enum.GetName(enhancementSet.SetType.GetType(), enhancementSet.SetType);
            items[2] = Convert.ToString(enhancementSet.LevelMin + 1, CultureInfo.InvariantCulture);
            items[3] = Convert.ToString(enhancementSet.LevelMax + 1, CultureInfo.InvariantCulture);
            items[4] = Convert.ToString(enhancementSet.Enhancements.Length, CultureInfo.InvariantCulture);
            var num1 = 0;
            var num2 = enhancementSet.Bonus.Length - 1;
            for (var index = 0; index <= num2; ++index)
                if (enhancementSet.Bonus[index].Index.Length > 0)
                    ++num1;
            items[5] = Convert.ToString(num1);
            lvSets.Items.Add(new ListViewItem(items, Index));
            lvSets.Items[lvSets.Items.Count - 1].Selected = true;
            lvSets.Items[lvSets.Items.Count - 1].EnsureVisible();
        }

        private void btnAdd_Click(object sender, EventArgs e)

        {
            var iSet = new EnhancementSet();
            using var frmSetEditPvP = new frmSetEditPvPNEW(ref iSet);
            var num = (int) frmSetEditPvP.ShowDialog();
            if (frmSetEditPvP.DialogResult != DialogResult.OK)
                return;
            DatabaseAPI.Database.EnhancementSets.Add(new EnhancementSet(frmSetEditPvP.mySet));
            ImageUpdate();
            AddListItem(DatabaseAPI.Database.EnhancementSets.Count - 1);
        }

        private void btnCancel_Click(object sender, EventArgs e)

        {
            Hide();
        }

        private void btnClone_Click(object sender, EventArgs e)

        {
            if (lvSets.SelectedIndices.Count <= 0)
                return;
            var iSet = new EnhancementSet(DatabaseAPI.Database.EnhancementSets[lvSets.SelectedIndices[0]]);
            iSet.DisplayName += " Copy";
            using var frmSetEditPvP = new frmSetEditPvPNEW(ref iSet);
            var num = (int) frmSetEditPvP.ShowDialog();
            if (frmSetEditPvP.DialogResult != DialogResult.OK)
                return;
            DatabaseAPI.Database.EnhancementSets.Add(new EnhancementSet(frmSetEditPvP.mySet));
            ImageUpdate();
            AddListItem(DatabaseAPI.Database.EnhancementSets.Count - 1);
        }

        private void btnDelete_Click(object sender, EventArgs e)

        {
            if (lvSets.SelectedIndices.Count <= 0 ||
                MessageBox.Show($"Really delete Set: {lvSets.SelectedItems[0].Text}?", "Are you sure?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            var selectedIndex = lvSets.SelectedIndices[0];
            DatabaseAPI.Database.EnhancementSets.RemoveAt(selectedIndex);
            DatabaseAPI.MatchEnhancementIDs();
            DisplayList();
            if (lvSets.Items.Count <= 0)
                return;
            if (lvSets.Items.Count > selectedIndex)
                lvSets.Items[selectedIndex].Selected = true;
            else if (lvSets.Items.Count == selectedIndex)
                lvSets.Items[selectedIndex - 1].Selected = true;
        }

        private void btnDown_Click(object sender, EventArgs e)

        {
            if (lvSets.SelectedIndices.Count <= 0)
                return;
            var selectedIndex = lvSets.SelectedIndices[0];
            if (selectedIndex >= lvSets.Items.Count - 1)
                return;
            EnhancementSet[] enhancementSetArray =
            {
                new EnhancementSet(DatabaseAPI.Database.EnhancementSets[selectedIndex]),
                new EnhancementSet(DatabaseAPI.Database.EnhancementSets[selectedIndex + 1])
            };
            DatabaseAPI.Database.EnhancementSets[selectedIndex + 1] = new EnhancementSet(enhancementSetArray[0]);
            DatabaseAPI.Database.EnhancementSets[selectedIndex] = new EnhancementSet(enhancementSetArray[1]);
            DatabaseAPI.MatchEnhancementIDs();
            var listViewItem1 = (ListViewItem) lvSets.Items[selectedIndex].Clone();
            var listViewItem2 = (ListViewItem) lvSets.Items[selectedIndex + 1].Clone();
            lvSets.Items[selectedIndex] = listViewItem2;
            lvSets.Items[selectedIndex + 1] = listViewItem1;
            lvSets.Items[selectedIndex + 1].Selected = true;
            lvSets.Items[selectedIndex + 1].EnsureVisible();
        }

        private void btnEdit_Click(object sender, EventArgs e)

        {
            if (lvSets.SelectedIndices.Count <= 0)
                return;
            var flag = false;
            var uidOld = "";
            var selectedIndex1 = lvSets.SelectedIndices[0];
            var enhancementSets = DatabaseAPI.Database.EnhancementSets;
            var selectedIndex2 = lvSets.SelectedIndices[0];
            /*string setName = Regex.Replace(lvSets.SelectedItems[0].Text, @"( \(\w+\))", "");
            var setNameIndex = enhancementSets.FindIndex(x => x.DisplayName == setName);*/
            var iSet = enhancementSets[selectedIndex2];
            enhancementSets[selectedIndex2] = iSet;
            using var frmSetEditPvP = new frmSetEditPvPNEW(ref iSet);
            var num = (int) frmSetEditPvP.ShowDialog();
            if (frmSetEditPvP.DialogResult != DialogResult.OK)
                return;
            if (frmSetEditPvP.mySet.Uid != DatabaseAPI.Database.EnhancementSets[lvSets.SelectedIndices[0]].Uid)
            {
                flag = true;
                uidOld = DatabaseAPI.Database.EnhancementSets[lvSets.SelectedIndices[0]].Uid;
            }

            DatabaseAPI.Database.EnhancementSets[lvSets.SelectedIndices[0]] = new EnhancementSet(frmSetEditPvP.mySet);
            ImageUpdate();
            UpdateListItem(selectedIndex1);
            if (!flag)
                return;
            RenameIOSet(uidOld, frmSetEditPvP.mySet.Uid);
            DatabaseAPI.MatchEnhancementIDs();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var serializer = MyApplication.GetSerializer();
            DatabaseAPI.SaveEnhancementDb(serializer);
            Hide();
        }

        private void btnUp_Click(object sender, EventArgs e)

        {
            if (lvSets.SelectedIndices.Count <= 0)
                return;
            var selectedIndex = lvSets.SelectedIndices[0];
            if (selectedIndex < 1)
                return;
            EnhancementSet[] enhancementSetArray =
            {
                new EnhancementSet(DatabaseAPI.Database.EnhancementSets[selectedIndex]),
                new EnhancementSet(DatabaseAPI.Database.EnhancementSets[selectedIndex - 1])
            };
            DatabaseAPI.Database.EnhancementSets[selectedIndex - 1] = new EnhancementSet(enhancementSetArray[0]);
            DatabaseAPI.Database.EnhancementSets[selectedIndex] = new EnhancementSet(enhancementSetArray[1]);
            DatabaseAPI.MatchEnhancementIDs();
            var listViewItem1 = (ListViewItem) lvSets.Items[selectedIndex].Clone();
            var listViewItem2 = (ListViewItem) lvSets.Items[selectedIndex - 1].Clone();
            lvSets.Items[selectedIndex] = listViewItem2;
            lvSets.Items[selectedIndex - 1] = listViewItem1;
            lvSets.Items[selectedIndex - 1].Selected = true;
            lvSets.Items[selectedIndex - 1].EnsureVisible();
        }

        private void DisplayList()
        {
            lvSets.BeginUpdate();
            lvSets.Items.Clear();
            ImageUpdate();
            var num = DatabaseAPI.Database.EnhancementSets.Count - 1;
            for (var Index = 0; Index <= num; ++Index)
                AddListItem(Index);
            if (lvSets.Items.Count > 0)
            {
                lvSets.Items[0].Selected = true;
                lvSets.Items[0].EnsureVisible();
            }

            lvSets.EndUpdate();
        }

        private void FillImageList()
        {
            var imageSize1 = ilSets.ImageSize;
            var width1 = imageSize1.Width;
            imageSize1 = ilSets.ImageSize;
            var height1 = imageSize1.Height;
            using var extendedBitmap = new ExtendedBitmap(width1, height1);
            ilSets.Images.Clear();
            var num = DatabaseAPI.Database.EnhancementSets.Count - 1;
            for (var index = 0; index <= num; ++index)
                if (DatabaseAPI.Database.EnhancementSets[index].ImageIdx > -1)
                {
                    extendedBitmap.Graphics.Clear(Color.White);
                    var graphics = extendedBitmap.Graphics;
                    I9Gfx.DrawEnhancementSet(ref graphics, DatabaseAPI.Database.EnhancementSets[index].ImageIdx);
                    ilSets.Images.Add(extendedBitmap.Bitmap);
                }
                else
                {
                    var images = ilSets.Images;
                    var imageSize2 = ilSets.ImageSize;
                    var width2 = imageSize2.Width;
                    imageSize2 = ilSets.ImageSize;
                    var height2 = imageSize2.Height;
                    var bitmap = new Bitmap(width2, height2);
                    images.Add(bitmap);
                }
        }

        private void frmSetListingPvP_Load(object sender, EventArgs e)
        {
            lvSets
                .GetType()
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(lvSets, true, null);
            DisplayList();
        }

        private void ImageUpdate()
        {
            if (NoReload.Checked)
                return;
            I9Gfx.LoadSets();
            FillImageList();
        }

        [DebuggerStepThrough]
        private void lvSets_DoubleClick(object sender, EventArgs e)

        {
            btnEdit_Click(RuntimeHelpers.GetObjectValue(sender), e);
        }

        private void lvSets_SelectedIndexChanged(object sender, EventArgs e)

        {
        }

        private void NoReload_CheckedChanged(object sender, EventArgs e)

        {
            ImageUpdate();
        }

        private static void RenameIOSet(string uidOld, string uidNew)

        {
            var num = DatabaseAPI.Database.Enhancements.Length - 1;
            for (var index = 0; index <= num; ++index)
                if (DatabaseAPI.Database.Enhancements[index].UIDSet == uidOld)
                    DatabaseAPI.Database.Enhancements[index].UIDSet = uidNew;
        }

        private void UpdateListItem(int Index)
        {
            var strArray = new string[6];
            var enhancementSet = DatabaseAPI.Database.EnhancementSets[Index];
            strArray[0] = enhancementSet.DisplayName + " (" + enhancementSet.ShortName + ")";
            strArray[1] = Enum.GetName(enhancementSet.SetType.GetType(), enhancementSet.SetType);
            strArray[2] = Convert.ToString(enhancementSet.LevelMin + 1, CultureInfo.InvariantCulture);
            strArray[3] = Convert.ToString(enhancementSet.LevelMax + 1, CultureInfo.InvariantCulture);
            strArray[4] = Convert.ToString(enhancementSet.Enhancements.Length, CultureInfo.InvariantCulture);
            var num1 = 0;
            var num2 = enhancementSet.Bonus.Length - 1;
            for (var index = 0; index <= num2; ++index)
                if (enhancementSet.Bonus[index].Index.Length > 0)
                    ++num1;
            strArray[5] = Convert.ToString(num1);
            if (num1 > 5)
            {
                var num3 = strArray.Length - 1;
                for (var index = 0; index <= num3; ++index)
                    lvSets.Items[Index].SubItems[index].Text = strArray[index];
                lvSets.Items[Index].ImageIndex = Index;
                lvSets.Refresh();
            }
        }
    }
}