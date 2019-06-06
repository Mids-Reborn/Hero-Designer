﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Base.Master_Classes;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Hero_Designer
{

    public partial class frmTweakMatching : Form
    {

        // (get) Token: 0x0600138C RID: 5004 RVA: 0x000C821C File Offset: 0x000C641C
        // (set) Token: 0x0600138D RID: 5005 RVA: 0x000C8234 File Offset: 0x000C6434
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


        // (get) Token: 0x0600138E RID: 5006 RVA: 0x000C8290 File Offset: 0x000C6490
        // (set) Token: 0x0600138F RID: 5007 RVA: 0x000C82A8 File Offset: 0x000C64A8
        internal virtual Button btnDel
        {
            get
            {
                return this._btnDel;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.btnDel_Click);
                if (this._btnDel != null)
                {
                    this._btnDel.Click -= eventHandler;
                }
                this._btnDel = value;
                if (this._btnDel != null)
                {
                    this._btnDel.Click += eventHandler;
                }
            }
        }


        // (get) Token: 0x06001390 RID: 5008 RVA: 0x000C8304 File Offset: 0x000C6504
        // (set) Token: 0x06001391 RID: 5009 RVA: 0x000C831C File Offset: 0x000C651C
        internal virtual Button Button1
        {
            get
            {
                return this._Button1;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.Button1_Click);
                if (this._Button1 != null)
                {
                    this._Button1.Click -= eventHandler;
                }
                this._Button1 = value;
                if (this._Button1 != null)
                {
                    this._Button1.Click += eventHandler;
                }
            }
        }


        // (get) Token: 0x06001392 RID: 5010 RVA: 0x000C8378 File Offset: 0x000C6578
        // (set) Token: 0x06001393 RID: 5011 RVA: 0x000C8390 File Offset: 0x000C6590
        internal virtual Button Button2
        {
            get
            {
                return this._Button2;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.Button2_Click);
                if (this._Button2 != null)
                {
                    this._Button2.Click -= eventHandler;
                }
                this._Button2 = value;
                if (this._Button2 != null)
                {
                    this._Button2.Click += eventHandler;
                }
            }
        }


        // (get) Token: 0x06001394 RID: 5012 RVA: 0x000C83EC File Offset: 0x000C65EC
        // (set) Token: 0x06001395 RID: 5013 RVA: 0x000C8404 File Offset: 0x000C6604
        internal virtual ComboBox cbAT1
        {
            get
            {
                return this._cbAT1;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.cbAT1_SelectedIndexChanged);
                if (this._cbAT1 != null)
                {
                    this._cbAT1.SelectedIndexChanged -= eventHandler;
                }
                this._cbAT1 = value;
                if (this._cbAT1 != null)
                {
                    this._cbAT1.SelectedIndexChanged += eventHandler;
                }
            }
        }


        // (get) Token: 0x06001396 RID: 5014 RVA: 0x000C8460 File Offset: 0x000C6660
        // (set) Token: 0x06001397 RID: 5015 RVA: 0x000C8478 File Offset: 0x000C6678
        internal virtual ComboBox cbPower
        {
            get
            {
                return this._cbPower;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.cbPower_SelectedIndexChanged);
                if (this._cbPower != null)
                {
                    this._cbPower.SelectedIndexChanged -= eventHandler;
                }
                this._cbPower = value;
                if (this._cbPower != null)
                {
                    this._cbPower.SelectedIndexChanged += eventHandler;
                }
            }
        }


        // (get) Token: 0x06001398 RID: 5016 RVA: 0x000C84D4 File Offset: 0x000C66D4
        // (set) Token: 0x06001399 RID: 5017 RVA: 0x000C84EC File Offset: 0x000C66EC
        internal virtual ComboBox cbSet1
        {
            get
            {
                return this._cbSet1;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.cbSet1_SelectedIndexChanged);
                if (this._cbSet1 != null)
                {
                    this._cbSet1.SelectedIndexChanged -= eventHandler;
                }
                this._cbSet1 = value;
                if (this._cbSet1 != null)
                {
                    this._cbSet1.SelectedIndexChanged += eventHandler;
                }
            }
        }


        // (get) Token: 0x0600139A RID: 5018 RVA: 0x000C8548 File Offset: 0x000C6748
        // (set) Token: 0x0600139B RID: 5019 RVA: 0x000C8560 File Offset: 0x000C6760
        internal virtual ComboBox cbType1
        {
            get
            {
                return this._cbType1;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.cbType1_SelectedIndexChanged);
                if (this._cbType1 != null)
                {
                    this._cbType1.SelectedIndexChanged -= eventHandler;
                }
                this._cbType1 = value;
                if (this._cbType1 != null)
                {
                    this._cbType1.SelectedIndexChanged += eventHandler;
                }
            }
        }


        // (get) Token: 0x0600139C RID: 5020 RVA: 0x000C85BC File Offset: 0x000C67BC
        // (set) Token: 0x0600139D RID: 5021 RVA: 0x000C85D4 File Offset: 0x000C67D4
        internal virtual GroupBox GroupBox1
        {
            get
            {
                return this._GroupBox1;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._GroupBox1 = value;
            }
        }


        // (get) Token: 0x0600139E RID: 5022 RVA: 0x000C85E0 File Offset: 0x000C67E0
        // (set) Token: 0x0600139F RID: 5023 RVA: 0x000C85F8 File Offset: 0x000C67F8
        internal virtual GroupBox GroupBox2
        {
            get
            {
                return this._GroupBox2;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._GroupBox2 = value;
            }
        }


        // (get) Token: 0x060013A0 RID: 5024 RVA: 0x000C8604 File Offset: 0x000C6804
        // (set) Token: 0x060013A1 RID: 5025 RVA: 0x000C861C File Offset: 0x000C681C
        internal virtual ListBox lstTweaks
        {
            get
            {
                return this._lstTweaks;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.lstTweaks_SelectedIndexChanged);
                if (this._lstTweaks != null)
                {
                    this._lstTweaks.SelectedIndexChanged -= eventHandler;
                }
                this._lstTweaks = value;
                if (this._lstTweaks != null)
                {
                    this._lstTweaks.SelectedIndexChanged += eventHandler;
                }
            }
        }


        // (get) Token: 0x060013A2 RID: 5026 RVA: 0x000C8678 File Offset: 0x000C6878
        // (set) Token: 0x060013A3 RID: 5027 RVA: 0x000C8690 File Offset: 0x000C6890
        internal virtual TextBox txtAddActual
        {
            get
            {
                return this._txtAddActual;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._txtAddActual = value;
            }
        }


        // (get) Token: 0x060013A4 RID: 5028 RVA: 0x000C869C File Offset: 0x000C689C
        // (set) Token: 0x060013A5 RID: 5029 RVA: 0x000C86B4 File Offset: 0x000C68B4
        internal virtual TextBox txtAddOvr
        {
            get
            {
                return this._txtAddOvr;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._txtAddOvr = value;
            }
        }


        // (get) Token: 0x060013A6 RID: 5030 RVA: 0x000C86C0 File Offset: 0x000C68C0
        // (set) Token: 0x060013A7 RID: 5031 RVA: 0x000C86D8 File Offset: 0x000C68D8
        internal virtual TextBox txtOvr
        {
            get
            {
                return this._txtOvr;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._txtOvr = value;
            }
        }


        public frmTweakMatching()
        {
            base.Load += this.frmTweakMatching_Load;
            this.Loaded = false;
            this.InitializeComponent();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            int num = -1;
            int num2 = MidsContext.Config.CompOverride.Length - 1;
            for (int index = 0; index <= num2; index++)
            {
                Enums.CompOverride[] compOverride = MidsContext.Config.CompOverride;
                int index2 = index;
                if (compOverride[index2].Power == this.cbPower.SelectedItem.ToString() & compOverride[index2].Powerset == this.cbSet1.SelectedItem.ToString())
                {
                    num = index;
                }
            }
            if (num > -1)
            {
                Interaction.MsgBox("An override for that powerset/power already exists!", MsgBoxStyle.Information, "Can't have duplicates!");
                this.lstTweaks.SelectedIndex = num;
            }
            else
            {
                if (this.txtAddOvr.Text != this.txtAddActual.Text & this.txtAddOvr.Text != "")
                {
                    MidsContext.Config.CompOverride = (Enums.CompOverride[])Utils.CopyArray(MidsContext.Config.CompOverride, new Enums.CompOverride[MidsContext.Config.CompOverride.Length + 1]);
                    Enums.CompOverride[] compOverride2 = MidsContext.Config.CompOverride;
                    int index3 = MidsContext.Config.CompOverride.Length - 1;
                    compOverride2[index3].Power = Conversions.ToString(this.cbPower.SelectedItem);
                    compOverride2[index3].Powerset = Conversions.ToString(this.cbSet1.SelectedItem);
                    compOverride2[index3].Override = this.txtAddOvr.Text;
                }
                this.listOverrides();
                this.lstTweaks.SelectedIndex = this.lstTweaks.Items.Count - 1;
            }
        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.lstTweaks.SelectedIndex >= 0)
            {
                Enums.CompOverride[] compOverrideArray = new Enums.CompOverride[MidsContext.Config.CompOverride.Length - 2 + 1];
                int selectedIndex = this.lstTweaks.SelectedIndex;
                int index = 0;
                int num = MidsContext.Config.CompOverride.Length - 1;
                for (int index2 = 0; index2 <= num; index2++)
                {
                    if (index2 != selectedIndex)
                    {
                        Enums.CompOverride[] compOverride = MidsContext.Config.CompOverride;
                        int index3 = index2;
                        compOverrideArray[index].Override = compOverride[index3].Override;
                        compOverrideArray[index].Power = compOverride[index3].Power;
                        compOverrideArray[index].Powerset = compOverride[index3].Powerset;
                        index++;
                    }
                }
                MidsContext.Config.CompOverride = new Enums.CompOverride[compOverrideArray.Length - 1 + 1];
                int num2 = MidsContext.Config.CompOverride.Length - 1;
                for (int index2 = 0; index2 <= num2; index2++)
                {
                    Enums.CompOverride[] compOverride2 = MidsContext.Config.CompOverride;
                    int index4 = index2;
                    compOverride2[index4].Override = compOverrideArray[index2].Override;
                    compOverride2[index4].Power = compOverrideArray[index2].Power;
                    compOverride2[index4].Powerset = compOverrideArray[index2].Powerset;
                }
                this.listOverrides();
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            base.Hide();
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            if (this.lstTweaks.SelectedIndex >= 0)
            {
                MidsContext.Config.CompOverride[this.lstTweaks.SelectedIndex].Override = this.txtOvr.Text;
                int selectedIndex = this.lstTweaks.SelectedIndex;
                this.listOverrides();
                this.lstTweaks.SelectedIndex = selectedIndex;
            }
        }


        private void cbAT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Loaded)
            {
                this.List_Sets();
            }
        }


        private void cbPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbPower.SelectedIndex >= 0)
            {
                this.txtAddActual.Text = DatabaseAPI.Database.Powersets[this.getSetIndex()].Powers[this.cbPower.SelectedIndex].DescShort;
                this.txtAddOvr.Text = this.txtAddActual.Text;
            }
        }


        private void cbSet1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Loaded)
            {
                this.GetPowers();
            }
        }


        private void cbType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Loaded)
            {
                this.List_Sets();
            }
        }


        private void frmTweakMatching_Load(object sender, EventArgs e)
        {
            this.list_AT();
            this.list_Type();
            this.List_Sets();
            this.GetPowers();
            this.listOverrides();
            this.Loaded = true;
        }


        public int getAT()
        {
            return this.cbAT1.SelectedIndex;
        }


        public void GetPowers()
        {
            int index = 0;
            int[] numArray = new int[2];
            this.cbPower.BeginUpdate();
            this.cbPower.Items.Clear();
            numArray[0] = this.getSetIndex();
            int num = DatabaseAPI.Database.Powersets[numArray[index]].Powers.Length - 1;
            for (int index2 = 0; index2 <= num; index2++)
            {
                this.cbPower.Items.Add(DatabaseAPI.Database.Powersets[numArray[index]].Powers[index2].DisplayName);
            }
            this.cbPower.SelectedIndex = 0;
            this.cbPower.EndUpdate();
        }


        public int getSetIndex()
        {
            ComboBox cbSet = this.cbSet1;
            return DatabaseAPI.GetPowersetIndexes(this.getAT(), this.getSetType())[cbSet.SelectedIndex].nID;
        }


        public Enums.ePowerSetType getSetType()
        {
            Enums.ePowerSetType ePowerSetType;
            switch (this.cbType1.SelectedIndex)
            {
                case 0:
                    ePowerSetType = Enums.ePowerSetType.Primary;
                    break;
                case 1:
                    ePowerSetType = Enums.ePowerSetType.Secondary;
                    break;
                case 2:
                    ePowerSetType = Enums.ePowerSetType.Ancillary;
                    break;
                default:
                    ePowerSetType = Enums.ePowerSetType.Primary;
                    break;
            }
            return ePowerSetType;
        }


        public void list_AT()
        {
            this.cbAT1.BeginUpdate();
            this.cbAT1.Items.Clear();
            int num = DatabaseAPI.Database.Classes.Length - 1;
            for (int index = 0; index <= num; index++)
            {
                this.cbAT1.Items.Add(DatabaseAPI.Database.Classes[index].DisplayName);
            }
            this.cbAT1.SelectedIndex = 0;
            this.cbAT1.EndUpdate();
        }


        public void List_Sets()
        {
            Enums.ePowerSetType iSet = Enums.ePowerSetType.None;
            ComboBox cbSet = this.cbSet1;
            ComboBox cbType = this.cbType1;
            int selectedIndex = this.cbAT1.SelectedIndex;
            switch (cbType.SelectedIndex)
            {
                case 0:
                    iSet = Enums.ePowerSetType.Primary;
                    break;
                case 1:
                    iSet = Enums.ePowerSetType.Secondary;
                    break;
                case 2:
                    iSet = Enums.ePowerSetType.Ancillary;
                    break;
            }
            cbSet.BeginUpdate();
            cbSet.Items.Clear();
            IPowerset[] powersetIndexes = DatabaseAPI.GetPowersetIndexes(selectedIndex, iSet);
            int num = powersetIndexes.Length - 1;
            for (int index = 0; index <= num; index++)
            {
                cbSet.Items.Add(powersetIndexes[index].DisplayName);
            }
            if (cbSet.Items.Count > 0)
            {
                cbSet.SelectedIndex = 0;
            }
            cbSet.EndUpdate();
        }


        public void list_Type()
        {
            this.cbType1.BeginUpdate();
            this.cbType1.Items.Clear();
            this.cbType1.Items.Add("Primary");
            this.cbType1.Items.Add("Secondary");
            this.cbType1.Items.Add("Ancillary");
            this.cbType1.SelectedIndex = 0;
            this.cbType1.EndUpdate();
        }


        public void listOverrides()
        {
            this.lstTweaks.BeginUpdate();
            this.lstTweaks.Items.Clear();
            int num = MidsContext.Config.CompOverride.Length - 1;
            for (int index = 0; index <= num; index++)
            {
                this.lstTweaks.Items.Add(MidsContext.Config.CompOverride[index].Powerset + "." + MidsContext.Config.CompOverride[index].Power);
            }
            if (this.lstTweaks.Items.Count > 0)
            {
                this.lstTweaks.SelectedIndex = 0;
            }
            this.lstTweaks.EndUpdate();
        }


        private void lstTweaks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstTweaks.SelectedIndex >= 0)
            {
                this.txtOvr.Text = MidsContext.Config.CompOverride[this.lstTweaks.SelectedIndex].Override;
            }
        }


        [AccessedThroughProperty("btnAdd")]
        private Button _btnAdd;


        [AccessedThroughProperty("btnDel")]
        private Button _btnDel;


        [AccessedThroughProperty("Button1")]
        private Button _Button1;


        [AccessedThroughProperty("Button2")]
        private Button _Button2;


        [AccessedThroughProperty("cbAT1")]
        private ComboBox _cbAT1;


        [AccessedThroughProperty("cbPower")]
        private ComboBox _cbPower;


        [AccessedThroughProperty("cbSet1")]
        private ComboBox _cbSet1;


        [AccessedThroughProperty("cbType1")]
        private ComboBox _cbType1;


        [AccessedThroughProperty("GroupBox1")]
        private GroupBox _GroupBox1;


        [AccessedThroughProperty("GroupBox2")]
        private GroupBox _GroupBox2;


        [AccessedThroughProperty("lstTweaks")]
        private ListBox _lstTweaks;


        [AccessedThroughProperty("txtAddActual")]
        private TextBox _txtAddActual;


        [AccessedThroughProperty("txtAddOvr")]
        private TextBox _txtAddOvr;


        [AccessedThroughProperty("txtOvr")]
        private TextBox _txtOvr;


        protected bool Loaded;
    }
}
