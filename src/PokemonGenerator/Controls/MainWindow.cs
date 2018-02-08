using Microsoft.Extensions.Options;
using PokemonGenerator.Editors;
using PokemonGenerator.Generators;
using PokemonGenerator.IO;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Validators;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    public enum ImageState
    {
        Unkown,
        Bad,
        Good,
        Warning,
        FileBad,
        FileGood,
        FileWarning
    }

    public partial class MainWindow : WindowBase
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
        private readonly INRageIniEditor _nRageIniEditor;
        private readonly IP64ConfigEditor _p64ConfigEditor;
        private readonly IPokemonGeneratorRunner _pokemonGeneratorRunner;
        private readonly IOptions<PersistentConfig> _options;
        private readonly IPokeGeneratorOptionsValidator _optionsValidator;
        private readonly IPersistentConfigManager _configManager;

        public MainWindow(
            IPokemonGeneratorRunner pokemonGeneratorRunner,
            IOptions<PersistentConfig> options,
            IP64ConfigEditor p64ConfigEditor,
            INRageIniEditor nRageIniEditor,
            IPersistentConfigManager configManager,
            IPokeGeneratorOptionsValidator optionsValidator)
        {
            _pokemonGeneratorRunner = pokemonGeneratorRunner;
            _nRageIniEditor = nRageIniEditor;
            _configManager = configManager;
            _p64ConfigEditor = p64ConfigEditor;
            _optionsValidator = optionsValidator;
            _options = options;

            // Init
            InitializeComponent();
            GroupBoxPlayerOneOptions.Initialize(options, _optionsValidator);
            GroupBoxPlayerTwoOptions.Initialize(options, _optionsValidator);

            // Data Bind
            MainWindowBindingSource.DataSource = _options.Value.Options;
            GroupBoxPlayerOneOptions.DataSource = _options.Value.Options.PlayerOne;
            GroupBoxPlayerTwoOptions.DataSource = _options.Value.Options.PlayerTwo;

            // Listen to children opening windows
            GroupBoxPlayerOneOptions.WindowOpenedEvent += ChildWindowOpenedEvent;
            GroupBoxPlayerTwoOptions.WindowOpenedEvent += ChildWindowOpenedEvent;

            GroupBoxPlayerOneOptions.WindowClosedEvent += ChildWindowClosedEvent;
            GroupBoxPlayerTwoOptions.WindowClosedEvent += ChildWindowClosedEvent;

            // Register keys
            RegisterHotKey(Handle, 0, (int)KeyModifier.Control, Keys.F12.GetHashCode());
        }

        /// <summary>
        /// Catch the Ctrl + F12 Macro and execute!
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            try
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
            catch
            {
                UnregisterHotKey(Handle, 0);
                WndProc(ref m);
            }
        }

        public override void Shown()
        {
            ValidateTopSection();
        }

        public override void Closed()
        {
            // Save configuration
            _configManager.Save();
        }

        private void ChildWindowOpenedEvent(object sender, WindowEventArgs args)
        {
            OnWindowOpenedEvent(sender, args);
        }

        private void ChildWindowClosedEvent(object sender, WindowEventArgs args)
        {
            OnWindowClosedEvent(sender, args);
        }

        private void UpdateText(TextBox textBox, string val)
        {
            textBox.Text = val;
            textBox.DataBindings[0].WriteValue();
        }

        private void TextProjN64LocationValidate(object sender, EventArgs e)
        {
            ValidateTopSection();
        }

        private bool ValidateTopSection()
        {
            if (CheckIfFileExistsAndAssignImage(TextProjN64Location, ImageProjN64Location, ".exe"))
            {
                GroupBoxPlayerOneOptions.Enabled = true;
                GroupBoxPlayerTwoOptions.Enabled = true;

                // get ini location
                var ini = Path.Combine(Path.GetDirectoryName(TextProjN64Location.Text), @"Config\NRage.ini");
                var cfg = Path.Combine(Path.GetDirectoryName(TextProjN64Location.Text), @"Config\Project64.cfg");

                if (File.Exists(ini))
                {
                    // Fill in blanks with ini data
                    _nRageIniEditor.FileName = ini;
                    var tup = _nRageIniEditor.GetRomAndSavFileLocation(1);
                    var tup2 = _nRageIniEditor.GetRomAndSavFileLocation(2);

                    if (string.IsNullOrWhiteSpace(_options.Value.Options.PlayerOne.InputSaveLocation))
                    {
                        GroupBoxPlayerOneOptions.InLocation = tup.Item2;
                    }

                    if (string.IsNullOrWhiteSpace(_options.Value.Options.PlayerOne.OutputSaveLocation))
                    {
                        GroupBoxPlayerOneOptions.OutLocation = string.IsNullOrWhiteSpace(tup.Item2) ?
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), @"Player1.sav") :
                            tup.Item2;
                    }

                    if (string.IsNullOrWhiteSpace(_options.Value.Options.PlayerTwo.InputSaveLocation))
                    {

                        GroupBoxPlayerTwoOptions.InLocation = tup2.Item2;
                    }

                    if (string.IsNullOrWhiteSpace(_options.Value.Options.PlayerTwo.OutputSaveLocation))
                    {
                        GroupBoxPlayerTwoOptions.OutLocation = string.IsNullOrWhiteSpace(tup2.Item2) ?
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), @"Player2.sav") :
                            tup2.Item2;
                    }
                }
                if (File.Exists(cfg))
                {
                    _p64ConfigEditor.FileName = cfg;
                }

                GroupBoxPlayerOneOptions.PlayerName = _options.Value.Options.PlayerOne.Name;
                GroupBoxPlayerTwoOptions.PlayerName = _options.Value.Options.PlayerTwo.Name;
                return ValidatePlayerSection();
            }
            else
            {
                GroupBoxPlayerOneOptions.Enabled = false;
                GroupBoxPlayerTwoOptions.Enabled = false;
                PanelBottom.Enabled = false;
                return false;
            }
        }

        private bool CheckIfFileExistsAndAssignImage(TextBox textbox, PictureBox pic, string expectedExtension)
        {
            var good = _optionsValidator.ValidateFileOption(textbox.Text, expectedExtension);
            ToggleErrorImageToPic(pic, good ? ImageState.FileGood : ImageState.FileBad);
            return good;
        }

        private bool CheckIfPathIsValidAndAssignImage(TextBox textbox, PictureBox pic, string expectedExtension)
        {
            var good = _optionsValidator.ValidateFilePathOption(textbox.Text, expectedExtension);
            ToggleErrorImageToPic(pic, good ? ImageState.FileGood : ImageState.FileBad);
            return good;
        }

        private void ToggleErrorImageToPic(PictureBox pic, ImageState state)
        {
            switch (state)
            {
                case ImageState.Bad:
                    pic.BackgroundImage = Properties.Resources.StatusInvalid_16x;
                    break;
                case ImageState.Good:
                    pic.BackgroundImage = Properties.Resources.StatusOK_16x;
                    break;
                case ImageState.Warning:
                    pic.BackgroundImage = Properties.Resources.StatusWarning_16x;
                    break;
                case ImageState.FileBad:
                    pic.BackgroundImage = Properties.Resources.FileError_16x;
                    break;
                case ImageState.FileGood:
                    pic.BackgroundImage = Properties.Resources.FileOK_16x;
                    break;
                case ImageState.FileWarning:
                    pic.BackgroundImage = Properties.Resources.FileWarning_16x;
                    break;
                default:
                    pic.BackgroundImage = null;
                    break;
            }
            pic.Show();
        }

        private bool ValidatePlayerSection()
        {
            // Check Player One In/Out Locations
            var good = GroupBoxPlayerOneOptions.Validate() && GroupBoxPlayerTwoOptions.Validate();

            // Check two outs are unique
            if (!_optionsValidator.ValidateUniquePath(GroupBoxPlayerOneOptions.OutLocation, GroupBoxPlayerTwoOptions.OutLocation))
            {
                GroupBoxPlayerOneOptions.SetOutLocationImage(ImageState.FileBad);
                GroupBoxPlayerTwoOptions.SetOutLocationImage(ImageState.FileBad);
                good = false;
            }

            // Enable and Validate Bottom Section
            PanelBottom.Enabled = good;
            return good ? ValidateBottomSection() : good;
        }

        private bool ValidateBottomSection()
        {
            var good = true;

            // Check Level
            var LevelGood = _optionsValidator.ValidateLevel((int)SelectLevel.Value);
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
            if (string.IsNullOrWhiteSpace(GroupBoxPlayerOneOptions.OutLocation) || string.IsNullOrWhiteSpace(GroupBoxPlayerTwoOptions.OutLocation))
            {
                throw new ArgumentNullException("Out Files must be specified");
            }

            PanelOuter.Enabled = false;
            PanelProgress.Show();
            PanelProgress.BringToFront();

            BackgroundPokemonGenerator.RunWorkerAsync(_options.Value);
        }

        private void ButtonProjN64LocationClick(object sender, EventArgs e)
        {
            UpdateText(TextProjN64Location, ChooseFile() ?? TextProjN64Location.Text);
            ValidateTopSection();
        }

        private void BackgroundPokemonGeneratorDoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var args = e.Argument as PersistentConfig;

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
            _pokemonGeneratorRunner.Run(args);
            Thread.Sleep(1000); // dance dance!

            // Start P64
            worker.ReportProgress(80, "STARTING PROJECT64...");

            // Update ini file with new sav locations
            if (!string.IsNullOrWhiteSpace(args.Options.PlayerOne.OutputSaveLocation) && !string.IsNullOrWhiteSpace(args.Options.PlayerTwo.OutputSaveLocation))
            {
                _nRageIniEditor.ChangeSavLocations(args.Options.PlayerOne.OutputSaveLocation, args.Options.PlayerTwo.OutputSaveLocation);
            }

            // Get Recent N64 Rom
            try
            {
                var rom = _p64ConfigEditor?.GetRecentRom() ?? "";

                //  Start N64 back up again
                var startInfo = new ProcessStartInfo
                {
                    FileName = Path.GetFullPath(args.Options.Project64Location),
                    Arguments = $"\"{rom}\""
                };

                Process.Start(startInfo);
            }
            catch
            {
                throw new ExternalException("Unable to launch Project64. Config file not found or corrupt.");
            }
        }

        private void BackgroundPokemonGeneratorProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LabelProgress.Text = e.UserState.ToString();
        }

        private void BackgroundPokemonGeneratorCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"Error encountered while running generator:\n{e.Error.ToString()}\nPlease check your input and try again.");
            }

            PanelOuter.Enabled = true;
            PanelProgress.Hide();
        }

        private void PlayerValidate(object sender, EventArgs e)
        {
            ValidatePlayerSection();
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            OnWindowOpenedEvent(this, new WindowEventArgs(typeof(OptionsWindowController)));
        }
    }
}