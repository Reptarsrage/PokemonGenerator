﻿using System.Windows.Forms;
using PokemonGenerator.Models.Configuration;

namespace PokemonGenerator.Windows.Options
{
    partial class RandomOptionsWindow
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
            this.components = new System.ComponentModel.Container();
            this.LayoutPanelMain = new System.Windows.Forms.Panel();
            this.FieldNumericAlreadyPickedMoveModifier = new PokemonGenerator.Controls.FieldNumeric();
            this.OptionsWindowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FieldNumericAlreadyPickedMoveEffectsModifier = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericDamageTypeModifier = new PokemonGenerator.Controls.FieldNumeric();
            this.PanelAllowDuplicates = new System.Windows.Forms.Panel();
            this.CheckBoxAllowDuplicates = new System.Windows.Forms.CheckBox();
            this.FieldNumericMean = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericSkew = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericStandardDeviation = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericSameTypeModifier = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericDamageModifier = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericPairedModifier = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericDamageTypeDelta = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericRandomMoveMinPower = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericRandomMoveMaxPower = new PokemonGenerator.Controls.FieldNumeric();
            this.TableLayoutPanelOutter = new System.Windows.Forms.TableLayoutPanel();
            this.LayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsWindowBindingSource)).BeginInit();
            this.PanelAllowDuplicates.SuspendLayout();
            this.TableLayoutPanelOutter.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanelMain
            // 
            this.LayoutPanelMain.AutoScroll = true;
            this.LayoutPanelMain.AutoSize = true;
            this.LayoutPanelMain.Controls.Add(this.FieldNumericAlreadyPickedMoveModifier);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericAlreadyPickedMoveEffectsModifier);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericDamageTypeModifier);
            this.LayoutPanelMain.Controls.Add(this.PanelAllowDuplicates);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericMean);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericSkew);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericStandardDeviation);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericSameTypeModifier);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericDamageModifier);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericPairedModifier);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericDamageTypeDelta);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericRandomMoveMinPower);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericRandomMoveMaxPower);
            this.LayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelMain.MinimumSize = new System.Drawing.Size(500, 200);
            this.LayoutPanelMain.Name = "LayoutPanelMain";
            this.LayoutPanelMain.Size = new System.Drawing.Size(944, 441);
            this.LayoutPanelMain.TabIndex = 1;
            this.LayoutPanelMain.TabStop = true;
            // 
            // FieldNumericAlreadyPickedMoveModifier
            // 
            this.FieldNumericAlreadyPickedMoveModifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericAlreadyPickedMoveModifier.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "AlreadyPickedMoveModifier", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 100));
            this.FieldNumericAlreadyPickedMoveModifier.DecimalPlaces = 2;
            this.FieldNumericAlreadyPickedMoveModifier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FieldNumericAlreadyPickedMoveModifier.Label = "Already Picked Move Modifier";
            this.FieldNumericAlreadyPickedMoveModifier.Location = new System.Drawing.Point(3, 249);
            this.FieldNumericAlreadyPickedMoveModifier.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FieldNumericAlreadyPickedMoveModifier.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericAlreadyPickedMoveModifier.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericAlreadyPickedMoveModifier.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericAlreadyPickedMoveModifier.Name = "FieldNumericAlreadyPickedMoveModifier";
            this.FieldNumericAlreadyPickedMoveModifier.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericAlreadyPickedMoveModifier.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericAlreadyPickedMoveModifier.TabIndex = 6;
            this.FieldNumericAlreadyPickedMoveModifier.TabStop = true;
            this.FieldNumericAlreadyPickedMoveModifier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // OptionsWindowBindingSource
            // 
            this.OptionsWindowBindingSource.DataSource = typeof(PokemonGenerator.Models.Configuration.GeneratorConfig);
            // 
            // FieldNumericAlreadyPickedMoveEffectsModifier
            // 
            this.FieldNumericAlreadyPickedMoveEffectsModifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericAlreadyPickedMoveEffectsModifier.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "AlreadyPickedMoveEffectsModifier", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 100));
            this.FieldNumericAlreadyPickedMoveEffectsModifier.DecimalPlaces = 2;
            this.FieldNumericAlreadyPickedMoveEffectsModifier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FieldNumericAlreadyPickedMoveEffectsModifier.Label = "Already Picked Move Effects Modifier";
            this.FieldNumericAlreadyPickedMoveEffectsModifier.Location = new System.Drawing.Point(3, 290);
            this.FieldNumericAlreadyPickedMoveEffectsModifier.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FieldNumericAlreadyPickedMoveEffectsModifier.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericAlreadyPickedMoveEffectsModifier.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericAlreadyPickedMoveEffectsModifier.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericAlreadyPickedMoveEffectsModifier.Name = "FieldNumericAlreadyPickedMoveEffectsModifier";
            this.FieldNumericAlreadyPickedMoveEffectsModifier.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericAlreadyPickedMoveEffectsModifier.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericAlreadyPickedMoveEffectsModifier.TabIndex = 7;
            this.FieldNumericAlreadyPickedMoveEffectsModifier.TabStop = true;
            this.FieldNumericAlreadyPickedMoveEffectsModifier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericDamageTypeModifier
            // 
            this.FieldNumericDamageTypeModifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericDamageTypeModifier.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "DamageTypeModifier", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 100));
            this.FieldNumericDamageTypeModifier.DecimalPlaces = 2;
            this.FieldNumericDamageTypeModifier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FieldNumericDamageTypeModifier.Label = "Damage Type Modifier";
            this.FieldNumericDamageTypeModifier.Location = new System.Drawing.Point(0, 208);
            this.FieldNumericDamageTypeModifier.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FieldNumericDamageTypeModifier.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericDamageTypeModifier.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericDamageTypeModifier.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericDamageTypeModifier.Name = "FieldNumericDamageTypeModifier";
            this.FieldNumericDamageTypeModifier.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericDamageTypeModifier.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericDamageTypeModifier.TabIndex = 5;
            this.FieldNumericDamageTypeModifier.TabStop = true;
            this.FieldNumericDamageTypeModifier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // PanelAllowDuplicates
            // 
            this.PanelAllowDuplicates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelAllowDuplicates.Controls.Add(this.CheckBoxAllowDuplicates);
            this.PanelAllowDuplicates.Location = new System.Drawing.Point(3, 495);
            this.PanelAllowDuplicates.MaximumSize = new System.Drawing.Size(900, 35);
            this.PanelAllowDuplicates.MinimumSize = new System.Drawing.Size(500, 35);
            this.PanelAllowDuplicates.Name = "PanelAllowDuplicates";
            this.PanelAllowDuplicates.Padding = new System.Windows.Forms.Padding(5);
            this.PanelAllowDuplicates.Size = new System.Drawing.Size(883, 35);
            this.PanelAllowDuplicates.TabIndex = 12;
            // 
            // CheckBoxAllowDuplicates
            // 
            this.CheckBoxAllowDuplicates.AutoSize = true;
            this.CheckBoxAllowDuplicates.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CheckBoxAllowDuplicates.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.OptionsWindowBindingSource, "AllowDuplicates", true));
            this.CheckBoxAllowDuplicates.Location = new System.Drawing.Point(8, 6);
            this.CheckBoxAllowDuplicates.Name = "CheckBoxAllowDuplicates";
            this.CheckBoxAllowDuplicates.Size = new System.Drawing.Size(122, 21);
            this.CheckBoxAllowDuplicates.TabIndex = 0;
            this.CheckBoxAllowDuplicates.Text = "Allow Duplicates";
            this.CheckBoxAllowDuplicates.UseVisualStyleBackColor = true;
            // 
            // FieldNumericMean
            // 
            this.FieldNumericMean.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericMean.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "Mean", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, new decimal(new int[] {
                    5,
                    0,
                    0,
                    65536})));
            this.FieldNumericMean.DecimalPlaces = 2;
            this.FieldNumericMean.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.FieldNumericMean.Label = "Mean";
            this.FieldNumericMean.Location = new System.Drawing.Point(3, 3);
            this.FieldNumericMean.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FieldNumericMean.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericMean.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericMean.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericMean.Name = "FieldNumericMean";
            this.FieldNumericMean.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericMean.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericMean.TabIndex = 0;
            this.FieldNumericMean.TabStop = true;
            this.FieldNumericMean.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericSkew
            // 
            this.FieldNumericSkew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericSkew.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "Skew", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, new decimal(new int[] {
                    3,
                    0,
                    0,
                    65536})));
            this.FieldNumericSkew.DecimalPlaces = 2;
            this.FieldNumericSkew.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.FieldNumericSkew.Label = "Skew";
            this.FieldNumericSkew.Location = new System.Drawing.Point(3, 44);
            this.FieldNumericSkew.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.FieldNumericSkew.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericSkew.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericSkew.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericSkew.Name = "FieldNumericSkew";
            this.FieldNumericSkew.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericSkew.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericSkew.TabIndex = 1;
            this.FieldNumericSkew.TabStop = true;
            this.FieldNumericSkew.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericStandardDeviation
            // 
            this.FieldNumericStandardDeviation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericStandardDeviation.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "StandardDeviation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, new decimal(new int[] {
                    1,
                    0,
                    0,
                    65536})));
            this.FieldNumericStandardDeviation.DecimalPlaces = 2;
            this.FieldNumericStandardDeviation.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.FieldNumericStandardDeviation.Label = "Standard Deviation";
            this.FieldNumericStandardDeviation.Location = new System.Drawing.Point(3, 85);
            this.FieldNumericStandardDeviation.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.FieldNumericStandardDeviation.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericStandardDeviation.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericStandardDeviation.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericStandardDeviation.Name = "FieldNumericStandardDeviation";
            this.FieldNumericStandardDeviation.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericStandardDeviation.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericStandardDeviation.TabIndex = 2;
            this.FieldNumericStandardDeviation.TabStop = true;
            this.FieldNumericStandardDeviation.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericSameTypeModifier
            // 
            this.FieldNumericSameTypeModifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericSameTypeModifier.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "SameTypeModifier", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, new decimal(new int[] {
                    15,
                    0,
                    0,
                    65536})));
            this.FieldNumericSameTypeModifier.DecimalPlaces = 1;
            this.FieldNumericSameTypeModifier.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FieldNumericSameTypeModifier.Label = "Same Type Modifier";
            this.FieldNumericSameTypeModifier.Location = new System.Drawing.Point(3, 126);
            this.FieldNumericSameTypeModifier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FieldNumericSameTypeModifier.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericSameTypeModifier.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericSameTypeModifier.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericSameTypeModifier.Name = "FieldNumericSameTypeModifier";
            this.FieldNumericSameTypeModifier.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericSameTypeModifier.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericSameTypeModifier.TabIndex = 3;
            this.FieldNumericSameTypeModifier.TabStop = true;
            this.FieldNumericSameTypeModifier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericDamageModifier
            // 
            this.FieldNumericDamageModifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericDamageModifier.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "DamageModifier", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 200));
            this.FieldNumericDamageModifier.DecimalPlaces = 1;
            this.FieldNumericDamageModifier.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FieldNumericDamageModifier.Label = "Damage Modifier";
            this.FieldNumericDamageModifier.Location = new System.Drawing.Point(3, 167);
            this.FieldNumericDamageModifier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FieldNumericDamageModifier.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericDamageModifier.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericDamageModifier.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericDamageModifier.Name = "FieldNumericDamageModifier";
            this.FieldNumericDamageModifier.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericDamageModifier.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericDamageModifier.TabIndex = 4;
            this.FieldNumericDamageModifier.TabStop = true;
            this.FieldNumericDamageModifier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericPairedModifier
            // 
            this.FieldNumericPairedModifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericPairedModifier.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "PairedModifier", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 2));
            this.FieldNumericPairedModifier.DecimalPlaces = 1;
            this.FieldNumericPairedModifier.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FieldNumericPairedModifier.Label = "Paired Modifier";
            this.FieldNumericPairedModifier.Location = new System.Drawing.Point(3, 331);
            this.FieldNumericPairedModifier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FieldNumericPairedModifier.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericPairedModifier.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericPairedModifier.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericPairedModifier.Name = "FieldNumericPairedModifier";
            this.FieldNumericPairedModifier.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericPairedModifier.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericPairedModifier.TabIndex = 8;
            this.FieldNumericPairedModifier.TabStop = true;
            this.FieldNumericPairedModifier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericDamageTypeDelta
            // 
            this.FieldNumericDamageTypeDelta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericDamageTypeDelta.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "DamageTypeDelta", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 15));
            this.FieldNumericDamageTypeDelta.DecimalPlaces = 0;
            this.FieldNumericDamageTypeDelta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FieldNumericDamageTypeDelta.Label = "Damage Type Delta";
            this.FieldNumericDamageTypeDelta.Location = new System.Drawing.Point(3, 372);
            this.FieldNumericDamageTypeDelta.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FieldNumericDamageTypeDelta.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericDamageTypeDelta.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericDamageTypeDelta.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericDamageTypeDelta.Name = "FieldNumericDamageTypeDelta";
            this.FieldNumericDamageTypeDelta.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericDamageTypeDelta.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericDamageTypeDelta.TabIndex = 9;
            this.FieldNumericDamageTypeDelta.TabStop = true;
            this.FieldNumericDamageTypeDelta.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericRandomMoveMinPower
            // 
            this.FieldNumericRandomMoveMinPower.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericRandomMoveMinPower.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "RandomMoveMinPower", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 40));
            this.FieldNumericRandomMoveMinPower.DecimalPlaces = 0;
            this.FieldNumericRandomMoveMinPower.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMinPower.Label = "Random Move Min Power";
            this.FieldNumericRandomMoveMinPower.Location = new System.Drawing.Point(3, 413);
            this.FieldNumericRandomMoveMinPower.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMinPower.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericRandomMoveMinPower.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMinPower.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericRandomMoveMinPower.Name = "FieldNumericRandomMoveMinPower";
            this.FieldNumericRandomMoveMinPower.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericRandomMoveMinPower.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericRandomMoveMinPower.TabIndex = 10;
            this.FieldNumericRandomMoveMinPower.TabStop = true;
            this.FieldNumericRandomMoveMinPower.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericRandomMoveMaxPower
            // 
            this.FieldNumericRandomMoveMaxPower.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericRandomMoveMaxPower.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "RandomMoveMaxPower", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 100));
            this.FieldNumericRandomMoveMaxPower.DecimalPlaces = 0;
            this.FieldNumericRandomMoveMaxPower.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMaxPower.Label = "Random Move Max Power";
            this.FieldNumericRandomMoveMaxPower.Location = new System.Drawing.Point(3, 454);
            this.FieldNumericRandomMoveMaxPower.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMaxPower.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericRandomMoveMaxPower.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMaxPower.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericRandomMoveMaxPower.Name = "FieldNumericRandomMoveMaxPower";
            this.FieldNumericRandomMoveMaxPower.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericRandomMoveMaxPower.Size = new System.Drawing.Size(883, 35);
            this.FieldNumericRandomMoveMaxPower.TabIndex = 11;
            this.FieldNumericRandomMoveMaxPower.TabStop = true;
            this.FieldNumericRandomMoveMaxPower.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // TableLayoutPanelOutter
            // 
            this.TableLayoutPanelOutter.ColumnCount = 1;
            this.TableLayoutPanelOutter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelMain, 0, 0);
            this.TableLayoutPanelOutter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanelOutter.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanelOutter.Name = "TableLayoutPanelOutter";
            this.TableLayoutPanelOutter.RowCount = 1;
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 441F));
            this.TableLayoutPanelOutter.Size = new System.Drawing.Size(944, 441);
            this.TableLayoutPanelOutter.TabIndex = 1;
            // 
            // RandomOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanelOutter);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "RandomOptionsWindow";
            this.Size = new System.Drawing.Size(944, 441);
            this.LayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptionsWindowBindingSource)).EndInit();
            this.PanelAllowDuplicates.ResumeLayout(false);
            this.PanelAllowDuplicates.PerformLayout();
            this.TableLayoutPanelOutter.ResumeLayout(false);
            this.TableLayoutPanelOutter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel LayoutPanelMain;
        private TableLayoutPanel TableLayoutPanelOutter;
        private Controls.FieldNumeric FieldNumericMean;
        private Controls.FieldNumeric FieldNumericSkew;
        private Controls.FieldNumeric FieldNumericStandardDeviation;
        private Controls.FieldNumeric FieldNumericSameTypeModifier;
        private Controls.FieldNumeric FieldNumericDamageModifier;
        private Controls.FieldNumeric FieldNumericPairedModifier;
        private Controls.FieldNumeric FieldNumericDamageTypeDelta;
        private Controls.FieldNumeric FieldNumericRandomMoveMinPower;
        private Controls.FieldNumeric FieldNumericRandomMoveMaxPower;
        private System.Windows.Forms.BindingSource OptionsWindowBindingSource;
        private Panel PanelAllowDuplicates;
        private CheckBox CheckBoxAllowDuplicates;
        private Controls.FieldNumeric FieldNumericDamageTypeModifier;
        private Controls.FieldNumeric FieldNumericAlreadyPickedMoveEffectsModifier;
        private Controls.FieldNumeric FieldNumericAlreadyPickedMoveModifier;
    }
}
