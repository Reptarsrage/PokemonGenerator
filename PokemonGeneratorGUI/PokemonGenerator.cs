using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PokemonGeneratorGUI
{
    public partial class PokemonGenerator : Form
    {
        /// <summary>
        /// Args to pass to background worker
        /// </summary>
        public struct WorkerArgs
        {
            public int level { get; set; }
            public string entropy { get; set; }
            public string i1 { get; set; }
            public string i2 { get; set; }
            public string o1 { get; set; }
            public string o2 { get; set; }
            public string p64 { get; set; }
            public string pgExe { get; set; }
            public string game1 { get; set; }
            public string game2 { get; set; }
            public string name1 { get; set; }
            public string name2 { get; set; }
        }

        // Imported to handle out-of-focus macro handeling
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        private string pokeGenEXELocation;
        private string projN64Location;
        private NRageIniEditor editor;
        private P64ConfigEditor n64Config;

        public PokemonGenerator()
        {
#if (DEBUG)
            pokeGenEXELocation = Path.GetFullPath(Path.Combine(Assembly.GetAssembly(typeof(Program)).Location, @"..\..\..\..\PokemonGenerator\bin\Debug\PokemonGenerator.exe"));
            projN64Location = @"G:\Project64\Project64.exe"; // TODO remove

#else
            pokeGenEXELocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"PokeGenerator\PokemonGenerator.exe");
            projN64Location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Project64\Project64.exe");

#endif

            InitializeComponent();

            int id = 0;     // The id of the hotkey. 
            RegisterHotKey(this.Handle, id, (int)KeyModifier.Control, Keys.F12.GetHashCode());
        }

        /// <summary>
        /// Catch the Ctrl + F12 Macro and execute!
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                /* Note that the three lines below are not needed if you only want to register one hotkey.
                 * The below lines are useful in case you want to register multiple keys, which you can use a switch with the id as argument, or if you want to know which key/modifier was pressed for some particular reason. */

                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

                // do something
                if (id == 0 && this.button_generate.Enabled)
                {
                    this.button3_Click(null, null);
                }
            }
        }

        private void PokemonGenerator_Load(object sender, EventArgs e)
        {
            this.comboBox_entropy.SelectedIndex = 0;
            this.textbox_pokemonGeneratorExeLocation.Text = pokeGenEXELocation;
            this.textbox_projN64Location.Text = projN64Location;
            ValidateGroup1();
        }

        private void textbox_pokemonGeneratorExeLocation_TextChanged(object sender, EventArgs e)
        {
            ValidateGroup1();
        }

        private void textbox_projN64Location_TextChanged(object sender, EventArgs e)
        {
            ValidateGroup1();
        }

        private bool ValidateGroup1()
        {
            var good = true;
            good &= CheckIfFileExistsAndAssignImage(textbox_projN64Location, pictureBox_projN64Location, false);
            good &= CheckIfFileExistsAndAssignImage(textbox_pokemonGeneratorExeLocation, pictureBox_pokemonGeneratorExeLocation, false);
            if (good)
            {
                // get ini location
                if (this.editor == null)
                {
                    var ini = Path.Combine(Path.GetDirectoryName(this.textbox_projN64Location.Text), @"Config\NRage.ini");
                    var cfg = Path.Combine(Path.GetDirectoryName(this.textbox_projN64Location.Text), @"Config\Project64.cfg");

                    if (File.Exists(ini))
                    {
                        this.editor = new NRageIniEditor(ini);
                        var tup = editor.GetRomAndSavFileLocation(1);
                        var tup2 = editor.GetRomAndSavFileLocation(2);

                        this.comboBox_1game.SelectedIndex = 0;
                        this.textBox_1Sav.Text = tup.Item2;
                        this.textBox_1Out.Text = string.IsNullOrWhiteSpace(tup.Item2) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), @"Player2.sav") : tup.Item2;

                        this.comboBox_2game.SelectedIndex = 0;
                        this.textBox_2Sav.Text = tup2.Item2;
                        this.textBox_2Out.Text = string.IsNullOrWhiteSpace(tup2.Item2) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), @"Player2.sav") : tup2.Item2;
                    }
                    if (File.Exists(cfg))
                    {
                        this.n64Config = new P64ConfigEditor(cfg);
                    }
                }
                this.textBox_1name.Text = "PLAYER1";
                this.textBox_2name.Text = "PLAYER2";
                this.groupBox_player1.Enabled = true;
                this.groupBox_player2.Enabled = true;
                return ValidateGroup2();
            }
            else
            {
                this.groupBox_player1.Enabled = false;
                this.groupBox_player2.Enabled = false;
                this.groupBox_bottom.Enabled = false;
                this.editor = null;
                this.n64Config = null;
                return false;
            }

        }

        private bool CheckIfFileExistsAndAssignImage(TextBox textbox, PictureBox pic, bool allowEmpty = true)
        {
            if (!string.IsNullOrEmpty(textbox.Text) && File.Exists(textbox.Text))
            {
                pic.Hide();
                return true;
            }
            else
            {
                AssignErrorImageTo(pic);
                return false;
            }
        }

        private void AssignErrorImageTo(PictureBox pic)
        {
            pic.Show();
        }

        private bool ValidateGroup2()
        {
            var good = true;

            //good &= CheckIfFileExistsAndAssignImage(textBox_1Rom, pictureBox1);
            good &= string.IsNullOrEmpty(textBox_1Sav.Text) || CheckIfFileExistsAndAssignImage(textBox_1Sav, pictureBox_1Sav);
            good &= CheckIfFileExistsAndAssignImage(textBox_1Out, pictureBox_1Out);
            if (!string.IsNullOrWhiteSpace(textBox_1name.Text))
            {
                pictureBox_1name.Hide();
            }
            else
            {
                AssignErrorImageTo(pictureBox_1name);
                good = false;
            }

            //good &= CheckIfFileExistsAndAssignImage(textBox_2Rom, pictureBox4);
            good &= string.IsNullOrEmpty(textBox_2Sav.Text) || CheckIfFileExistsAndAssignImage(textBox_2Sav, pictureBox_2Sav);
            good &= CheckIfFileExistsAndAssignImage(textBox_2Out, pictureBox_2Out);
            if (!string.IsNullOrWhiteSpace(textBox_2name.Text))
            {
                pictureBox_2name.Hide();
            }
            else
            {
                AssignErrorImageTo(pictureBox_2name);
                good = false;
            }

            if (good)
            {
                this.groupBox_bottom.Enabled = true;
                return ValidateGroup3();
            }
            else
            {
                this.groupBox_bottom.Enabled = false;
                return false;
            }
        }

        private bool ValidateGroup3()
        {
            if (comboBox_entropy.SelectedIndex >= 0 && comboBox_entropy.SelectedIndex <= comboBox_entropy.Items.Count &&
                numericUpDown_level.Value >= 5 && numericUpDown_level.Value <= 100)
            {
                this.button_generate.Enabled = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        private string ChooseFile(string filter = "Application|*.exe")
        {
            this.openFileDialog.Filter = filter;
            this.openFileDialog.FileName = "";
            if (this.openFileDialog.ShowDialog().Equals(DialogResult.OK))
            {
                return this.openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        private void group1Validater(object sender, EventArgs e)
        {
            ValidateGroup1();
        }

        private void group2Validater(object sender, EventArgs e)
        {
            ValidateGroup2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox_1Out.Text) || string.IsNullOrWhiteSpace(this.textBox_2Out.Text))
            {
                throw new ArgumentNullException("Out Files must be specified");
            }

            this.groupBox4.Enabled = false;
            this.panel1.Show();
            this.panel1.BringToFront();

            WorkerArgs args = new WorkerArgs();
            args.level = (int)this.numericUpDown_level.Value;
            args.entropy = this.comboBox_entropy.SelectedItem.ToString();
            args.i1 = this.textBox_1Sav.Text;
            args.i2 = this.textBox_2Sav.Text;
            args.o1 = this.textBox_1Out.Text;
            args.o2 = this.textBox_2Out.Text;
            args.p64 = this.textbox_projN64Location.Text;
            args.pgExe = this.textbox_pokemonGeneratorExeLocation.Text;
            args.game1 = this.comboBox_1game.SelectedItem.ToString();
            args.game2 = this.comboBox_2game.SelectedItem.ToString();
            args.name1 = this.textBox_1name.Text;
            args.name2 = this.textBox_2name.Text;
            this.backgroundWorker.RunWorkerAsync(args);
        }

        private void PokemonGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, 0);       // Unregister hotkey with id 0 before closing the form. You might want to call this more than once with different id values if you are planning to register more than one hotkey.
        }

        private void button_projN64Location_Click(object sender, EventArgs e)
        {
            this.textbox_projN64Location.Text = ChooseFile() ?? this.textbox_projN64Location.Text;
            ValidateGroup1();
        }

        private void button_pokemonGeneratorExeLocation_Click(object sender, EventArgs e)
        {
            this.textbox_pokemonGeneratorExeLocation.Text = ChooseFile() ?? this.textbox_pokemonGeneratorExeLocation.Text;
            ValidateGroup1();
        }

        private void button_1Sav_Click(object sender, EventArgs e)
        {
            this.textBox_1Sav.Text = ChooseFile("Save Files|*.sav") ?? this.textBox_1Sav.Text;
            ValidateGroup2();
        }

        private void textBox_1Out_Click(object sender, EventArgs e)
        {
            this.textBox_1Out.Text = ChooseFile("Save Files|*.sav") ?? this.textBox_1Out.Text;
            ValidateGroup2();
        }

        private void button_2Out_Click(object sender, EventArgs e)
        {
            this.textBox_2Out.Text = ChooseFile("Save Files|*.sav") ?? this.textBox_2Out.Text;
            ValidateGroup2();
        }

        private void button_2Sav_Click(object sender, EventArgs e)
        {
            this.textBox_2Sav.Text = ChooseFile("Save Files|*.sav") ?? this.textBox_2Sav.Text;
            ValidateGroup2();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            WorkerArgs wArg = (WorkerArgs)e.Argument;

            worker.ReportProgress(10, "CLOSING PROJECT64...");
            Thread.Sleep(500); // watch pika dance!


            // Kill N64 if running
            foreach (var process in Process.GetProcessesByName("Project64"))
            {
                process.Kill();
            }

            while (Process.GetProcessesByName("Project64").Any())
            {
                Thread.Sleep(50); // wait for closed
            }

            worker.ReportProgress(20, "GENERATING POKEMON...");

            // Start generate pokemon process
            StringBuilder args = new StringBuilder();
            args.Append($" --level {wArg.level} ");
            args.Append($" --entropy {wArg.entropy} ");
            args.Append($" --game1  {wArg.game1}");
            args.Append($" --game2 {wArg.game2}");
            if (!string.IsNullOrWhiteSpace(wArg.i1)) { args.Append($" --i1 \"{wArg.i1}\" "); }
            if (!string.IsNullOrWhiteSpace(wArg.i2)) { args.Append($" --i2 \"{wArg.i2}\" "); }
            if (!string.IsNullOrWhiteSpace(wArg.o1)) { args.Append($" --o1 \"{wArg.o1}\" "); }
            if (!string.IsNullOrWhiteSpace(wArg.o2)) { args.Append($" --o2 \"{wArg.o2}\" "); }
            if (!string.IsNullOrWhiteSpace(wArg.name1)) { args.Append($" --name1 \"{wArg.name1}\" "); }
            if (!string.IsNullOrWhiteSpace(wArg.name2)) { args.Append($" --name2 \"{wArg.name2}\" "); }

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.GetFullPath(wArg.pgExe);
            startInfo.Arguments = args.ToString();
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            var p = Process.Start(startInfo);

            if (p.WaitForExit(10000))
            {
                worker.ReportProgress(80, "STARTING PROJECT64...");

                // Update ini file with new sav locations
                if (!string.IsNullOrWhiteSpace(wArg.o1) && !string.IsNullOrWhiteSpace(wArg.o2))
                {
                    editor.ChangeSavLocations(wArg.o1, wArg.o2);
                }

                // Get Recent N64 Rom
                var rom = n64Config?.GetRecentRom() ?? "";

                //  Start N64 back up again
                startInfo = new ProcessStartInfo();
                startInfo.FileName = Path.GetFullPath(wArg.p64);
                startInfo.Arguments = $"\"{rom}\"";
                Process.Start(startInfo);
            }
            else
            {
                // timed out
                throw new TimeoutException("Timed out running pokemon generator. Please try again.");
            }


            Thread.Sleep(1000); // dance dance!
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.label11.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }

            this.groupBox4.Enabled = true;
            this.panel1.Hide();
        }

        private void ValidateGroup2_Event(object sender, EventArgs e)
        {
            ValidateGroup2();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}