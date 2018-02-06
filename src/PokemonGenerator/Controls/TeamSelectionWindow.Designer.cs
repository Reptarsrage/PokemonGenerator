using System.Windows.Forms;

namespace PokemonGenerator.Forms
{
    partial class TeamSelectionWindow
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
            this.LayoutPanelBottom = new System.Windows.Forms.Panel();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.TableLayoutPanelOutter = new System.Windows.Forms.TableLayoutPanel();
            this.LayoutPanelBottom.SuspendLayout();
            this.TableLayoutPanelOutter.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanelMain
            // 
            this.LayoutPanelMain.AutoScroll = true;
            this.LayoutPanelMain.AutoSize = true;
            this.LayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelMain.Location = new System.Drawing.Point(100, 0);
            this.LayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelMain.Name = "LayoutPanelMain";
            this.LayoutPanelMain.Size = new System.Drawing.Size(844, 406);
            this.LayoutPanelMain.TabIndex = 1;
            this.LayoutPanelMain.TabStop = true;
            // 
            // LayoutPanelBottom
            // 
            this.LayoutPanelBottom.AutoSize = true;
            this.LayoutPanelBottom.Controls.Add(this.ButtonOk);
            this.LayoutPanelBottom.Controls.Add(this.ButtonCancel);
            this.LayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelBottom.Location = new System.Drawing.Point(100, 406);
            this.LayoutPanelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelBottom.Name = "LayoutPanelBottom";
            this.LayoutPanelBottom.Size = new System.Drawing.Size(844, 35);
            this.LayoutPanelBottom.TabIndex = 0;
            // 
            // ButtonOk
            // 
            this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOk.Location = new System.Drawing.Point(742, 5);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(90, 25);
            this.ButtonOk.TabIndex = 6;
            this.ButtonOk.Text = "Ok";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOkClick);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.Location = new System.Drawing.Point(646, 5);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(90, 25);
            this.ButtonCancel.TabIndex = 5;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
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
            this.TableLayoutPanelOutter.ColumnCount = 2;
            this.TableLayoutPanelOutter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.TableLayoutPanelOutter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelBottom, 1, 1);
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelMain, 1, 0);
            this.TableLayoutPanelOutter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanelOutter.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanelOutter.Name = "TableLayoutPanelOutter";
            this.TableLayoutPanelOutter.RowCount = 2;
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.TableLayoutPanelOutter.Size = new System.Drawing.Size(944, 441);
            this.TableLayoutPanelOutter.TabIndex = 1;
            // 
            // TeamSelectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanelOutter);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "TeamSelectionWindow";
            this.Size = new System.Drawing.Size(944, 441);
            this.LayoutPanelBottom.ResumeLayout(false);
            this.TableLayoutPanelOutter.ResumeLayout(false);
            this.TableLayoutPanelOutter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel LayoutPanelMain;
        private Panel LayoutPanelBottom;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private TableLayoutPanel TableLayoutPanelOutter;
        private Button ButtonOk;
        private Button ButtonCancel;
    }
}
