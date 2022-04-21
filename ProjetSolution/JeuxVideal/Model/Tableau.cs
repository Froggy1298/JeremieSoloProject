using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

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
                    _dimension = 10;
                if(value >= 10)
                    _dimension = value; 
            }
        }
        Cell[,] TableauCells { get; set; }


        public List<Cell> ConstructionDuTableau(int size)
        {
            Dimension = size;
            Cell cellTemp;
            TableauCells = new Cell[Dimension, Dimension];
            List<Cell> ListCells = new List<Cell>();
            for(int i = 0 ; i < Dimension; i++)
            {
                for(int j = 0; j < Dimension; j++)
                {
                    /*if ((i + j) % 2 == 0)
                    {
                        cellTemp = new Cell(i, j, true);
                    }
                    else
                    {
                        cellTemp = new Cell(i, j, false);

                    }*/
                    if ((rand.Next(10) % 2) == 0)
                    { 
                        cellTemp = new Cell(i, j, true);
                    }
                    else
                    {
                        cellTemp = new Cell(i, j, false);
                    }
                    TableauCells[i, j] = cellTemp;
                    ListCells.Add(cellTemp);
                }
            }

            foreach(Cell ThisCell in ListCells)
            {
                ThisCell.CellsVoisine = new List<Cell>();
                for(int i = -1 ; i <= 1 ; i++)
                {
                    for (int j = -1 ; j <= 1 ; j++)
                    {
                        if (i == 0 && j == 0)
                            continue;
                        ThisCell.CellsVoisine.Add(TableauCells[(((ThisCell.YIndex+ Dimension) + j) % Dimension), ((ThisCell.XIndex + Dimension) + i) % Dimension]);
                    }
                }

            }

            return ListCells;
        }

       


    }
}
