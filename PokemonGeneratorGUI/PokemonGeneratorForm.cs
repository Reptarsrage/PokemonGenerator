using PokemonGenerator;
using PokemonGenerator.Models;
using PokemonGeneratorGUI.Editors;
using PokemonGeneratorGUI.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PokemonGeneratorGUI
{
    public partial class PokemonGeneratorForm : Form
    {
        // Imported to handle out-of-focus macro handeling
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
        private readonly string projN64Location;
        private INRageIniEditor editor;
        private IP64ConfigEditor n64Config;
        private IPokemonGeneratorRunner pokemonGeneratorRunner;
        private string contentDirectory;
        private string outputDirectory;

        public PokemonGeneratorForm()
        {
#if (DEBUG)
            projN64Location = @"G:\Project64\Project64.exe"; // TODO remove
            AppDomain.CurrentDomain.SetData("DataDirectory", AssemblyDirectory);
            contentDirectory = AssemblyDirectory;
            outputDirectory = Path.Combine(contentDirectory, "Output");
#else
            projN64Location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Project64\Project64.exe");
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\"));
            contentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\");
            outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
#endif
            InitializeComponent();
            RegisterHotKey(Handle, 0, (int)KeyModifier.Control, Keys.F12.GetHashCode());
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
                 * The below lines are useful in case you want to register multiple keys, which you can use a switch with the id as argument, 
                 * or if you want to know which key/modifier was pressed for some particular reason. */
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                var id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

                // do something
                if (id == 0 && ButtonGenerate.Enabled)
                {
                    ButtonGenerateClick(null, null);
                }
            }
        }

        private void PokemonGeneratorLoad(object sender, EventArgs e)
        {
            SelectEntropy.SelectedIndex = 0;
            TextProjN64Location.Text = projN64Location;
            ValidateTopSection();
        }

        private void TextProjN64LocationValidate(object sender, EventArgs e)
        {
            ValidateTopSection();
        }

        private bool ValidateTopSection()
        {
            if (CheckIfFileExistsAndAssignImage(TextProjN64Location, ImageProjN64Location, ".exe"))
            {
                // get ini location
                if (editor == null)
                {
                    var ini = Path.Combine(Path.GetDirectoryName(TextProjN64Location.Text), @"Config\NRage.ini");
                    var cfg = Path.Combine(Path.GetDirectoryName(TextProjN64Location.Text), @"Config\Project64.cfg");

                    if (File.Exists(ini))
                    {
                        editor = new NRageIniEditor(ini);
                        var tup = editor.GetRomAndSavFileLocation(1);
                        var tup2 = editor.GetRomAndSavFileLocation(2);

                        SelectPlayerOneGame.SelectedIndex = 0;
                        TextPlayerOneInLocation.Text = tup.Item2;
                        TextPlayerOneOutLocation.Text = string.IsNullOrWhiteSpace(tup.Item2) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), @"Player2.sav") : tup.Item2;

                        SelectPlayerTwoGame.SelectedIndex = 0;
                        TextPlayerTwoInLocation.Text = tup2.Item2;
                        TextPlayerTwoOutLocation.Text = string.IsNullOrWhiteSpace(tup2.Item2) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), @"Player2.sav") : tup2.Item2;
                    }
                    if (File.Exists(cfg))
                    {
                        n64Config = new P64ConfigEditor(cfg);
                    }
                }
                TextPlayerOneName.Text = "PLAYER1";
                TextPlayerTwoName.Text = "PLAYER2";
                GroupBoxPlayerOne.Enabled = true;
                GroupBoxPlayerTwo.Enabled = true;
                return ValidatePlayerSection();
            }
            else
            {
                GroupBoxPlayerOne.Enabled = false;
                GroupBoxPlayerTwo.Enabled = false;
                GroupBoxBottom.Enabled = false;
                editor = null;
                n64Config = null;
                return false;
            }
        }

        private bool CheckIfFileExistsAndAssignImage(TextBox textbox, PictureBox pic, string expectedExtension)
        {
            var error = string.IsNullOrEmpty(textbox.Text) || 
                !File.Exists(textbox.Text) ||
                !Path.GetExtension(textbox.Text).Equals(expectedExtension, StringComparison.OrdinalIgnoreCase);
            ToggleErrorImageToPic(pic, error);
            return !error;
        }

        private bool CheckIfPathIsValidAndAssignImage(TextBox textbox, PictureBox pic, string expectedExtension)
        {
            var error = string.IsNullOrEmpty(textbox.Text) || 
                textbox.Text.IndexOfAny(Path.GetInvalidPathChars()) != -1 ||
                !Path.GetExtension(textbox.Text).Equals(expectedExtension, StringComparison.OrdinalIgnoreCase);
            ToggleErrorImageToPic(pic, error);
            return !error;
        }

        private void ToggleErrorImageToPic(PictureBox pic, bool error)
        {
            if (error)
            {
                pic.Show();
            }
            else
            {
                pic.Hide();
            }
        }

        private bool ValidatePlayerSection()
        {
            var good = true;

            // Check Player One In/Out Locations
            good &= CheckIfFileExistsAndAssignImage(TextPlayerOneInLocation, ImagePlayerOneInLocation, ".sav");
            good &= CheckIfPathIsValidAndAssignImage(TextPlayerOneOutLocation, ImagePlayerOneOutLocation, ".sav");

            // Check Player One Name
            var TextPlayerOneNameGood = !string.IsNullOrWhiteSpace(TextPlayerOneName.Text);
            ToggleErrorImageToPic(ImagePlayerOneName, !TextPlayerOneNameGood);
            good &= TextPlayerOneNameGood;

            // Check Player Two In/Out Locations
            good &= CheckIfFileExistsAndAssignImage(TextPlayerTwoInLocation, ImagePlayerTwoInLocation, ".sav");
            good &= CheckIfPathIsValidAndAssignImage(TextPlayerTwoOutLocation, ImagePlayerTwoOutLocation, ".sav");

            // Check Player Two Name
            var TextPlayerTwoNameGood = !string.IsNullOrWhiteSpace(TextPlayerTwoName.Text);
            ToggleErrorImageToPic(ImagePlayerTwoName, !TextPlayerTwoNameGood);
            good &= TextPlayerTwoNameGood;

            // Check two outs are unique
            if (!string.IsNullOrWhiteSpace(TextPlayerTwoOutLocation.Text) && 
                !string.IsNullOrWhiteSpace(TextPlayerOneOutLocation.Text) && 
                Path.GetFullPath(TextPlayerTwoOutLocation.Text).Equals(Path.GetFullPath(TextPlayerOneOutLocation.Text)))
            {
                ToggleErrorImageToPic(ImagePlayerTwoOutLocation, true);
                ToggleErrorImageToPic(ImagePlayerOneOutLocation, true);
                good = false;
            }

            // Enable and Validate Bottom Section
            GroupBoxBottom.Enabled = good;
            return good ? ValidateBottomSection() : good;
        }

        private bool ValidateBottomSection()
        {
            var good = true;

            // Check Entropy
            var EntropyGood = SelectEntropy.SelectedIndex >= 0 && SelectEntropy.SelectedIndex <= SelectEntropy.Items.Count;
            good &= EntropyGood;

            // Check Level
            var LevelGood = SelectLevel.Value >= 5 && SelectLevel.Value <= 100;
            good &= LevelGood;

            ButtonGenerate.Enabled = good;
            return good;
        }

        private string ChooseFile(string filter = "Application|*.exe")
        {
            OpenFileDialog.Filter = filter;
            OpenFileDialog.FileName = "";
            return OpenFileDialog.ShowDialog().Equals(DialogResult.OK) ? OpenFileDialog.FileName : null;
        }

        private void TopSectionValidater(object sender, EventArgs e)
        {
            ValidateTopSection();
        }

        private void PlayerValidater(object sender, EventArgs e)
        {
            ValidatePlayerSection();
        }

        private void ButtonGenerateClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextPlayerOneOutLocation.Text) || string.IsNullOrWhiteSpace(TextPlayerTwoOutLocation.Text))
            {
                throw new ArgumentNullException("Out Files must be specified");
            }

            GroupBoxOuter.Enabled = false;
            PanelProgress.Show();
            PanelProgress.BringToFront();

            var args = new BackgroundPokemonGeneratorArgs
            {
                PokeGeneratorArguments = new PokeGeneratorArguments
                {
                    Level = (int)SelectLevel.Value,
                    EntropyVal = SelectEntropy.SelectedItem.ToString(),
                    InputSavOne = TextPlayerOneInLocation.Text,
                    InputSavTwo = TextPlayerTwoInLocation.Text,
                    OutputSav1 = TextPlayerOneOutLocation.Text,
                    OutputSav2 = TextPlayerTwoOutLocation.Text,
                    GameOne = SelectPlayerOneGame.SelectedItem.ToString(),
                    GameTwo = SelectPlayerTwoGame.SelectedItem.ToString(),
                    NameOne = TextPlayerOneName.Text,
                    NameTwo = TextPlayerTwoName.Text
                },
                Project64Location = TextProjN64Location.Text,
            };
            BackgroundPokemonGenerator.RunWorkerAsync(args);
        }

        private void PokemonGeneratorClosing(object sender, FormClosingEventArgs e)
        {
            // Unregister hotkey with id 0
            UnregisterHotKey(Handle, 0);
        }

        private void ButtonProjN64LocationClick(object sender, EventArgs e)
        {
            TextProjN64Location.Text = ChooseFile() ?? TextProjN64Location.Text;
            ValidateTopSection();
        }

        private void ButtonPlayerOneInLocationClick(object sender, EventArgs e)
        {
            TextPlayerOneInLocation.Text = ChooseFile("Save Files|*.sav") ?? TextPlayerOneInLocation.Text;
            ValidatePlayerSection();
        }

        private void ButtonPlayerOneOutLocationClick(object sender, EventArgs e)
        {
            TextPlayerOneOutLocation.Text = ChooseFile("Save Files|*.sav") ?? TextPlayerOneOutLocation.Text;
            ValidatePlayerSection();
        }

        private void ButtonPlayerTwoOutLocationClick(object sender, EventArgs e)
        {
            TextPlayerTwoOutLocation.Text = ChooseFile("Save Files|*.sav") ?? TextPlayerTwoOutLocation.Text;
            ValidatePlayerSection();
        }

        private void ButtonPlayerTwoInLocationClick(object sender, EventArgs e)
        {
            TextPlayerTwoInLocation.Text = ChooseFile("Save Files|*.sav") ?? TextPlayerTwoInLocation.Text;
            ValidatePlayerSection();
        }

        private void BackgroundPokemonGeneratorDoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var args = e.Argument as BackgroundPokemonGeneratorArgs;

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
            pokemonGeneratorRunner = new PokemonGeneratorRunner(contentDirectory, outputDirectory, args.PokeGeneratorArguments);
            pokemonGeneratorRunner.Run();
            Thread.Sleep(1000); // dance dance!

            // Start P64
            worker.ReportProgress(80, "STARTING PROJECT64...");

            // Update ini file with new sav locations
            if (!string.IsNullOrWhiteSpace(args.PokeGeneratorArguments.OutputSav1) && !string.IsNullOrWhiteSpace(args.PokeGeneratorArguments.OutputSav2))
            {
                editor.ChangeSavLocations(args.PokeGeneratorArguments.OutputSav1, args.PokeGeneratorArguments.OutputSav2);
            }

            // Get Recent N64 Rom
            var rom = n64Config?.GetRecentRom() ?? "";

            //  Start N64 back up again
            var startInfo = new ProcessStartInfo
            {
                FileName = Path.GetFullPath(args.Project64Location),
                Arguments = $"\"{rom}\""
            };
            Process.Start(startInfo);
        }

        private void BackgroundPokemonGeneratorProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LabelProgress.Text = e.UserState.ToString();
        }

        private void BackgroundPokemonGeneratorCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }

            GroupBoxOuter.Enabled = true;
            PanelProgress.Hide();
        }

        private void PlayerValidate(object sender, EventArgs e)
        {
            ValidatePlayerSection();
        }

        private string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}