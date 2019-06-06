﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Hero_Designer
{


    public partial class frmSalvageEdit : Form
    {

        // (get) Token: 0x0600119E RID: 4510 RVA: 0x000B0F80 File Offset: 0x000AF180
        // (set) Token: 0x0600119F RID: 4511 RVA: 0x000B0F98 File Offset: 0x000AF198
        internal virtual Button btnAdd
        {
            get
            {
                return this._btnAdd;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.btnAdd_Click);
                if (this._btnAdd != null)
                {
                    this._btnAdd.Click -= eventHandler;
                }
                this._btnAdd = value;
                if (this._btnAdd != null)
                {
                    this._btnAdd.Click += eventHandler;
                }
            }
        }


        // (get) Token: 0x060011A0 RID: 4512 RVA: 0x000B0FF4 File Offset: 0x000AF1F4
        // (set) Token: 0x060011A1 RID: 4513 RVA: 0x000B100C File Offset: 0x000AF20C
        internal virtual Button btnCancel
        {
            get
            {
                return this._btnCancel;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.btnCancel_Click);
                if (this._btnCancel != null)
                {
                    this._btnCancel.Click -= eventHandler;
                }
                this._btnCancel = value;
                if (this._btnCancel != null)
                {
                    this._btnCancel.Click += eventHandler;
                }
            }
        }


        // (get) Token: 0x060011A2 RID: 4514 RVA: 0x000B1068 File Offset: 0x000AF268
        // (set) Token: 0x060011A3 RID: 4515 RVA: 0x000B1080 File Offset: 0x000AF280
        internal virtual Button btnDelete
        {
            get
            {
                return this._btnDelete;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.btnDelete_Click);
                if (this._btnDelete != null)
                {
                    this._btnDelete.Click -= eventHandler;
                }
                this._btnDelete = value;
                if (this._btnDelete != null)
                {
                    this._btnDelete.Click += eventHandler;
                }
            }
        }


        // (get) Token: 0x060011A4 RID: 4516 RVA: 0x000B10DC File Offset: 0x000AF2DC
        // (set) Token: 0x060011A5 RID: 4517 RVA: 0x000B10F4 File Offset: 0x000AF2F4
        internal virtual Button btnImport
        {
            get
            {
                return this._btnImport;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.btnImport_Click);
                if (this._btnImport != null)
                {
                    this._btnImport.Click -= eventHandler;
                }
                this._btnImport = value;
                if (this._btnImport != null)
                {
                    this._btnImport.Click += eventHandler;
                }
            }
        }


        // (get) Token: 0x060011A6 RID: 4518 RVA: 0x000B1150 File Offset: 0x000AF350
        // (set) Token: 0x060011A7 RID: 4519 RVA: 0x000B1168 File Offset: 0x000AF368
        internal virtual Button btnOK
        {
            get
            {
                return this._btnOK;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.btnOK_Click);
                if (this._btnOK != null)
                {
                    this._btnOK.Click -= eventHandler;
                }
                this._btnOK = value;
                if (this._btnOK != null)
                {
                    this._btnOK.Click += eventHandler;
                }
            }
        }


        // (get) Token: 0x060011A8 RID: 4520 RVA: 0x000B11C4 File Offset: 0x000AF3C4
        // (set) Token: 0x060011A9 RID: 4521 RVA: 0x000B11DC File Offset: 0x000AF3DC
        internal virtual ComboBox cbLevel
        {
            get
            {
                return this._cbLevel;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.cbLevel_SelectedIndexChanged);
                if (this._cbLevel != null)
                {
                    this._cbLevel.SelectedIndexChanged -= eventHandler;
                }
                this._cbLevel = value;
                if (this._cbLevel != null)
                {
                    this._cbLevel.SelectedIndexChanged += eventHandler;
                }
            }
        }


        // (get) Token: 0x060011AA RID: 4522 RVA: 0x000B1238 File Offset: 0x000AF438
        // (set) Token: 0x060011AB RID: 4523 RVA: 0x000B1250 File Offset: 0x000AF450
        internal virtual ComboBox cbOrigin
        {
            get
            {
                return this._cbOrigin;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.cbOrigin_SelectedIndexChanged);
                if (this._cbOrigin != null)
                {
                    this._cbOrigin.SelectedIndexChanged -= eventHandler;
                }
                this._cbOrigin = value;
                if (this._cbOrigin != null)
                {
                    this._cbOrigin.SelectedIndexChanged += eventHandler;
                }
            }
        }


        // (get) Token: 0x060011AC RID: 4524 RVA: 0x000B12AC File Offset: 0x000AF4AC
        // (set) Token: 0x060011AD RID: 4525 RVA: 0x000B12C4 File Offset: 0x000AF4C4
        internal virtual ComboBox cbRarity
        {
            get
            {
                return this._cbRarity;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.cbRarity_SelectedIndexChanged);
                if (this._cbRarity != null)
                {
                    this._cbRarity.SelectedIndexChanged -= eventHandler;
                }
                this._cbRarity = value;
                if (this._cbRarity != null)
                {
                    this._cbRarity.SelectedIndexChanged += eventHandler;
                }
            }
        }


        // (get) Token: 0x060011AE RID: 4526 RVA: 0x000B1320 File Offset: 0x000AF520
        // (set) Token: 0x060011AF RID: 4527 RVA: 0x000B1338 File Offset: 0x000AF538
        internal virtual ColumnHeader ColumnHeader1
        {
            get
            {
                return this._ColumnHeader1;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._ColumnHeader1 = value;
            }
        }


        // (get) Token: 0x060011B0 RID: 4528 RVA: 0x000B1344 File Offset: 0x000AF544
        // (set) Token: 0x060011B1 RID: 4529 RVA: 0x000B135C File Offset: 0x000AF55C
        internal virtual ColumnHeader ColumnHeader2
        {
            get
            {
                return this._ColumnHeader2;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._ColumnHeader2 = value;
            }
        }


        // (get) Token: 0x060011B2 RID: 4530 RVA: 0x000B1368 File Offset: 0x000AF568
        // (set) Token: 0x060011B3 RID: 4531 RVA: 0x000B1380 File Offset: 0x000AF580
        internal virtual ColumnHeader ColumnHeader3
        {
            get
            {
                return this._ColumnHeader3;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._ColumnHeader3 = value;
            }
        }


        // (get) Token: 0x060011B4 RID: 4532 RVA: 0x000B138C File Offset: 0x000AF58C
        // (set) Token: 0x060011B5 RID: 4533 RVA: 0x000B13A4 File Offset: 0x000AF5A4
        internal virtual ColumnHeader ColumnHeader4
        {
            get
            {
                return this._ColumnHeader4;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._ColumnHeader4 = value;
            }
        }


        // (get) Token: 0x060011B6 RID: 4534 RVA: 0x000B13B0 File Offset: 0x000AF5B0
        // (set) Token: 0x060011B7 RID: 4535 RVA: 0x000B13C8 File Offset: 0x000AF5C8
        internal virtual Label Label1
        {
            get
            {
                return this._Label1;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._Label1 = value;
            }
        }


        // (get) Token: 0x060011B8 RID: 4536 RVA: 0x000B13D4 File Offset: 0x000AF5D4
        // (set) Token: 0x060011B9 RID: 4537 RVA: 0x000B13EC File Offset: 0x000AF5EC
        internal virtual Label Label2
        {
            get
            {
                return this._Label2;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._Label2 = value;
            }
        }


        // (get) Token: 0x060011BA RID: 4538 RVA: 0x000B13F8 File Offset: 0x000AF5F8
        // (set) Token: 0x060011BB RID: 4539 RVA: 0x000B1410 File Offset: 0x000AF610
        internal virtual Label Label3
        {
            get
            {
                return this._Label3;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._Label3 = value;
            }
        }


        // (get) Token: 0x060011BC RID: 4540 RVA: 0x000B141C File Offset: 0x000AF61C
        // (set) Token: 0x060011BD RID: 4541 RVA: 0x000B1434 File Offset: 0x000AF634
        internal virtual Label Label4
        {
            get
            {
                return this._Label4;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._Label4 = value;
            }
        }


        // (get) Token: 0x060011BE RID: 4542 RVA: 0x000B1440 File Offset: 0x000AF640
        // (set) Token: 0x060011BF RID: 4543 RVA: 0x000B1458 File Offset: 0x000AF658
        internal virtual Label Label5
        {
            get
            {
                return this._Label5;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._Label5 = value;
            }
        }


        // (get) Token: 0x060011C0 RID: 4544 RVA: 0x000B1464 File Offset: 0x000AF664
        // (set) Token: 0x060011C1 RID: 4545 RVA: 0x000B147C File Offset: 0x000AF67C
        internal virtual ListView lvSalvage
        {
            get
            {
                return this._lvSalvage;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.lvSalvage_SelectedIndexChanged);
                if (this._lvSalvage != null)
                {
                    this._lvSalvage.SelectedIndexChanged -= eventHandler;
                }
                this._lvSalvage = value;
                if (this._lvSalvage != null)
                {
                    this._lvSalvage.SelectedIndexChanged += eventHandler;
                }
            }
        }


        // (get) Token: 0x060011C2 RID: 4546 RVA: 0x000B14D8 File Offset: 0x000AF6D8
        // (set) Token: 0x060011C3 RID: 4547 RVA: 0x000B14F0 File Offset: 0x000AF6F0
        internal virtual TextBox txtExternal
        {
            get
            {
                return this._txtExternal;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.txtExternal_TextChanged);
                if (this._txtExternal != null)
                {
                    this._txtExternal.TextChanged -= eventHandler;
                }
                this._txtExternal = value;
                if (this._txtExternal != null)
                {
                    this._txtExternal.TextChanged += eventHandler;
                }
            }
        }


        // (get) Token: 0x060011C4 RID: 4548 RVA: 0x000B154C File Offset: 0x000AF74C
        // (set) Token: 0x060011C5 RID: 4549 RVA: 0x000B1564 File Offset: 0x000AF764
        internal virtual TextBox txtInternal
        {
            get
            {
                return this._txtInternal;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.txtInternal_TextChanged);
                if (this._txtInternal != null)
                {
                    this._txtInternal.TextChanged -= eventHandler;
                }
                this._txtInternal = value;
                if (this._txtInternal != null)
                {
                    this._txtInternal.TextChanged += eventHandler;
                }
            }
        }


        public frmSalvageEdit()
        {
            base.Load += this.frmSalvageEdit_Load;
            this.Updating = true;
            this.InitializeComponent();
        }


        protected void AddListItem(int Index)
        {
            string[] items = new string[4];
            if (!(Index > DatabaseAPI.Database.Salvage.Length - 1 | Index < 0))
            {
                items[0] = DatabaseAPI.Database.Salvage[Index].ExternalName;
                items[1] = Enum.GetName(DatabaseAPI.Database.Salvage[Index].Origin.GetType(), DatabaseAPI.Database.Salvage[Index].Origin);
                items[2] = Enum.GetName(DatabaseAPI.Database.Salvage[Index].Rarity.GetType(), DatabaseAPI.Database.Salvage[Index].Rarity);
                items[3] = Conversions.ToString(DatabaseAPI.Database.Salvage[Index].LevelMin + 1) + " - " + Conversions.ToString(DatabaseAPI.Database.Salvage[Index].LevelMax + 1);
                this.lvSalvage.Items.Add(new ListViewItem(items));
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            IDatabase database = DatabaseAPI.Database;
            Salvage[] salvageArray = (Salvage[])Utils.CopyArray(database.Salvage, new Salvage[DatabaseAPI.Database.Salvage.Length + 1]);
            database.Salvage = salvageArray;
            DatabaseAPI.Database.Salvage[DatabaseAPI.Database.Salvage.Length - 1] = new Salvage();
            this.AddListItem(DatabaseAPI.Database.Salvage.Length - 1);
            this.lvSalvage.Items[this.lvSalvage.Items.Count - 1].Selected = true;
            this.lvSalvage.Items[this.lvSalvage.Items.Count - 1].EnsureVisible();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            DatabaseAPI.LoadSalvage();
            base.Close();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.lvSalvage.SelectedItems.Count >= 1 && !this.Updating)
            {
                int selectedIndex = this.lvSalvage.SelectedIndices[0];
                Salvage[] salvageArray = new Salvage[DatabaseAPI.Database.Salvage.Length - 2 + 1];
                int index = -1;
                int num = DatabaseAPI.Database.Salvage.Length - 1;
                for (int index2 = 0; index2 <= num; index2++)
                {
                    if (index2 != selectedIndex)
                    {
                        index++;
                        salvageArray[index] = new Salvage(ref DatabaseAPI.Database.Salvage[index2]);
                    }
                }
                DatabaseAPI.Database.Salvage = new Salvage[salvageArray.Length - 1 + 1];
                int num2 = DatabaseAPI.Database.Salvage.Length - 1;
                for (int index2 = 0; index2 <= num2; index2++)
                {
                    DatabaseAPI.Database.Salvage[index2] = new Salvage(ref salvageArray[index2]);
                }
                this.FillList();
                if (this.lvSalvage.Items.Count > selectedIndex)
                {
                    this.lvSalvage.Items[selectedIndex].Selected = true;
                }
                else if (this.lvSalvage.Items.Count > selectedIndex - 1)
                {
                    this.lvSalvage.Items[selectedIndex - 1].Selected = true;
                }
                else if (this.lvSalvage.Items.Count > 0)
                {
                    this.lvSalvage.Items[0].Selected = true;
                }
            }
        }


        private void btnImport_Click(object sender, EventArgs e)
        {
            char[] chArray = new char[]
            {
                '\r'
            };
            string[] strArray = Clipboard.GetDataObject().GetData("System.String", true).ToString().Split(chArray);
            chArray[0] = '\t';
            Salvage[] salvageArray = new Salvage[0];
            DatabaseAPI.Database.Salvage = salvageArray;
            int num = strArray.Length - 1;
            for (int index = 0; index <= num; index++)
            {
                string[] strArray2 = strArray[index].Split(chArray);
                if (strArray2.Length > 7)
                {
                    IDatabase database = DatabaseAPI.Database;
                    salvageArray = (Salvage[])Utils.CopyArray(database.Salvage, new Salvage[DatabaseAPI.Database.Salvage.Length + 1]);
                    database.Salvage = salvageArray;
                    DatabaseAPI.Database.Salvage[DatabaseAPI.Database.Salvage.Length - 1] = new Salvage();
                    Salvage salvage = DatabaseAPI.Database.Salvage[DatabaseAPI.Database.Salvage.Length - 1];
                    if (!strArray2[0].StartsWith("S") & strArray2[0].Length > 2)
                    {
                        strArray2[0] = strArray2[0].Substring(1);
                    }
                    salvage.InternalName = strArray2[0];
                    salvage.ExternalName = strArray2[1];
                    if (strArray2[10].StartsWith("10"))
                    {
                        salvage.LevelMin = 9;
                        salvage.LevelMax = 24;
                    }
                    else if (strArray2[10].StartsWith("26"))
                    {
                        salvage.LevelMin = 25;
                        salvage.LevelMax = 39;
                    }
                    else
                    {
                        salvage.LevelMin = 40;
                        salvage.LevelMax = 52;
                    }
                    if (strArray2[9].IndexOf("Magic") > -1)
                    {
                        salvage.Origin = Salvage.SalvageOrigin.Magic;
                    }
                    else
                    {
                        salvage.Origin = Salvage.SalvageOrigin.Tech;
                    }
                    salvage.Rarity = (Recipe.RecipeRarity)Math.Round(Conversion.Val(strArray2[6]) - 1.0);
                }
            }
            this.FillList();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            DatabaseAPI.SaveSalvage();
            base.Close();
        }


        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvSalvage.SelectedItems.Count >= 1 && !this.Updating)
            {
                int selectedIndex = this.lvSalvage.SelectedIndices[0];
                if (this.cbLevel.SelectedIndex == 0)
                {
                    DatabaseAPI.Database.Salvage[selectedIndex].LevelMin = 9;
                    DatabaseAPI.Database.Salvage[selectedIndex].LevelMax = 24;
                }
                else if (this.cbLevel.SelectedIndex == 1)
                {
                    DatabaseAPI.Database.Salvage[selectedIndex].LevelMin = 25;
                    DatabaseAPI.Database.Salvage[selectedIndex].LevelMax = 39;
                }
                else
                {
                    DatabaseAPI.Database.Salvage[selectedIndex].LevelMin = 40;
                    DatabaseAPI.Database.Salvage[selectedIndex].LevelMax = 52;
                }
                this.UpdateListItem(selectedIndex);
            }
        }


        private void cbOrigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvSalvage.SelectedItems.Count >= 1 && !this.Updating)
            {
                int selectedIndex = this.lvSalvage.SelectedIndices[0];
                DatabaseAPI.Database.Salvage[selectedIndex].Origin = (Salvage.SalvageOrigin)this.cbOrigin.SelectedIndex;
                this.UpdateListItem(selectedIndex);
            }
        }


        private void cbRarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvSalvage.SelectedItems.Count >= 1 && !this.Updating)
            {
                int selectedIndex = this.lvSalvage.SelectedIndices[0];
                DatabaseAPI.Database.Salvage[selectedIndex].Rarity = (Recipe.RecipeRarity)this.cbRarity.SelectedIndex;
                this.UpdateListItem(selectedIndex);
            }
        }


        protected void DisplayItem(int Index)
        {
            if (!(Index > DatabaseAPI.Database.Salvage.Length - 1 | Index < 0))
            {
                this.Updating = true;
                this.cbRarity.SelectedIndex = (int)DatabaseAPI.Database.Salvage[Index].Rarity;
                this.cbOrigin.SelectedIndex = (int)DatabaseAPI.Database.Salvage[Index].Origin;
                int levelMin = DatabaseAPI.Database.Salvage[Index].LevelMin;
                if (levelMin < 25)
                {
                    this.cbLevel.SelectedIndex = 0;
                }
                else if (levelMin < 40)
                {
                    this.cbLevel.SelectedIndex = 1;
                }
                else if (levelMin < 53)
                {
                    this.cbLevel.SelectedIndex = 2;
                }
                this.txtExternal.Text = DatabaseAPI.Database.Salvage[Index].ExternalName;
                this.txtInternal.Text = DatabaseAPI.Database.Salvage[Index].InternalName;
                this.Updating = false;
            }
        }


        protected void FillList()
        {
            this.lvSalvage.BeginUpdate();
            this.lvSalvage.Items.Clear();
            int num = DatabaseAPI.Database.Salvage.Length - 1;
            for (int Index = 0; Index <= num; Index++)
            {
                this.AddListItem(Index);
            }
            this.lvSalvage.EndUpdate();
        }


        private void frmSalvageEdit_Load(object sender, EventArgs e)
        {
            Salvage.SalvageOrigin salvageOrigin = Salvage.SalvageOrigin.Tech;
            Recipe.RecipeRarity recipeRarity = Recipe.RecipeRarity.Common;
            this.FillList();
            this.cbRarity.Items.AddRange(Enum.GetNames(recipeRarity.GetType()));
            this.cbOrigin.Items.AddRange(Enum.GetNames(salvageOrigin.GetType()));
            this.cbLevel.Items.Add("10 - 25");
            this.cbLevel.Items.Add("26 - 40");
            this.cbLevel.Items.Add("41 - 53");
            this.Updating = false;
            if (this.lvSalvage.Items.Count > 0)
            {
                this.lvSalvage.Items[0].Selected = true;
            }
        }


        private void lvSalvage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvSalvage.SelectedIndices.Count > 0)
            {
                this.DisplayItem(this.lvSalvage.SelectedIndices[0]);
            }
        }


        private void txtExternal_TextChanged(object sender, EventArgs e)
        {
            if (this.lvSalvage.SelectedItems.Count >= 1 && !this.Updating)
            {
                int selectedIndex = this.lvSalvage.SelectedIndices[0];
                DatabaseAPI.Database.Salvage[selectedIndex].ExternalName = this.txtExternal.Text;
                this.UpdateListItem(selectedIndex);
            }
        }


        private void txtInternal_TextChanged(object sender, EventArgs e)
        {
            if (this.lvSalvage.SelectedItems.Count >= 1 && !this.Updating)
            {
                int selectedIndex = this.lvSalvage.SelectedIndices[0];
                DatabaseAPI.Database.Salvage[selectedIndex].ExternalName = this.txtInternal.Text;
                this.UpdateListItem(selectedIndex);
            }
        }


        protected void UpdateListItem(int Index)
        {
            if (!(Index > DatabaseAPI.Database.Salvage.Length - 1 | Index < 0))
            {
                this.lvSalvage.Items[Index].SubItems[0].Text = DatabaseAPI.Database.Salvage[Index].ExternalName;
                this.lvSalvage.Items[Index].SubItems[1].Text = Enum.GetName(DatabaseAPI.Database.Salvage[Index].Origin.GetType(), DatabaseAPI.Database.Salvage[Index].Origin);
                this.lvSalvage.Items[Index].SubItems[2].Text = Enum.GetName(DatabaseAPI.Database.Salvage[Index].Rarity.GetType(), DatabaseAPI.Database.Salvage[Index].Rarity);
                this.lvSalvage.Items[Index].SubItems[3].Text = Conversions.ToString(DatabaseAPI.Database.Salvage[Index].LevelMin + 1) + " - " + Conversions.ToString(DatabaseAPI.Database.Salvage[Index].LevelMax + 1);
            }
        }


        [AccessedThroughProperty("btnAdd")]
        private Button _btnAdd;


        [AccessedThroughProperty("btnCancel")]
        private Button _btnCancel;


        [AccessedThroughProperty("btnDelete")]
        private Button _btnDelete;


        [AccessedThroughProperty("btnImport")]
        private Button _btnImport;


        [AccessedThroughProperty("btnOK")]
        private Button _btnOK;


        [AccessedThroughProperty("cbLevel")]
        private ComboBox _cbLevel;


        [AccessedThroughProperty("cbOrigin")]
        private ComboBox _cbOrigin;


        [AccessedThroughProperty("cbRarity")]
        private ComboBox _cbRarity;


        [AccessedThroughProperty("ColumnHeader1")]
        private ColumnHeader _ColumnHeader1;


        [AccessedThroughProperty("ColumnHeader2")]
        private ColumnHeader _ColumnHeader2;


        [AccessedThroughProperty("ColumnHeader3")]
        private ColumnHeader _ColumnHeader3;


        [AccessedThroughProperty("ColumnHeader4")]
        private ColumnHeader _ColumnHeader4;


        [AccessedThroughProperty("Label1")]
        private Label _Label1;


        [AccessedThroughProperty("Label2")]
        private Label _Label2;


        [AccessedThroughProperty("Label3")]
        private Label _Label3;


        [AccessedThroughProperty("Label4")]
        private Label _Label4;


        [AccessedThroughProperty("Label5")]
        private Label _Label5;


        [AccessedThroughProperty("lvSalvage")]
        private ListView _lvSalvage;


        [AccessedThroughProperty("txtExternal")]
        private TextBox _txtExternal;


        [AccessedThroughProperty("txtInternal")]
        private TextBox _txtInternal;


        public bool Updating;
    }
}
