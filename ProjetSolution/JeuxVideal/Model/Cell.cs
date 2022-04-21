using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace JeuxVideal.Model
{
    public class Cell : INotifyPropertyChanged
    {
        #region Propriété et Valeur 
        public int XIndex { get; private set; }
        public int YIndex { get; private set; }
        public double XAffichage 
        {
            get { return XIndex * 15; }
        }
        public double YAffchage
        {
            get { return YIndex * 15; }
        }

        private bool _isAlive;

        #region propriété changer
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; NotifyPropertyChanged(); }
        }

        public bool IsAliveNext { get; set; }
            
        
        public List<Cell> CellsVoisine { get; set; }

        public int NbCellVoisine { get; set; }

        #endregion

        #region Constructeur
        /*public Cell(int _XIndex, int _YIndex)
            : this(_XIndex, _YIndex, false)
        {}*/

        public Cell(int _XIndex, int _YIndex, bool _isAlive)
        {
            XIndex = _XIndex;
            YIndex = _YIndex;
            IsAlive = _isAlive;
        }
        #endregion 

        public void CountLivingNeighbours()
        {
            NbCellVoisine = 0;

            NbCellVoisine =  (from voisin in this.CellsVoisine
                    where voisin.IsAlive
                    select voisin).Count();

            /*foreach(Cell Voisin in CellVoisine)
            {
                if(Voisin.IsAlive)
                {
                    NbCellVoisine++;
                }
            }*/
        }




    }
}
