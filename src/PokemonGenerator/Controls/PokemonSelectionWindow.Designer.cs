using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    partial class PokemonSelectionWindow
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
            this.LayoutPanelMain = new System.Windows.Forms.FlowLayoutPanel();
            this.LayoutPanelTop = new System.Windows.Forms.Panel();
            this.ButtonSelectNone = new System.Windows.Forms.Button();
            this.ButtonSelectAll = new System.Windows.Forms.Button();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.TableLayoutPanelOutter = new System.Windows.Forms.TableLayoutPanel();
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
            this.LayoutPanelMain.Size = new System.Drawing.Size(944, 406);
            this.LayoutPanelMain.TabIndex = 1;
            this.LayoutPanelMain.TabStop = true;
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
            // ButtonSelectNone
            // 
            this.ButtonSelectNone.Location = new System.Drawing.Point(108, 5);
            this.ButtonSelectNone.Name = "ButtonSelectNone";
            this.ButtonSelectNone.Size = new System.Drawing.Size(90, 25);
            this.ButtonSelectNone.TabIndex = 6;
            this.ButtonSelectNone.Text = "Select None";
            this.ButtonSelectNone.UseVisualStyleBackColor = true;
            this.ButtonSelectNone.Click += new System.EventHandler(this.SelectNone);
            // 
            // ButtonSelectAll
            // 
            this.ButtonSelectAll.Location = new System.Drawing.Point(12, 5);
            this.ButtonSelectAll.Name = "ButtonSelectAll";
            this.ButtonSelectAll.Size = new System.Drawing.Size(90, 25);
            this.ButtonSelectAll.TabIndex = 5;
            this.ButtonSelectAll.Text = "Select All";
            this.ButtonSelectAll.UseVisualStyleBackColor = true;
            this.ButtonSelectAll.Click += new System.EventHandler(this.SelectAll);
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.WorkerReportsProgress = true;
            this.BackgroundWorker.WorkerSupportsCancellation = true;
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerDoWork);
            this.BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerProgressChanged);
            this.BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerCompleted);
            // 
            // TableLayoutPanelOutter
            // 
            this.TableLayoutPanelOutter.ColumnCount = 1;
            this.TableLayoutPanelOutter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelTop, 0, 0);
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelMain, 0, 1);
            this.TableLayoutPanelOutter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanelOutter.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanelOutter.Name = "TableLayoutPanelOutter";
            this.TableLayoutPanelOutter.RowCount = 2;
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanelOutter.Size = new System.Drawing.Size(944, 441);
            this.TableLayoutPanelOutter.TabIndex = 1;
            // 
            // PokemonOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanelOutter);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "PokemonOptionsWindow";
            this.Size = new System.Drawing.Size(944, 441);
            this.LayoutPanelTop.ResumeLayout(false);
            this.TableLayoutPanelOutter.ResumeLayout(false);
            this.TableLayoutPanelOutter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel LayoutPanelMain;
        private Panel LayoutPanelTop;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private TableLayoutPanel TableLayoutPanelOutter;
        private Button ButtonSelectNone;
        private Button ButtonSelectAll;
    }
}
