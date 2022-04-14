using System;
using System.Collections.Generic;
using System.Text;

namespace JeuxVideal.Model
{
    public class Cell
    {
        public int ColIndex { get; private set; }
        public int RowIndex { get; private set; }
        public int LivingNeighbours { get; set; }
        public bool IsAlive { get; private set; }
        public bool IsAliveNext { get; private set; }

        public Cell(int _colIndex, int _rowIndex)
            : this(_colIndex, _rowIndex, false)
        {}


        public Cell(int _colIndex, int _rowIndex, bool _isAlive)
        {
            RowIndex = _colIndex;
            ColIndex = _rowIndex;
            IsAlive = _isAlive;
        }
        public void CountLivingNeighbours()
        {
         
        }




    }
}
