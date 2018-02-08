namespace PokemonGenerator.Controls
{
    partial class MainWindow
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
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            this.LabelProjN64Location = new System.Windows.Forms.Label();
            this.TextProjN64Location = new System.Windows.Forms.TextBox();
            this.MainWindowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PanelProgress = new System.Windows.Forms.Panel();
            this.LabelProgress = new System.Windows.Forms.Label();
            this.ImageProgress = new System.Windows.Forms.PictureBox();
            this.SelectLevel = new System.Windows.Forms.NumericUpDown();
            this.LabelLevel = new System.Windows.Forms.Label();
            this.ButtonProjN64Location = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PanelBottom = new System.Windows.Forms.Panel();
            this.ButtonSettings = new System.Windows.Forms.Button();
            this.ButtonGenerate = new System.Windows.Forms.Button();
            this.BackgroundPokemonGenerator = new System.ComponentModel.BackgroundWorker();
            this.PanelOuter = new System.Windows.Forms.Panel();
            this.TableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.PanelTop = new System.Windows.Forms.Panel();
            this.PanelFormInputTop = new System.Windows.Forms.Panel();
            this.ImageProjN64Location = new System.Windows.Forms.PictureBox();
            this.ToolTipProjN64Location = new System.Windows.Forms.ToolTip(this.components);
            this.GroupBoxPlayerOneOptions = new PlayerOptionsGroupBox();
            this.GroupBoxPlayerTwoOptions = new PlayerOptionsGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainWindowBindingSource)).BeginInit();
            this.PanelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectLevel)).BeginInit();
            this.PanelBottom.SuspendLayout();
            this.PanelOuter.SuspendLayout();
            this.TableLayoutPanelMain.SuspendLayout();
            this.PanelTop.SuspendLayout();
            this.PanelFormInputTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageProjN64Location)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelProjN64Location
            // 
            this.LabelProjN64Location.Location = new System.Drawing.Point(9, 21);
            this.LabelProjN64Location.Name = "LabelProjN64Location";
            this.LabelProjN64Location.Size = new System.Drawing.Size(132, 17);
            this.LabelProjN64Location.TabIndex = 5;
            this.LabelProjN64Location.Text = "Project N64 Location:";
            // 
            // TextProjN64Location
            // 
            this.TextProjN64Location.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextProjN64Location.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MainWindowBindingSource, "Project64Location", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, ""));
            this.TextProjN64Location.Location = new System.Drawing.Point(0, 0);
            this.TextProjN64Location.Margin = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.TextProjN64Location.MaxLength = 1000;
            this.TextProjN64Location.Name = "TextProjN64Location";
            this.TextProjN64Location.Size = new System.Drawing.Size(575, 25);
            this.TextProjN64Location.TabIndex = 3;
            this.TextProjN64Location.Validated += new System.EventHandler(this.TextProjN64LocationValidate);
            // 
            // MainWindowBindingSource
            // 
            this.MainWindowBindingSource.DataSource = typeof(PokemonGenerator.Models.Configuration.PlayerOptions);
            // 
            // PanelProgress
            // 
            this.PanelProgress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelProgress.Controls.Add(this.LabelProgress);
            this.PanelProgress.Controls.Add(this.ImageProgress);
            this.PanelProgress.Location = new System.Drawing.Point(350, 200);
            this.PanelProgress.Name = "PanelProgress";
            this.PanelProgress.Size = new System.Drawing.Size(200, 100);
            this.PanelProgress.TabIndex = 1;
            this.PanelProgress.Visible = false;
            // 
            // LabelProgress
            // 
            this.LabelProgress.AutoEllipsis = true;
            this.LabelProgress.Location = new System.Drawing.Point(3, 73);
            this.LabelProgress.Name = "LabelProgress";
            this.LabelProgress.Size = new System.Drawing.Size(194, 28);
            this.LabelProgress.TabIndex = 0;
            this.LabelProgress.Text = "GENERATING...";
            this.LabelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ImageProgress
            // 
            this.ImageProgress.Image = global::PokemonGenerator.Properties.Resources.pikagif;
            this.ImageProgress.Location = new System.Drawing.Point(66, 4);
            this.ImageProgress.Name = "ImageProgress";
            this.ImageProgress.Size = new System.Drawing.Size(62, 66);
            this.ImageProgress.TabIndex = 1;
            this.ImageProgress.TabStop = false;
            // 
            // SelectLevel
            // 
            this.SelectLevel.AutoSize = true;
            this.SelectLevel.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.MainWindowBindingSource, "Level", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, "50"));
            this.SelectLevel.Location = new System.Drawing.Point(66, 10);
            this.SelectLevel.Margin = new System.Windows.Forms.Padding(0);
            this.SelectLevel.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.SelectLevel.Name = "SelectLevel";
            this.SelectLevel.Size = new System.Drawing.Size(140, 25);
            this.SelectLevel.TabIndex = 16;
            this.SelectLevel.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // LabelLevel
            // 
            this.LabelLevel.AutoSize = true;
            this.LabelLevel.Location = new System.Drawing.Point(26, 12);
            this.LabelLevel.Name = "LabelLevel";
            this.LabelLevel.Size = new System.Drawing.Size(40, 17);
            this.LabelLevel.TabIndex = 19;
            this.LabelLevel.Text = "Level:";
            // 
            // ButtonProjN64Location
            // 
            this.ButtonProjN64Location.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonProjN64Location.Image = global::PokemonGenerator.Properties.Resources.OpenfileDialog_16x;
            this.ButtonProjN64Location.Location = new System.Drawing.Point(147, 11);
            this.ButtonProjN64Location.Name = "ButtonProjN64Location";
            this.ButtonProjN64Location.Size = new System.Drawing.Size(95, 33);
            this.ButtonProjN64Location.TabIndex = 2;
            this.ButtonProjN64Location.Text = "Choose File";
            this.ButtonProjN64Location.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonProjN64Location.UseVisualStyleBackColor = true;
            this.ButtonProjN64Location.Click += new System.EventHandler(this.ButtonProjN64LocationClick);
            // 
            // PanelBottom
            // 
            this.PanelBottom.AutoSize = true;
            this.PanelBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TableLayoutPanelMain.SetColumnSpan(this.PanelBottom, 2);
            this.PanelBottom.Controls.Add(this.ButtonSettings);
            this.PanelBottom.Controls.Add(this.ButtonGenerate);
            this.PanelBottom.Controls.Add(this.SelectLevel);
            this.PanelBottom.Controls.Add(this.LabelLevel);
            this.PanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBottom.Location = new System.Drawing.Point(0, 480);
            this.PanelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.PanelBottom.Name = "PanelBottom";
            this.PanelBottom.Size = new System.Drawing.Size(884, 120);
            this.PanelBottom.TabIndex = 16;
            // 
            // ButtonSettings
            // 
            this.ButtonSettings.AutoSize = true;
            this.ButtonSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonSettings.Image = global::PokemonGenerator.Properties.Resources.Settings_16x;
            this.ButtonSettings.Location = new System.Drawing.Point(126, 48);
            this.ButtonSettings.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonSettings.Name = "ButtonSettings";
            this.ButtonSettings.Size = new System.Drawing.Size(80, 27);
            this.ButtonSettings.TabIndex = 20;
            this.ButtonSettings.Text = "Settings";
            this.ButtonSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonSettings.UseVisualStyleBackColor = true;
            this.ButtonSettings.Click += new System.EventHandler(this.ButtonSettings_Click);
            // 
            // ButtonGenerate
            // 
            this.ButtonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonGenerate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonGenerate.Enabled = false;
            this.ButtonGenerate.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonGenerate.Location = new System.Drawing.Point(425, 10);
            this.ButtonGenerate.Margin = new System.Windows.Forms.Padding(10);
            this.ButtonGenerate.Name = "ButtonGenerate";
            this.ButtonGenerate.Size = new System.Drawing.Size(449, 100);
            this.ButtonGenerate.TabIndex = 18;
            this.ButtonGenerate.Text = "Generate (CTRL+F12)";
            this.ButtonGenerate.UseVisualStyleBackColor = true;
            this.ButtonGenerate.Click += new System.EventHandler(this.ButtonGenerateClick);
            // 
            // BackgroundPokemonGenerator
            // 
            this.BackgroundPokemonGenerator.WorkerReportsProgress = true;
            this.BackgroundPokemonGenerator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundPokemonGeneratorDoWork);
            this.BackgroundPokemonGenerator.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundPokemonGeneratorProgressChanged);
            this.BackgroundPokemonGenerator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundPokemonGeneratorCompleted);
            // 
            // PanelOuter
            // 
            this.PanelOuter.AutoSize = true;
            this.PanelOuter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelOuter.Controls.Add(this.TableLayoutPanelMain);
            this.PanelOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelOuter.Location = new System.Drawing.Point(0, 0);
            this.PanelOuter.Margin = new System.Windows.Forms.Padding(0);
            this.PanelOuter.Name = "PanelOuter";
            this.PanelOuter.Size = new System.Drawing.Size(884, 600);
            this.PanelOuter.TabIndex = 0;
            // 
            // TableLayoutPanelMain
            // 
            this.TableLayoutPanelMain.AutoSize = true;
            this.TableLayoutPanelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TableLayoutPanelMain.ColumnCount = 2;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelMain.Controls.Add(this.PanelTop, 0, 0);
            this.TableLayoutPanelMain.Controls.Add(this.PanelBottom, 0, 2);
            this.TableLayoutPanelMain.Controls.Add(this.GroupBoxPlayerOneOptions, 0, 1);
            this.TableLayoutPanelMain.Controls.Add(this.GroupBoxPlayerTwoOptions, 1, 1);
            this.TableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 3;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(884, 600);
            this.TableLayoutPanelMain.TabIndex = 17;
            // 
            // PanelTop
            // 
            this.PanelTop.AutoSize = true;
            this.PanelTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TableLayoutPanelMain.SetColumnSpan(this.PanelTop, 2);
            this.PanelTop.Controls.Add(this.PanelFormInputTop);
            this.PanelTop.Controls.Add(this.LabelProjN64Location);
            this.PanelTop.Controls.Add(this.ButtonProjN64Location);
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelTop.Location = new System.Drawing.Point(0, 0);
            this.PanelTop.Margin = new System.Windows.Forms.Padding(0);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(884, 120);
            this.PanelTop.TabIndex = 18;
            // 
            // PanelFormInputTop
            // 
            this.PanelFormInputTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelFormInputTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelFormInputTop.Controls.Add(this.TextProjN64Location);
            this.PanelFormInputTop.Controls.Add(this.ImageProjN64Location);
            this.PanelFormInputTop.Location = new System.Drawing.Point(10, 50);
            this.PanelFormInputTop.Margin = new System.Windows.Forms.Padding(0);
            this.PanelFormInputTop.MaximumSize = new System.Drawing.Size(700, 25);
            this.PanelFormInputTop.MinimumSize = new System.Drawing.Size(400, 25);
            this.PanelFormInputTop.Name = "PanelFormInputTop";
            this.PanelFormInputTop.Size = new System.Drawing.Size(600, 25);
            this.PanelFormInputTop.TabIndex = 6;
            // 
            // ImageProjN64Location
            // 
            this.ImageProjN64Location.BackgroundImage = global::PokemonGenerator.Properties.Resources.FileError_16x;
            this.ImageProjN64Location.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ImageProjN64Location.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImageProjN64Location.Dock = System.Windows.Forms.DockStyle.Right;
            this.ImageProjN64Location.ErrorImage = null;
            this.ImageProjN64Location.InitialImage = null;
            this.ImageProjN64Location.Location = new System.Drawing.Point(575, 0);
            this.ImageProjN64Location.Margin = new System.Windows.Forms.Padding(0);
            this.ImageProjN64Location.MaximumSize = new System.Drawing.Size(25, 25);
            this.ImageProjN64Location.MinimumSize = new System.Drawing.Size(25, 25);
            this.ImageProjN64Location.Name = "ImageProjN64Location";
            this.ImageProjN64Location.Size = new System.Drawing.Size(25, 25);
            this.ImageProjN64Location.TabIndex = 2;
            this.ImageProjN64Location.TabStop = false;
            this.ToolTipProjN64Location.SetToolTip(this.ImageProjN64Location, "Path to the Project 64 executable must exist");
            this.ImageProjN64Location.Visible = false;
            this.ImageProjN64Location.Click += new System.EventHandler(this.TopSectionValidater);
            // 
            // GroupBoxPlayerOneOptions
            // 
            this.GroupBoxPlayerOneOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBoxPlayerOneOptions.Location = new System.Drawing.Point(3, 123);
            this.GroupBoxPlayerOneOptions.Name = "GroupBoxPlayerOneOptions";
            this.GroupBoxPlayerOneOptions.Size = new System.Drawing.Size(436, 354);
            this.GroupBoxPlayerOneOptions.TabIndex = 19;
            this.GroupBoxPlayerOneOptions.TabStop = false;
            this.GroupBoxPlayerOneOptions.Text = "Player One Config";
            // 
            // GroupBoxPlayerTwoOptions
            // 
            this.GroupBoxPlayerTwoOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBoxPlayerTwoOptions.Location = new System.Drawing.Point(445, 123);
            this.GroupBoxPlayerTwoOptions.Name = "GroupBoxPlayerTwoOptions";
            this.GroupBoxPlayerTwoOptions.Size = new System.Drawing.Size(436, 354);
            this.GroupBoxPlayerTwoOptions.TabIndex = 20;
            this.GroupBoxPlayerTwoOptions.TabStop = false;
            this.GroupBoxPlayerTwoOptions.Text = "Player Two Config";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelOuter);
            this.Controls.Add(this.PanelProgress);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainWindow";
            this.Size = new System.Drawing.Size(884, 600);
            ((System.ComponentModel.ISupportInitialize)(this.MainWindowBindingSource)).EndInit();
            this.PanelProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImageProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectLevel)).EndInit();
            this.PanelBottom.ResumeLayout(false);
            this.PanelBottom.PerformLayout();
            this.PanelOuter.ResumeLayout(false);
            this.PanelOuter.PerformLayout();
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.PerformLayout();
            this.PanelTop.ResumeLayout(false);
            this.PanelFormInputTop.ResumeLayout(false);
            this.PanelFormInputTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageProjN64Location)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.HelpProvider HelpProvider;
        private System.Windows.Forms.Label LabelProjN64Location;
        private System.Windows.Forms.TextBox TextProjN64Location;
        private System.Windows.Forms.NumericUpDown SelectLevel;
        private System.Windows.Forms.Label LabelLevel;
        private System.Windows.Forms.Button ButtonProjN64Location;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Panel PanelBottom;
        private System.Windows.Forms.Button ButtonGenerate;
        private System.Windows.Forms.Panel PanelProgress;
        private System.Windows.Forms.PictureBox ImageProgress;
        private System.Windows.Forms.Label LabelProgress;
        private System.Windows.Forms.PictureBox ImageProjN64Location;
        private System.ComponentModel.BackgroundWorker BackgroundPokemonGenerator;
        private System.Windows.Forms.Panel PanelOuter;
        private System.Windows.Forms.ToolTip ToolTipProjN64Location;
        private System.Windows.Forms.BindingSource MainWindowBindingSource;
        private System.Windows.Forms.Button ButtonSettings;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        private System.Windows.Forms.Panel PanelTop;
        private System.Windows.Forms.Panel PanelFormInputTop;
        private PlayerOptionsGroupBox GroupBoxPlayerOneOptions;
        private PlayerOptionsGroupBox GroupBoxPlayerTwoOptions;
    }
}
