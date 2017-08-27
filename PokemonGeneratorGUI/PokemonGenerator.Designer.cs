namespace PokemonGeneratorGUI
{
    partial class PokemonGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokemonGenerator));
            this.textbox_pokemonGeneratorExeLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this.label2 = new System.Windows.Forms.Label();
            this.textbox_projN64Location = new System.Windows.Forms.TextBox();
            this.groupBox_player1 = new System.Windows.Forms.GroupBox();
            this.pictureBox_1name = new System.Windows.Forms.PictureBox();
            this.textBox_1name = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox_1game = new System.Windows.Forms.ComboBox();
            this.button_1Out = new System.Windows.Forms.Button();
            this.button_1Sav = new System.Windows.Forms.Button();
            this.pictureBox_1Out = new System.Windows.Forms.PictureBox();
            this.pictureBox_1Sav = new System.Windows.Forms.PictureBox();
            this.textBox_1Out = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_1Sav = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.groupBox_player2 = new System.Windows.Forms.GroupBox();
            this.pictureBox_2name = new System.Windows.Forms.PictureBox();
            this.textBox_2name = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox_2game = new System.Windows.Forms.ComboBox();
            this.button_2Out = new System.Windows.Forms.Button();
            this.button_2Sav = new System.Windows.Forms.Button();
            this.pictureBox_2Out = new System.Windows.Forms.PictureBox();
            this.pictureBox_2Sav = new System.Windows.Forms.PictureBox();
            this.textBox_2Out = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_2Sav = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_level = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_entropy = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_pokemonGeneratorExeLocation = new System.Windows.Forms.Button();
            this.button_projN64Location = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_bottom = new System.Windows.Forms.GroupBox();
            this.button_generate = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox_projN64Location = new System.Windows.Forms.PictureBox();
            this.pictureBox_pokemonGeneratorExeLocation = new System.Windows.Forms.PictureBox();
            this.groupBox_player1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_1name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_1Out)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_1Sav)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.groupBox_player2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_2name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_2Out)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_2Sav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_level)).BeginInit();
            this.groupBox_bottom.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_projN64Location)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_pokemonGeneratorExeLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // textbox_pokemonGeneratorExeLocation
            // 
            this.textbox_pokemonGeneratorExeLocation.Location = new System.Drawing.Point(18, 42);
            this.textbox_pokemonGeneratorExeLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textbox_pokemonGeneratorExeLocation.MaxLength = 500;
            this.textbox_pokemonGeneratorExeLocation.Name = "textbox_pokemonGeneratorExeLocation";
            this.textbox_pokemonGeneratorExeLocation.Size = new System.Drawing.Size(410, 25);
            this.textbox_pokemonGeneratorExeLocation.TabIndex = 0;
            this.textbox_pokemonGeneratorExeLocation.Validated += new System.EventHandler(this.textbox_pokemonGeneratorExeLocation_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "PokemonGenerator.exe location:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(471, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Project N64 Location:";
            // 
            // textbox_projN64Location
            // 
            this.textbox_projN64Location.Location = new System.Drawing.Point(476, 42);
            this.textbox_projN64Location.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textbox_projN64Location.MaxLength = 500;
            this.textbox_projN64Location.Name = "textbox_projN64Location";
            this.textbox_projN64Location.Size = new System.Drawing.Size(426, 25);
            this.textbox_projN64Location.TabIndex = 2;
            this.textbox_projN64Location.Validated += new System.EventHandler(this.textbox_projN64Location_TextChanged);
            // 
            // groupBox_player1
            // 
            this.groupBox_player1.Controls.Add(this.pictureBox_1name);
            this.groupBox_player1.Controls.Add(this.textBox_1name);
            this.groupBox_player1.Controls.Add(this.label12);
            this.groupBox_player1.Controls.Add(this.comboBox_1game);
            this.groupBox_player1.Controls.Add(this.button_1Out);
            this.groupBox_player1.Controls.Add(this.button_1Sav);
            this.groupBox_player1.Controls.Add(this.pictureBox_1Out);
            this.groupBox_player1.Controls.Add(this.pictureBox_1Sav);
            this.groupBox_player1.Controls.Add(this.textBox_1Out);
            this.groupBox_player1.Controls.Add(this.label5);
            this.groupBox_player1.Controls.Add(this.textBox_1Sav);
            this.groupBox_player1.Controls.Add(this.label4);
            this.groupBox_player1.Controls.Add(this.label3);
            this.groupBox_player1.Enabled = false;
            this.groupBox_player1.Location = new System.Drawing.Point(16, 90);
            this.groupBox_player1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox_player1.Name = "groupBox_player1";
            this.groupBox_player1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox_player1.Size = new System.Drawing.Size(443, 243);
            this.groupBox_player1.TabIndex = 4;
            this.groupBox_player1.TabStop = false;
            this.groupBox_player1.Text = "Player 1 Config";
            // 
            // pictureBox_1name
            // 
            this.pictureBox_1name.ErrorImage = null;
            this.pictureBox_1name.InitialImage = null;
            this.pictureBox_1name.Location = new System.Drawing.Point(328, 43);
            this.pictureBox_1name.Name = "pictureBox_1name";
            this.pictureBox_1name.Size = new System.Drawing.Size(25, 25);
            this.pictureBox_1name.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_1name.TabIndex = 21;
            this.pictureBox_1name.TabStop = false;
            this.pictureBox_1name.Visible = false;
            this.pictureBox_1name.Click += new System.EventHandler(this.group2Validater);
            // 
            // textBox_1name
            // 
            this.textBox_1name.Location = new System.Drawing.Point(8, 43);
            this.textBox_1name.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_1name.MaxLength = 8;
            this.textBox_1name.Name = "textBox_1name";
            this.textBox_1name.Size = new System.Drawing.Size(314, 25);
            this.textBox_1name.TabIndex = 20;
            this.textBox_1name.Validated += new System.EventHandler(this.ValidateGroup2_Event);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 17);
            this.label12.TabIndex = 19;
            this.label12.Text = "Player Name:";
            // 
            // comboBox_1game
            // 
            this.comboBox_1game.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_1game.FormattingEnabled = true;
            this.comboBox_1game.Items.AddRange(new object[] {
            "Gold",
            "Silver"});
            this.comboBox_1game.Location = new System.Drawing.Point(8, 94);
            this.comboBox_1game.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_1game.Name = "comboBox_1game";
            this.comboBox_1game.Size = new System.Drawing.Size(315, 25);
            this.comboBox_1game.TabIndex = 12;
            // 
            // button_1Out
            // 
            this.button_1Out.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_1Out.Location = new System.Drawing.Point(140, 187);
            this.button_1Out.Name = "button_1Out";
            this.button_1Out.Size = new System.Drawing.Size(77, 21);
            this.button_1Out.TabIndex = 18;
            this.button_1Out.Text = "Choose File";
            this.button_1Out.UseVisualStyleBackColor = true;
            this.button_1Out.Click += new System.EventHandler(this.textBox_1Out_Click);
            // 
            // button_1Sav
            // 
            this.button_1Sav.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_1Sav.Location = new System.Drawing.Point(140, 126);
            this.button_1Sav.Name = "button_1Sav";
            this.button_1Sav.Size = new System.Drawing.Size(77, 21);
            this.button_1Sav.TabIndex = 17;
            this.button_1Sav.Text = "Choose File";
            this.button_1Sav.UseVisualStyleBackColor = true;
            this.button_1Sav.Click += new System.EventHandler(this.button_1Sav_Click);
            // 
            // pictureBox_1Out
            // 
            this.pictureBox_1Out.ErrorImage = null;
            this.pictureBox_1Out.InitialImage = null;
            this.pictureBox_1Out.Location = new System.Drawing.Point(329, 210);
            this.pictureBox_1Out.Name = "pictureBox_1Out";
            this.pictureBox_1Out.Size = new System.Drawing.Size(25, 25);
            this.pictureBox_1Out.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_1Out.TabIndex = 15;
            this.pictureBox_1Out.TabStop = false;
            this.pictureBox_1Out.Visible = false;
            this.pictureBox_1Out.Click += new System.EventHandler(this.group2Validater);
            // 
            // pictureBox_1Sav
            // 
            this.pictureBox_1Sav.ErrorImage = null;
            this.pictureBox_1Sav.InitialImage = null;
            this.pictureBox_1Sav.Location = new System.Drawing.Point(329, 150);
            this.pictureBox_1Sav.Name = "pictureBox_1Sav";
            this.pictureBox_1Sav.Size = new System.Drawing.Size(25, 25);
            this.pictureBox_1Sav.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_1Sav.TabIndex = 14;
            this.pictureBox_1Sav.TabStop = false;
            this.pictureBox_1Sav.Visible = false;
            this.pictureBox_1Sav.Click += new System.EventHandler(this.group2Validater);
            // 
            // textBox_1Out
            // 
            this.textBox_1Out.Location = new System.Drawing.Point(9, 210);
            this.textBox_1Out.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_1Out.MaxLength = 500;
            this.textBox_1Out.Name = "textBox_1Out";
            this.textBox_1Out.Size = new System.Drawing.Size(314, 25);
            this.textBox_1Out.TabIndex = 5;
            this.textBox_1Out.Validated += new System.EventHandler(this.ValidateGroup2_Event);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Output sav Location:";
            // 
            // textBox_1Sav
            // 
            this.textBox_1Sav.Location = new System.Drawing.Point(9, 150);
            this.textBox_1Sav.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_1Sav.MaxLength = 500;
            this.textBox_1Sav.Name = "textBox_1Sav";
            this.textBox_1Sav.Size = new System.Drawing.Size(314, 25);
            this.textBox_1Sav.TabIndex = 3;
            this.textBox_1Sav.Validated += new System.EventHandler(this.ValidateGroup2_Event);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Game sav Location:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Game:";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.pictureBox7);
            this.panel1.Location = new System.Drawing.Point(380, 140);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 999;
            this.panel1.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoEllipsis = true;
            this.label11.Location = new System.Drawing.Point(3, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(194, 28);
            this.label11.TabIndex = 0;
            this.label11.Text = "GENERATING...";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::PokemonGeneratorGUI.Properties.Resources.pikagif;
            this.pictureBox7.Location = new System.Drawing.Point(66, 4);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(62, 66);
            this.pictureBox7.TabIndex = 1;
            this.pictureBox7.TabStop = false;
            // 
            // groupBox_player2
            // 
            this.groupBox_player2.Controls.Add(this.pictureBox_2name);
            this.groupBox_player2.Controls.Add(this.textBox_2name);
            this.groupBox_player2.Controls.Add(this.label13);
            this.groupBox_player2.Controls.Add(this.comboBox_2game);
            this.groupBox_player2.Controls.Add(this.button_2Out);
            this.groupBox_player2.Controls.Add(this.button_2Sav);
            this.groupBox_player2.Controls.Add(this.pictureBox_2Out);
            this.groupBox_player2.Controls.Add(this.pictureBox_2Sav);
            this.groupBox_player2.Controls.Add(this.textBox_2Out);
            this.groupBox_player2.Controls.Add(this.label6);
            this.groupBox_player2.Controls.Add(this.textBox_2Sav);
            this.groupBox_player2.Controls.Add(this.label7);
            this.groupBox_player2.Controls.Add(this.label8);
            this.groupBox_player2.Enabled = false;
            this.groupBox_player2.Location = new System.Drawing.Point(477, 90);
            this.groupBox_player2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox_player2.Name = "groupBox_player2";
            this.groupBox_player2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox_player2.Size = new System.Drawing.Size(425, 243);
            this.groupBox_player2.TabIndex = 6;
            this.groupBox_player2.TabStop = false;
            this.groupBox_player2.Text = "Player 2 Config";
            // 
            // pictureBox_2name
            // 
            this.pictureBox_2name.ErrorImage = null;
            this.pictureBox_2name.InitialImage = null;
            this.pictureBox_2name.Location = new System.Drawing.Point(326, 43);
            this.pictureBox_2name.Name = "pictureBox_2name";
            this.pictureBox_2name.Size = new System.Drawing.Size(25, 25);
            this.pictureBox_2name.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_2name.TabIndex = 22;
            this.pictureBox_2name.TabStop = false;
            this.pictureBox_2name.Visible = false;
            this.pictureBox_2name.Click += new System.EventHandler(this.group2Validater);
            // 
            // textBox_2name
            // 
            this.textBox_2name.Location = new System.Drawing.Point(6, 43);
            this.textBox_2name.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_2name.MaxLength = 8;
            this.textBox_2name.Name = "textBox_2name";
            this.textBox_2name.Size = new System.Drawing.Size(314, 25);
            this.textBox_2name.TabIndex = 22;
            this.textBox_2name.Validated += new System.EventHandler(this.ValidateGroup2_Event);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 17);
            this.label13.TabIndex = 21;
            this.label13.Text = "Player Name:";
            // 
            // comboBox_2game
            // 
            this.comboBox_2game.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_2game.FormattingEnabled = true;
            this.comboBox_2game.Items.AddRange(new object[] {
            "Gold",
            "Silver"});
            this.comboBox_2game.Location = new System.Drawing.Point(5, 94);
            this.comboBox_2game.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_2game.Name = "comboBox_2game";
            this.comboBox_2game.Size = new System.Drawing.Size(315, 25);
            this.comboBox_2game.TabIndex = 19;
            // 
            // button_2Out
            // 
            this.button_2Out.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_2Out.Location = new System.Drawing.Point(129, 187);
            this.button_2Out.Name = "button_2Out";
            this.button_2Out.Size = new System.Drawing.Size(77, 21);
            this.button_2Out.TabIndex = 21;
            this.button_2Out.Text = "Choose File";
            this.button_2Out.UseVisualStyleBackColor = true;
            this.button_2Out.Click += new System.EventHandler(this.button_2Out_Click);
            // 
            // button_2Sav
            // 
            this.button_2Sav.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_2Sav.Location = new System.Drawing.Point(129, 126);
            this.button_2Sav.Name = "button_2Sav";
            this.button_2Sav.Size = new System.Drawing.Size(77, 21);
            this.button_2Sav.TabIndex = 20;
            this.button_2Sav.Text = "Choose File";
            this.button_2Sav.UseVisualStyleBackColor = true;
            this.button_2Sav.Click += new System.EventHandler(this.button_2Sav_Click);
            // 
            // pictureBox_2Out
            // 
            this.pictureBox_2Out.ErrorImage = null;
            this.pictureBox_2Out.InitialImage = null;
            this.pictureBox_2Out.Location = new System.Drawing.Point(326, 210);
            this.pictureBox_2Out.Name = "pictureBox_2Out";
            this.pictureBox_2Out.Size = new System.Drawing.Size(25, 25);
            this.pictureBox_2Out.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_2Out.TabIndex = 18;
            this.pictureBox_2Out.TabStop = false;
            this.pictureBox_2Out.Visible = false;
            this.pictureBox_2Out.Click += new System.EventHandler(this.group2Validater);
            // 
            // pictureBox_2Sav
            // 
            this.pictureBox_2Sav.ErrorImage = null;
            this.pictureBox_2Sav.InitialImage = null;
            this.pictureBox_2Sav.Location = new System.Drawing.Point(326, 150);
            this.pictureBox_2Sav.Name = "pictureBox_2Sav";
            this.pictureBox_2Sav.Size = new System.Drawing.Size(25, 25);
            this.pictureBox_2Sav.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_2Sav.TabIndex = 17;
            this.pictureBox_2Sav.TabStop = false;
            this.pictureBox_2Sav.Visible = false;
            this.pictureBox_2Sav.Click += new System.EventHandler(this.group2Validater);
            // 
            // textBox_2Out
            // 
            this.textBox_2Out.Location = new System.Drawing.Point(6, 210);
            this.textBox_2Out.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_2Out.MaxLength = 500;
            this.textBox_2Out.Name = "textBox_2Out";
            this.textBox_2Out.Size = new System.Drawing.Size(314, 25);
            this.textBox_2Out.TabIndex = 5;
            this.textBox_2Out.Validated += new System.EventHandler(this.ValidateGroup2_Event);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Output sav Location:";
            // 
            // textBox_2Sav
            // 
            this.textBox_2Sav.Location = new System.Drawing.Point(6, 150);
            this.textBox_2Sav.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_2Sav.MaxLength = 500;
            this.textBox_2Sav.Name = "textBox_2Sav";
            this.textBox_2Sav.Size = new System.Drawing.Size(314, 25);
            this.textBox_2Sav.TabIndex = 3;
            this.textBox_2Sav.Validated += new System.EventHandler(this.ValidateGroup2_Event);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Game sav Location:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Game:";
            // 
            // numericUpDown_level
            // 
            this.numericUpDown_level.Location = new System.Drawing.Point(66, 18);
            this.numericUpDown_level.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDown_level.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_level.Name = "numericUpDown_level";
            this.numericUpDown_level.Size = new System.Drawing.Size(140, 25);
            this.numericUpDown_level.TabIndex = 7;
            this.numericUpDown_level.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "Level";
            // 
            // comboBox_entropy
            // 
            this.comboBox_entropy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_entropy.FormattingEnabled = true;
            this.comboBox_entropy.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High",
            "Chaos"});
            this.comboBox_entropy.Location = new System.Drawing.Point(66, 64);
            this.comboBox_entropy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_entropy.Name = "comboBox_entropy";
            this.comboBox_entropy.Size = new System.Drawing.Size(140, 25);
            this.comboBox_entropy.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 17);
            this.label10.TabIndex = 10;
            this.label10.Text = "Entropy";
            // 
            // button_pokemonGeneratorExeLocation
            // 
            this.button_pokemonGeneratorExeLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_pokemonGeneratorExeLocation.Location = new System.Drawing.Point(218, 20);
            this.button_pokemonGeneratorExeLocation.Name = "button_pokemonGeneratorExeLocation";
            this.button_pokemonGeneratorExeLocation.Size = new System.Drawing.Size(77, 21);
            this.button_pokemonGeneratorExeLocation.TabIndex = 13;
            this.button_pokemonGeneratorExeLocation.Text = "Choose File";
            this.button_pokemonGeneratorExeLocation.UseVisualStyleBackColor = true;
            this.button_pokemonGeneratorExeLocation.Click += new System.EventHandler(this.button_pokemonGeneratorExeLocation_Click);
            // 
            // button_projN64Location
            // 
            this.button_projN64Location.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_projN64Location.Location = new System.Drawing.Point(612, 20);
            this.button_projN64Location.Name = "button_projN64Location";
            this.button_projN64Location.Size = new System.Drawing.Size(77, 21);
            this.button_projN64Location.TabIndex = 14;
            this.button_projN64Location.Text = "Choose File";
            this.button_projN64Location.UseVisualStyleBackColor = true;
            this.button_projN64Location.Click += new System.EventHandler(this.button_projN64Location_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // groupBox_bottom
            // 
            this.groupBox_bottom.Controls.Add(this.button_generate);
            this.groupBox_bottom.Controls.Add(this.numericUpDown_level);
            this.groupBox_bottom.Controls.Add(this.label9);
            this.groupBox_bottom.Controls.Add(this.comboBox_entropy);
            this.groupBox_bottom.Controls.Add(this.label10);
            this.groupBox_bottom.Location = new System.Drawing.Point(13, 340);
            this.groupBox_bottom.Name = "groupBox_bottom";
            this.groupBox_bottom.Size = new System.Drawing.Size(889, 116);
            this.groupBox_bottom.TabIndex = 15;
            this.groupBox_bottom.TabStop = false;
            // 
            // button_generate
            // 
            this.button_generate.Enabled = false;
            this.button_generate.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_generate.Location = new System.Drawing.Point(464, 18);
            this.button_generate.Name = "button_generate";
            this.button_generate.Size = new System.Drawing.Size(357, 83);
            this.button_generate.TabIndex = 11;
            this.button_generate.Text = "Generate (CTRL+F12)";
            this.button_generate.UseVisualStyleBackColor = true;
            this.button_generate.Click += new System.EventHandler(this.button3_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.textbox_pokemonGeneratorExeLocation);
            this.groupBox4.Controls.Add(this.pictureBox_projN64Location);
            this.groupBox4.Controls.Add(this.textbox_projN64Location);
            this.groupBox4.Controls.Add(this.pictureBox_pokemonGeneratorExeLocation);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.groupBox_bottom);
            this.groupBox4.Controls.Add(this.groupBox_player1);
            this.groupBox4.Controls.Add(this.button_projN64Location);
            this.groupBox4.Controls.Add(this.groupBox_player2);
            this.groupBox4.Controls.Add(this.button_pokemonGeneratorExeLocation);
            this.groupBox4.Location = new System.Drawing.Point(1, -7);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox4.Size = new System.Drawing.Size(942, 478);
            this.groupBox4.TabIndex = 1000;
            this.groupBox4.TabStop = false;
            // 
            // pictureBox_projN64Location
            // 
            this.pictureBox_projN64Location.Image = global::PokemonGeneratorGUI.Properties.Resources.BAD;
            this.pictureBox_projN64Location.Location = new System.Drawing.Point(909, 42);
            this.pictureBox_projN64Location.Name = "pictureBox_projN64Location";
            this.pictureBox_projN64Location.Size = new System.Drawing.Size(25, 25);
            this.pictureBox_projN64Location.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_projN64Location.TabIndex = 17;
            this.pictureBox_projN64Location.TabStop = false;
            this.pictureBox_projN64Location.Visible = false;
            this.pictureBox_projN64Location.Click += new System.EventHandler(this.group1Validater);
            // 
            // pictureBox_pokemonGeneratorExeLocation
            // 
            this.pictureBox_pokemonGeneratorExeLocation.Image = global::PokemonGeneratorGUI.Properties.Resources.BAD;
            this.pictureBox_pokemonGeneratorExeLocation.Location = new System.Drawing.Point(444, 42);
            this.pictureBox_pokemonGeneratorExeLocation.Name = "pictureBox_pokemonGeneratorExeLocation";
            this.pictureBox_pokemonGeneratorExeLocation.Size = new System.Drawing.Size(25, 25);
            this.pictureBox_pokemonGeneratorExeLocation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_pokemonGeneratorExeLocation.TabIndex = 16;
            this.pictureBox_pokemonGeneratorExeLocation.TabStop = false;
            this.pictureBox_pokemonGeneratorExeLocation.Visible = false;
            this.pictureBox_pokemonGeneratorExeLocation.Click += new System.EventHandler(this.group1Validater);
            // 
            // PokemonGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 441);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "PokemonGenerator";
            this.Text = "PokéGenerator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PokemonGenerator_FormClosing);
            this.Load += new System.EventHandler(this.PokemonGenerator_Load);
            this.groupBox_player1.ResumeLayout(false);
            this.groupBox_player1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_1name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_1Out)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_1Sav)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.groupBox_player2.ResumeLayout(false);
            this.groupBox_player2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_2name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_2Out)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_2Sav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_level)).EndInit();
            this.groupBox_bottom.ResumeLayout(false);
            this.groupBox_bottom.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_projN64Location)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_pokemonGeneratorExeLocation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textbox_pokemonGeneratorExeLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HelpProvider helpProvider;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textbox_projN64Location;
        private System.Windows.Forms.GroupBox groupBox_player1;
        private System.Windows.Forms.TextBox textBox_1Out;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_1Sav;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox_player2;
        private System.Windows.Forms.TextBox textBox_2Out;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_2Sav;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_level;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_entropy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button_pokemonGeneratorExeLocation;
        private System.Windows.Forms.Button button_projN64Location;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox_bottom;
        private System.Windows.Forms.Button button_generate;
        private System.Windows.Forms.PictureBox pictureBox_1Out;
        private System.Windows.Forms.PictureBox pictureBox_1Sav;
        private System.Windows.Forms.PictureBox pictureBox_2Out;
        private System.Windows.Forms.PictureBox pictureBox_2Sav;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button_1Out;
        private System.Windows.Forms.Button button_1Sav;
        private System.Windows.Forms.Button button_2Out;
        private System.Windows.Forms.Button button_2Sav;
        private System.Windows.Forms.PictureBox pictureBox_projN64Location;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBox_1game;
        private System.Windows.Forms.ComboBox comboBox_2game;
        private System.Windows.Forms.TextBox textBox_1name;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_2name;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox_1name;
        private System.Windows.Forms.PictureBox pictureBox_2name;
        private System.Windows.Forms.PictureBox pictureBox_pokemonGeneratorExeLocation;
    }
}

