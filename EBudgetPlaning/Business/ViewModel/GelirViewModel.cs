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
            AllGelirList = gelirDb.AllGetGelir();
            gelirModel = new GelirModel();
            CheckedAllGelir = true;
            Liste = AllGelirList;
            VisibleComboBox = Visibility.Hidden;
            SearchGelirList = gelirDb.getSearchGelirList();
            KategoriList = gelirDb.getKategori();
            CheckBox = false;
            VisibleObjectKategori = Visibility.Visible;
            VisibleObject = Visibility.Collapsed;
            ComboList = gelirDb.getKategoriName();
        }

        #endregion

        #region Members

        private List<GrafikModel> grafikModel;

        /// <summary>
        /// kategori comboboxın görünürlüğü
        /// </summary>
        private Visibility visibleObjectKategori;

        /// <summary>
        /// Kategori textinin görünürlüğü
        /// </summary>
        private Visibility visibleObject;

        /// <summary>
        /// CheckBoxın durumunu tutmak için
        /// </summary>
        private bool checkBox;

        /// <summary>
        /// Kategoriye ve miktar bilgisini tutar
        /// </summary>
        private ObservableCollection<KategoriModel> kategoriList;

        /// <summary>
        /// Comboboxa kategorileri eklemek için
        /// </summary>
        private ObservableCollection<string> comboList;
        
        /// <summary>
        /// Bu ayki gelirleri tutan liste
        /// </summary>
        private ObservableCollection<GelirModel> liste;

        /// <summary>
        /// Nesnelerin görünürlüğünü tutar
        /// </summary>
        private Visibility visibleComboBox;

        /// <summary>
        /// Gelir Modeli
        /// </summary>
        GelirModel gelirModel;

        /// <summary>
        /// Gelir Database erişim sınıfı
        /// </summary>
        GelirDbAccess gelirDb;

        /// <summary>
        /// Bütün gelir listesi
        /// </summary>
        ObservableCollection<GelirModel> allgelirList;

        /// <summary>
        /// Aranan tarih arasındaki bilgileri tutar
        /// </summary>
        private List<string> searchGelirList;

        /// <summary>
        /// Seçilen itemı tutar
        /// </summary>
        private string selectedItem;

        /// <summary>
        /// CheckBoxın checked bilgisi
        /// </summary>
        private bool checkedAllGelir;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CloseGelirWindow;
        public event EventHandler AddGelirEvent;
        public event EventHandler UpdateGelirEvent;

        #endregion

        #region Properties 

        public List<GrafikModel> GrafikModel
        {
            get { return grafikModel; }
            set
            {
                grafikModel = value;
                OnPropertyChanged(nameof(GrafikModel));
            }
        }

        public Visibility VisibleObjectKategori
        {
            get { return visibleObjectKategori; }
            set
            {
                visibleObjectKategori = value;
                OnPropertyChanged(nameof(VisibleObjectKategori));
            }
        }

        public Visibility VisibleObject
        {
            get { return visibleObject; }
            set
            {
                visibleObject = value;
                OnPropertyChanged(nameof(VisibleObject));
            }
        }

        public bool CheckBox
        {
            get { return checkBox; }
            set
            {
                checkBox = value;
                OnPropertyChanged(nameof(CheckBox));
            }
        }

        public ObservableCollection<KategoriModel> KategoriList
        {
            get { return kategoriList; }
            set
            {
                kategoriList = value;
                OnPropertyChanged(nameof(KategoriList));
            }
        }

        public ObservableCollection<string> ComboList
        {
            get { return comboList; }
            set
            {
                comboList = value;
                OnPropertyChanged(nameof(comboList));
            }
        }

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
        public string SelectedItem { get; set; }
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
        private ICommand checkboxCommand;

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

        public ICommand CheckboxCommand
        {
            get
            {
                if (checkboxCommand == null)
                    checkboxCommand = new RelayCommand(forKategoriVisible);
                return checkboxCommand;
            }
        }

        #endregion

        #region Metods

        private void forKategoriVisible()
        {
            if (!CheckBox)
            {
                VisibleObjectKategori = Visibility.Visible;
                VisibleObject = Visibility.Hidden;
            }
            else
            {
                VisibleObjectKategori = Visibility.Hidden;
                VisibleObject = Visibility.Visible;
            }
        }

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
