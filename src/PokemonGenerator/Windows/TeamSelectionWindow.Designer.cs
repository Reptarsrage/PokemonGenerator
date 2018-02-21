using System.Windows.Forms;

namespace PokemonGenerator.Windows
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.LayoutPanelBottom = new System.Windows.Forms.Panel();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.TableLayoutPanelOutter = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PictureTeamSixth = new PokemonGenerator.Controls.SVGViewer();
            this.PictureTeamFifth = new PokemonGenerator.Controls.SVGViewer();
            this.PictureTeamFourth = new PokemonGenerator.Controls.SVGViewer();
            this.PictureTeamThird = new PokemonGenerator.Controls.SVGViewer();
            this.PictureTeamSecond = new PokemonGenerator.Controls.SVGViewer();
            this.PictureTeamFirst = new PokemonGenerator.Controls.SVGViewer();
            this.BackgroundWorkerTeam = new System.ComponentModel.BackgroundWorker();
            this.LayoutPanelMain.SuspendLayout();
            this.LayoutPanelBottom.SuspendLayout();
            this.TableLayoutPanelOutter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LayoutPanelMain
            // 
            this.LayoutPanelMain.AutoScroll = true;
            this.LayoutPanelMain.AutoSize = true;
            this.LayoutPanelMain.Controls.Add(this.flowLayoutPanel1);
            this.LayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelMain.Location = new System.Drawing.Point(100, 0);
            this.LayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutPanelMain.Name = "LayoutPanelMain";
            this.LayoutPanelMain.Size = new System.Drawing.Size(844, 406);
            this.LayoutPanelMain.TabIndex = 0;
            this.LayoutPanelMain.TabStop = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel1.TabIndex = 0;
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
            this.LayoutPanelBottom.TabIndex = 1;
            this.LayoutPanelBottom.TabStop = true;
            // 
            // ButtonOk
            // 
            this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOk.Location = new System.Drawing.Point(742, 5);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(90, 25);
            this.ButtonOk.TabIndex = 1;
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
            this.ButtonCancel.TabIndex = 0;
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
            // 
            // TableLayoutPanelOutter
            // 
            this.TableLayoutPanelOutter.ColumnCount = 2;
            this.TableLayoutPanelOutter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.TableLayoutPanelOutter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelBottom, 1, 1);
            this.TableLayoutPanelOutter.Controls.Add(this.LayoutPanelMain, 1, 0);
            this.TableLayoutPanelOutter.Controls.Add(this.panel1, 0, 0);
            this.TableLayoutPanelOutter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanelOutter.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanelOutter.Name = "TableLayoutPanelOutter";
            this.TableLayoutPanelOutter.RowCount = 2;
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelOutter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.TableLayoutPanelOutter.Size = new System.Drawing.Size(944, 441);
            this.TableLayoutPanelOutter.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PictureTeamSixth);
            this.panel1.Controls.Add(this.PictureTeamFifth);
            this.panel1.Controls.Add(this.PictureTeamFourth);
            this.panel1.Controls.Add(this.PictureTeamThird);
            this.panel1.Controls.Add(this.PictureTeamSecond);
            this.panel1.Controls.Add(this.PictureTeamFirst);
            this.panel1.Location = new System.Drawing.Point(25, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(50, 300);
            this.panel1.TabIndex = 2;
            // 
            // PictureTeamSixth
            // 
            this.PictureTeamSixth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamSixth.Dock = System.Windows.Forms.DockStyle.Top;
            this.PictureTeamSixth.Image = null;
            this.PictureTeamSixth.Location = new System.Drawing.Point(0, 250);
            this.PictureTeamSixth.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamSixth.Name = "PictureTeamSixth";
            this.PictureTeamSixth.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamSixth.SvgImage = null;
            this.PictureTeamSixth.TabIndex = 5;
            this.PictureTeamSixth.TabStop = false;
            // 
            // PictureTeamFifth
            // 
            this.PictureTeamFifth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamFifth.Dock = System.Windows.Forms.DockStyle.Top;
            this.PictureTeamFifth.Image = null;
            this.PictureTeamFifth.Location = new System.Drawing.Point(0, 200);
            this.PictureTeamFifth.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamFifth.Name = "PictureTeamFifth";
            this.PictureTeamFifth.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamFifth.SvgImage = null;
            this.PictureTeamFifth.TabIndex = 4;
            this.PictureTeamFifth.TabStop = false;
            // 
            // PictureTeamFourth
            // 
            this.PictureTeamFourth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamFourth.Dock = System.Windows.Forms.DockStyle.Top;
            this.PictureTeamFourth.Image = null;
            this.PictureTeamFourth.Location = new System.Drawing.Point(0, 150);
            this.PictureTeamFourth.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamFourth.Name = "PictureTeamFourth";
            this.PictureTeamFourth.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamFourth.SvgImage = null;
            this.PictureTeamFourth.TabIndex = 3;
            this.PictureTeamFourth.TabStop = false;
            // 
            // PictureTeamThird
            // 
            this.PictureTeamThird.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamThird.Dock = System.Windows.Forms.DockStyle.Top;
            this.PictureTeamThird.Image = null;
            this.PictureTeamThird.Location = new System.Drawing.Point(0, 100);
            this.PictureTeamThird.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamThird.Name = "PictureTeamThird";
            this.PictureTeamThird.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamThird.SvgImage = null;
            this.PictureTeamThird.TabIndex = 2;
            this.PictureTeamThird.TabStop = false;
            // 
            // PictureTeamSecond
            // 
            this.PictureTeamSecond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamSecond.Dock = System.Windows.Forms.DockStyle.Top;
            this.PictureTeamSecond.Image = null;
            this.PictureTeamSecond.Location = new System.Drawing.Point(0, 50);
            this.PictureTeamSecond.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamSecond.Name = "PictureTeamSecond";
            this.PictureTeamSecond.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamSecond.SvgImage = null;
            this.PictureTeamSecond.TabIndex = 1;
            this.PictureTeamSecond.TabStop = false;
            // 
            // PictureTeamFirst
            // 
            this.PictureTeamFirst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamFirst.Dock = System.Windows.Forms.DockStyle.Top;
            this.PictureTeamFirst.Image = null;
            this.PictureTeamFirst.Location = new System.Drawing.Point(0, 0);
            this.PictureTeamFirst.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamFirst.Name = "PictureTeamFirst";
            this.PictureTeamFirst.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamFirst.SvgImage = null;
            this.PictureTeamFirst.TabIndex = 0;
            this.PictureTeamFirst.TabStop = false;
            // 
            // BackgroundWorkerTeam
            // 
            this.BackgroundWorkerTeam.WorkerReportsProgress = true;
            this.BackgroundWorkerTeam.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerTeamDoWork);
            this.BackgroundWorkerTeam.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerTeamProgressChanged);
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
            this.LayoutPanelMain.ResumeLayout(false);
            this.LayoutPanelBottom.ResumeLayout(false);
            this.TableLayoutPanelOutter.ResumeLayout(false);
            this.TableLayoutPanelOutter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel LayoutPanelMain;
        private Panel LayoutPanelBottom;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private TableLayoutPanel TableLayoutPanelOutter;
        private Button ButtonOk;
        private Button ButtonCancel;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel1;
        private Controls.SVGViewer PictureTeamSixth;
        private Controls.SVGViewer PictureTeamFifth;
        private Controls.SVGViewer PictureTeamFourth;
        private Controls.SVGViewer PictureTeamThird;
        private Controls.SVGViewer PictureTeamSecond;
        private Controls.SVGViewer PictureTeamFirst;
        private System.ComponentModel.BackgroundWorker BackgroundWorkerTeam;
    }
}