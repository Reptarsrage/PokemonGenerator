using System.Windows.Forms;

namespace PokemonGenerator.Forms
{
    partial class PokemonSettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokemonSettingsForm));
            this.LayoutPanelMain = new System.Windows.Forms.FlowLayoutPanel();
            this.LayoutPanelBottom = new System.Windows.Forms.Panel();
            this.LabelCount = new System.Windows.Forms.Label();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.LayoutPanelTop = new System.Windows.Forms.Panel();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.TableLayoutPanelOutter = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonSelectAll = new System.Windows.Forms.Button();
            this.ButtonSelectNone = new System.Windows.Forms.Button();
            this.LayoutPanelBottom.SuspendLayout();
            this.LayoutPanelTop.SuspendLayout();
            this.TableLayoutPanelOutter.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanelMain
            // 
            this.LayoutPanelMain.AutoScroll = true;
            this.LayoutPanelMain.AutoSize = true;
            this.LayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelMain.Location = new System.Drawing.Point(0, 35);
            this.LayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelMain.Name = "LayoutPanelMain";
            this.LayoutPanelMain.Size = new System.Drawing.Size(944, 371);
            this.LayoutPanelMain.TabIndex = 1;
            this.LayoutPanelMain.TabStop = true;
            // 
            // LayoutPanelBottom
            // 
            this.LayoutPanelBottom.AutoSize = true;
            this.LayoutPanelBottom.Controls.Add(this.LabelCount);
            this.LayoutPanelBottom.Controls.Add(this.ButtonSave);
            this.LayoutPanelBottom.Controls.Add(this.ButtonCancel);
            this.LayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelBottom.Location = new System.Drawing.Point(0, 406);
            this.LayoutPanelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelBottom.Name = "LayoutPanelBottom";
            this.LayoutPanelBottom.Size = new System.Drawing.Size(944, 35);
            this.LayoutPanelBottom.TabIndex = 0;
            // 
            // LabelCount
            // 
            this.LabelCount.AutoSize = true;
            this.LabelCount.Location = new System.Drawing.Point(10, 6);
            this.LabelCount.Margin = new System.Windows.Forms.Padding(3);
            this.LabelCount.Name = "LabelCount";
            this.LabelCount.Size = new System.Drawing.Size(80, 17);
            this.LabelCount.TabIndex = 4;
            this.LabelCount.Text = "0/0 Selected";
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
            this.ButtonSave.Click += new System.EventHandler(this.buttonSave_Click);
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
            this.ButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // LayoutPanelTop
            // 
            this.LayoutPanelTop.AutoSize = true;
            this.LayoutPanelTop.Controls.Add(this.ButtonSelectNone);
            this.LayoutPanelTop.Controls.Add(this.ButtonSelectAll);
            this.LayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelTop.Location = new System.Drawing.Point(0, 0);
            this.LayoutPanelTop.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelTop.Name = "LayoutPanelTop";
            this.LayoutPanelTop.Size = new System.Drawing.Size(944, 35);
            this.LayoutPanelTop.TabIndex = 0;
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.WorkerReportsProgress = true;
            this.BackgroundWorker.WorkerSupportsCancellation = true;
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // TableLayoutPanelOutter
            // 
            this.TableLayoutPanelOutter.ColumnCount = 1;
            this.TableLayoutPanelOutter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelTop, 0, 0);
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelMain, 0, 1);
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelBottom, 0, 2);
            this.TableLayoutPanelOutter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanelOutter.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanelOutter.Name = "TableLayoutPanelOutter";
            this.TableLayoutPanelOutter.RowCount = 3;
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.TableLayoutPanelOutter.Size = new System.Drawing.Size(944, 441);
            this.TableLayoutPanelOutter.TabIndex = 1;
            // 
            // ButtonSelectAll
            // 
            this.ButtonSelectAll.Location = new System.Drawing.Point(12, 5);
            this.ButtonSelectAll.Name = "ButtonSelectAll";
            this.ButtonSelectAll.Size = new System.Drawing.Size(90, 25);
            this.ButtonSelectAll.TabIndex = 5;
            this.ButtonSelectAll.Text = "Select All";
            this.ButtonSelectAll.UseVisualStyleBackColor = true;
            this.ButtonSelectAll.Click += new System.EventHandler(this.ButtonSelectAll_Click);
            // 
            // ButtonSelectNone
            // 
            this.ButtonSelectNone.Location = new System.Drawing.Point(108, 5);
            this.ButtonSelectNone.Name = "ButtonSelectNone";
            this.ButtonSelectNone.Size = new System.Drawing.Size(90, 25);
            this.ButtonSelectNone.TabIndex = 6;
            this.ButtonSelectNone.Text = "Select None";
            this.ButtonSelectNone.UseVisualStyleBackColor = true;
            this.ButtonSelectNone.Click += new System.EventHandler(this.ButtonSelectNone_Click);
            // 
            // PokemonSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 441);
            this.Controls.Add(this.TableLayoutPanelOutter);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "PokemonSettingsForm";
            this.Text = "PokéGenerator";
            this.Load += new System.EventHandler(this.PokemonGeneratorLoad);
            this.LayoutPanelBottom.ResumeLayout(false);
            this.LayoutPanelBottom.PerformLayout();
            this.LayoutPanelTop.ResumeLayout(false);
            this.TableLayoutPanelOutter.ResumeLayout(false);
            this.TableLayoutPanelOutter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel LayoutPanelMain;
        private Panel LayoutPanelBottom;
        private Panel LayoutPanelTop;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private TableLayoutPanel TableLayoutPanelOutter;
        private Button ButtonSave;
        private Button ButtonCancel;
        private Label LabelCount;
        private Button ButtonSelectNone;
        private Button ButtonSelectAll;
    }
}