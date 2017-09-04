namespace PokemonGenerator.Forms
{
    partial class PokemonGeneratorForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokemonGeneratorForm));
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            this.LabelProjN64Location = new System.Windows.Forms.Label();
            this.TextProjN64Location = new System.Windows.Forms.TextBox();
            this.GroupBoxPlayerOne = new System.Windows.Forms.GroupBox();
            this.ImagePlayerOneName = new System.Windows.Forms.PictureBox();
            this.TextPlayerOneName = new System.Windows.Forms.TextBox();
            this.LabelPlayerOneName = new System.Windows.Forms.Label();
            this.SelectPlayerOneGame = new System.Windows.Forms.ComboBox();
            this.ButtonPlayerOneOutLocation = new System.Windows.Forms.Button();
            this.ButtonPlayerOneInLocation = new System.Windows.Forms.Button();
            this.ImagePlayerOneOutLocation = new System.Windows.Forms.PictureBox();
            this.ImagePlayerOneInLocation = new System.Windows.Forms.PictureBox();
            this.TextPlayerOneOutLocation = new System.Windows.Forms.TextBox();
            this.LabelPlayerOneOutLocation = new System.Windows.Forms.Label();
            this.TextPlayerOneInLocation = new System.Windows.Forms.TextBox();
            this.LabelPlayerOneInLocation = new System.Windows.Forms.Label();
            this.LabelPlayerOneGame = new System.Windows.Forms.Label();
            this.PanelProgress = new System.Windows.Forms.Panel();
            this.LabelProgress = new System.Windows.Forms.Label();
            this.ImageProgress = new System.Windows.Forms.PictureBox();
            this.GroupBoxPlayerTwo = new System.Windows.Forms.GroupBox();
            this.ImagePlayerTwoName = new System.Windows.Forms.PictureBox();
            this.TextPlayerTwoName = new System.Windows.Forms.TextBox();
            this.LabelPlayerTwoName = new System.Windows.Forms.Label();
            this.SelectPlayerTwoGame = new System.Windows.Forms.ComboBox();
            this.ButtonPlayerTwoOutLocation = new System.Windows.Forms.Button();
            this.ButtonPlayerTwoInLocation = new System.Windows.Forms.Button();
            this.ImagePlayerTwoOutLocation = new System.Windows.Forms.PictureBox();
            this.ImagePlayerTwoInLocation = new System.Windows.Forms.PictureBox();
            this.TextPlayerTwoOutLocation = new System.Windows.Forms.TextBox();
            this.LabelPlayerTwoOutLocation = new System.Windows.Forms.Label();
            this.TextPlayerTwoInLocation = new System.Windows.Forms.TextBox();
            this.LabelPlayerTwoInLocation = new System.Windows.Forms.Label();
            this.LabelPlayerTwoGame = new System.Windows.Forms.Label();
            this.SelectLevel = new System.Windows.Forms.NumericUpDown();
            this.LabelLevel = new System.Windows.Forms.Label();
            this.SelectEntropy = new System.Windows.Forms.ComboBox();
            this.LabelEntropy = new System.Windows.Forms.Label();
            this.ButtonProjN64Location = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.GroupBoxBottom = new System.Windows.Forms.GroupBox();
            this.ButtonGenerate = new System.Windows.Forms.Button();
            this.BackgroundPokemonGenerator = new System.ComponentModel.BackgroundWorker();
            this.GroupBoxOuter = new System.Windows.Forms.GroupBox();
            this.ImageProjN64Location = new System.Windows.Forms.PictureBox();
            this.ToolTipProjN64Location = new System.Windows.Forms.ToolTip(this.components);
            this.GroupBoxPlayerOne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerOneName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerOneOutLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerOneInLocation)).BeginInit();
            this.PanelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageProgress)).BeginInit();
            this.GroupBoxPlayerTwo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerTwoName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerTwoOutLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerTwoInLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectLevel)).BeginInit();
            this.GroupBoxBottom.SuspendLayout();
            this.GroupBoxOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageProjN64Location)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelProjN64Location
            // 
            this.LabelProjN64Location.AutoSize = true;
            this.LabelProjN64Location.Location = new System.Drawing.Point(22, 18);
            this.LabelProjN64Location.Name = "LabelProjN64Location";
            this.LabelProjN64Location.Size = new System.Drawing.Size(132, 17);
            this.LabelProjN64Location.TabIndex = 5;
            this.LabelProjN64Location.Text = "Project N64 Location:";
            // 
            // TextProjN64Location
            // 
            this.TextProjN64Location.Location = new System.Drawing.Point(24, 42);
            this.TextProjN64Location.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextProjN64Location.MaxLength = 500;
            this.TextProjN64Location.Name = "TextProjN64Location";
            this.TextProjN64Location.Size = new System.Drawing.Size(878, 25);
            this.TextProjN64Location.TabIndex = 3;
            this.TextProjN64Location.Validated += new System.EventHandler(this.TextProjN64LocationValidate);
            // 
            // GroupBoxPlayerOne
            // 
            this.GroupBoxPlayerOne.Controls.Add(this.ImagePlayerOneName);
            this.GroupBoxPlayerOne.Controls.Add(this.TextPlayerOneName);
            this.GroupBoxPlayerOne.Controls.Add(this.LabelPlayerOneName);
            this.GroupBoxPlayerOne.Controls.Add(this.SelectPlayerOneGame);
            this.GroupBoxPlayerOne.Controls.Add(this.ButtonPlayerOneOutLocation);
            this.GroupBoxPlayerOne.Controls.Add(this.ButtonPlayerOneInLocation);
            this.GroupBoxPlayerOne.Controls.Add(this.ImagePlayerOneOutLocation);
            this.GroupBoxPlayerOne.Controls.Add(this.ImagePlayerOneInLocation);
            this.GroupBoxPlayerOne.Controls.Add(this.TextPlayerOneOutLocation);
            this.GroupBoxPlayerOne.Controls.Add(this.LabelPlayerOneOutLocation);
            this.GroupBoxPlayerOne.Controls.Add(this.TextPlayerOneInLocation);
            this.GroupBoxPlayerOne.Controls.Add(this.LabelPlayerOneInLocation);
            this.GroupBoxPlayerOne.Controls.Add(this.LabelPlayerOneGame);
            this.GroupBoxPlayerOne.Enabled = false;
            this.GroupBoxPlayerOne.Location = new System.Drawing.Point(16, 90);
            this.GroupBoxPlayerOne.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBoxPlayerOne.Name = "GroupBoxPlayerOne";
            this.GroupBoxPlayerOne.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBoxPlayerOne.Size = new System.Drawing.Size(443, 243);
            this.GroupBoxPlayerOne.TabIndex = 4;
            this.GroupBoxPlayerOne.TabStop = false;
            this.GroupBoxPlayerOne.Text = "Player 1 Config";
            // 
            // ImagePlayerOneName
            // 
            this.ImagePlayerOneName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImagePlayerOneName.ErrorImage = null;
            this.ImagePlayerOneName.Image = global::PokemonGenerator.Properties.Resources.BAD;
            this.ImagePlayerOneName.InitialImage = null;
            this.ImagePlayerOneName.Location = new System.Drawing.Point(328, 43);
            this.ImagePlayerOneName.Name = "ImagePlayerOneName";
            this.ImagePlayerOneName.Size = new System.Drawing.Size(25, 25);
            this.ImagePlayerOneName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImagePlayerOneName.TabIndex = 0;
            this.ImagePlayerOneName.TabStop = false;
            this.ToolTipProjN64Location.SetToolTip(this.ImagePlayerOneName, "Player name must be alpha numeric, and between one and eight characters long");
            this.ImagePlayerOneName.Visible = false;
            this.ImagePlayerOneName.Click += new System.EventHandler(this.PlayerValidater);
            // 
            // TextPlayerOneName
            // 
            this.TextPlayerOneName.Location = new System.Drawing.Point(8, 43);
            this.TextPlayerOneName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextPlayerOneName.MaxLength = 8;
            this.TextPlayerOneName.Name = "TextPlayerOneName";
            this.TextPlayerOneName.Size = new System.Drawing.Size(314, 25);
            this.TextPlayerOneName.TabIndex = 4;
            this.TextPlayerOneName.Validated += new System.EventHandler(this.PlayerValidate);
            // 
            // LabelPlayerOneName
            // 
            this.LabelPlayerOneName.AutoSize = true;
            this.LabelPlayerOneName.Location = new System.Drawing.Point(5, 22);
            this.LabelPlayerOneName.Name = "LabelPlayerOneName";
            this.LabelPlayerOneName.Size = new System.Drawing.Size(85, 17);
            this.LabelPlayerOneName.TabIndex = 5;
            this.LabelPlayerOneName.Text = "Player Name:";
            // 
            // SelectPlayerOneGame
            // 
            this.SelectPlayerOneGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectPlayerOneGame.FormattingEnabled = true;
            this.SelectPlayerOneGame.Items.AddRange(new object[] {
            "Gold",
            "Silver"});
            this.SelectPlayerOneGame.Location = new System.Drawing.Point(8, 94);
            this.SelectPlayerOneGame.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SelectPlayerOneGame.Name = "SelectPlayerOneGame";
            this.SelectPlayerOneGame.Size = new System.Drawing.Size(315, 25);
            this.SelectPlayerOneGame.TabIndex = 5;
            // 
            // ButtonPlayerOneOutLocation
            // 
            this.ButtonPlayerOneOutLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPlayerOneOutLocation.Location = new System.Drawing.Point(140, 187);
            this.ButtonPlayerOneOutLocation.Name = "ButtonPlayerOneOutLocation";
            this.ButtonPlayerOneOutLocation.Size = new System.Drawing.Size(77, 21);
            this.ButtonPlayerOneOutLocation.TabIndex = 8;
            this.ButtonPlayerOneOutLocation.Text = "Choose File";
            this.ButtonPlayerOneOutLocation.UseVisualStyleBackColor = true;
            this.ButtonPlayerOneOutLocation.Click += new System.EventHandler(this.ButtonPlayerOneOutLocationClick);
            // 
            // ButtonPlayerOneInLocation
            // 
            this.ButtonPlayerOneInLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPlayerOneInLocation.Location = new System.Drawing.Point(140, 126);
            this.ButtonPlayerOneInLocation.Name = "ButtonPlayerOneInLocation";
            this.ButtonPlayerOneInLocation.Size = new System.Drawing.Size(77, 21);
            this.ButtonPlayerOneInLocation.TabIndex = 6;
            this.ButtonPlayerOneInLocation.Text = "Choose File";
            this.ButtonPlayerOneInLocation.UseVisualStyleBackColor = true;
            this.ButtonPlayerOneInLocation.Click += new System.EventHandler(this.ButtonPlayerOneInLocationClick);
            // 
            // ImagePlayerOneOutLocation
            // 
            this.ImagePlayerOneOutLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImagePlayerOneOutLocation.ErrorImage = null;
            this.ImagePlayerOneOutLocation.Image = global::PokemonGenerator.Properties.Resources.BAD;
            this.ImagePlayerOneOutLocation.InitialImage = null;
            this.ImagePlayerOneOutLocation.Location = new System.Drawing.Point(329, 210);
            this.ImagePlayerOneOutLocation.Name = "ImagePlayerOneOutLocation";
            this.ImagePlayerOneOutLocation.Size = new System.Drawing.Size(25, 25);
            this.ImagePlayerOneOutLocation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImagePlayerOneOutLocation.TabIndex = 9;
            this.ImagePlayerOneOutLocation.TabStop = false;
            this.ToolTipProjN64Location.SetToolTip(this.ImagePlayerOneOutLocation, "Output Save location must be a valid path. Players cannot have the same output sa" +
        "ve file.");
            this.ImagePlayerOneOutLocation.Visible = false;
            this.ImagePlayerOneOutLocation.Click += new System.EventHandler(this.PlayerValidater);
            // 
            // ImagePlayerOneInLocation
            // 
            this.ImagePlayerOneInLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImagePlayerOneInLocation.ErrorImage = null;
            this.ImagePlayerOneInLocation.Image = global::PokemonGenerator.Properties.Resources.BAD;
            this.ImagePlayerOneInLocation.InitialImage = null;
            this.ImagePlayerOneInLocation.Location = new System.Drawing.Point(329, 150);
            this.ImagePlayerOneInLocation.Name = "ImagePlayerOneInLocation";
            this.ImagePlayerOneInLocation.Size = new System.Drawing.Size(25, 25);
            this.ImagePlayerOneInLocation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImagePlayerOneInLocation.TabIndex = 10;
            this.ImagePlayerOneInLocation.TabStop = false;
            this.ToolTipProjN64Location.SetToolTip(this.ImagePlayerOneInLocation, "Input Save location must be an existing sav file");
            this.ImagePlayerOneInLocation.Visible = false;
            this.ImagePlayerOneInLocation.Click += new System.EventHandler(this.PlayerValidater);
            // 
            // TextPlayerOneOutLocation
            // 
            this.TextPlayerOneOutLocation.Location = new System.Drawing.Point(9, 210);
            this.TextPlayerOneOutLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextPlayerOneOutLocation.MaxLength = 500;
            this.TextPlayerOneOutLocation.Name = "TextPlayerOneOutLocation";
            this.TextPlayerOneOutLocation.Size = new System.Drawing.Size(314, 25);
            this.TextPlayerOneOutLocation.TabIndex = 9;
            this.TextPlayerOneOutLocation.Validated += new System.EventHandler(this.PlayerValidate);
            // 
            // LabelPlayerOneOutLocation
            // 
            this.LabelPlayerOneOutLocation.AutoSize = true;
            this.LabelPlayerOneOutLocation.Location = new System.Drawing.Point(5, 188);
            this.LabelPlayerOneOutLocation.Name = "LabelPlayerOneOutLocation";
            this.LabelPlayerOneOutLocation.Size = new System.Drawing.Size(127, 17);
            this.LabelPlayerOneOutLocation.TabIndex = 11;
            this.LabelPlayerOneOutLocation.Text = "Output sav Location:";
            // 
            // TextPlayerOneInLocation
            // 
            this.TextPlayerOneInLocation.Location = new System.Drawing.Point(9, 150);
            this.TextPlayerOneInLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextPlayerOneInLocation.MaxLength = 500;
            this.TextPlayerOneInLocation.Name = "TextPlayerOneInLocation";
            this.TextPlayerOneInLocation.Size = new System.Drawing.Size(314, 25);
            this.TextPlayerOneInLocation.TabIndex = 7;
            this.TextPlayerOneInLocation.Validated += new System.EventHandler(this.PlayerValidate);
            // 
            // LabelPlayerOneInLocation
            // 
            this.LabelPlayerOneInLocation.AutoSize = true;
            this.LabelPlayerOneInLocation.Location = new System.Drawing.Point(5, 127);
            this.LabelPlayerOneInLocation.Name = "LabelPlayerOneInLocation";
            this.LabelPlayerOneInLocation.Size = new System.Drawing.Size(121, 17);
            this.LabelPlayerOneInLocation.TabIndex = 12;
            this.LabelPlayerOneInLocation.Text = "Game sav Location:";
            // 
            // LabelPlayerOneGame
            // 
            this.LabelPlayerOneGame.AutoSize = true;
            this.LabelPlayerOneGame.Location = new System.Drawing.Point(5, 75);
            this.LabelPlayerOneGame.Name = "LabelPlayerOneGame";
            this.LabelPlayerOneGame.Size = new System.Drawing.Size(45, 17);
            this.LabelPlayerOneGame.TabIndex = 13;
            this.LabelPlayerOneGame.Text = "Game:";
            // 
            // PanelProgress
            // 
            this.PanelProgress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelProgress.Controls.Add(this.LabelProgress);
            this.PanelProgress.Controls.Add(this.ImageProgress);
            this.PanelProgress.Location = new System.Drawing.Point(380, 140);
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
            // GroupBoxPlayerTwo
            // 
            this.GroupBoxPlayerTwo.Controls.Add(this.ImagePlayerTwoName);
            this.GroupBoxPlayerTwo.Controls.Add(this.TextPlayerTwoName);
            this.GroupBoxPlayerTwo.Controls.Add(this.LabelPlayerTwoName);
            this.GroupBoxPlayerTwo.Controls.Add(this.SelectPlayerTwoGame);
            this.GroupBoxPlayerTwo.Controls.Add(this.ButtonPlayerTwoOutLocation);
            this.GroupBoxPlayerTwo.Controls.Add(this.ButtonPlayerTwoInLocation);
            this.GroupBoxPlayerTwo.Controls.Add(this.ImagePlayerTwoOutLocation);
            this.GroupBoxPlayerTwo.Controls.Add(this.ImagePlayerTwoInLocation);
            this.GroupBoxPlayerTwo.Controls.Add(this.TextPlayerTwoOutLocation);
            this.GroupBoxPlayerTwo.Controls.Add(this.LabelPlayerTwoOutLocation);
            this.GroupBoxPlayerTwo.Controls.Add(this.TextPlayerTwoInLocation);
            this.GroupBoxPlayerTwo.Controls.Add(this.LabelPlayerTwoInLocation);
            this.GroupBoxPlayerTwo.Controls.Add(this.LabelPlayerTwoGame);
            this.GroupBoxPlayerTwo.Enabled = false;
            this.GroupBoxPlayerTwo.Location = new System.Drawing.Point(477, 90);
            this.GroupBoxPlayerTwo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBoxPlayerTwo.Name = "GroupBoxPlayerTwo";
            this.GroupBoxPlayerTwo.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBoxPlayerTwo.Size = new System.Drawing.Size(425, 243);
            this.GroupBoxPlayerTwo.TabIndex = 10;
            this.GroupBoxPlayerTwo.TabStop = false;
            this.GroupBoxPlayerTwo.Text = "Player 2 Config";
            // 
            // ImagePlayerTwoName
            // 
            this.ImagePlayerTwoName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImagePlayerTwoName.ErrorImage = null;
            this.ImagePlayerTwoName.Image = global::PokemonGenerator.Properties.Resources.BAD;
            this.ImagePlayerTwoName.InitialImage = null;
            this.ImagePlayerTwoName.Location = new System.Drawing.Point(326, 43);
            this.ImagePlayerTwoName.Name = "ImagePlayerTwoName";
            this.ImagePlayerTwoName.Size = new System.Drawing.Size(25, 25);
            this.ImagePlayerTwoName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImagePlayerTwoName.TabIndex = 0;
            this.ImagePlayerTwoName.TabStop = false;
            this.ToolTipProjN64Location.SetToolTip(this.ImagePlayerTwoName, "Player name must be alpha numeric, and between one and eight characters long\r\n");
            this.ImagePlayerTwoName.Visible = false;
            this.ImagePlayerTwoName.Click += new System.EventHandler(this.PlayerValidater);
            // 
            // TextPlayerTwoName
            // 
            this.TextPlayerTwoName.Location = new System.Drawing.Point(6, 43);
            this.TextPlayerTwoName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextPlayerTwoName.MaxLength = 8;
            this.TextPlayerTwoName.Name = "TextPlayerTwoName";
            this.TextPlayerTwoName.Size = new System.Drawing.Size(314, 25);
            this.TextPlayerTwoName.TabIndex = 10;
            this.TextPlayerTwoName.Validated += new System.EventHandler(this.PlayerValidate);
            // 
            // LabelPlayerTwoName
            // 
            this.LabelPlayerTwoName.AutoSize = true;
            this.LabelPlayerTwoName.Location = new System.Drawing.Point(3, 22);
            this.LabelPlayerTwoName.Name = "LabelPlayerTwoName";
            this.LabelPlayerTwoName.Size = new System.Drawing.Size(85, 17);
            this.LabelPlayerTwoName.TabIndex = 11;
            this.LabelPlayerTwoName.Text = "Player Name:";
            // 
            // SelectPlayerTwoGame
            // 
            this.SelectPlayerTwoGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectPlayerTwoGame.FormattingEnabled = true;
            this.SelectPlayerTwoGame.Items.AddRange(new object[] {
            "Gold",
            "Silver"});
            this.SelectPlayerTwoGame.Location = new System.Drawing.Point(5, 94);
            this.SelectPlayerTwoGame.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SelectPlayerTwoGame.Name = "SelectPlayerTwoGame";
            this.SelectPlayerTwoGame.Size = new System.Drawing.Size(315, 25);
            this.SelectPlayerTwoGame.TabIndex = 11;
            // 
            // ButtonPlayerTwoOutLocation
            // 
            this.ButtonPlayerTwoOutLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPlayerTwoOutLocation.Location = new System.Drawing.Point(129, 187);
            this.ButtonPlayerTwoOutLocation.Name = "ButtonPlayerTwoOutLocation";
            this.ButtonPlayerTwoOutLocation.Size = new System.Drawing.Size(77, 21);
            this.ButtonPlayerTwoOutLocation.TabIndex = 14;
            this.ButtonPlayerTwoOutLocation.Text = "Choose File";
            this.ButtonPlayerTwoOutLocation.UseVisualStyleBackColor = true;
            this.ButtonPlayerTwoOutLocation.Click += new System.EventHandler(this.ButtonPlayerTwoOutLocationClick);
            // 
            // ButtonPlayerTwoInLocation
            // 
            this.ButtonPlayerTwoInLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPlayerTwoInLocation.Location = new System.Drawing.Point(129, 126);
            this.ButtonPlayerTwoInLocation.Name = "ButtonPlayerTwoInLocation";
            this.ButtonPlayerTwoInLocation.Size = new System.Drawing.Size(77, 21);
            this.ButtonPlayerTwoInLocation.TabIndex = 12;
            this.ButtonPlayerTwoInLocation.Text = "Choose File";
            this.ButtonPlayerTwoInLocation.UseVisualStyleBackColor = true;
            this.ButtonPlayerTwoInLocation.Click += new System.EventHandler(this.ButtonPlayerTwoInLocationClick);
            // 
            // ImagePlayerTwoOutLocation
            // 
            this.ImagePlayerTwoOutLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImagePlayerTwoOutLocation.ErrorImage = null;
            this.ImagePlayerTwoOutLocation.Image = global::PokemonGenerator.Properties.Resources.BAD;
            this.ImagePlayerTwoOutLocation.InitialImage = null;
            this.ImagePlayerTwoOutLocation.Location = new System.Drawing.Point(326, 210);
            this.ImagePlayerTwoOutLocation.Name = "ImagePlayerTwoOutLocation";
            this.ImagePlayerTwoOutLocation.Size = new System.Drawing.Size(25, 25);
            this.ImagePlayerTwoOutLocation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImagePlayerTwoOutLocation.TabIndex = 15;
            this.ImagePlayerTwoOutLocation.TabStop = false;
            this.ToolTipProjN64Location.SetToolTip(this.ImagePlayerTwoOutLocation, "Output Save location must be a valid path. Players cannot have the same output sa" +
        "ve file.");
            this.ImagePlayerTwoOutLocation.Visible = false;
            this.ImagePlayerTwoOutLocation.Click += new System.EventHandler(this.PlayerValidater);
            // 
            // ImagePlayerTwoInLocation
            // 
            this.ImagePlayerTwoInLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImagePlayerTwoInLocation.ErrorImage = null;
            this.ImagePlayerTwoInLocation.Image = global::PokemonGenerator.Properties.Resources.BAD;
            this.ImagePlayerTwoInLocation.InitialImage = null;
            this.ImagePlayerTwoInLocation.Location = new System.Drawing.Point(326, 150);
            this.ImagePlayerTwoInLocation.Name = "ImagePlayerTwoInLocation";
            this.ImagePlayerTwoInLocation.Size = new System.Drawing.Size(25, 25);
            this.ImagePlayerTwoInLocation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImagePlayerTwoInLocation.TabIndex = 16;
            this.ImagePlayerTwoInLocation.TabStop = false;
            this.ToolTipProjN64Location.SetToolTip(this.ImagePlayerTwoInLocation, "Input Save location must be an existing sav file");
            this.ImagePlayerTwoInLocation.Visible = false;
            this.ImagePlayerTwoInLocation.Click += new System.EventHandler(this.PlayerValidater);
            // 
            // TextPlayerTwoOutLocation
            // 
            this.TextPlayerTwoOutLocation.Location = new System.Drawing.Point(6, 210);
            this.TextPlayerTwoOutLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextPlayerTwoOutLocation.MaxLength = 500;
            this.TextPlayerTwoOutLocation.Name = "TextPlayerTwoOutLocation";
            this.TextPlayerTwoOutLocation.Size = new System.Drawing.Size(314, 25);
            this.TextPlayerTwoOutLocation.TabIndex = 15;
            this.TextPlayerTwoOutLocation.Validated += new System.EventHandler(this.PlayerValidate);
            // 
            // LabelPlayerTwoOutLocation
            // 
            this.LabelPlayerTwoOutLocation.AutoSize = true;
            this.LabelPlayerTwoOutLocation.Location = new System.Drawing.Point(2, 188);
            this.LabelPlayerTwoOutLocation.Name = "LabelPlayerTwoOutLocation";
            this.LabelPlayerTwoOutLocation.Size = new System.Drawing.Size(127, 17);
            this.LabelPlayerTwoOutLocation.TabIndex = 17;
            this.LabelPlayerTwoOutLocation.Text = "Output sav Location:";
            // 
            // TextPlayerTwoInLocation
            // 
            this.TextPlayerTwoInLocation.Location = new System.Drawing.Point(6, 150);
            this.TextPlayerTwoInLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextPlayerTwoInLocation.MaxLength = 500;
            this.TextPlayerTwoInLocation.Name = "TextPlayerTwoInLocation";
            this.TextPlayerTwoInLocation.Size = new System.Drawing.Size(314, 25);
            this.TextPlayerTwoInLocation.TabIndex = 13;
            this.TextPlayerTwoInLocation.Validated += new System.EventHandler(this.PlayerValidate);
            // 
            // LabelPlayerTwoInLocation
            // 
            this.LabelPlayerTwoInLocation.AutoSize = true;
            this.LabelPlayerTwoInLocation.Location = new System.Drawing.Point(2, 127);
            this.LabelPlayerTwoInLocation.Name = "LabelPlayerTwoInLocation";
            this.LabelPlayerTwoInLocation.Size = new System.Drawing.Size(121, 17);
            this.LabelPlayerTwoInLocation.TabIndex = 18;
            this.LabelPlayerTwoInLocation.Text = "Game sav Location:";
            // 
            // LabelPlayerTwoGame
            // 
            this.LabelPlayerTwoGame.AutoSize = true;
            this.LabelPlayerTwoGame.Location = new System.Drawing.Point(2, 75);
            this.LabelPlayerTwoGame.Name = "LabelPlayerTwoGame";
            this.LabelPlayerTwoGame.Size = new System.Drawing.Size(45, 17);
            this.LabelPlayerTwoGame.TabIndex = 19;
            this.LabelPlayerTwoGame.Text = "Game:";
            // 
            // SelectLevel
            // 
            this.SelectLevel.Location = new System.Drawing.Point(66, 18);
            this.SelectLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.LabelLevel.Location = new System.Drawing.Point(9, 21);
            this.LabelLevel.Name = "LabelLevel";
            this.LabelLevel.Size = new System.Drawing.Size(37, 17);
            this.LabelLevel.TabIndex = 19;
            this.LabelLevel.Text = "Level";
            // 
            // SelectEntropy
            // 
            this.SelectEntropy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectEntropy.Enabled = false;
            this.SelectEntropy.FormattingEnabled = true;
            this.SelectEntropy.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High",
            "Chaos"});
            this.SelectEntropy.Location = new System.Drawing.Point(66, 64);
            this.SelectEntropy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SelectEntropy.Name = "SelectEntropy";
            this.SelectEntropy.Size = new System.Drawing.Size(140, 25);
            this.SelectEntropy.TabIndex = 17;
            // 
            // LabelEntropy
            // 
            this.LabelEntropy.AutoSize = true;
            this.LabelEntropy.Enabled = false;
            this.LabelEntropy.Location = new System.Drawing.Point(9, 68);
            this.LabelEntropy.Name = "LabelEntropy";
            this.LabelEntropy.Size = new System.Drawing.Size(53, 17);
            this.LabelEntropy.TabIndex = 20;
            this.LabelEntropy.Text = "Entropy";
            // 
            // ButtonProjN64Location
            // 
            this.ButtonProjN64Location.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonProjN64Location.Location = new System.Drawing.Point(160, 17);
            this.ButtonProjN64Location.Name = "ButtonProjN64Location";
            this.ButtonProjN64Location.Size = new System.Drawing.Size(77, 21);
            this.ButtonProjN64Location.TabIndex = 2;
            this.ButtonProjN64Location.Text = "Choose File";
            this.ButtonProjN64Location.UseVisualStyleBackColor = true;
            this.ButtonProjN64Location.Click += new System.EventHandler(this.ButtonProjN64LocationClick);
            // 
            // GroupBoxBottom
            // 
            this.GroupBoxBottom.Controls.Add(this.ButtonGenerate);
            this.GroupBoxBottom.Controls.Add(this.SelectLevel);
            this.GroupBoxBottom.Controls.Add(this.LabelLevel);
            this.GroupBoxBottom.Controls.Add(this.SelectEntropy);
            this.GroupBoxBottom.Controls.Add(this.LabelEntropy);
            this.GroupBoxBottom.Location = new System.Drawing.Point(13, 340);
            this.GroupBoxBottom.Name = "GroupBoxBottom";
            this.GroupBoxBottom.Size = new System.Drawing.Size(889, 116);
            this.GroupBoxBottom.TabIndex = 16;
            this.GroupBoxBottom.TabStop = false;
            // 
            // ButtonGenerate
            // 
            this.ButtonGenerate.Enabled = false;
            this.ButtonGenerate.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonGenerate.Location = new System.Drawing.Point(464, 18);
            this.ButtonGenerate.Name = "ButtonGenerate";
            this.ButtonGenerate.Size = new System.Drawing.Size(357, 83);
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
            // GroupBoxOuter
            // 
            this.GroupBoxOuter.Controls.Add(this.ImageProjN64Location);
            this.GroupBoxOuter.Controls.Add(this.TextProjN64Location);
            this.GroupBoxOuter.Controls.Add(this.LabelProjN64Location);
            this.GroupBoxOuter.Controls.Add(this.GroupBoxBottom);
            this.GroupBoxOuter.Controls.Add(this.GroupBoxPlayerOne);
            this.GroupBoxOuter.Controls.Add(this.ButtonProjN64Location);
            this.GroupBoxOuter.Controls.Add(this.GroupBoxPlayerTwo);
            this.GroupBoxOuter.Location = new System.Drawing.Point(1, -7);
            this.GroupBoxOuter.Margin = new System.Windows.Forms.Padding(0);
            this.GroupBoxOuter.Name = "GroupBoxOuter";
            this.GroupBoxOuter.Padding = new System.Windows.Forms.Padding(0);
            this.GroupBoxOuter.Size = new System.Drawing.Size(942, 478);
            this.GroupBoxOuter.TabIndex = 0;
            this.GroupBoxOuter.TabStop = false;
            // 
            // ImageProjN64Location
            // 
            this.ImageProjN64Location.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImageProjN64Location.Image = global::PokemonGenerator.Properties.Resources.BAD;
            this.ImageProjN64Location.Location = new System.Drawing.Point(909, 42);
            this.ImageProjN64Location.Name = "ImageProjN64Location";
            this.ImageProjN64Location.Size = new System.Drawing.Size(25, 25);
            this.ImageProjN64Location.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImageProjN64Location.TabIndex = 2;
            this.ImageProjN64Location.TabStop = false;
            this.ToolTipProjN64Location.SetToolTip(this.ImageProjN64Location, "Path to the Project 64 executable must exist");
            this.ImageProjN64Location.Visible = false;
            this.ImageProjN64Location.Click += new System.EventHandler(this.TopSectionValidater);
            // 
            // PokemonGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 441);
            this.Controls.Add(this.GroupBoxOuter);
            this.Controls.Add(this.PanelProgress);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "PokemonGeneratorForm";
            this.Text = "PokéGenerator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PokemonGeneratorClosing);
            this.Load += new System.EventHandler(this.PokemonGeneratorLoad);
            this.GroupBoxPlayerOne.ResumeLayout(false);
            this.GroupBoxPlayerOne.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerOneName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerOneOutLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerOneInLocation)).EndInit();
            this.PanelProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImageProgress)).EndInit();
            this.GroupBoxPlayerTwo.ResumeLayout(false);
            this.GroupBoxPlayerTwo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerTwoName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerTwoOutLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePlayerTwoInLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectLevel)).EndInit();
            this.GroupBoxBottom.ResumeLayout(false);
            this.GroupBoxBottom.PerformLayout();
            this.GroupBoxOuter.ResumeLayout(false);
            this.GroupBoxOuter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageProjN64Location)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.HelpProvider HelpProvider;
        private System.Windows.Forms.Label LabelProjN64Location;
        private System.Windows.Forms.TextBox TextProjN64Location;
        private System.Windows.Forms.GroupBox GroupBoxPlayerOne;
        private System.Windows.Forms.TextBox TextPlayerOneOutLocation;
        private System.Windows.Forms.Label LabelPlayerOneOutLocation;
        private System.Windows.Forms.TextBox TextPlayerOneInLocation;
        private System.Windows.Forms.Label LabelPlayerOneInLocation;
        private System.Windows.Forms.Label LabelPlayerOneGame;
        private System.Windows.Forms.GroupBox GroupBoxPlayerTwo;
        private System.Windows.Forms.TextBox TextPlayerTwoOutLocation;
        private System.Windows.Forms.Label LabelPlayerTwoOutLocation;
        private System.Windows.Forms.TextBox TextPlayerTwoInLocation;
        private System.Windows.Forms.Label LabelPlayerTwoInLocation;
        private System.Windows.Forms.Label LabelPlayerTwoGame;
        private System.Windows.Forms.NumericUpDown SelectLevel;
        private System.Windows.Forms.Label LabelLevel;
        private System.Windows.Forms.ComboBox SelectEntropy;
        private System.Windows.Forms.Label LabelEntropy;
        private System.Windows.Forms.Button ButtonProjN64Location;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.GroupBox GroupBoxBottom;
        private System.Windows.Forms.Button ButtonGenerate;
        private System.Windows.Forms.PictureBox ImagePlayerOneOutLocation;
        private System.Windows.Forms.PictureBox ImagePlayerOneInLocation;
        private System.Windows.Forms.PictureBox ImagePlayerTwoOutLocation;
        private System.Windows.Forms.PictureBox ImagePlayerTwoInLocation;
        private System.Windows.Forms.Panel PanelProgress;
        private System.Windows.Forms.PictureBox ImageProgress;
        private System.Windows.Forms.Label LabelProgress;
        private System.Windows.Forms.Button ButtonPlayerOneOutLocation;
        private System.Windows.Forms.Button ButtonPlayerOneInLocation;
        private System.Windows.Forms.Button ButtonPlayerTwoOutLocation;
        private System.Windows.Forms.Button ButtonPlayerTwoInLocation;
        private System.Windows.Forms.PictureBox ImageProjN64Location;
        private System.ComponentModel.BackgroundWorker BackgroundPokemonGenerator;
        private System.Windows.Forms.GroupBox GroupBoxOuter;
        private System.Windows.Forms.ComboBox SelectPlayerOneGame;
        private System.Windows.Forms.ComboBox SelectPlayerTwoGame;
        private System.Windows.Forms.TextBox TextPlayerOneName;
        private System.Windows.Forms.Label LabelPlayerOneName;
        private System.Windows.Forms.TextBox TextPlayerTwoName;
        private System.Windows.Forms.Label LabelPlayerTwoName;
        private System.Windows.Forms.PictureBox ImagePlayerOneName;
        private System.Windows.Forms.PictureBox ImagePlayerTwoName;
        private System.Windows.Forms.ToolTip ToolTipProjN64Location;
    }
}