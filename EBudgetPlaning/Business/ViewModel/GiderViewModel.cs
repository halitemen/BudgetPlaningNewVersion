using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EBudgetPlaning.Business.Model;

namespace EBudgetPlaning.Business.ViewModel
{
    /// <summary>
    /// Gelir ViewModel'i
    /// </summary>
    public class GiderViewModel : INotifyPropertyChanged
    {
        #region Constructor

        public GiderViewModel()
        {
            giderDb = new GiderDbAccess();
            AllGiderList = giderDb.allGider();
            giderModel = new GiderModel();
        }

        #endregion

        #region Members

        //Gider Modeli
        GiderModel giderModel;

        //Gider Database erişim sınıfı
        GiderDbAccess giderDb;

        //Bütün Gider listesi
        ObservableCollection<GiderModel> allGiderList;

        //Seçilen tarih
        private DateTime selectDate;

        #endregion

        #region Properties

        public string buttonName { get; set; }
        public ObservableCollection<GiderModel> AllGiderList
        {
            get { return allGiderList; }
            set
            {
                allGiderList = value;
                OnPropertyChanged(nameof(AllGiderList));
            }
        }
        public int Id
        {
            get { return giderModel.Id; }
            set
            {
                giderModel.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string GiderAdi
        {
            get { return giderModel.GiderAdi; }
            set
            {
                giderModel.GiderAdi = value;
                OnPropertyChanged(nameof(GiderAdi));
            }
        }
        public string GiderMiktari
        {
            get { return giderModel.GiderMiktari; }
            set
            {
                giderModel.GiderMiktari = value;
                OnPropertyChanged(nameof(GiderMiktari));
            }
        }
        public string GiderTarihi
        {
            get { return giderModel.GiderTarihi; }
            set
            {
                giderModel.GiderTarihi = value;
                OnPropertyChanged(nameof(GiderTarihi));
            }
        }

        #endregion

        #region Events

        public event EventHandler CloseGiderWindow;
        public event EventHandler AddGiderEvent;
        public event EventHandler UpdateGiderEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ICommand

        private ICommand giderCommand;

        #endregion

        #region Command

        public ICommand GiderCommand
        {
            get
            {
                if (giderCommand == null)
                    giderCommand = new RelayCommand(Gider);
                return giderCommand;
            }
        }

        #endregion

        #region Metods

        /// <summary>
        /// buttonun adına göre günceller ya da yeni kayıt ekler
        /// </summary>
        private void Gider()
        {
            if (buttonName == "Güncelle")
            {
                giderDb.UpdateGider(giderModel);
                UpdateGiderEvent?.Invoke(giderModel, new EventArgs());
                CloseGiderWindow?.Invoke(this, new EventArgs());
            }
            else
            {
                giderDb.addGider(giderModel);
                AddGiderEvent?.Invoke(giderModel, new EventArgs());
                CloseGiderWindow?.Invoke(this, new EventArgs());
            }

        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(propName)));
        }


        #endregion
    }
}