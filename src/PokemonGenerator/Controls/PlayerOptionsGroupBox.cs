using Microsoft.Extensions.Options;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Validators;
using System;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    public partial class PlayerOptionsGroupBox : WindowBase
    {
        private IOptions<PersistentConfig> _options;
        private IPokeGeneratorOptionsValidator _optionsValidator;

        public PlayerOptionsGroupBox() { /* For Designer only */ }

        public void Initialize(
            IOptions<PersistentConfig> options,
            IPokeGeneratorOptionsValidator optionsValidator)
        {
            _optionsValidator = optionsValidator;
            _options = options;

            // Init
            InitializeComponent();
            GroupBox.Text = Text;
            SelectPlayerVersion.DataSource = new[] { "Gold", "Silver" };
        }

        public new bool Validate()
        {
            return ValidatePlayerSection();
        }

        public string PlayerName
        {
            get
            {
                return TextPlayerName.Text;
            }
            set
            {
                UpdateText(TextPlayerName, value);
            }
        }

        public string InLocation
        {
            get
            {
                return TextPlayerInLocation.Text;
            }
            set
            {
                UpdateText(TextPlayerInLocation, value);
            }
        }

        public string OutLocation
        {
            get
            {
                return TextPlayerOutLocation.Text;
            }
            set
            {
                UpdateText(TextPlayerOutLocation, value);
            }
        }

        public PlayerOptions DataSource
        {
            get
            {
                return DataSource;
            }
            set
            {
                BindingSource.DataSource = value;
            }
        }

        public void SetOutLocationImage(ImageState state) => ToggleErrorImageToPic(ImagePlayerOutLocation, state);

        public void SetInLocationImage(ImageState state) => ToggleErrorImageToPic(ImagePlayerInLocation, state);

        public void SetNameImage(ImageState state) => ToggleErrorImageToPic(ImagePlayerName, state);

        private void UpdateText(TextBox textBox, string val)
        {
            textBox.Text = val;
            textBox.DataBindings[0].WriteValue();
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
            var good = CheckIfFileExistsAndAssignImage(TextPlayerInLocation, ImagePlayerInLocation, ".sav");
            good &= CheckIfPathIsValidAndAssignImage(TextPlayerOutLocation, ImagePlayerOutLocation, ".sav");

            // Check Player One Name
            var TextPlayerNameGood = _optionsValidator.ValidateName(TextPlayerName.Text);
            ToggleErrorImageToPic(ImagePlayerName, TextPlayerNameGood ? ImageState.Good : ImageState.Bad);
            good &= TextPlayerNameGood;

            return good;
        }

        private string ChooseFile(string filter = "Application|*.exe")
        {
            OpenFileDialog.Filter = filter;
            OpenFileDialog.FileName = "";
            return OpenFileDialog.ShowDialog().Equals(DialogResult.OK) ? OpenFileDialog.FileName : null;
        }

        private void ButtonPlayerInLocationClick(object sender, EventArgs e)
        {
            UpdateText(TextPlayerInLocation, ChooseFile("Save Files|*.sav") ?? TextPlayerInLocation.Text);
            ValidatePlayerSection();
        }

        private void ButtonPlayerOutLocationClick(object sender, EventArgs e)
        {
            UpdateText(TextPlayerOutLocation, ChooseFile("Save Files|*.sav") ?? TextPlayerOutLocation.Text);
            ValidatePlayerSection();
        }

        private void PlayerValidate(object sender, EventArgs e)
        {
            ValidatePlayerSection();
        }

        private void TeamClick(object sender, MouseEventArgs e)
        {
            OnWindowOpenedEvent(this, new WindowEventArgs(typeof(TeamSelectionWindow)));
        }

        private void TeamClick(object sender, EventArgs e)
        {
            OnWindowOpenedEvent(this, new WindowEventArgs(typeof(TeamSelectionWindow)));
        }
    }
}