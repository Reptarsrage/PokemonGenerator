﻿using PokemonGenerator.Editors;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using PokemonGenerator.Validators;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PokemonGenerator.Forms
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
        private readonly INRageIniEditor _nRageIniEditor;
        private readonly IP64ConfigEditor _p64ConfigEditor;
        private readonly IPokemonGeneratorRunner _pokemonGeneratorRunner;
        private readonly IPersistentConfigManager _configManager;
        private readonly IPokeGeneratorOptionsValidator _optionsValidator;
        private readonly string _contentDirectory;
        private readonly string _outputDirectory;

        private PersistentConfig _config;

        public PokemonGeneratorForm(IPokemonGeneratorRunner pokemonGeneratorRunner,
            IPersistentConfigManager configManager,
            IP64ConfigEditor p64ConfigEditor,
            INRageIniEditor nRageIniEditor,
            IPokeGeneratorOptionsValidator optionsValidator,
            string contentDirectory, string outputDirectory)
        {
            _contentDirectory = contentDirectory;
            _outputDirectory = outputDirectory;
            _pokemonGeneratorRunner = pokemonGeneratorRunner;
            _configManager = configManager;
            _nRageIniEditor = nRageIniEditor;
            _p64ConfigEditor = p64ConfigEditor;
            _optionsValidator = optionsValidator;

            // Load Persistent Config
            var configFileName = ConfigurationManager.AppSettings["configFileName"];
            if (!Path.IsPathRooted(configFileName))
            {
                configFileName = Path.Combine(_contentDirectory, configFileName);
            }
            _configManager.ConfigFilePath = configFileName;
            _config = _configManager.Load();

            // Init
            InitializeComponent();
            RegisterHotKey(Handle, 0, (int)KeyModifier.Control, Keys.F12.GetHashCode());

            // Data Bind
            TextPlayerOneInLocation.DataBindings.Add(new Binding("Text", _config.Options, "InputSaveOne", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty));
            TextPlayerTwoInLocation.DataBindings.Add(new Binding("Text", _config.Options, "InputSaveTwo", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty));
            TextPlayerOneName.DataBindings.Add(new Binding("Text", _config.Options, "NameOne", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty));
            TextPlayerTwoName.DataBindings.Add(new Binding("Text", _config.Options, "NameTwo", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty));
            TextPlayerOneOutLocation.DataBindings.Add(new Binding("Text", _config.Options, "OutputSaveOne", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty));
            TextPlayerTwoOutLocation.DataBindings.Add(new Binding("Text", _config.Options, "OutputSaveTwo", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty));
            TextProjN64Location.DataBindings.Add(new Binding("Text", _config.Options, "Project64Location", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty));
            SelectLevel.DataBindings.Add(new Binding("Value", _config.Options, "Level", true, DataSourceUpdateMode.OnPropertyChanged, 0));
            SelectPlayerOneGame.DataBindings.Add(new Binding("Text", _config.Options, "GameOne", true, DataSourceUpdateMode.OnPropertyChanged, 0));
            SelectPlayerTwoGame.DataBindings.Add(new Binding("Text", _config.Options, "GameTwo", true, DataSourceUpdateMode.OnPropertyChanged, 0));
            SelectEntropy.SelectedIndex = 0;
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

        private void PokemonGeneratorLoad(object sender, EventArgs e)
        {
            SelectEntropy.SelectedIndex = 0;
            ValidateTopSection();
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
                GroupBoxPlayerOne.Enabled = true;
                GroupBoxPlayerTwo.Enabled = true;

                // get ini location
                if (string.IsNullOrWhiteSpace(_nRageIniEditor.FileName))
                {
                    var ini = Path.Combine(Path.GetDirectoryName(TextProjN64Location.Text), @"Config\NRage.ini");
                    var cfg = Path.Combine(Path.GetDirectoryName(TextProjN64Location.Text), @"Config\Project64.cfg");

                    if (File.Exists(ini))
                    {
                        _nRageIniEditor.FileName = ini;
                        var tup = _nRageIniEditor.GetRomAndSavFileLocation(1);
                        var tup2 = _nRageIniEditor.GetRomAndSavFileLocation(2);

                        SelectPlayerOneGame.SelectedIndex = string.IsNullOrWhiteSpace(_config.Options.GameOne) ? 0 : SelectPlayerOneGame.Items.IndexOf(_config.Options.GameOne);
                        if (string.IsNullOrWhiteSpace(_config.Options.InputSaveOne)) UpdateText(TextPlayerOneInLocation, tup.Item2);
                        if (string.IsNullOrWhiteSpace(_config.Options.OutputSaveOne)) UpdateText(TextPlayerOneOutLocation, string.IsNullOrWhiteSpace(tup.Item2) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), @"Player2.sav") : tup.Item2);

                        SelectPlayerTwoGame.SelectedIndex = string.IsNullOrWhiteSpace(_config.Options.GameOne) ? 0 : SelectPlayerTwoGame.Items.IndexOf(_config.Options.GameOne);
                        if (string.IsNullOrWhiteSpace(_config.Options.InputSaveOne)) UpdateText(TextPlayerTwoInLocation, tup2.Item2);
                        if (string.IsNullOrWhiteSpace(_config.Options.OutputSaveOne)) UpdateText(TextPlayerTwoOutLocation, string.IsNullOrWhiteSpace(tup2.Item2) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), @"Player2.sav") : tup2.Item2);
                    }
                    if (File.Exists(cfg))
                    {
                        _p64ConfigEditor.FileName = cfg;
                    }
                }
                UpdateText(TextPlayerOneName, _config.Options.NameOne);
                UpdateText(TextPlayerTwoName, _config.Options.NameTwo);
                return ValidatePlayerSection();
            }
            else
            {
                GroupBoxPlayerOne.Enabled = false;
                GroupBoxPlayerTwo.Enabled = false;
                GroupBoxBottom.Enabled = false;
                return false;
            }
        }

        private bool CheckIfFileExistsAndAssignImage(TextBox textbox, PictureBox pic, string expectedExtension)
        {
            var error = _optionsValidator.ValidateFileOption(textbox.Text, expectedExtension);
            ToggleErrorImageToPic(pic, error);
            return !error;
        }

        private bool CheckIfPathIsValidAndAssignImage(TextBox textbox, PictureBox pic, string expectedExtension)
        {
            var error = _optionsValidator.ValidateFilePathOption(textbox.Text, expectedExtension);
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
            // Check Player One In/Out Locations
            var good = CheckIfFileExistsAndAssignImage(TextPlayerOneInLocation, ImagePlayerOneInLocation, ".sav");
            good &= CheckIfPathIsValidAndAssignImage(TextPlayerOneOutLocation, ImagePlayerOneOutLocation, ".sav");

            // Check Player One Name
            var TextPlayerOneNameGood = _optionsValidator.ValidateName(TextPlayerOneName.Text);
            ToggleErrorImageToPic(ImagePlayerOneName, !TextPlayerOneNameGood);
            good &= TextPlayerOneNameGood;

            // Check Player Two In/Out Locations
            good &= CheckIfFileExistsAndAssignImage(TextPlayerTwoInLocation, ImagePlayerTwoInLocation, ".sav");
            good &= CheckIfPathIsValidAndAssignImage(TextPlayerTwoOutLocation, ImagePlayerTwoOutLocation, ".sav");

            // Check Player Two Name
            var TextPlayerTwoNameGood = _optionsValidator.ValidateName(TextPlayerTwoName.Text);
            ToggleErrorImageToPic(ImagePlayerTwoName, !TextPlayerTwoNameGood);
            good &= TextPlayerTwoNameGood;

            // Check two outs are unique
            if (!_optionsValidator.ValidateUniquePath(TextPlayerTwoOutLocation.Text, TextPlayerOneOutLocation.Text))
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
            var EntropyGood = _optionsValidator.ValidateEntropy(SelectEntropy.Text);

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
            if (string.IsNullOrWhiteSpace(TextPlayerOneOutLocation.Text) || string.IsNullOrWhiteSpace(TextPlayerTwoOutLocation.Text))
            {
                throw new ArgumentNullException("Out Files must be specified");
            }

            GroupBoxOuter.Enabled = false;
            PanelProgress.Show();
            PanelProgress.BringToFront();

            BackgroundPokemonGenerator.RunWorkerAsync(_config);
        }

        private void PokemonGeneratorClosing(object sender, FormClosingEventArgs e)
        {
            // Unregister hotkey with id 0
            UnregisterHotKey(Handle, 0);

            // Save configuration
            _configManager.Save(_config);
        }

        private void ButtonProjN64LocationClick(object sender, EventArgs e)
        {
            UpdateText(TextProjN64Location, ChooseFile() ?? TextProjN64Location.Text);
            ValidateTopSection();
        }

        private void ButtonPlayerOneInLocationClick(object sender, EventArgs e)
        {
            UpdateText(TextPlayerOneInLocation, ChooseFile("Save Files|*.sav") ?? TextPlayerOneInLocation.Text);
            ValidatePlayerSection();
        }

        private void ButtonPlayerOneOutLocationClick(object sender, EventArgs e)
        {
            UpdateText(TextPlayerOneOutLocation, ChooseFile("Save Files|*.sav") ?? TextPlayerOneOutLocation.Text);
            ValidatePlayerSection();
        }

        private void ButtonPlayerTwoOutLocationClick(object sender, EventArgs e)
        {
            UpdateText(TextPlayerTwoOutLocation, ChooseFile("Save Files|*.sav") ?? TextPlayerTwoOutLocation.Text);
            ValidatePlayerSection();
        }

        private void ButtonPlayerTwoInLocationClick(object sender, EventArgs e)
        {
            UpdateText(TextPlayerTwoInLocation, ChooseFile("Save Files|*.sav") ?? TextPlayerTwoInLocation.Text);
            ValidatePlayerSection();
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
            if (!string.IsNullOrWhiteSpace(args.Options.OutputSaveOne) && !string.IsNullOrWhiteSpace(args.Options.OutputSaveTwo))
            {
                _nRageIniEditor.ChangeSavLocations(args.Options.OutputSaveOne, args.Options.OutputSaveTwo);
            }

            // Get Recent N64 Rom
            var rom = _p64ConfigEditor?.GetRecentRom() ?? "";

            //  Start N64 back up again
            var startInfo = new ProcessStartInfo
            {
                FileName = Path.GetFullPath(args.Options.Project64Location),
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
                MessageBox.Show($"Error encountered while running generator:\n{e.Error.Message}\nPlease check your input and try again.");
            }

            GroupBoxOuter.Enabled = true;
            PanelProgress.Hide();
        }

        private void PlayerValidate(object sender, EventArgs e)
        {
            ValidatePlayerSection();
        }
    }
}