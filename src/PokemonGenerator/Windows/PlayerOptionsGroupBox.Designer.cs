namespace PokemonGenerator.Windows
{
    partial class PlayerOptionsGroupBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerOptionsGroupBox));
            this.BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PanelFormInputPlayerName = new System.Windows.Forms.Panel();
            this.ImagePlayerName = new System.Windows.Forms.PictureBox();
            this.TextPlayerName = new System.Windows.Forms.TextBox();
            this.PanelFormInputPlayerOutLocation = new System.Windows.Forms.Panel();
            this.ImagePlayerOutLocation = new System.Windows.Forms.PictureBox();
            this.TextPlayerOutLocation = new System.Windows.Forms.TextBox();
            this.PanelFormInputPlayerInLocation = new System.Windows.Forms.Panel();
            this.ImagePlayerInLocation = new System.Windows.Forms.PictureBox();
            this.TextPlayerInLocation = new System.Windows.Forms.TextBox();
            this.LabelPlayerName = new System.Windows.Forms.Label();
            this.SelectPlayerVersion = new System.Windows.Forms.ComboBox();
            this.ButtonPlayerOutLocation = new System.Windows.Forms.Button();
            this.ButtonPlayerInLocation = new System.Windows.Forms.Button();
            this.LabelPlayerOutLocation = new System.Windows.Forms.Label();
            this.LabelPlayerInLocation = new System.Windows.Forms.Label();
            this.LabelPlayerGame = new System.Windows.Forms.Label();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.LabelTeam = new System.Windows.Forms.Label();
            this.PanelTeam = new System.Windows.Forms.Panel();
            this.PictureTeamSixth = new PokemonGenerator.Controls.SVGViewer();
            this.PictureTeamFifth = new PokemonGenerator.Controls.SVGViewer();
            this.PictureTeamFourth = new PokemonGenerator.Controls.SVGViewer();
            this.PictureTeamThird = new PokemonGenerator.Controls.SVGViewer();
            this.PictureTeamSecond = new PokemonGenerator.Controls.SVGViewer();
            this.PictureTeamFirst = new PokemonGenerator.Controls.SVGViewer();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.PanelFormInputPlayerName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerName)).BeginInit();
            this.PanelFormInputPlayerOutLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerOutLocation)).BeginInit();
            this.PanelFormInputPlayerInLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerInLocation)).BeginInit();
            this.GroupBox.SuspendLayout();
            this.PanelTeam.SuspendLayout();
            this.SuspendLayout();
            // 
            // BindingSource
            // 
            this.BindingSource.DataSource = typeof(PokemonGenerator.Models.Configuration.PlayerOptions);
            // 
            // PanelFormInputPlayerName
            // 
            this.PanelFormInputPlayerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelFormInputPlayerName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelFormInputPlayerName.Controls.Add(this.ImagePlayerName);
            this.PanelFormInputPlayerName.Controls.Add(this.TextPlayerName);
            this.PanelFormInputPlayerName.Location = new System.Drawing.Point(10, 50);
            this.PanelFormInputPlayerName.Margin = new System.Windows.Forms.Padding(0);
            this.PanelFormInputPlayerName.MaximumSize = new System.Drawing.Size(700, 25);
            this.PanelFormInputPlayerName.MinimumSize = new System.Drawing.Size(200, 25);
            this.PanelFormInputPlayerName.Name = "PanelFormInputPlayerName";
            this.PanelFormInputPlayerName.Size = new System.Drawing.Size(700, 25);
            this.PanelFormInputPlayerName.TabIndex = 6;
            // 
            // ImagePlayerName
            // 
            this.ImagePlayerName.BackgroundImage = global::PokemonGenerator.Properties.Resources.StatusInvalid_16x;
            this.ImagePlayerName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ImagePlayerName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImagePlayerName.Dock = System.Windows.Forms.DockStyle.Right;
            this.ImagePlayerName.ErrorImage = null;
            this.ImagePlayerName.InitialImage = null;
            this.ImagePlayerName.Location = new System.Drawing.Point(675, 0);
            this.ImagePlayerName.Margin = new System.Windows.Forms.Padding(0);
            this.ImagePlayerName.MaximumSize = new System.Drawing.Size(25, 25);
            this.ImagePlayerName.MinimumSize = new System.Drawing.Size(25, 25);
            this.ImagePlayerName.Name = "ImagePlayerName";
            this.ImagePlayerName.Size = new System.Drawing.Size(25, 25);
            this.ImagePlayerName.TabIndex = 0;
            this.ImagePlayerName.TabStop = false;
            this.ImagePlayerName.Visible = false;
            this.ImagePlayerName.Click += new System.EventHandler(this.PlayerValidate);
            // 
            // TextPlayerName
            // 
            this.TextPlayerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextPlayerName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, ""));
            this.TextPlayerName.Location = new System.Drawing.Point(0, 0);
            this.TextPlayerName.Margin = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.TextPlayerName.MaxLength = 8;
            this.TextPlayerName.Name = "TextPlayerName";
            this.TextPlayerName.Size = new System.Drawing.Size(675, 20);
            this.TextPlayerName.TabIndex = 4;
            this.TextPlayerName.Validated += new System.EventHandler(this.PlayerValidate);
            // 
            // PanelFormInputPlayerOutLocation
            // 
            this.PanelFormInputPlayerOutLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelFormInputPlayerOutLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelFormInputPlayerOutLocation.Controls.Add(this.ImagePlayerOutLocation);
            this.PanelFormInputPlayerOutLocation.Controls.Add(this.TextPlayerOutLocation);
            this.PanelFormInputPlayerOutLocation.Location = new System.Drawing.Point(10, 250);
            this.PanelFormInputPlayerOutLocation.Margin = new System.Windows.Forms.Padding(0);
            this.PanelFormInputPlayerOutLocation.MaximumSize = new System.Drawing.Size(700, 25);
            this.PanelFormInputPlayerOutLocation.MinimumSize = new System.Drawing.Size(200, 25);
            this.PanelFormInputPlayerOutLocation.Name = "PanelFormInputPlayerOutLocation";
            this.PanelFormInputPlayerOutLocation.Size = new System.Drawing.Size(700, 25);
            this.PanelFormInputPlayerOutLocation.TabIndex = 6;
            // 
            // ImagePlayerOutLocation
            // 
            this.ImagePlayerOutLocation.BackgroundImage = global::PokemonGenerator.Properties.Resources.FileError_16x;
            this.ImagePlayerOutLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ImagePlayerOutLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImagePlayerOutLocation.Dock = System.Windows.Forms.DockStyle.Right;
            this.ImagePlayerOutLocation.ErrorImage = null;
            this.ImagePlayerOutLocation.InitialImage = null;
            this.ImagePlayerOutLocation.Location = new System.Drawing.Point(675, 0);
            this.ImagePlayerOutLocation.Margin = new System.Windows.Forms.Padding(0);
            this.ImagePlayerOutLocation.MaximumSize = new System.Drawing.Size(25, 25);
            this.ImagePlayerOutLocation.MinimumSize = new System.Drawing.Size(25, 25);
            this.ImagePlayerOutLocation.Name = "ImagePlayerOutLocation";
            this.ImagePlayerOutLocation.Size = new System.Drawing.Size(25, 25);
            this.ImagePlayerOutLocation.TabIndex = 9;
            this.ImagePlayerOutLocation.TabStop = false;
            this.ImagePlayerOutLocation.Visible = false;
            this.ImagePlayerOutLocation.Click += new System.EventHandler(this.PlayerValidate);
            // 
            // TextPlayerOutLocation
            // 
            this.TextPlayerOutLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextPlayerOutLocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "OutputSaveLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, ""));
            this.TextPlayerOutLocation.Location = new System.Drawing.Point(0, 0);
            this.TextPlayerOutLocation.Margin = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.TextPlayerOutLocation.MaxLength = 500;
            this.TextPlayerOutLocation.Name = "TextPlayerOutLocation";
            this.TextPlayerOutLocation.Size = new System.Drawing.Size(675, 20);
            this.TextPlayerOutLocation.TabIndex = 9;
            this.TextPlayerOutLocation.Validated += new System.EventHandler(this.PlayerValidate);
            // 
            // PanelFormInputPlayerInLocation
            // 
            this.PanelFormInputPlayerInLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelFormInputPlayerInLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelFormInputPlayerInLocation.Controls.Add(this.ImagePlayerInLocation);
            this.PanelFormInputPlayerInLocation.Controls.Add(this.TextPlayerInLocation);
            this.PanelFormInputPlayerInLocation.Location = new System.Drawing.Point(10, 175);
            this.PanelFormInputPlayerInLocation.Margin = new System.Windows.Forms.Padding(0);
            this.PanelFormInputPlayerInLocation.MaximumSize = new System.Drawing.Size(700, 25);
            this.PanelFormInputPlayerInLocation.MinimumSize = new System.Drawing.Size(200, 25);
            this.PanelFormInputPlayerInLocation.Name = "PanelFormInputPlayerInLocation";
            this.PanelFormInputPlayerInLocation.Size = new System.Drawing.Size(700, 25);
            this.PanelFormInputPlayerInLocation.TabIndex = 6;
            // 
            // ImagePlayerInLocation
            // 
            this.ImagePlayerInLocation.BackgroundImage = global::PokemonGenerator.Properties.Resources.FileError_16x;
            this.ImagePlayerInLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ImagePlayerInLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImagePlayerInLocation.Dock = System.Windows.Forms.DockStyle.Right;
            this.ImagePlayerInLocation.ErrorImage = null;
            this.ImagePlayerInLocation.InitialImage = null;
            this.ImagePlayerInLocation.Location = new System.Drawing.Point(675, 0);
            this.ImagePlayerInLocation.Margin = new System.Windows.Forms.Padding(0);
            this.ImagePlayerInLocation.MaximumSize = new System.Drawing.Size(25, 25);
            this.ImagePlayerInLocation.MinimumSize = new System.Drawing.Size(25, 25);
            this.ImagePlayerInLocation.Name = "ImagePlayerInLocation";
            this.ImagePlayerInLocation.Size = new System.Drawing.Size(25, 25);
            this.ImagePlayerInLocation.TabIndex = 10;
            this.ImagePlayerInLocation.TabStop = false;
            this.ImagePlayerInLocation.Visible = false;
            this.ImagePlayerInLocation.Click += new System.EventHandler(this.PlayerValidate);
            // 
            // TextPlayerInLocation
            // 
            this.TextPlayerInLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextPlayerInLocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "InputSaveLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, ""));
            this.TextPlayerInLocation.Location = new System.Drawing.Point(0, 0);
            this.TextPlayerInLocation.Margin = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.TextPlayerInLocation.MaxLength = 500;
            this.TextPlayerInLocation.Name = "TextPlayerInLocation";
            this.TextPlayerInLocation.Size = new System.Drawing.Size(675, 20);
            this.TextPlayerInLocation.TabIndex = 7;
            this.TextPlayerInLocation.Validated += new System.EventHandler(this.PlayerValidate);
            // 
            // LabelPlayerName
            // 
            this.LabelPlayerName.AutoSize = true;
            this.LabelPlayerName.Location = new System.Drawing.Point(10, 25);
            this.LabelPlayerName.Margin = new System.Windows.Forms.Padding(0);
            this.LabelPlayerName.Name = "LabelPlayerName";
            this.LabelPlayerName.Size = new System.Drawing.Size(70, 13);
            this.LabelPlayerName.TabIndex = 5;
            this.LabelPlayerName.Text = "Player Name:";
            // 
            // SelectPlayerVersion
            // 
            this.SelectPlayerVersion.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.BindingSource, "GameVersion", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "Gold"));
            this.SelectPlayerVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectPlayerVersion.FormattingEnabled = true;
            this.SelectPlayerVersion.Location = new System.Drawing.Point(10, 100);
            this.SelectPlayerVersion.Margin = new System.Windows.Forms.Padding(0);
            this.SelectPlayerVersion.Name = "SelectPlayerVersion";
            this.SelectPlayerVersion.Size = new System.Drawing.Size(100, 21);
            this.SelectPlayerVersion.TabIndex = 5;
            // 
            // ButtonPlayerOutLocation
            // 
            this.ButtonPlayerOutLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPlayerOutLocation.Image = global::PokemonGenerator.Properties.Resources.OpenfileDialog_16x;
            this.ButtonPlayerOutLocation.Location = new System.Drawing.Point(140, 214);
            this.ButtonPlayerOutLocation.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonPlayerOutLocation.Name = "ButtonPlayerOutLocation";
            this.ButtonPlayerOutLocation.Size = new System.Drawing.Size(99, 30);
            this.ButtonPlayerOutLocation.TabIndex = 8;
            this.ButtonPlayerOutLocation.Text = "Choose File";
            this.ButtonPlayerOutLocation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonPlayerOutLocation.UseVisualStyleBackColor = true;
            this.ButtonPlayerOutLocation.Click += new System.EventHandler(this.ButtonPlayerOutLocationClick);
            // 
            // ButtonPlayerInLocation
            // 
            this.ButtonPlayerInLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPlayerInLocation.Image = global::PokemonGenerator.Properties.Resources.OpenfileDialog_16x;
            this.ButtonPlayerInLocation.Location = new System.Drawing.Point(140, 139);
            this.ButtonPlayerInLocation.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonPlayerInLocation.Name = "ButtonPlayerInLocation";
            this.ButtonPlayerInLocation.Size = new System.Drawing.Size(99, 31);
            this.ButtonPlayerInLocation.TabIndex = 6;
            this.ButtonPlayerInLocation.Text = "Choose File";
            this.ButtonPlayerInLocation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonPlayerInLocation.UseVisualStyleBackColor = true;
            this.ButtonPlayerInLocation.Click += new System.EventHandler(this.ButtonPlayerInLocationClick);
            // 
            // LabelPlayerOutLocation
            // 
            this.LabelPlayerOutLocation.AutoSize = true;
            this.LabelPlayerOutLocation.Location = new System.Drawing.Point(10, 220);
            this.LabelPlayerOutLocation.Margin = new System.Windows.Forms.Padding(0);
            this.LabelPlayerOutLocation.Name = "LabelPlayerOutLocation";
            this.LabelPlayerOutLocation.Size = new System.Drawing.Size(106, 13);
            this.LabelPlayerOutLocation.TabIndex = 11;
            this.LabelPlayerOutLocation.Text = "Output sav Location:";
            // 
            // LabelPlayerInLocation
            // 
            this.LabelPlayerInLocation.AutoSize = true;
            this.LabelPlayerInLocation.Location = new System.Drawing.Point(10, 145);
            this.LabelPlayerInLocation.Margin = new System.Windows.Forms.Padding(0);
            this.LabelPlayerInLocation.Name = "LabelPlayerInLocation";
            this.LabelPlayerInLocation.Size = new System.Drawing.Size(102, 13);
            this.LabelPlayerInLocation.TabIndex = 12;
            this.LabelPlayerInLocation.Text = "Game sav Location:";
            // 
            // LabelPlayerGame
            // 
            this.LabelPlayerGame.AutoSize = true;
            this.LabelPlayerGame.Location = new System.Drawing.Point(10, 80);
            this.LabelPlayerGame.Margin = new System.Windows.Forms.Padding(0);
            this.LabelPlayerGame.Name = "LabelPlayerGame";
            this.LabelPlayerGame.Size = new System.Drawing.Size(38, 13);
            this.LabelPlayerGame.TabIndex = 13;
            this.LabelPlayerGame.Text = "Game:";
            // 
            // GroupBox
            // 
            this.GroupBox.Controls.Add(this.LabelTeam);
            this.GroupBox.Controls.Add(this.PanelTeam);
            this.GroupBox.Controls.Add(this.PanelFormInputPlayerName);
            this.GroupBox.Controls.Add(this.PanelFormInputPlayerOutLocation);
            this.GroupBox.Controls.Add(this.PanelFormInputPlayerInLocation);
            this.GroupBox.Controls.Add(this.LabelPlayerName);
            this.GroupBox.Controls.Add(this.SelectPlayerVersion);
            this.GroupBox.Controls.Add(this.ButtonPlayerOutLocation);
            this.GroupBox.Controls.Add(this.ButtonPlayerInLocation);
            this.GroupBox.Controls.Add(this.LabelPlayerOutLocation);
            this.GroupBox.Controls.Add(this.LabelPlayerInLocation);
            this.GroupBox.Controls.Add(this.LabelPlayerGame);
            this.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox.Location = new System.Drawing.Point(0, 0);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new System.Drawing.Size(800, 600);
            this.GroupBox.TabIndex = 0;
            this.GroupBox.TabStop = false;
            this.GroupBox.Text = "Player Config";
            // 
            // LabelTeam
            // 
            this.LabelTeam.AutoSize = true;
            this.LabelTeam.Location = new System.Drawing.Point(10, 300);
            this.LabelTeam.Margin = new System.Windows.Forms.Padding(0);
            this.LabelTeam.Name = "LabelTeam";
            this.LabelTeam.Size = new System.Drawing.Size(37, 13);
            this.LabelTeam.TabIndex = 15;
            this.LabelTeam.Text = "Team:";
            // 
            // PanelTeam
            // 
            this.PanelTeam.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.PanelTeam.Controls.Add(this.PictureTeamSixth);
            this.PanelTeam.Controls.Add(this.PictureTeamFifth);
            this.PanelTeam.Controls.Add(this.PictureTeamFourth);
            this.PanelTeam.Controls.Add(this.PictureTeamThird);
            this.PanelTeam.Controls.Add(this.PictureTeamSecond);
            this.PanelTeam.Controls.Add(this.PictureTeamFirst);
            this.PanelTeam.Location = new System.Drawing.Point(10, 320);
            this.PanelTeam.Name = "PanelTeam";
            this.PanelTeam.Size = new System.Drawing.Size(300, 50);
            this.PanelTeam.TabIndex = 14;
            this.PanelTeam.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TeamClick);
            // 
            // PictureTeamSixth
            // 
            this.PictureTeamSixth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamSixth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureTeamSixth.Dock = System.Windows.Forms.DockStyle.Left;
            this.PictureTeamSixth.Location = new System.Drawing.Point(250, 0);
            this.PictureTeamSixth.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamSixth.Name = "PictureTeamSixth";
            this.PictureTeamSixth.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamSixth.TabIndex = 5;
            this.PictureTeamSixth.TabStop = false;
            this.PictureTeamSixth.Click += new System.EventHandler(this.TeamClick);
            this.PictureTeamSixth.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TeamClick);
            // 
            // PictureTeamFifth
            // 
            this.PictureTeamFifth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamFifth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureTeamFifth.Dock = System.Windows.Forms.DockStyle.Left;
            this.PictureTeamFifth.Location = new System.Drawing.Point(200, 0);
            this.PictureTeamFifth.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamFifth.Name = "PictureTeamFifth";
            this.PictureTeamFifth.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamFifth.TabIndex = 4;
            this.PictureTeamFifth.TabStop = false;
            this.PictureTeamFifth.Click += new System.EventHandler(this.TeamClick);
            this.PictureTeamFifth.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TeamClick);
            // 
            // PictureTeamFourth
            // 
            this.PictureTeamFourth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamFourth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureTeamFourth.Dock = System.Windows.Forms.DockStyle.Left;
            this.PictureTeamFourth.Location = new System.Drawing.Point(150, 0);
            this.PictureTeamFourth.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamFourth.Name = "PictureTeamFourth";
            this.PictureTeamFourth.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamFourth.TabIndex = 3;
            this.PictureTeamFourth.TabStop = false;
            this.PictureTeamFourth.Click += new System.EventHandler(this.TeamClick);
            this.PictureTeamFourth.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TeamClick);
            // 
            // PictureTeamThird
            // 
            this.PictureTeamThird.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamThird.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureTeamThird.Dock = System.Windows.Forms.DockStyle.Left;
            this.PictureTeamThird.Location = new System.Drawing.Point(100, 0);
            this.PictureTeamThird.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamThird.Name = "PictureTeamThird";
            this.PictureTeamThird.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamThird.TabIndex = 2;
            this.PictureTeamThird.TabStop = false;
            this.PictureTeamThird.Click += new System.EventHandler(this.TeamClick);
            this.PictureTeamThird.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TeamClick);
            // 
            // PictureTeamSecond
            // 
            this.PictureTeamSecond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamSecond.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureTeamSecond.Dock = System.Windows.Forms.DockStyle.Left;
            this.PictureTeamSecond.Location = new System.Drawing.Point(50, 0);
            this.PictureTeamSecond.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamSecond.Name = "PictureTeamSecond";
            this.PictureTeamSecond.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamSecond.TabIndex = 1;
            this.PictureTeamSecond.TabStop = false;
            this.PictureTeamSecond.Click += new System.EventHandler(this.TeamClick);
            this.PictureTeamSecond.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TeamClick);
            // 
            // PictureTeamFirst
            // 
            this.PictureTeamFirst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureTeamFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureTeamFirst.Dock = System.Windows.Forms.DockStyle.Left;
            this.PictureTeamFirst.Location = new System.Drawing.Point(0, 0);
            this.PictureTeamFirst.Margin = new System.Windows.Forms.Padding(0);
            this.PictureTeamFirst.Name = "PictureTeamFirst";
            this.PictureTeamFirst.Size = new System.Drawing.Size(50, 50);
            this.PictureTeamFirst.TabIndex = 0;
            this.PictureTeamFirst.TabStop = false;
            this.PictureTeamFirst.Click += new System.EventHandler(this.TeamClick);
            this.PictureTeamFirst.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TeamClick);
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.WorkerReportsProgress = true;
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerDoWork);
            this.BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerProgressChanged);
            // 
            // PlayerOptionsGroupBox
            // 
            this.Controls.Add(this.GroupBox);
            this.Name = "PlayerOptionsGroupBox";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.PanelFormInputPlayerName.ResumeLayout(false);
            this.PanelFormInputPlayerName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerName)).EndInit();
            this.PanelFormInputPlayerOutLocation.ResumeLayout(false);
            this.PanelFormInputPlayerOutLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerOutLocation)).EndInit();
            this.PanelFormInputPlayerInLocation.ResumeLayout(false);
            this.PanelFormInputPlayerInLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerInLocation)).EndInit();
            this.GroupBox.ResumeLayout(false);
            this.GroupBox.PerformLayout();
            this.PanelTeam.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox TextPlayerOutLocation;
        private System.Windows.Forms.Label LabelPlayerOutLocation;
        private System.Windows.Forms.TextBox TextPlayerInLocation;
        private System.Windows.Forms.Label LabelPlayerInLocation;
        private System.Windows.Forms.Label LabelPlayerGame;
        private System.Windows.Forms.PictureBox ImagePlayerOutLocation;
        private System.Windows.Forms.PictureBox ImagePlayerInLocation;
        private System.Windows.Forms.Button ButtonPlayerOutLocation;
        private System.Windows.Forms.Button ButtonPlayerInLocation;
        private System.Windows.Forms.ComboBox SelectPlayerVersion;
        private System.Windows.Forms.TextBox TextPlayerName;
        private System.Windows.Forms.Label LabelPlayerName;
        private System.Windows.Forms.PictureBox ImagePlayerName;
        private System.Windows.Forms.BindingSource BindingSource;
        private System.Windows.Forms.Panel PanelFormInputPlayerName;
        private System.Windows.Forms.Panel PanelFormInputPlayerOutLocation;
        private System.Windows.Forms.Panel PanelFormInputPlayerInLocation;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.Label LabelTeam;
        private System.Windows.Forms.Panel PanelTeam;
        private Controls.SVGViewer PictureTeamFirst;
        private Controls.SVGViewer PictureTeamSixth;
        private Controls.SVGViewer PictureTeamFifth;
        private Controls.SVGViewer PictureTeamFourth;
        private Controls.SVGViewer PictureTeamThird;
        private Controls.SVGViewer PictureTeamSecond;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
    }
}
