using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
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
            GelirList = gelirDb.getGelir();
            AllGelirList = gelirDb.AllGetGelir();
            gelirModel = new GelirModel();
            CheckedAllGelir = true;
            Liste = AllGelirList;
            VisibleComboBox = Visibility.Hidden;
            SearchGelirList = gelirDb.getSearchGelirList();
        }

        #endregion

        #region Members

        private ObservableCollection<GelirModel> liste;

        private Visibility visibleComboBox;

        //Gelir Modeli
        GelirModel gelirModel;

        //Gelir Database erişim sınıfı
        GelirDbAccess gelirDb;

        //Bütün gelir listesi
        ObservableCollection<GelirModel> allgelirList;

        //Aya göre gelir listesi
        ObservableCollection<GelirModel> gelirList;

        private List<string> searchGelirList;

        private string selectedItem;

        private bool checkedAllGelir;


        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CloseGelirWindow;
        public event EventHandler AddGelirEvent;
        public event EventHandler UpdateGelirEvent;

        #endregion

        #region Properties 

        public Visibility VisibleComboBox
        {
            get { return visibleComboBox; }
            set
            {
                visibleComboBox = value;
                OnPropertyChanged(nameof(VisibleComboBox));
            }
        }

        public bool CheckedAllGelir
        {
            get { return checkedAllGelir; }
            set
            {
                checkedAllGelir = value;
            }
        }
        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(GelirList));
            }
        }

        public List<string> SearchGelirList
        {
            get { return searchGelirList; }
            set
            {
                searchGelirList = value;
                OnPropertyChanged(nameof(searchGelirList));
            }
        }
        public string buttonName { get; set; }

        public ObservableCollection<GelirModel> Liste
        {
            get { return liste; }
            set
            {
                liste = value;
                OnPropertyChanged(nameof(Liste));
            }
        }
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
        private ICommand getListCommand;

        #endregion

        #region Command

        public ICommand GetListCommand
        {
            get
            {
                if (getListCommand == null)
                    getListCommand = new RelayCommand(getList);
                return getListCommand;
            }
        }

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

        private void getList()
        {
            if (CheckedAllGelir == true)
            {
                Liste = AllGelirList;
                VisibleComboBox = Visibility.Hidden;
            }
            else
            {
                VisibleComboBox = Visibility.Visible;
                Liste = null;
                Liste = new ObservableCollection<GelirModel>();
                string value;
                if (SelectedItem != null)
                {
                    for (int i = 0; i < AllGelirList.Count; i++)
                    {
                        if (AllGelirList[i].GelirTarihi.Length == 10)
                        {
                            value = AllGelirList[i].GelirTarihi.Substring(3, 7).ToString();
                        }
                        else
                        {
                            value = AllGelirList[i].GelirTarihi.Substring(2, 7).ToString();
                        }
                        if (value == SelectedItem)
                        {
                            Liste.Add(new GelirModel
                            {
                                Id = AllGelirList[i].Id,
                                GelirAdi = AllGelirList[i].GelirAdi,
                                GelirMiktari = AllGelirList[i].GelirMiktari,
                                GelirTarihi = AllGelirList[i].GelirTarihi
                            });
                        }
                    }
                }
            }
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
