using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuxVideal.Model
{
    public class Cell
    {
        #region Propriété et Valeur 
        public int XIndex { get; private set; }
        public int YIndex { get; private set; }
     

       
        private bool _isAlive;

        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        public bool IsAliveNext { get; private set; }
            
        
        List<Cell> CellVoisine { get; set; }

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

            NbCellVoisine =  (from voisin in this.CellVoisine
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
