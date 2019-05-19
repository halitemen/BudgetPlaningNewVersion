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
            CheckedAllGider = true;
            Liste = AllGiderList;
            VisibleObject = Visibility.Hidden;
            MyComboList = giderDb.getSearchGiderList();
        }

        #endregion

        #region Members

        private ObservableCollection<GiderModel> liste;

        private Visibility visibleObject;

        List<string> myComboList;

        //Gider Modeli
        GiderModel giderModel;

        //Gider Database erişim sınıfı
        GiderDbAccess giderDb;

        //Bütün Gider listesi
        ObservableCollection<GiderModel> allGiderList;

        private bool checkedAllGider;

        #endregion

        #region Properties

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

        public ObservableCollection<GiderModel> Liste
        {
            get { return liste; }
            set
            {
                liste = value;
                OnPropertyChanged(nameof(Liste));
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

        #endregion

        #region Events

        public event EventHandler CloseGiderWindow;
        public event EventHandler AddGiderEvent;
        public event EventHandler UpdateGiderEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ICommand

        private ICommand giderCommand;
        private ICommand getListeCommand;

        #endregion

        #region Command

        public ICommand GetListeCommand
        {
            get
            {
                if (getListeCommand == null)
                    getListeCommand = new RelayCommand(getList);
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

        private void getList()
        {
            if (CheckedAllGider == true)
            {                
                Liste = AllGiderList;
                VisibleObject = Visibility.Hidden;
            }
            else
            {
                VisibleObject = Visibility.Visible;
                Liste = null;
                Liste = new ObservableCollection<GiderModel>();
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
                            Liste.Add(new GiderModel
                            {
                                Id = AllGiderList[i].Id,
                                GiderAdi = AllGiderList[i].GiderAdi,
                                GiderMiktari = AllGiderList[i].GiderMiktari,
                                GiderTarihi = AllGiderList[i].GiderTarihi
                            });
                        }
                    }
                }
            }
        }


        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(propName)));
        }


        #endregion
    }
}