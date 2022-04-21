using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JeuxVideal.Model
{
    internal class Serialize
    {
        private readonly BinaryFormatter formatter = new BinaryFormatter();
        private readonly List<Cell> cells;
        private bool[,] currentState;
        private string _filePath;


        public Serialize(List<Cell> gameCells, int taille)
        {
            cells = gameCells;
            currentState = new bool[taille, taille]; 
            this.GetCurrentGameState();
        }
        public void GetCurrentGameState()
        {
            foreach (var cell in this.cells)
            {
                this.currentState[(int)cell.YIndex, (int)cell.XIndex] = cell.IsAlive;
            }
        }

        public void SaveFile()
        {
            if (string.IsNullOrEmpty(_filePath))
                this.GetFilePath();

            this.GetCurrentGameState();

            using(var stream = File.OpenWrite(this._filePath))
            {
                this.formatter.Serialize(stream, this.currentState);
            }
        }
        public void GetFilePath()
        {
            var windowDialog = new Microsoft.Win32.SaveFileDialog();
            windowDialog.Filter = "Game of Life files (*.GOL)|*.GOL|All files (*.*)|*.*";
            windowDialog.FilterIndex = 1;
            windowDialog.RestoreDirectory = true;
            if ((bool)windowDialog.ShowDialog())
                _filePath = windowDialog.FileName;
            else
                return;

        }
        public void LoadFile()
        {
            var windowDialog = new Microsoft.Win32.OpenFileDialog();

            windowDialog.Filter = "Game of Life files (*.GOL)|*.GOL|All files (*.*)|*.*";
            windowDialog.FilterIndex = 1;
            //windowDialog.RestoreDirectory = true;
            

        }


    }
}
