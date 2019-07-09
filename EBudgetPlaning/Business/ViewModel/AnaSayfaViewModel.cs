using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EBudgetPlaning.Business.Helper;
using EBudgetPlaning.Business.Model;
using EBudgetPlaning.Business.View;
using OfficeOpenXml;
using System.Windows.Forms;
using System.IO;

namespace EBudgetPlaning.Business.ViewModel
{
    /// <summary>
    /// Ana Sayfa Backend Classı
    /// </summary>
    public class AnaSayfaViewModel : INotifyPropertyChanged
    {
        #region Constructor

        public AnaSayfaViewModel()
        {
            gelirDB = new GelirDbAccess();
            giderDB = new GiderDbAccess();
            GelirList = gelirDB.getGelir();
            GiderList = giderDB.getGider();
            ToplamGelir = gelirToplam();
            ToplamGider = giderToplam();
        }

        #endregion

        #region Members

        private AnaSayfa anaSayfa;

        /// <summary>
        /// Toplam gideri tutar
        /// </summary>
        private int toplamGider;

        /// <summary>
        /// Toplam geliri tutar
        /// </summary>
        private int toplamGelir;

        /// <summary>
        /// Gelir database sınıfı
        /// </summary>
        GelirDbAccess gelirDB;

        /// <summary>
        /// Gider database sınıfı
        /// </summary>
        GiderDbAccess giderDB;

        /// <summary>
        /// Gelir List
        /// </summary>
        private ObservableCollection<GelirModel> gelirList;

        /// <summary>
        /// Gider List
        /// </summary>
        private ObservableCollection<GiderModel> giderList;

        #endregion

        #region Properties

        public int ToplamGider
        {
            get { return toplamGider; }
            set
            {
                toplamGider = value;
                OnNotifyPropertyChanged(nameof(ToplamGider));
            }
        }

        public int ToplamGelir
        {
            get { return toplamGelir; }
            set
            {
                toplamGelir = value;
                OnNotifyPropertyChanged(nameof(ToplamGelir));
            }
        }

        public ObservableCollection<GelirModel> GelirList
        {
            get { return gelirList; }
            set
            {
                gelirList = value;
                OnNotifyPropertyChanged(nameof(GelirList));
            }
        }

        public ObservableCollection<GiderModel> GiderList
        {
            get { return giderList; }
            set
            {
                giderList = value;
                OnNotifyPropertyChanged(nameof(GiderList));
            }
        }

        public static GelirModel selectedGelirModel { get; set; }

        public static GiderModel selectedGiderModel { get; set; }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ICommand

        private ICommand mainPageCommand;

        #endregion

        #region Command

        public ICommand MainPageCommand
        {
            get
            {
                if (mainPageCommand == null)
                    mainPageCommand = new RelayCommand<string>(mainPage);
                return mainPageCommand;
            }
        }


        #endregion

        #region Metods

        /// <summary>
        /// Ana sayfa üzerinde ki buttonları yönlendiren metot
        /// </summary>
        /// <param name="val">Buttonun valuesi</param>
        private void mainPage(string val)
        {
            switch (val)
            {
                case "GELIREKLE":
                    using (Window1 windowGelir = new Window1())
                    {
                        windowGelir.gelirViewModel.buttonName = "GELİR EKLE";
                        windowGelir.gelirViewModel.AddGelirEvent += GelirViewModel_closeGelirWindow;
                        windowGelir.ShowDialog();
                    }
                    break;

                case "GELIRGUNCELLE":
                    if (selectedGelirModel != null)
                    {
                        using (Window1 windowGelir = new Window1())
                        {
                            windowGelir.gelirViewModel.buttonName = "Güncelle";
                            windowGelir.gelirViewModel.Id = selectedGelirModel.Id;
                            windowGelir.gelirViewModel.GelirAdi = selectedGelirModel.GelirAdi;
                            windowGelir.gelirViewModel.GelirMiktari = selectedGelirModel.GelirMiktari;
                            windowGelir.gelirViewModel.GelirTarihi = selectedGelirModel.GelirTarihi;
                            windowGelir.gelirViewModel.UpdateGelirEvent += GelirViewModel_UpdateGelirEvent;
                            windowGelir.ShowDialog();
                        }
                    }
                    else
                        MessageBox.Show("Lütfen Listeden Kayıt Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;

                case "GELIRSIL":
                    if (selectedGelirModel != null)
                    {
                        gelirDB.DeleteGelir(selectedGelirModel);
                        GelirList.Remove(selectedGelirModel);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Lütfen Silinecek Kaydı Seçiniz.");
                    }
                    break;
                case "GELIRLERIGOR":
                    using (AllincomeWindow allIncome = new AllincomeWindow())
                    {
                        allIncome.ShowDialog();
                    }

                    break;
                case "EXCELEAKTAR":
                    if (GelirList.Count != 0)
                    {
                        System.Windows.Forms.SaveFileDialog saveFile = new System.Windows.Forms.SaveFileDialog()
                        {
                            Filter = "Excel Dosyası |.xlsx",
                            ValidateNames = true,
                            FileName = string.Format("Gelir Bilgileri Excel Raporu")

                        };
                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {
                            ExcelPackage excelPackage = new ExcelPackage();
                            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Kitap1");
                            excelWorksheet.Cells[1, 1].Value = "Gelir No";
                            excelWorksheet.Cells[1, 2].Value = "Gelir Adı";
                            excelWorksheet.Cells[1, 3].Value = "Gelir Miktarı";
                            excelWorksheet.Cells[1, 4].Value = "Gelir Tarihi";

                            for (int i = 0; i < GelirList.Count; i++)
                            {
                                excelWorksheet.Cells[i + 2, 1].Value = GelirList[i].Id;
                                excelWorksheet.Cells[i + 2, 2].Value = GelirList[i].GelirAdi;
                                excelWorksheet.Cells[i + 2, 3].Value = GelirList[i].GelirMiktari;
                                excelWorksheet.Cells[i + 2, 4].Value = GelirList[i].GelirTarihi;
                            }
                            var file = new FileInfo(saveFile.FileName);
                            excelPackage.SaveAs(file);
                            MessageBox.Show("Başarıyla Dışarı Aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("Bir sorun oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;

                case "GELİRGRAFIK":
                    using(GrafikWindowGelir grafikWindowGelir = new GrafikWindowGelir())
                    {
                        grafikWindowGelir.ShowDialog();
                    }
                    break;
                case "GIDEREKLE":
                    using (GiderWindow window = new GiderWindow())
                    {
                        window.giderViewModel.buttonName = "GİDER EKLE";
                        window.giderViewModel.AddGiderEvent += GiderViewModel_closeGiderWindow;
                        window.ShowDialog();
                    }
                    break;
                case "GIDERSIL":

                    if (selectedGiderModel != null)
                    {
                        giderDB.DeleteGider(selectedGiderModel);
                        GiderList.Remove(selectedGiderModel);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Lütfen Silinecek Kaydı Seçiniz.");
                    }

                    break;
                case "GUNCELLE":
                    if (selectedGiderModel != null)
                    {
                        using (GiderWindow windowGider = new GiderWindow())
                        {
                            windowGider.giderViewModel.buttonName = "Güncelle";
                            windowGider.giderViewModel.Id = selectedGiderModel.Id;
                            windowGider.giderViewModel.GiderAdi = selectedGiderModel.GiderAdi;
                            windowGider.giderViewModel.GiderMiktari = selectedGiderModel.GiderMiktari;
                            windowGider.giderViewModel.GiderTarihi = selectedGiderModel.GiderTarihi;
                            windowGider.giderViewModel.UpdateGiderEvent += GiderViewModel_UpdateGiderEvent;
                            windowGider.ShowDialog();
                        }
                    }
                    else
                        MessageBox.Show("Lütfen Listeden Kayıt Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                case "GIDERLERIGOR":
                    using (Window2 gider = new Window2())
                    {
                        gider.ShowDialog();
                    }
                    break;
                case "EXCELAL":

                    if (GiderList.Count != 0)
                    {
                        System.Windows.Forms.SaveFileDialog saveFile = new System.Windows.Forms.SaveFileDialog()
                        {
                            Filter = "Excel Dosyası |.xlsx",
                            ValidateNames = true,
                            FileName = string.Format("Gider Bilgileri Excel Raporu")

                        };
                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {
                            ExcelPackage excelPackage = new ExcelPackage();
                            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Kitap1");
                            excelWorksheet.Cells[1, 1].Value = "Gider No";
                            excelWorksheet.Cells[1, 2].Value = "Gider Adı";
                            excelWorksheet.Cells[1, 3].Value = "Gider Miktarı";
                            excelWorksheet.Cells[1, 4].Value = "Gider Tarihi";

                            for (int i = 0; i < GiderList.Count; i++)
                            {
                                excelWorksheet.Cells[i + 2, 1].Value = GiderList[i].Id;
                                excelWorksheet.Cells[i + 2, 2].Value = GiderList[i].GiderAdi;
                                excelWorksheet.Cells[i + 2, 3].Value = GiderList[i].GiderMiktari;
                                excelWorksheet.Cells[i + 2, 4].Value = GiderList[i].GiderTarihi;
                            }
                            var file = new FileInfo(saveFile.FileName);
                            excelPackage.SaveAs(file);
                            MessageBox.Show("Başarıyla Dışarı Aktarıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("Bir sorun oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                case "GELIRANALİZİ":
                    using (GelirAnalizi analiz = new GelirAnalizi())
                    {
                        analiz.ShowDialog();
                    }
                    break;
                case "GIDERANALIZI":
                    using (GiderAnalizi analiz = new GiderAnalizi())
                    {
                        analiz.ShowDialog();
                    }
                    break;
                case "GIDERGRAFIGI":
                    using (GrafikWindowGider grafikGiderWindow = new GrafikWindowGider())
                    {
                        grafikGiderWindow.ShowDialog();
                    }
                    break;
            }
        }

        private int gelirToplam()
        {
            int gelir = 0;
            for (int i = 0; i < GelirList.Count; i++)
            {
                gelir += Convert.ToInt32(GelirList[i].GelirMiktari);
            }
            return gelir;
        }

        private int giderToplam()
        {
            int gider = 0;
            for (int i = 0; i < GiderList.Count; i++)
            {
                gider += Convert.ToInt32(GiderList[i].GiderMiktari);
            }
            return gider;
        }

        private void GiderViewModel_UpdateGiderEvent(object sender, EventArgs e)
        {
            var gider = (GiderModel)sender;
            giderList.GiderListUpdate(ref giderList, selectedGiderModel.Id, gider);
        }

        private void GiderViewModel_closeGiderWindow(object sender, EventArgs e)
        {
            var gider = (GiderModel)sender;
            GiderList.Add(gider);
        }

        private void GelirViewModel_closeGelirWindow(object sender, EventArgs e)
        {
            var gelir = (GelirModel)sender;
            GelirList.Add(gelir);
        }

        private void GelirViewModel_UpdateGelirEvent(object sender, EventArgs e)
        {
            var gelir = (GelirModel)sender;
            gelirList.GelirListUpdate(ref gelirList, selectedGelirModel.Id, gelir);
        }

        private void OnNotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
