using PokemonGenerator.DAL;
using PokemonGenerator.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PokemonGenerator.Forms
{
    public partial class PokemonSettingsForm : Form
    {
        private readonly IPokemonDA _pokemonDA;
        
        public PokemonSettingsForm(IPokemonDA pokemonDA)
        {
            InitializeComponent();
            _pokemonDA = pokemonDA;
        }

        private void PokemonGeneratorLoad(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
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
            var item = new SpriteButton(poke.Id - 1, true, true);
            item.Name = poke.Id.ToString();
            item.Text = poke.Identifier.ToUpper();
            layoutPanel.Controls.Add(item);
        }
    }
}