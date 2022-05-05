using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace JeuxVideal.Model
{
    internal class Serialize
    {
        private readonly BinaryFormatter formatter = new BinaryFormatter();
        private readonly ObservableCollection<Cell> cells;
        private bool[,] currentState;
        private string _StringPath;


        public Serialize(ObservableCollection<Cell> gameCells, int taille)
        {
            cells = gameCells;
            currentState = new bool[taille, taille]; 
            this.GetEtatGrille();
        }
        public void GetEtatGrille()
        {
            foreach (var cell in this.cells)
            {
                this.currentState[(int)cell.YIndex, (int)cell.XIndex] = cell.IsAlive;
            }
        }

        public void SauvegardeFichier()
        {
            this.GetFilePathSave();

            if (_StringPath != null)
            {
                this.GetEtatGrille();
                using(FileStream stream = File.OpenWrite(_StringPath))
                {
                    this.formatter.Serialize(stream, this.currentState);
                }
            }


        }
        private void GetFilePathSave()
        {
            var windowDialog = new SaveFileDialog();
            windowDialog.Filter = "Game of Life files (*.GAY)|*.GAY";
            windowDialog.FilterIndex = 1;
            windowDialog.RestoreDirectory = true;
            if ((bool)windowDialog.ShowDialog())
                _StringPath = windowDialog.FileName;
            else
                return;

        }
        private void GetFilePathLoad()
        {
            var windowDialog = new OpenFileDialog();
            windowDialog.Filter = "Game of Life files (*.GAY)|*.GAY";
            windowDialog.FilterIndex = 1;
            windowDialog.RestoreDirectory = true;
            if ((bool)windowDialog.ShowDialog())
                _StringPath = windowDialog.FileName;
            else
                return;
        }
        public void ChargerFicher()
        {
            this.GetFilePathLoad();
            if(_StringPath != null)
            {
                using (FileStream stream = File.OpenRead(_StringPath))
                {
                    currentState = (bool[,])formatter.Deserialize(stream);
                }
                ChargerTableau();
            }

        }

        private void ChargerTableau()
        {
            foreach(Cell c in cells)
            {
                c.IsAlive = currentState[c.YIndex, c.XIndex];
            }
        }
    }
}
