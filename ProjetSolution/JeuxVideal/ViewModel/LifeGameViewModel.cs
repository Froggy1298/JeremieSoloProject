using JeuxVideal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;

namespace JeuxVideal.ViewModel
{
    internal class LifeGameViewModel
    {
        ObservableCollection<Cell> Cells = new ObservableCollection<Cell>();
        
        public LifeGameViewModel()
        {

        }
    }
}
