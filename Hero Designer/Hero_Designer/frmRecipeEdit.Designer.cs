﻿namespace Hero_Designer
{

	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class frmRecipeEdit : global::System.Windows.Forms.Form
	{

		[global::System.Diagnostics.DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}


		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Hero_Designer.frmRecipeEdit));
			this.lvDPA = new global::System.Windows.Forms.ListView();
			this.ColumnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnHeader3 = new global::System.Windows.Forms.ColumnHeader();
			this.ColumnHeader4 = new global::System.Windows.Forms.ColumnHeader();
			this.btnImport = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.btnReGuess = new global::System.Windows.Forms.Button();
			this.GroupBox1 = new global::System.Windows.Forms.GroupBox();
			this.btnGuessCost = new global::System.Windows.Forms.Button();
			this.udSal4 = new global::System.Windows.Forms.NumericUpDown();
			this.Label14 = new global::System.Windows.Forms.Label();
			this.cbSal4 = new global::System.Windows.Forms.ComboBox();
			this.udSal3 = new global::System.Windows.Forms.NumericUpDown();
			this.Label13 = new global::System.Windows.Forms.Label();
			this.cbSal3 = new global::System.Windows.Forms.ComboBox();
			this.udSal2 = new global::System.Windows.Forms.NumericUpDown();
			this.Label12 = new global::System.Windows.Forms.Label();
			this.cbSal2 = new global::System.Windows.Forms.ComboBox();
			this.udSal1 = new global::System.Windows.Forms.NumericUpDown();
			this.Label11 = new global::System.Windows.Forms.Label();
			this.cbSal1 = new global::System.Windows.Forms.ComboBox();
			this.udSal0 = new global::System.Windows.Forms.NumericUpDown();
			this.Label10 = new global::System.Windows.Forms.Label();
			this.cbSal0 = new global::System.Windows.Forms.ComboBox();
			this.Label9 = new global::System.Windows.Forms.Label();
			this.udCraftM = new global::System.Windows.Forms.NumericUpDown();
			this.Label8 = new global::System.Windows.Forms.Label();
			this.udCraft = new global::System.Windows.Forms.NumericUpDown();
			this.Label7 = new global::System.Windows.Forms.Label();
			this.udBuyM = new global::System.Windows.Forms.NumericUpDown();
			this.Label6 = new global::System.Windows.Forms.Label();
			this.udBuy = new global::System.Windows.Forms.NumericUpDown();
			this.Label5 = new global::System.Windows.Forms.Label();
			this.udLevel = new global::System.Windows.Forms.NumericUpDown();
			this.lstItems = new global::System.Windows.Forms.ListBox();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.cbRarity = new global::System.Windows.Forms.ComboBox();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.txtRecipeName = new global::System.Windows.Forms.TextBox();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.cbEnh = new global::System.Windows.Forms.ComboBox();
			this.GroupBox2 = new global::System.Windows.Forms.GroupBox();
			this.btnI50 = new global::System.Windows.Forms.Button();
			this.btnI40 = new global::System.Windows.Forms.Button();
			this.btnI25 = new global::System.Windows.Forms.Button();
			this.btnI20 = new global::System.Windows.Forms.Button();
			this.btnIncrement = new global::System.Windows.Forms.Button();
			this.btnDown = new global::System.Windows.Forms.Button();
			this.btnUp = new global::System.Windows.Forms.Button();
			this.btnDel = new global::System.Windows.Forms.Button();
			this.btnAdd = new global::System.Windows.Forms.Button();
			this.lblEnh = new global::System.Windows.Forms.Label();
			this.txtExtern = new global::System.Windows.Forms.TextBox();
			this.Label15 = new global::System.Windows.Forms.Label();
			this.Label4 = new global::System.Windows.Forms.Label();
			this.btnRAdd = new global::System.Windows.Forms.Button();
			this.btnRDel = new global::System.Windows.Forms.Button();
			this.btnRUp = new global::System.Windows.Forms.Button();
			this.btnRDown = new global::System.Windows.Forms.Button();
			this.btnImportUpdate = new global::System.Windows.Forms.Button();
			this.btnRunSeq = new global::System.Windows.Forms.Button();
			this.GroupBox1.SuspendLayout();
			this.udSal4.BeginInit();
			this.udSal3.BeginInit();
			this.udSal2.BeginInit();
			this.udSal1.BeginInit();
			this.udSal0.BeginInit();
			this.udCraftM.BeginInit();
			this.udCraft.BeginInit();
			this.udBuyM.BeginInit();
			this.udBuy.BeginInit();
			this.udLevel.BeginInit();
			this.GroupBox2.SuspendLayout();
			base.SuspendLayout();
			this.lvDPA.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.ColumnHeader1,
				this.ColumnHeader2,
				this.ColumnHeader3,
				this.ColumnHeader4
			});
			this.lvDPA.FullRowSelect = true;
			this.lvDPA.HideSelection = false;
			global::System.Drawing.Point point = new global::System.Drawing.Point(12, 12);
			this.lvDPA.Location = point;
			this.lvDPA.MultiSelect = false;
			this.lvDPA.Name = "lvDPA";
			global::System.Drawing.Size size = new global::System.Drawing.Size(599, 273);
			this.lvDPA.Size = size;
			this.lvDPA.TabIndex = 0;
			this.lvDPA.UseCompatibleStateImageBehavior = false;
			this.lvDPA.View = global::System.Windows.Forms.View.Details;
			this.ColumnHeader1.Text = "Recipe";
			this.ColumnHeader1.Width = 226;
			this.ColumnHeader2.Text = "Enhancement";
			this.ColumnHeader2.Width = 183;
			this.ColumnHeader3.Text = "Rarity";
			this.ColumnHeader3.Width = 84;
			this.ColumnHeader4.Text = "Entries";
			point = new global::System.Drawing.Point(356, 491);
			this.btnImport.Location = point;
			this.btnImport.Name = "btnImport";
			size = new global::System.Drawing.Size(102, 24);
			this.btnImport.Size = size;
			this.btnImport.TabIndex = 6;
			this.btnImport.Text = "Import w/Clear";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			point = new global::System.Drawing.Point(12, 491);
			this.btnCancel.Location = point;
			this.btnCancel.Name = "btnCancel";
			size = new global::System.Drawing.Size(113, 24);
			this.btnCancel.Size = size;
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			point = new global::System.Drawing.Point(131, 491);
			this.btnOK.Location = point;
			this.btnOK.Name = "btnOK";
			size = new global::System.Drawing.Size(113, 24);
			this.btnOK.Size = size;
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "Save && Close";
			this.btnOK.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(464, 491);
			this.btnReGuess.Location = point;
			this.btnReGuess.Name = "btnReGuess";
			size = new global::System.Drawing.Size(147, 24);
			this.btnReGuess.Size = size;
			this.btnReGuess.TabIndex = 7;
			this.btnReGuess.Text = "Re-Guess all recipes";
			this.btnReGuess.UseVisualStyleBackColor = true;
			this.GroupBox1.Controls.Add(this.btnGuessCost);
			this.GroupBox1.Controls.Add(this.udSal4);
			this.GroupBox1.Controls.Add(this.Label14);
			this.GroupBox1.Controls.Add(this.cbSal4);
			this.GroupBox1.Controls.Add(this.udSal3);
			this.GroupBox1.Controls.Add(this.Label13);
			this.GroupBox1.Controls.Add(this.cbSal3);
			this.GroupBox1.Controls.Add(this.udSal2);
			this.GroupBox1.Controls.Add(this.Label12);
			this.GroupBox1.Controls.Add(this.cbSal2);
			this.GroupBox1.Controls.Add(this.udSal1);
			this.GroupBox1.Controls.Add(this.Label11);
			this.GroupBox1.Controls.Add(this.cbSal1);
			this.GroupBox1.Controls.Add(this.udSal0);
			this.GroupBox1.Controls.Add(this.Label10);
			this.GroupBox1.Controls.Add(this.cbSal0);
			this.GroupBox1.Controls.Add(this.Label9);
			this.GroupBox1.Controls.Add(this.udCraftM);
			this.GroupBox1.Controls.Add(this.Label8);
			this.GroupBox1.Controls.Add(this.udCraft);
			this.GroupBox1.Controls.Add(this.Label7);
			this.GroupBox1.Controls.Add(this.udBuyM);
			this.GroupBox1.Controls.Add(this.Label6);
			this.GroupBox1.Controls.Add(this.udBuy);
			this.GroupBox1.Controls.Add(this.Label5);
			this.GroupBox1.Controls.Add(this.udLevel);
			point = new global::System.Drawing.Point(12, 321);
			this.GroupBox1.Location = point;
			this.GroupBox1.Name = "GroupBox1";
			size = new global::System.Drawing.Size(599, 164);
			this.GroupBox1.Size = size;
			this.GroupBox1.TabIndex = 8;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Recipe Entry:";
			point = new global::System.Drawing.Point(185, 105);
			this.btnGuessCost.Location = point;
			this.btnGuessCost.Name = "btnGuessCost";
			size = new global::System.Drawing.Size(58, 20);
			this.btnGuessCost.Size = size;
			this.btnGuessCost.TabIndex = 36;
			this.btnGuessCost.Text = "Guess";
			this.btnGuessCost.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(523, 133);
			this.udSal4.Location = point;
			int[] array = new int[4];
			array[0] = 1024;
			decimal num = new decimal(array);
			this.udSal4.Maximum = num;
			this.udSal4.Name = "udSal4";
			size = new global::System.Drawing.Size(59, 20);
			this.udSal4.Size = size;
			this.udSal4.TabIndex = 350;
			this.udSal4.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			array = new int[4];
			array[0] = 1;
			num = new decimal(array);
			this.udSal4.Value = num;
			point = new global::System.Drawing.Point(225, 131);
			this.Label14.Location = point;
			this.Label14.Name = "Label14";
			size = new global::System.Drawing.Size(86, 22);
			this.Label14.Size = size;
			this.Label14.TabIndex = 34;
			this.Label14.Text = "Ingredient 5:";
			this.Label14.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.cbSal4.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.Append;
			this.cbSal4.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbSal4.FormattingEnabled = true;
			point = new global::System.Drawing.Point(317, 131);
			this.cbSal4.Location = point;
			this.cbSal4.Name = "cbSal4";
			size = new global::System.Drawing.Size(202, 22);
			this.cbSal4.Size = size;
			this.cbSal4.TabIndex = 33;
			point = new global::System.Drawing.Point(523, 105);
			this.udSal3.Location = point;
			array = new int[4];
			array[0] = 1024;
			num = new decimal(array);
			this.udSal3.Maximum = num;
			this.udSal3.Name = "udSal3";
			size = new global::System.Drawing.Size(59, 20);
			this.udSal3.Size = size;
			this.udSal3.TabIndex = 320;
			this.udSal3.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			array = new int[4];
			array[0] = 1;
			num = new decimal(array);
			this.udSal3.Value = num;
			point = new global::System.Drawing.Point(225, 103);
			this.Label13.Location = point;
			this.Label13.Name = "Label13";
			size = new global::System.Drawing.Size(86, 22);
			this.Label13.Size = size;
			this.Label13.TabIndex = 31;
			this.Label13.Text = "Ingredient 4:";
			this.Label13.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.cbSal3.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.Append;
			this.cbSal3.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbSal3.FormattingEnabled = true;
			point = new global::System.Drawing.Point(317, 103);
			this.cbSal3.Location = point;
			this.cbSal3.Name = "cbSal3";
			size = new global::System.Drawing.Size(202, 22);
			this.cbSal3.Size = size;
			this.cbSal3.TabIndex = 30;
			point = new global::System.Drawing.Point(523, 77);
			this.udSal2.Location = point;
			array = new int[4];
			array[0] = 1024;
			num = new decimal(array);
			this.udSal2.Maximum = num;
			this.udSal2.Name = "udSal2";
			size = new global::System.Drawing.Size(59, 20);
			this.udSal2.Size = size;
			this.udSal2.TabIndex = 290;
			this.udSal2.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			array = new int[4];
			array[0] = 1;
			num = new decimal(array);
			this.udSal2.Value = num;
			point = new global::System.Drawing.Point(225, 75);
			this.Label12.Location = point;
			this.Label12.Name = "Label12";
			size = new global::System.Drawing.Size(86, 22);
			this.Label12.Size = size;
			this.Label12.TabIndex = 28;
			this.Label12.Text = "Ingredient 3:";
			this.Label12.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.cbSal2.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.Append;
			this.cbSal2.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbSal2.FormattingEnabled = true;
			point = new global::System.Drawing.Point(317, 75);
			this.cbSal2.Location = point;
			this.cbSal2.Name = "cbSal2";
			size = new global::System.Drawing.Size(202, 22);
			this.cbSal2.Size = size;
			this.cbSal2.TabIndex = 27;
			point = new global::System.Drawing.Point(523, 49);
			this.udSal1.Location = point;
			array = new int[4];
			array[0] = 1024;
			num = new decimal(array);
			this.udSal1.Maximum = num;
			this.udSal1.Name = "udSal1";
			size = new global::System.Drawing.Size(59, 20);
			this.udSal1.Size = size;
			this.udSal1.TabIndex = 260;
			this.udSal1.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			array = new int[4];
			array[0] = 1;
			num = new decimal(array);
			this.udSal1.Value = num;
			point = new global::System.Drawing.Point(225, 47);
			this.Label11.Location = point;
			this.Label11.Name = "Label11";
			size = new global::System.Drawing.Size(86, 22);
			this.Label11.Size = size;
			this.Label11.TabIndex = 25;
			this.Label11.Text = "Ingredient 2:";
			this.Label11.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.cbSal1.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.Append;
			this.cbSal1.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbSal1.FormattingEnabled = true;
			point = new global::System.Drawing.Point(317, 47);
			this.cbSal1.Location = point;
			this.cbSal1.Name = "cbSal1";
			size = new global::System.Drawing.Size(202, 22);
			this.cbSal1.Size = size;
			this.cbSal1.TabIndex = 24;
			point = new global::System.Drawing.Point(523, 21);
			this.udSal0.Location = point;
			array = new int[4];
			array[0] = 1024;
			num = new decimal(array);
			this.udSal0.Maximum = num;
			this.udSal0.Name = "udSal0";
			size = new global::System.Drawing.Size(59, 20);
			this.udSal0.Size = size;
			this.udSal0.TabIndex = 230;
			this.udSal0.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			array = new int[4];
			array[0] = 1;
			num = new decimal(array);
			this.udSal0.Value = num;
			point = new global::System.Drawing.Point(225, 19);
			this.Label10.Location = point;
			this.Label10.Name = "Label10";
			size = new global::System.Drawing.Size(86, 22);
			this.Label10.Size = size;
			this.Label10.TabIndex = 22;
			this.Label10.Text = "Ingredient 1:";
			this.Label10.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			this.cbSal0.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.Append;
			this.cbSal0.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbSal0.FormattingEnabled = true;
			point = new global::System.Drawing.Point(317, 19);
			this.cbSal0.Location = point;
			this.cbSal0.Name = "cbSal0";
			size = new global::System.Drawing.Size(202, 22);
			this.cbSal0.Size = size;
			this.cbSal0.TabIndex = 21;
			point = new global::System.Drawing.Point(6, 133);
			this.Label9.Location = point;
			this.Label9.Name = "Label9";
			size = new global::System.Drawing.Size(86, 20);
			this.Label9.Size = size;
			this.Label9.TabIndex = 20;
			this.Label9.Text = "Craft Cost (M):";
			this.Label9.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			point = new global::System.Drawing.Point(98, 133);
			this.udCraftM.Location = point;
			array = new int[4];
			array[0] = 1000000;
			num = new decimal(array);
			this.udCraftM.Maximum = num;
			num = new decimal(new int[]
			{
				1,
				0,
				0,
				int.MinValue
			});
			this.udCraftM.Minimum = num;
			this.udCraftM.Name = "udCraftM";
			size = new global::System.Drawing.Size(112, 20);
			this.udCraftM.Size = size;
			this.udCraftM.TabIndex = 19;
			this.udCraftM.ThousandsSeparator = true;
			point = new global::System.Drawing.Point(6, 105);
			this.Label8.Location = point;
			this.Label8.Name = "Label8";
			size = new global::System.Drawing.Size(86, 20);
			this.Label8.Size = size;
			this.Label8.TabIndex = 18;
			this.Label8.Text = "Craft Cost:";
			this.Label8.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			point = new global::System.Drawing.Point(98, 105);
			this.udCraft.Location = point;
			array = new int[4];
			array[0] = 1000000;
			num = new decimal(array);
			this.udCraft.Maximum = num;
			num = new decimal(new int[]
			{
				1,
				0,
				0,
				int.MinValue
			});
			this.udCraft.Minimum = num;
			this.udCraft.Name = "udCraft";
			size = new global::System.Drawing.Size(81, 20);
			this.udCraft.Size = size;
			this.udCraft.TabIndex = 17;
			this.udCraft.ThousandsSeparator = true;
			point = new global::System.Drawing.Point(6, 77);
			this.Label7.Location = point;
			this.Label7.Name = "Label7";
			size = new global::System.Drawing.Size(86, 20);
			this.Label7.Size = size;
			this.Label7.TabIndex = 16;
			this.Label7.Text = "Buy Cost (M):";
			this.Label7.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			point = new global::System.Drawing.Point(98, 77);
			this.udBuyM.Location = point;
			array = new int[4];
			array[0] = 1000000;
			num = new decimal(array);
			this.udBuyM.Maximum = num;
			num = new decimal(new int[]
			{
				1,
				0,
				0,
				int.MinValue
			});
			this.udBuyM.Minimum = num;
			this.udBuyM.Name = "udBuyM";
			size = new global::System.Drawing.Size(112, 20);
			this.udBuyM.Size = size;
			this.udBuyM.TabIndex = 15;
			this.udBuyM.ThousandsSeparator = true;
			point = new global::System.Drawing.Point(6, 49);
			this.Label6.Location = point;
			this.Label6.Name = "Label6";
			size = new global::System.Drawing.Size(86, 20);
			this.Label6.Size = size;
			this.Label6.TabIndex = 14;
			this.Label6.Text = "Buy Cost:";
			this.Label6.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			point = new global::System.Drawing.Point(98, 49);
			this.udBuy.Location = point;
			array = new int[4];
			array[0] = 1000000;
			num = new decimal(array);
			this.udBuy.Maximum = num;
			num = new decimal(new int[]
			{
				1,
				0,
				0,
				int.MinValue
			});
			this.udBuy.Minimum = num;
			this.udBuy.Name = "udBuy";
			size = new global::System.Drawing.Size(112, 20);
			this.udBuy.Size = size;
			this.udBuy.TabIndex = 13;
			this.udBuy.ThousandsSeparator = true;
			point = new global::System.Drawing.Point(6, 21);
			this.Label5.Location = point;
			this.Label5.Name = "Label5";
			size = new global::System.Drawing.Size(86, 20);
			this.Label5.Size = size;
			this.Label5.TabIndex = 12;
			this.Label5.Text = "Level:";
			this.Label5.TextAlign = global::System.Drawing.ContentAlignment.MiddleRight;
			point = new global::System.Drawing.Point(98, 21);
			this.udLevel.Location = point;
			array = new int[4];
			array[0] = 53;
			num = new decimal(array);
			this.udLevel.Maximum = num;
			this.udLevel.Name = "udLevel";
			size = new global::System.Drawing.Size(70, 20);
			this.udLevel.Size = size;
			this.udLevel.TabIndex = 0;
			array = new int[4];
			array[0] = 1;
			num = new decimal(array);
			this.udLevel.Value = num;
			this.lstItems.FormattingEnabled = true;
			this.lstItems.ItemHeight = 14;
			point = new global::System.Drawing.Point(6, 251);
			this.lstItems.Location = point;
			this.lstItems.Name = "lstItems";
			size = new global::System.Drawing.Size(202, 172);
			this.lstItems.Size = size;
			this.lstItems.TabIndex = 0;
			point = new global::System.Drawing.Point(6, 104);
			this.Label3.Location = point;
			this.Label3.Name = "Label3";
			size = new global::System.Drawing.Size(86, 22);
			this.Label3.Size = size;
			this.Label3.TabIndex = 11;
			this.Label3.Text = "Rarity:";
			this.Label3.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.cbRarity.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbRarity.FormattingEnabled = true;
			point = new global::System.Drawing.Point(6, 129);
			this.cbRarity.Location = point;
			this.cbRarity.Name = "cbRarity";
			size = new global::System.Drawing.Size(158, 22);
			this.cbRarity.Size = size;
			this.cbRarity.TabIndex = 10;
			point = new global::System.Drawing.Point(6, 16);
			this.Label1.Location = point;
			this.Label1.Name = "Label1";
			size = new global::System.Drawing.Size(126, 20);
			this.Label1.Size = size;
			this.Label1.TabIndex = 13;
			this.Label1.Text = "Internal Name:";
			this.Label1.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			point = new global::System.Drawing.Point(6, 39);
			this.txtRecipeName.Location = point;
			this.txtRecipeName.Name = "txtRecipeName";
			size = new global::System.Drawing.Size(158, 20);
			this.txtRecipeName.Size = size;
			this.txtRecipeName.TabIndex = 12;
			point = new global::System.Drawing.Point(6, 154);
			this.Label2.Location = point;
			this.Label2.Name = "Label2";
			size = new global::System.Drawing.Size(86, 18);
			this.Label2.Size = size;
			this.Label2.TabIndex = 15;
			this.Label2.Text = "Enhancement:";
			this.Label2.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.cbEnh.AutoCompleteMode = global::System.Windows.Forms.AutoCompleteMode.Append;
			this.cbEnh.AutoCompleteSource = global::System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbEnh.FormattingEnabled = true;
			point = new global::System.Drawing.Point(6, 172);
			this.cbEnh.Location = point;
			this.cbEnh.Name = "cbEnh";
			size = new global::System.Drawing.Size(202, 22);
			this.cbEnh.Size = size;
			this.cbEnh.TabIndex = 14;
			this.GroupBox2.Controls.Add(this.btnI50);
			this.GroupBox2.Controls.Add(this.btnI40);
			this.GroupBox2.Controls.Add(this.btnI25);
			this.GroupBox2.Controls.Add(this.btnI20);
			this.GroupBox2.Controls.Add(this.btnIncrement);
			this.GroupBox2.Controls.Add(this.btnDown);
			this.GroupBox2.Controls.Add(this.btnUp);
			this.GroupBox2.Controls.Add(this.btnDel);
			this.GroupBox2.Controls.Add(this.btnAdd);
			this.GroupBox2.Controls.Add(this.lblEnh);
			this.GroupBox2.Controls.Add(this.txtExtern);
			this.GroupBox2.Controls.Add(this.Label15);
			this.GroupBox2.Controls.Add(this.Label4);
			this.GroupBox2.Controls.Add(this.lstItems);
			this.GroupBox2.Controls.Add(this.Label2);
			this.GroupBox2.Controls.Add(this.txtRecipeName);
			this.GroupBox2.Controls.Add(this.cbEnh);
			this.GroupBox2.Controls.Add(this.cbRarity);
			this.GroupBox2.Controls.Add(this.Label1);
			this.GroupBox2.Controls.Add(this.Label3);
			point = new global::System.Drawing.Point(617, 12);
			this.GroupBox2.Location = point;
			this.GroupBox2.Name = "GroupBox2";
			size = new global::System.Drawing.Size(214, 523);
			this.GroupBox2.Size = size;
			this.GroupBox2.TabIndex = 9;
			this.GroupBox2.TabStop = false;
			this.GroupBox2.Text = "Recipe:";
			point = new global::System.Drawing.Point(117, 489);
			this.btnI50.Location = point;
			this.btnI50.Name = "btnI50";
			size = new global::System.Drawing.Size(31, 24);
			this.btnI50.Size = size;
			this.btnI50.TabIndex = 28;
			this.btnI50.Text = "50";
			this.btnI50.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(80, 489);
			this.btnI40.Location = point;
			this.btnI40.Name = "btnI40";
			size = new global::System.Drawing.Size(31, 24);
			this.btnI40.Size = size;
			this.btnI40.TabIndex = 27;
			this.btnI40.Text = "40";
			this.btnI40.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(43, 489);
			this.btnI25.Location = point;
			this.btnI25.Name = "btnI25";
			size = new global::System.Drawing.Size(31, 24);
			this.btnI25.Size = size;
			this.btnI25.TabIndex = 26;
			this.btnI25.Text = "25";
			this.btnI25.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(6, 489);
			this.btnI20.Location = point;
			this.btnI20.Name = "btnI20";
			size = new global::System.Drawing.Size(31, 24);
			this.btnI20.Size = size;
			this.btnI20.TabIndex = 25;
			this.btnI20.Text = "20";
			this.btnI20.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(154, 489);
			this.btnIncrement.Location = point;
			this.btnIncrement.Name = "btnIncrement";
			size = new global::System.Drawing.Size(54, 24);
			this.btnIncrement.Size = size;
			this.btnIncrement.TabIndex = 24;
			this.btnIncrement.Text = "+ 1";
			this.btnIncrement.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(108, 459);
			this.btnDown.Location = point;
			this.btnDown.Name = "btnDown";
			size = new global::System.Drawing.Size(100, 24);
			this.btnDown.Size = size;
			this.btnDown.TabIndex = 23;
			this.btnDown.Text = "Down";
			this.btnDown.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(108, 429);
			this.btnUp.Location = point;
			this.btnUp.Name = "btnUp";
			size = new global::System.Drawing.Size(100, 24);
			this.btnUp.Size = size;
			this.btnUp.TabIndex = 22;
			this.btnUp.Text = "Up";
			this.btnUp.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(6, 459);
			this.btnDel.Location = point;
			this.btnDel.Name = "btnDel";
			size = new global::System.Drawing.Size(100, 24);
			this.btnDel.Size = size;
			this.btnDel.TabIndex = 21;
			this.btnDel.Text = "Delete";
			this.btnDel.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(6, 429);
			this.btnAdd.Location = point;
			this.btnAdd.Name = "btnAdd";
			size = new global::System.Drawing.Size(100, 24);
			this.btnAdd.Size = size;
			this.btnAdd.TabIndex = 20;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(6, 196);
			this.lblEnh.Location = point;
			this.lblEnh.Name = "lblEnh";
			size = new global::System.Drawing.Size(202, 40);
			this.lblEnh.Size = size;
			this.lblEnh.TabIndex = 17;
			this.lblEnh.Text = "EnhancementName";
			this.lblEnh.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			point = new global::System.Drawing.Point(6, 85);
			this.txtExtern.Location = point;
			this.txtExtern.Name = "txtExtern";
			size = new global::System.Drawing.Size(158, 20);
			this.txtExtern.Size = size;
			this.txtExtern.TabIndex = 18;
			point = new global::System.Drawing.Point(6, 62);
			this.Label15.Location = point;
			this.Label15.Name = "Label15";
			size = new global::System.Drawing.Size(86, 20);
			this.Label15.Size = size;
			this.Label15.TabIndex = 19;
			this.Label15.Text = "External Name:";
			this.Label15.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			point = new global::System.Drawing.Point(6, 226);
			this.Label4.Location = point;
			this.Label4.Name = "Label4";
			size = new global::System.Drawing.Size(86, 22);
			this.Label4.Size = size;
			this.Label4.TabIndex = 16;
			this.Label4.Text = "Recipe Entries:";
			this.Label4.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			point = new global::System.Drawing.Point(12, 291);
			this.btnRAdd.Location = point;
			this.btnRAdd.Name = "btnRAdd";
			size = new global::System.Drawing.Size(100, 24);
			this.btnRAdd.Size = size;
			this.btnRAdd.TabIndex = 21;
			this.btnRAdd.Text = "Add";
			this.btnRAdd.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(118, 291);
			this.btnRDel.Location = point;
			this.btnRDel.Name = "btnRDel";
			size = new global::System.Drawing.Size(100, 24);
			this.btnRDel.Size = size;
			this.btnRDel.TabIndex = 22;
			this.btnRDel.Text = "Delete";
			this.btnRDel.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(405, 291);
			this.btnRUp.Location = point;
			this.btnRUp.Name = "btnRUp";
			size = new global::System.Drawing.Size(100, 24);
			this.btnRUp.Size = size;
			this.btnRUp.TabIndex = 23;
			this.btnRUp.Text = "Up";
			this.btnRUp.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(511, 291);
			this.btnRDown.Location = point;
			this.btnRDown.Name = "btnRDown";
			size = new global::System.Drawing.Size(100, 24);
			this.btnRDown.Size = size;
			this.btnRDown.TabIndex = 24;
			this.btnRDown.Text = "Down";
			this.btnRDown.UseVisualStyleBackColor = true;
			point = new global::System.Drawing.Point(250, 491);
			this.btnImportUpdate.Location = point;
			this.btnImportUpdate.Name = "btnImportUpdate";
			size = new global::System.Drawing.Size(100, 24);
			this.btnImportUpdate.Size = size;
			this.btnImportUpdate.TabIndex = 25;
			this.btnImportUpdate.Text = "Import Update";
			this.btnImportUpdate.UseVisualStyleBackColor = true;
			this.btnRunSeq.Enabled = false;
			point = new global::System.Drawing.Point(250, 291);
			this.btnRunSeq.Location = point;
			this.btnRunSeq.Name = "btnRunSeq";
			size = new global::System.Drawing.Size(100, 24);
			this.btnRunSeq.Size = size;
			this.btnRunSeq.TabIndex = 26;
			this.btnRunSeq.Text = "Run Sequence";
			this.btnRunSeq.UseVisualStyleBackColor = true;
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			size = new global::System.Drawing.Size(843, 537);
			base.ClientSize = size;
			base.Controls.Add(this.btnRunSeq);
			base.Controls.Add(this.btnImportUpdate);
			base.Controls.Add(this.btnRDown);
			base.Controls.Add(this.btnRUp);
			base.Controls.Add(this.btnRDel);
			base.Controls.Add(this.btnRAdd);
			base.Controls.Add(this.GroupBox2);
			base.Controls.Add(this.GroupBox1);
			base.Controls.Add(this.btnReGuess);
			base.Controls.Add(this.btnImport);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.lvDPA);
			this.Font = new global::System.Drawing.Font("Arial", 11f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Pixel, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmRecipeEdit";
			base.ShowInTaskbar = false;
			this.Text = "Recipe Editor";
			this.GroupBox1.ResumeLayout(false);
			this.udSal4.EndInit();
			this.udSal3.EndInit();
			this.udSal2.EndInit();
			this.udSal1.EndInit();
			this.udSal0.EndInit();
			this.udCraftM.EndInit();
			this.udCraft.EndInit();
			this.udBuyM.EndInit();
			this.udBuy.EndInit();
			this.udLevel.EndInit();
			this.GroupBox2.ResumeLayout(false);
			this.GroupBox2.PerformLayout();
			base.ResumeLayout(false);
		}


		private global::System.ComponentModel.IContainer components;
	}
}
