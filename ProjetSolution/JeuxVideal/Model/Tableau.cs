using System;
using System.Collections.Generic;
using System.Text;

namespace JeuxVideal.Model
{
    class Tableau
    {
        private int _dimension;
        private Random rand = new Random();

        public int Dimension
        {
            get { return _dimension; }
            set 
            {
                if (value < 10)
                    _dimension = value;
                if(value >= 10)
                    _dimension = value; 
            }
        }
        Cell[,] TableauCells { get; set; }
        public List<Cell> ConstructionDuTableau(int dimension)
        {
            Cell cellTemp;
            TableauCells = new Cell[dimension, dimension];
            List<Cell> ListCells = new List<Cell>();
            for(int i = 0 ; i < dimension; i++)
            {
                for(int j = 0; j < dimension; j++)
                {
                    
                    if ((rand.Next(10) % 2) == 0)
                    { 
                        cellTemp = new Cell(i, j, true);
                    }
                    else
                    {
                        cellTemp = new Cell(i, j, true);
                    }
                    TableauCells[i, j] = cellTemp;
                    ListCells.Add(cellTemp);
                }
            }
            return ListCells;
        }

       


    }
}
