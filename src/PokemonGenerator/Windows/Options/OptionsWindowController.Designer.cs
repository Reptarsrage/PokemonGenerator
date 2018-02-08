using System.Windows.Forms;

namespace PokemonGenerator.Windows.Options
{
    partial class OptionsWindowController
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
            this.LayoutPanelBottom = new System.Windows.Forms.Panel();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.ListOptions = new System.Windows.Forms.ListBox();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PanelInner = new System.Windows.Forms.Panel();
            this.LayoutPanelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.TableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanelBottom
            // 
            this.LayoutPanelBottom.AutoSize = true;
            this.LayoutPanelBottom.Controls.Add(this.ButtonSave);
            this.LayoutPanelBottom.Controls.Add(this.ButtonCancel);
            this.LayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelBottom.Location = new System.Drawing.Point(0, 454);
            this.LayoutPanelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelBottom.Name = "LayoutPanelBottom";
            this.LayoutPanelBottom.Size = new System.Drawing.Size(494, 35);
            this.LayoutPanelBottom.TabIndex = 0;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSave.Location = new System.Drawing.Point(407, 5);
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
            this.ButtonCancel.Location = new System.Drawing.Point(326, 6);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 25);
            this.ButtonCancel.TabIndex = 3;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.ListOptions);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.TableLayoutPanel);
            this.SplitContainer.Size = new System.Drawing.Size(747, 489);
            this.SplitContainer.SplitterDistance = 249;
            this.SplitContainer.TabIndex = 0;
            // 
            // ListOptions
            // 
            this.ListOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListOptions.FormattingEnabled = true;
            this.ListOptions.ItemHeight = 16;
            this.ListOptions.Location = new System.Drawing.Point(0, 0);
            this.ListOptions.Margin = new System.Windows.Forms.Padding(0);
            this.ListOptions.Name = "ListOptions";
            this.ListOptions.Size = new System.Drawing.Size(249, 489);
            this.ListOptions.TabIndex = 0;
            this.ListOptions.SelectedIndexChanged += new System.EventHandler(this.ListOptionsChanged);
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.ColumnCount = 1;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel.Controls.Add(this.LayoutPanelBottom, 0, 1);
            this.TableLayoutPanel.Controls.Add(this.PanelInner, 0, 0);
            this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 2;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(494, 489);
            this.TableLayoutPanel.TabIndex = 1;
            // 
            // PanelInner
            // 
            this.PanelInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelInner.Location = new System.Drawing.Point(0, 0);
            this.PanelInner.Margin = new System.Windows.Forms.Padding(0);
            this.PanelInner.Name = "PanelInner";
            this.PanelInner.Size = new System.Drawing.Size(494, 454);
            this.PanelInner.TabIndex = 1;
            // 
            // OptionsWindowController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainer);
            this.Name = "OptionsWindowController";
            this.Size = new System.Drawing.Size(747, 489);
            this.LayoutPanelBottom.ResumeLayout(false);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel LayoutPanelBottom;
        private Button ButtonSave;
        private Button ButtonCancel;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.ListBox ListOptions;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        private Panel PanelInner;
    }
}
