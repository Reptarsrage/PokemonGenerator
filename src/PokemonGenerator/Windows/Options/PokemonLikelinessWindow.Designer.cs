using System.Windows.Forms;
using PokemonGenerator.Models.Configuration;

namespace PokemonGenerator.Windows.Options
{
    partial class PokemonLikelinessWindow
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
            this.FieldNumericStandard = new PokemonGenerator.Controls.FieldNumeric();
            this.OptionsWindowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FieldNumericLegendary = new PokemonGenerator.Controls.FieldNumeric();
            this.FieldNumericSpecial = new PokemonGenerator.Controls.FieldNumeric();
            this.TableLayoutPanelOutter = new System.Windows.Forms.TableLayoutPanel();
            this.LayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionsWindowBindingSource)).BeginInit();
            this.TableLayoutPanelOutter.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanelMain
            // 
            this.LayoutPanelMain.AutoScroll = true;
            this.LayoutPanelMain.AutoSize = true;
            this.LayoutPanelMain.Controls.Add(this.FieldNumericStandard);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericLegendary);
            this.LayoutPanelMain.Controls.Add(this.FieldNumericSpecial);
            this.LayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelMain.MinimumSize = new System.Drawing.Size(500, 200);
            this.LayoutPanelMain.Name = "LayoutPanelMain";
            this.LayoutPanelMain.Size = new System.Drawing.Size(944, 441);
            this.LayoutPanelMain.TabIndex = 1;
            // 
            // FieldNumericStandard
            // 
            this.FieldNumericStandard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericStandard.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "PokemonLiklihood.Standard", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, new decimal(new int[] {
                    5,
                    0,
                    0,
                    65536})));
            this.FieldNumericStandard.DecimalPlaces = 2;
            this.FieldNumericStandard.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.FieldNumericStandard.Label = "Standard";
            this.FieldNumericStandard.Location = new System.Drawing.Point(3, 3);
            this.FieldNumericStandard.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.FieldNumericStandard.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericStandard.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericStandard.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericStandard.Name = "FieldNumericStandard";
            this.FieldNumericStandard.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericStandard.Size = new System.Drawing.Size(900, 35);
            this.FieldNumericStandard.TabIndex = 1;
            this.FieldNumericStandard.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // OptionsWindowBindingSource
            // 
            this.OptionsWindowBindingSource.DataSource = typeof(GeneratorConfig);
            // 
            // FieldNumericLegendary
            // 
            this.FieldNumericLegendary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericLegendary.BackColor = CustomColors.Legendary;
            this.FieldNumericLegendary.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "PokemonLiklihood.Legendary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, new decimal(new int[] {
                    3,
                    0,
                    0,
                    65536})));
            this.FieldNumericLegendary.DecimalPlaces = 2;
            this.FieldNumericLegendary.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.FieldNumericLegendary.Label = "Legendary";
            this.FieldNumericLegendary.Location = new System.Drawing.Point(3, 44);
            this.FieldNumericLegendary.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.FieldNumericLegendary.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericLegendary.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericLegendary.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericLegendary.Name = "FieldNumericLegendary";
            this.FieldNumericLegendary.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericLegendary.Size = new System.Drawing.Size(900, 35);
            this.FieldNumericLegendary.TabIndex = 2;
            this.FieldNumericLegendary.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // FieldNumericSpecial
            // 
            this.FieldNumericSpecial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldNumericSpecial.BackColor = CustomColors.Special;
            this.FieldNumericSpecial.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.OptionsWindowBindingSource, "PokemonLiklihood.Special", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, new decimal(new int[] {
                    1,
                    0,
                    0,
                    65536})));
            this.FieldNumericSpecial.DecimalPlaces = 2;
            this.FieldNumericSpecial.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.FieldNumericSpecial.Label = "Special";
            this.FieldNumericSpecial.Location = new System.Drawing.Point(3, 85);
            this.FieldNumericSpecial.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.FieldNumericSpecial.MaximumSize = new System.Drawing.Size(900, 35);
            this.FieldNumericSpecial.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.FieldNumericSpecial.MinimumSize = new System.Drawing.Size(500, 35);
            this.FieldNumericSpecial.Name = "FieldNumericSpecial";
            this.FieldNumericSpecial.Padding = new System.Windows.Forms.Padding(5);
            this.FieldNumericSpecial.Size = new System.Drawing.Size(900, 35);
            this.FieldNumericSpecial.TabIndex = 3;
            this.FieldNumericSpecial.Value = new decimal(new int[] {
            3,
            0,
            0,
            131072});
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
            // PokemonLikelinessWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanelOutter);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "PokemonLikelinessWindow";
            this.Size = new System.Drawing.Size(944, 441);
            this.LayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptionsWindowBindingSource)).EndInit();
            this.TableLayoutPanelOutter.ResumeLayout(false);
            this.TableLayoutPanelOutter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel LayoutPanelMain;
        private TableLayoutPanel TableLayoutPanelOutter;
        private Controls.FieldNumeric FieldNumericStandard;
        private Controls.FieldNumeric FieldNumericLegendary;
        private Controls.FieldNumeric FieldNumericSpecial;
        private System.Windows.Forms.BindingSource OptionsWindowBindingSource;
    }
}
