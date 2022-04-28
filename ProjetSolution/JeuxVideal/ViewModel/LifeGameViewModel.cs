﻿using JeuxVideal.Model;
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

        public ObservableCollection<Cell> Cells { get; set; }
        public Serialize Enregistreur { get; set; }
        private int canvasWidth;
        private Random rand = new Random();
        private bool _estEnPause = true;

       
       



        public LifeGameViewModel(int size)
        {
            BoutonPlay = new CommandeRelais(PlayGame);
            BoutonPause= new CommandeRelais(PauseGame);
            BoutonReset = new CommandeRelais(ResetGame);
            BoutonAleatoire = new CommandeRelais(AleatoireTableau);


            Cells = new ObservableCollection<Cell>();
            Cells = new Tableau().ConstructionDuTableau(size);

            Enregistreur = new Serialize(Cells, size);

            CanvasWidth = size * 10;
            CanvasHeight = size * 10;
        }


       


     
        public async void PlayingGame()
        {
            while(!_estEnPause)
            {
                //await.Task.Run 
                foreach(Cell c in Cells)
                {
                    c.CountLivingNeighbours();

                    c.IsAliveNext = ((c.NbCellVoisine == 3) || ((c.NbCellVoisine == 2) && c.IsAlive));
                }
                foreach(Cell c in Cells)
                {
                    c.IsAlive = c.IsAliveNext;
                }
                await Task.Delay(50);
                
            }
        }


        #region Bouton Aléatoire
        //La commande du bouton Aléatoire
        public ICommand BoutonAleatoire { get; set; }
        private void AleatoireTableau (object param)
        {
            PauseGame(param);
            foreach (Cell cel in Cells)
            {
                if ((rand.Next(10) % 2) == 0)
                    cel.IsAlive = true;
                else
                    cel.IsAlive = false;
            }
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
            
        }
        #endregion

        #region Bouton Enregistrer
        //La commande du bouton Enregistrer
        public ICommand BoutonEnregistrer { get; set; }
        private void EnregistrerGame(object param)
        {

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
            foreach(Cell c in Cells)
            {
                c.IsAlive = false;
            }
        }
        #endregion

        


    }
}
