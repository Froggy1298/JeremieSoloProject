using JeuxVideal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Server.VueModele;
using System.Diagnostics;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace JeuxVideal.ViewModel
{
    internal class LifeGameViewModel : INotifyPropertyChanged
    {
        #region Poutine de VM
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public ObservableCollection<Cell> leTableauCell { get; set; }
        public Serialize Enregistreur { get; set; }
        private int canvasWidth;
        private Random rand = new Random();
        private bool _estEnPause = true;
        private int _size;
       
       



        public LifeGameViewModel(int size)
        {
            BoutonPlay = new CommandeRelais(PlayGame,BoutonPlayCanExecute);
            BoutonPause= new CommandeRelais(PauseGame);
            BoutonReset = new CommandeRelais(ResetGame);
            BoutonAleatoire = new CommandeRelais(AleatoireTableau);
            BoutonCharger = new CommandeRelais(ChargerGame);    
            BoutonEnregistrer = new CommandeRelais(EnregistrerGame);
            BoutonForme1 = new CommandeRelais(Forme1);
            BoutonForme2 = new CommandeRelais(Forme2);
            BoutonForme3 = new CommandeRelais(Forme3);


           leTableauCell = new ObservableCollection<Cell>();
            leTableauCell = new Tableau().ConstructionDuTableau(size);

            Enregistreur = new Serialize(leTableauCell, size);

            CanvasWidth = size * 10;
            CanvasHeight = size * 10;
            _size = size;
        }


       


     
        public async void PlayingGame()
        {
            while(!_estEnPause && (NombreIte != 0 || IteInfinie))
            {
                //await.Task.1Run 
                if(NombreIte !=0 && !IteInfinie)
                    NombreIte--;
                foreach(Cell c in leTableauCell)
                {
                    c.CountLivingNeighbours();

                    c.IsAliveNext = ((c.NbCellVoisine == 3) || ((c.NbCellVoisine == 2) && c.IsAlive));
                }
                foreach(Cell c in leTableauCell)
                {
                    c.IsAlive = c.IsAliveNext;
                }
                //TODO CHANGER LE TEMPS D'ITÉRATION
                await Task.Delay(500);
                if (NombreIte == 0 && !IteInfinie)
                    PauseGame(default);
                
            }
        }


        #region Bouton Aléatoire
        //La commande du bouton Aléatoire
        public ICommand BoutonAleatoire { get; set; }
        private void AleatoireTableau (object param)
        {
            PauseGame(param);
            foreach (Cell cel in leTableauCell)
            {
                if ((rand.Next(10) % 2) == 0)
                    cel.IsAlive = true;
                else
                    cel.IsAlive = false;
            }
            /* Fonction plus petit
             foreach (Cell cel in leTableauCell)
                cel.IsAlive = rand.Next(10) % 2 == 0;
            */
        }
        #endregion

        #region Binding grandeur canva
        public int CanvasWidth
        {
            get { return canvasWidth; }
            set { canvasWidth = value; }
        }

        private int canvasHeight;

        public int CanvasHeight
        {
            get { return canvasHeight; }
            set { canvasHeight = value; }
        }
        #endregion

        #region Bouton Charger
        //La commande du bouton Charger
        public ICommand BoutonCharger { get; set; }
        private void ChargerGame(object param)
        {
            Enregistreur.ChargerFicher();
        }
        #endregion

        #region Bouton Enregistrer
        //La commande du bouton Enregistrer
        public ICommand BoutonEnregistrer { get; set; }
        private void EnregistrerGame(object param)
        {
            Enregistreur.SauvegardeFichier();
        }
        #endregion

        #region Boutton Play
        //La commande du bouton play
        public ICommand BoutonPlay { get; set; }
        private void PlayGame(object param)
        {
            VisibilityPlayButton = "Hidden";
            VisibilityPauseButton = "Visible";
            _estEnPause = false;
            PlayingGame();

        }
        public bool BoutonPlayCanExecute(object sender)
        {
            return (NombreIte != 0 || IteInfinie);
        }
        //Pour gerer la visibilité du bouton play
        private String _visibilityPlayButton = "Visible";
        public String VisibilityPlayButton
        {
            get { return _visibilityPlayButton; }
            set { _visibilityPlayButton = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region Boutton Pause
        //La commande du bouton pause
        public ICommand BoutonPause { get; set; }
        private void PauseGame(object param)
        {
            VisibilityPlayButton = "Visible";
            VisibilityPauseButton = "Hidden";
            _estEnPause = true;
        }
        //Pour Gerer la visibilité du bouton pause
        private String _visibilityPauseButton = "Hidden";
        public String VisibilityPauseButton
        {
            get { return _visibilityPauseButton; }
            set { _visibilityPauseButton = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region Bouton Reset
        //La commande du bouton reset
        public ICommand BoutonReset { get; set; }
        private void ResetGame(object param)
        {
            PauseGame(param);
            foreach(Cell c in leTableauCell)
            {
                c.IsAlive = false;
            }
        }
        #endregion

        #region Bouton Forme1

        public ICommand BoutonForme1 { get; set; }
        private void Forme1(object param)
        {
            ResetGame(param);
            for(int i = 2; i < _size; i = i + 4)
            {
                foreach(Cell c in leTableauCell)
                {
                    if(c.XIndex == i)
                    {
                        c.IsAlive = true;
                    }
                }
            }
        }

        #endregion

        #region Bouton Forme2

        public ICommand BoutonForme2 { get; set; }
        private void Forme2(object param)
        {
            ResetGame(param);

            for (int i = 3; i < _size; i = i + 4)
            {
                foreach (Cell c in leTableauCell)
                {
                    if (c.XIndex == i)
                    {
                        if (c.YIndex % 4 == 0)
                        {
                            c.IsAlive = false;
                        }
                        else
                        {
                            c.IsAlive = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region Bouton Forme3

        public ICommand BoutonForme3 { get; set; }
        private void Forme3(object param)
        {
            ResetGame(param);
           foreach(Cell cell in leTableauCell)
           {
                if (cell.XIndex == 1 && cell.YIndex == 48)
                    cell.IsAlive = true;
                if (cell.XIndex == 2 && cell.YIndex == 48)
                    cell.IsAlive = true;
                if (cell.XIndex == 3 && cell.YIndex == 48)
                    cell.IsAlive = true;
                if (cell.XIndex == 3 && cell.YIndex == 47)
                    cell.IsAlive = true;
                if (cell.XIndex == 2 && cell.YIndex == 46)
                    cell.IsAlive = true;

                if (cell.XIndex == 48 && cell.YIndex == 1)
                    cell.IsAlive = true;
                if (cell.XIndex == 48 && cell.YIndex == 2)
                    cell.IsAlive = true;
                if (cell.XIndex == 48 && cell.YIndex == 3)
                    cell.IsAlive = true;
                if (cell.XIndex == 47 && cell.YIndex == 3)
                    cell.IsAlive = true;
                if (cell.XIndex == 46 && cell.YIndex == 2)
                    cell.IsAlive = true;

            }



        }

        #endregion

        #region Nomre d'itération
        private int _nbIteration;

        public int NombreIte
        {
            get { return _nbIteration; }
            set { _nbIteration = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region Itération Infinie
        private bool _iterationInfinie;

        public bool IteInfinie
        {
            get { return _iterationInfinie; }
            set { _iterationInfinie = value; NotifyPropertyChanged(); }
        }





        #endregion

    }
}
