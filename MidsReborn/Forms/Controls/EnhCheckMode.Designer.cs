﻿using Mids_Reborn.Controls;
using Mids_Reborn.Core.Base.Master_Classes;

namespace Mids_Reborn.Forms.Controls
{
    partial class EnhCheckMode
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Mids_Reborn.Forms.Controls.ImageButtonEx.BaseImages baseImages1 = new Mids_Reborn.Forms.Controls.ImageButtonEx.BaseImages();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnhCheckMode));
            Mids_Reborn.Forms.Controls.ImageButtonEx.AltImages altImages1 = new Mids_Reborn.Forms.Controls.ImageButtonEx.AltImages();
            Mids_Reborn.Forms.Controls.ImageButtonEx.Outline outline1 = new Mids_Reborn.Forms.Controls.ImageButtonEx.Outline();
            Mids_Reborn.Forms.Controls.ImageButtonEx.StateText stateText1 = new Mids_Reborn.Forms.Controls.ImageButtonEx.StateText();
            this.pSalvageSummary = new System.Windows.Forms.Panel();
            this.imageButtonEx1 = new Mids_Reborn.Forms.Controls.ImageButtonEx();
            this.lblCatalysts = new System.Windows.Forms.Label();
            this.lblBoosters = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblEnhObtained = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pSalvageSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pSalvageSummary
            // 
            this.pSalvageSummary.BackColor = System.Drawing.Color.Transparent;
            this.pSalvageSummary.Controls.Add(this.imageButtonEx1);
            this.pSalvageSummary.Controls.Add(this.lblCatalysts);
            this.pSalvageSummary.Controls.Add(this.lblBoosters);
            this.pSalvageSummary.Controls.Add(this.pictureBox3);
            this.pSalvageSummary.Controls.Add(this.pictureBox2);
            this.pSalvageSummary.Controls.Add(this.lblEnhObtained);
            this.pSalvageSummary.Controls.Add(this.pictureBox1);
            this.pSalvageSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSalvageSummary.Location = new System.Drawing.Point(0, 0);
            this.pSalvageSummary.Margin = new System.Windows.Forms.Padding(0);
            this.pSalvageSummary.Name = "pSalvageSummary";
            this.pSalvageSummary.Size = new System.Drawing.Size(434, 35);
            this.pSalvageSummary.TabIndex = 20;
            // 
            // imageButtonEx1
            // 
            this.imageButtonEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageButtonEx1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            baseImages1.Background = ((System.Drawing.Image)(resources.GetObject("baseImages1.Background")));
            baseImages1.Hover = ((System.Drawing.Image)(resources.GetObject("baseImages1.Hover")));
            this.imageButtonEx1.Images = baseImages1;
            altImages1.Background = ((System.Drawing.Image)(resources.GetObject("altImages1.Background")));
            altImages1.Hover = ((System.Drawing.Image)(resources.GetObject("altImages1.Hover")));
            this.imageButtonEx1.ImagesAlt = altImages1;
            this.imageButtonEx1.Location = new System.Drawing.Point(330, 4);
            this.imageButtonEx1.Name = "imageButtonEx1";
            this.imageButtonEx1.Size = new System.Drawing.Size(97, 28);
            this.imageButtonEx1.TabIndex = 7;
            this.imageButtonEx1.Text = "Exit Check Mode";
            outline1.Color = System.Drawing.Color.Black;
            outline1.Width = 3;
            this.imageButtonEx1.TextOutline = outline1;
            this.imageButtonEx1.ToggleState = Mids_Reborn.Forms.Controls.ImageButtonEx.States.ToggledOff;
            stateText1.Indeterminate = "Indeterminate State";
            stateText1.ToggledOff = "ToggledOff State";
            stateText1.ToggledOn = "ToggledOn State";
            this.imageButtonEx1.ToggleText = stateText1;
            this.imageButtonEx1.UseAlt = false;
            this.imageButtonEx1.Click += new System.EventHandler(this.imageButtonEx1_Click);
            // 
            // lblCatalysts
            // 
            this.lblCatalysts.AutoSize = true;
            this.lblCatalysts.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCatalysts.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCatalysts.Location = new System.Drawing.Point(204, 12);
            this.lblCatalysts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCatalysts.Name = "lblCatalysts";
            this.lblCatalysts.Size = new System.Drawing.Size(33, 15);
            this.lblCatalysts.TabIndex = 6;
            this.lblCatalysts.Text = "x100";
            // 
            // lblBoosters
            // 
            this.lblBoosters.AutoSize = true;
            this.lblBoosters.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblBoosters.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblBoosters.Location = new System.Drawing.Point(290, 12);
            this.lblBoosters.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBoosters.Name = "lblBoosters";
            this.lblBoosters.Size = new System.Drawing.Size(33, 15);
            this.lblBoosters.TabIndex = 5;
            this.lblBoosters.Text = "x100";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(245, 0);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(37, 37);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(159, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // lblEnhObtained
            // 
            this.lblEnhObtained.AutoSize = true;
            this.lblEnhObtained.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEnhObtained.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblEnhObtained.Location = new System.Drawing.Point(42, 12);
            this.lblEnhObtained.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnhObtained.Name = "lblEnhObtained";
            this.lblEnhObtained.Size = new System.Drawing.Size(109, 15);
            this.lblEnhObtained.TabIndex = 1;
            this.lblEnhObtained.Text = "Obtained: 100/100";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // EnhCheckMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.pSalvageSummary);
            this.DoubleBuffered = true;
            this.Name = "EnhCheckMode";
            this.Size = new System.Drawing.Size(434, 35);
            this.pSalvageSummary.ResumeLayout(false);
            this.pSalvageSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pSalvageSummary;
        private System.Windows.Forms.Label lblCatalysts;
        private System.Windows.Forms.Label lblBoosters;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblEnhObtained;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ImageButtonEx imageButtonEx1;
    }
}
