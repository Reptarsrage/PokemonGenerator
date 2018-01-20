using PokemonGenerator.DAL;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PokemonGenerator.Forms
{
    public partial class PokemonSettingsForm : Form
    {
        private readonly IPokemonDA _pokemonDA;
        private readonly PersistentConfig _config;
        private readonly IPersistentConfigManager _configManager;
        private readonly string _contentDirectory;

        private int _total;
        private int _selected;

        public PokemonSettingsForm(IPokemonDA pokemonDA, 
            IPersistentConfigManager persistentConfigManager, 
            IDirectoryUtility directoryUtility)
        {
            InitializeComponent();
            _pokemonDA = pokemonDA;
            _configManager = persistentConfigManager;
            _contentDirectory = directoryUtility.ContentDirectory();

            // Load Persistent Config
            var configFileName = ConfigurationManager.AppSettings["configFileName"];
            if (!Path.IsPathRooted(configFileName))
            {
                configFileName = Path.Combine(_contentDirectory, configFileName);
            }
            _configManager.ConfigFilePath = configFileName;
            _config = _configManager.Load();
        }

        private void UpdateCount()
        {
            LabelCount.Text = $"{_selected}/{_total} Selected";
        }

        private void PokemonGeneratorLoad(object sender, EventArgs e)
        {
            BackgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var pokemon = _pokemonDA.GetAllPokemon();

            var worker = sender as BackgroundWorker;

            foreach (var poke in pokemon)
            {
                worker.ReportProgress(1, poke);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var poke = e.UserState as PokemonEntry;
            var selected = _config.Configuration.IgnoredPokemon.All(id => poke.Id != id);

            // Create Item
            var item = new SpriteButton(poke.Id - 1 /* Convert to zero based from pokemon 1-based id */, selected)
            {
                Name = poke.Id.ToString(),
                Text = poke.Identifier.ToUpper()
            };

            // Add Item
            LayoutPanelMain.Controls.Add(item);

            // Maintain info
            _total++;
            if (selected)
            {
                _selected++;
            }

            // Bind events
            item.ItemSelctedEvent += Item_ItemSelctedEvent;
        }

        private void Item_ItemSelctedEvent(object sender, ItemSelectedEventArgs args)
        {
            var button = sender as SpriteButton;
            var idx = button.Index + 1 /* Convert back from zero based to pokemon 1-based id */;

            _selected += args.Selected ? 1 : -1;

            if (args.Selected)
            {
                _config.Configuration.IgnoredPokemon.Remove(idx);
            } else
            {
                _config.Configuration.IgnoredPokemon.Add(idx);
            }

            UpdateCount();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_selected < 6)
            {
                MessageBox.Show("Please selecte at least 6 Pokemon.", "Unable to save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            _configManager.Save(_config);
            Close();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateCount();
        }

        private void ButtonSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var btn in LayoutPanelMain.Controls.OfType<SpriteButton>())
            {
                btn.Checked = true;
            }
        }

        private void ButtonSelectNone_Click(object sender, EventArgs e)
        {
            foreach (var btn in LayoutPanelMain.Controls.OfType<SpriteButton>())
            {
                btn.Checked = false;
            }
        }
    }
}