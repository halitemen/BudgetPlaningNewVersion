using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EBudgetPlaning.Business.Model;

namespace EBudgetPlaning.Business.ViewModel
{
    /// <summary>
    /// Gelir ViewModeli
    /// </summary>
    public class GelirViewModel : INotifyPropertyChanged
    {
        #region Constructor 

        public GelirViewModel()
        {
            gelirDb = new GelirDbAccess();
            AllGelirList = gelirDb.AllGetGelir();
            GelirList = gelirDb.getGelir();
            gelirModel = new GelirModel();
        }

        #endregion

        #region Members

        //Gelir Modeli
        GelirModel gelirModel;

        //Gelir Database erişim sınıfı
        GelirDbAccess gelirDb;

        //Bütün gelir listesi
        ObservableCollection<GelirModel> allgelirList;

        ObservableCollection<GelirModel> gelirList;



        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CloseGelirWindow;
        public event EventHandler AddGelirEvent;
        public event EventHandler UpdateGelirEvent;

        #endregion

        #region Properties 

        public string buttonName { get; set; }  
        
        public ObservableCollection<GelirModel> GelirList
        {
            get { return gelirList; }
            set
            {
                gelirList = value;
                OnPropertyChanged(nameof(GelirList));
            }
        }
        public ObservableCollection<GelirModel> AllGelirList
        {
            get { return allgelirList; }
            set
            {
                allgelirList = value;
                OnPropertyChanged(nameof(AllGelirList));
            }
        }
        public int Id
        {
            get { return gelirModel.Id; }
            set
            {
                gelirModel.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string GelirAdi
        {
            get { return gelirModel.GelirAdi; }
            set
            {
                gelirModel.GelirAdi = value;
                OnPropertyChanged(nameof(GelirAdi));
            }
        }
        public string GelirMiktari
        {
            get { return gelirModel.GelirMiktari; }
            set
            {
                gelirModel.GelirMiktari = value;
                OnPropertyChanged(nameof(GelirMiktari));
            }
        }
        public string GelirTarihi
        {
            get { return gelirModel.GelirTarihi; }
            set
            {
                gelirModel.GelirTarihi = value;
                OnPropertyChanged(nameof(GelirTarihi));
            }
        }


        #endregion

        #region ICommand

        private ICommand gelirCommand;

        #endregion

        #region Command

        public ICommand GelirCommand
        {
            get
            {
                if (gelirCommand == null)
                    gelirCommand = new RelayCommand(Gelir);
                return gelirCommand;
            }
        }

        #endregion

        #region Metods

        private void Gelir()
        {
            if (buttonName == "Güncelle")
            {
                gelirDb.UpdateGelir(gelirModel);
                UpdateGelirEvent?.Invoke(gelirModel, new EventArgs());
                CloseGelirWindow?.Invoke(this, new EventArgs());
            }
            else
            {
                gelirDb.AddGelir(gelirModel);
                AddGelirEvent?.Invoke(gelirModel, new EventArgs());
                CloseGelirWindow?.Invoke(this, new EventArgs());
            }
           
        }
        
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
