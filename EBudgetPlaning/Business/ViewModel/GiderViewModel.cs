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
            VisibleComboBox = Visibility.Hidden;
            CheckedAllGider = true;
            ListeGider = AllGiderList;
            MyComboList = giderDb.getSearchGiderList();
            KategoriList = giderDb.getKategori();
            CheckBox = false;
            VisibleObject = Visibility.Collapsed;
            VisibleObjectKategori = Visibility.Visible;
            ComboList = giderDb.getKategoriName();
        }

        #endregion

        #region Members

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
        /// Kategori Listesini gruplayıp tutar
        /// </summary>
        private ObservableCollection<KategoriModel> kategoriList;

        /// <summary>
        /// kategori combobox
        /// </summary>
        private ObservableCollection<string> comboList;

        /// <summary>
        /// Form üzerindeki nesnelerin görünürlüğü
        /// </summary>
        private Visibility visibleComboBox;

        /// <summary>
        /// Bütün giderlerin Listesi
        /// </summary>
        private ObservableCollection<GiderModel> liste;

        /// <summary>
        /// Ay ve yıl listesi
        /// </summary>
        List<string> myComboList;

        /// <summary>
        /// Gider Modeli
        /// </summary>
        GiderModel giderModel;

        /// <summary>
        /// Gider Database sınıfı erisimi
        /// </summary>
        GiderDbAccess giderDb;

        /// <summary>
        /// Bütün Gider listesi databaseden alır
        /// </summary>
        ObservableCollection<GiderModel> allGiderList;

        /// <summary>
        /// CheckBoxın checked durumu
        /// </summary>
        private bool checkedAllGider;

        #endregion

        #region Properties

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
        public ObservableCollection<string> ComboList
        {
            get { return comboList; }
            set
            {
                comboList = value;
                OnPropertyChanged(nameof(ComboList));
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

        public bool CheckedAllGider
        {
            get { return checkedAllGider; }
            set
            {
                checkedAllGider = value;
                OnPropertyChanged(nameof(CheckedAllGider));
            }
        }

        public string SelectedItem { get; set; }

        public ObservableCollection<GiderModel> ListeGider
        {
            get { return liste; }
            set
            {
                liste = value;
                OnPropertyChanged(nameof(ListeGider));
            }
        }



        public List<string> MyComboList
        {
            get { return myComboList; }
            set
            {
                myComboList = value;
                OnPropertyChanged(nameof(MyComboList));
            }
        }

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

        public ObservableCollection<KategoriModel> KategoriList
        {
            get { return kategoriList; }
            set { kategoriList = value; }
        }



        #endregion

        #region Events

        public event EventHandler CloseGiderWindow;
        public event EventHandler AddGiderEvent;
        public event EventHandler UpdateGiderEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ICommand

        private ICommand checkboxCommand;
        private ICommand giderCommand;
        private ICommand getListeCommand;

        #endregion

        #region Command

        public ICommand CheckboxCommand
        {
            get
            {
                if (checkboxCommand == null)
                    checkboxCommand = new RelayCommand(forKategoriVisible);
                return checkboxCommand;
            }
        }

        public ICommand GetListeCommand
        {
            get
            {
                if (getListeCommand == null)
                    getListeCommand = new RelayCommand(getGiderList);
                return getListeCommand;
            }
        }
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

        private void getGiderList()
        {
            if (CheckedAllGider == true)
            {
                ListeGider = AllGiderList;
                VisibleComboBox = Visibility.Hidden;
            }
            else
            {
                VisibleComboBox = Visibility.Visible;
                ListeGider = null;
                ListeGider = new ObservableCollection<GiderModel>();
                string value;
                if (SelectedItem != null)
                {
                    for (int i = 0; i < AllGiderList.Count; i++)
                    {
                        if (AllGiderList[i].GiderTarihi.Length == 10)
                        {
                            value = AllGiderList[i].GiderTarihi.Substring(3, 7).ToString();
                        }
                        else
                        {
                            value = AllGiderList[i].GiderTarihi.Substring(2, 7).ToString();
                        }
                        if (value == SelectedItem)
                        {
                            giderModel = new GiderModel
                            {
                                Id = AllGiderList[i].Id,
                                GiderAdi = AllGiderList[i].GiderAdi,
                                GiderMiktari = AllGiderList[i].GiderMiktari,
                                GiderTarihi = AllGiderList[i].GiderTarihi
                            };
                            ListeGider.Add(giderModel);
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