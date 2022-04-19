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

        ObservableCollection<Cell> Cells = new ObservableCollection<Cell>();
        
        public LifeGameViewModel()
        {
            BoutonPlay = new CommandeRelais(PlayGame);
            BoutonPause= new CommandeRelais(PauseGame);



        }




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

        }
        #endregion




    }
}
