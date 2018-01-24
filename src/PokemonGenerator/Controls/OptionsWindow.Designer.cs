using System.Windows.Forms;

namespace PokemonGenerator.Forms
{
    partial class OptionsWindow
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
            this.LayoutPanelMain = new System.Windows.Forms.FlowLayoutPanel();
            this.FieldNumericMean = new PokemonGenerator.Controls.FieldNumeric();
            this.OptionsWindowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FieldNumericSkew = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericStandardDeviation = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericSameTypeModifier = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericDamageModifier = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericPairedModifier = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericDamageTypeDelta = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericRandomMoveMinPower = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericRandomMoveMaxPower = new PokemonGenerator.Controls.FieldNumeric();
            this.LayoutPanelBottom = new System.Windows.Forms.Panel();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.TableLayoutPanelOutter = new System.Windows.Forms.TableLayoutPanel();
            this.LayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsWindowBindingSource)).BeginInit();
            this.LayoutPanelBottom.SuspendLayout();
            this.TableLayoutPanelOutter.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanelMain
            // 
            this.LayoutPanelMain.AutoScroll = true;
            this.LayoutPanelMain.AutoSize = true;
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
            this.LayoutPanelMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.LayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelMain.Name = "LayoutPanelMain";
            this.LayoutPanelMain.Size = new System.Drawing.Size(944, 406);
            this.LayoutPanelMain.TabIndex = 1;
            // 
            // FieldNumericMean
            // 
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
            this.FieldNumericMean.Location = new System.Drawing.Point(3, 3);
            this.FieldNumericMean.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FieldNumericMean.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericMean.Name = "FieldNumericMean";
            this.FieldNumericMean.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericMean.Size = new System.Drawing.Size(600, 35);
            this.FieldNumericMean.TabIndex = 1;
            this.FieldNumericMean.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // OptionsWindowBindingSource
            // 
            this.OptionsWindowBindingSource.DataSource = typeof(PokemonGenerator.Models.PokemonGeneratorConfig);
            // 
            // FieldNumericSkew
            // 
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
            this.FieldNumericSkew.Location = new System.Drawing.Point(3, 9);
            this.FieldNumericSkew.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.FieldNumericSkew.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericSkew.Name = "FieldNumericSkew";
            this.FieldNumericSkew.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericSkew.Size = new System.Drawing.Size(600, 35);
            this.FieldNumericSkew.TabIndex = 2;
            this.FieldNumericSkew.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericStandardDeviation
            // 
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
            this.FieldNumericStandardDeviation.Location = new System.Drawing.Point(3, 15);
            this.FieldNumericStandardDeviation.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.FieldNumericStandardDeviation.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericStandardDeviation.Name = "FieldNumericStandardDeviation";
            this.FieldNumericStandardDeviation.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericStandardDeviation.Size = new System.Drawing.Size(600, 35);
            this.FieldNumericStandardDeviation.TabIndex = 3;
            this.FieldNumericStandardDeviation.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericSameTypeModifier
            // 
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
            this.FieldNumericSameTypeModifier.Location = new System.Drawing.Point(3, 21);
            this.FieldNumericSameTypeModifier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FieldNumericSameTypeModifier.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericSameTypeModifier.Name = "FieldNumericSameTypeModifier";
            this.FieldNumericSameTypeModifier.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericSameTypeModifier.Size = new System.Drawing.Size(600, 35);
            this.FieldNumericSameTypeModifier.TabIndex = 4;
            this.FieldNumericSameTypeModifier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericDamageModifier
            // 
            this.FieldNumericDamageModifier.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "DamageModifier", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 200));
            this.FieldNumericDamageModifier.DecimalPlaces = 1;
            this.FieldNumericDamageModifier.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FieldNumericDamageModifier.Location = new System.Drawing.Point(3, 27);
            this.FieldNumericDamageModifier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FieldNumericDamageModifier.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericDamageModifier.Name = "FieldNumericDamageModifier";
            this.FieldNumericDamageModifier.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericDamageModifier.Size = new System.Drawing.Size(600, 35);
            this.FieldNumericDamageModifier.TabIndex = 5;
            this.FieldNumericDamageModifier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericPairedModifier
            // 
            this.FieldNumericPairedModifier.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "PairedModifier", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 2));
            this.FieldNumericPairedModifier.DecimalPlaces = 1;
            this.FieldNumericPairedModifier.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FieldNumericPairedModifier.Location = new System.Drawing.Point(3, 68);
            this.FieldNumericPairedModifier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FieldNumericPairedModifier.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericPairedModifier.Name = "FieldNumericPairedModifier";
            this.FieldNumericPairedModifier.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericPairedModifier.Size = new System.Drawing.Size(600, 35);
            this.FieldNumericPairedModifier.TabIndex = 6;
            this.FieldNumericPairedModifier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericDamageTypeDelta
            // 
            this.FieldNumericDamageTypeDelta.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "DamageTypeDelta", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 15));
            this.FieldNumericDamageTypeDelta.DecimalPlaces = 0;
            this.FieldNumericDamageTypeDelta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FieldNumericDamageTypeDelta.Location = new System.Drawing.Point(3, 74);
            this.FieldNumericDamageTypeDelta.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FieldNumericDamageTypeDelta.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericDamageTypeDelta.Name = "FieldNumericDamageTypeDelta";
            this.FieldNumericDamageTypeDelta.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericDamageTypeDelta.Size = new System.Drawing.Size(600, 35);
            this.FieldNumericDamageTypeDelta.TabIndex = 7;
            this.FieldNumericDamageTypeDelta.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericRandomMoveMinPower
            // 
            this.FieldNumericRandomMoveMinPower.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "RandomMoveMinPower", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 40));
            this.FieldNumericRandomMoveMinPower.DecimalPlaces = 0;
            this.FieldNumericRandomMoveMinPower.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMinPower.Location = new System.Drawing.Point(3, 80);
            this.FieldNumericRandomMoveMinPower.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMinPower.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMinPower.Name = "FieldNumericRandomMoveMinPower";
            this.FieldNumericRandomMoveMinPower.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericRandomMoveMinPower.Size = new System.Drawing.Size(600, 35);
            this.FieldNumericRandomMoveMinPower.TabIndex = 8;
            this.FieldNumericRandomMoveMinPower.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FieldNumericRandomMoveMaxPower
            // 
            this.FieldNumericRandomMoveMaxPower.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "RandomMoveMaxPower", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, 100));
            this.FieldNumericRandomMoveMaxPower.DecimalPlaces = 0;
            this.FieldNumericRandomMoveMaxPower.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMaxPower.Location = new System.Drawing.Point(3, 86);
            this.FieldNumericRandomMoveMaxPower.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMaxPower.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.FieldNumericRandomMoveMaxPower.Name = "FieldNumericRandomMoveMaxPower";
            this.FieldNumericRandomMoveMaxPower.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericRandomMoveMaxPower.Size = new System.Drawing.Size(600, 35);
            this.FieldNumericRandomMoveMaxPower.TabIndex = 9;
            this.FieldNumericRandomMoveMaxPower.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // LayoutPanelBottom
            // 
            this.LayoutPanelBottom.AutoSize = true;
            this.LayoutPanelBottom.Controls.Add(this.ButtonSave);
            this.LayoutPanelBottom.Controls.Add(this.ButtonCancel);
            this.LayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelBottom.Location = new System.Drawing.Point(0, 406);
            this.LayoutPanelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelBottom.Name = "LayoutPanelBottom";
            this.LayoutPanelBottom.Size = new System.Drawing.Size(944, 35);
            this.LayoutPanelBottom.TabIndex = 0;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSave.Location = new System.Drawing.Point(857, 5);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 25);
            this.ButtonSave.TabIndex = 2;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.Location = new System.Drawing.Point(776, 6);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 25);
            this.ButtonCancel.TabIndex = 3;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // TableLayoutPanelOutter
            // 
            this.TableLayoutPanelOutter.ColumnCount = 1;
            this.TableLayoutPanelOutter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelMain, 0, 0);
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelBottom, 0, 1);
            this.TableLayoutPanelOutter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanelOutter.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanelOutter.Name = "TableLayoutPanelOutter";
            this.TableLayoutPanelOutter.RowCount = 2;
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanelOutter.Size = new System.Drawing.Size(944, 441);
            this.TableLayoutPanelOutter.TabIndex = 1;
            // 
            // OptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanelOutter);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "OptionsWindow";
            this.Size = new System.Drawing.Size(944, 441);
            this.LayoutPanelMain.ResumeLayout(false);
            this.LayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsWindowBindingSource)).EndInit();
            this.LayoutPanelBottom.ResumeLayout(false);
            this.TableLayoutPanelOutter.ResumeLayout(false);
            this.TableLayoutPanelOutter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel LayoutPanelMain;
        private Panel LayoutPanelBottom;
        private TableLayoutPanel TableLayoutPanelOutter;
        private Button ButtonSave;
        private Button ButtonCancel;
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
    }
}
