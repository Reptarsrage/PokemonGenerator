namespace PokemonGenerator.Forms
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

            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.PanelFormInputPlayerName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerName)).BeginInit();
            this.PanelFormInputPlayerOutLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerOutLocation)).BeginInit();
            this.PanelFormInputPlayerInLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerInLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // MainWindowBindingSource
            // 
            this.BindingSource.DataSource = typeof(PokemonGenerator.Models.PokeGeneratorPlayerOptions);
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
            this.PanelFormInputPlayerName.Size = new System.Drawing.Size(400, 25);
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
            this.ImagePlayerName.Location = new System.Drawing.Point(375, 0);
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
            this.TextPlayerName.Size = new System.Drawing.Size(375, 25);
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
            this.PanelFormInputPlayerOutLocation.Size = new System.Drawing.Size(400, 25);
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
            this.ImagePlayerOutLocation.Location = new System.Drawing.Point(375, 0);
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
            this.TextPlayerOutLocation.Size = new System.Drawing.Size(375, 25);
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
            this.PanelFormInputPlayerInLocation.Size = new System.Drawing.Size(400, 25);
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
            this.ImagePlayerInLocation.Location = new System.Drawing.Point(375, 0);
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
            this.TextPlayerInLocation.Size = new System.Drawing.Size(375, 25);
            this.TextPlayerInLocation.TabIndex = 7;
            this.TextPlayerInLocation.Validated += new System.EventHandler(this.PlayerValidate);
            // 
            // LabelPlayerName
            // 
            this.LabelPlayerName.AutoSize = true;
            this.LabelPlayerName.Location = new System.Drawing.Point(10, 25);
            this.LabelPlayerName.Margin = new System.Windows.Forms.Padding(0);
            this.LabelPlayerName.Name = "LabelPlayerName";
            this.LabelPlayerName.Size = new System.Drawing.Size(85, 17);
            this.LabelPlayerName.TabIndex = 5;
            this.LabelPlayerName.Text = "Player Name:";
            // 
            // SelectPlayerGame
            // 
            this.SelectPlayerVersion.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.BindingSource, "GameVersion", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "Gold"));
            this.SelectPlayerVersion.DataSource = new object[] {
        ((object)("Gold")),
        ((object)("Silver"))};
            this.SelectPlayerVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectPlayerVersion.FormattingEnabled = true;
            this.SelectPlayerVersion.Location = new System.Drawing.Point(10, 100);
            this.SelectPlayerVersion.Margin = new System.Windows.Forms.Padding(0);
            this.SelectPlayerVersion.Name = "SelectPlayerGame";
            this.SelectPlayerVersion.Size = new System.Drawing.Size(100, 25);
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
            this.LabelPlayerOutLocation.Size = new System.Drawing.Size(127, 17);
            this.LabelPlayerOutLocation.TabIndex = 11;
            this.LabelPlayerOutLocation.Text = "Output sav Location:";
            // 
            // LabelPlayerInLocation
            // 
            this.LabelPlayerInLocation.AutoSize = true;
            this.LabelPlayerInLocation.Location = new System.Drawing.Point(10, 145);
            this.LabelPlayerInLocation.Margin = new System.Windows.Forms.Padding(0);
            this.LabelPlayerInLocation.Name = "LabelPlayerInLocation";
            this.LabelPlayerInLocation.Size = new System.Drawing.Size(121, 17);
            this.LabelPlayerInLocation.TabIndex = 12;
            this.LabelPlayerInLocation.Text = "Game sav Location:";
            // 
            // LabelPlayerGame
            // 
            this.LabelPlayerGame.AutoSize = true;
            this.LabelPlayerGame.Location = new System.Drawing.Point(10, 80);
            this.LabelPlayerGame.Margin = new System.Windows.Forms.Padding(0);
            this.LabelPlayerGame.Name = "LabelPlayerGame";
            this.LabelPlayerGame.Size = new System.Drawing.Size(45, 17);
            this.LabelPlayerGame.TabIndex = 13;
            this.LabelPlayerGame.Text = "Game:";
            // 
            // PlayerOptionsControl
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.PanelFormInputPlayerName);
            this.Controls.Add(this.PanelFormInputPlayerOutLocation);
            this.Controls.Add(this.PanelFormInputPlayerInLocation);
            this.Controls.Add(this.LabelPlayerName);
            this.Controls.Add(this.SelectPlayerVersion);
            this.Controls.Add(this.ButtonPlayerOutLocation);
            this.Controls.Add(this.ButtonPlayerInLocation);
            this.Controls.Add(this.LabelPlayerOutLocation);
            this.Controls.Add(this.LabelPlayerInLocation);
            this.Controls.Add(this.LabelPlayerGame);
            this.Text = "Player Config";
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
            this.ResumeLayout(false);
            this.PerformLayout();
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
    }
}
