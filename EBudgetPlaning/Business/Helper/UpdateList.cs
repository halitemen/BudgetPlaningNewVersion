using System;
using System.Collections.ObjectModel;
using EBudgetPlaning.Business.Model;

namespace EBudgetPlaning.Business.Helper
{
    /// <summary>
    /// Listeyi Güncellemek için yardımcı class
    /// </summary>
    public static class UpdateGelirList
    {
        #region Metods

        /// <summary>
        /// Gelir Listesini günceleyebilmek için
        /// </summary>
        /// <param name="gelirler"></param>
        /// <param name="gelir"></param>
        /// <param name="id"></param>
        /// <param name="gelirModel"></param>
        public static void GelirListUpdate(this ObservableCollection<GelirModel> gelirler, 
            ref ObservableCollection<GelirModel> gelir, int id, GelirModel gelirModel)
        {
           
            for (int i = 0; i < gelirler.Count; i++)
            {
                if (gelirler[i].Id == id)
                {
                    gelirler[i] = gelirModel;
                    return;
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Gider Listelisini güncellemek için yardımcı class
    /// </summary>
    public static class UpdateGiderList
    {
        #region

        /// <summary>
        /// Gider listesini güncellemek için 
        /// </summary>
        /// <param name="giderler"></param>
        /// <param name="gider"></param>
        /// <param name="id"></param>
        /// <param name="giderModel"></param>
        public static void GiderListUpdate(this ObservableCollection<GiderModel> giderler,
            ref ObservableCollection<GiderModel> gider, int id, GiderModel giderModel)
        {

            for (int i = 0; i < giderler.Count; i++)
            {
                if (giderler[i].Id == id)
                {
                    giderler[i] = giderModel;
                    return;
                }
            }
        }

        public static void KategoriListUpdate(this ObservableCollection<KategoriModel> kategoriler,
            ref ObservableCollection<KategoriModel> kategori, string name, int deger)
        {

            for (int i = 0; i < kategoriler.Count; i++)
            {
                if (kategoriler[i].KategoriAdi == name)
                {
                    int val = Convert.ToInt32(kategoriler[i].Degeri);
                    val += deger;
                    kategoriler[i].Degeri = val.ToString();
                    return;
                }
            }
        }


        #endregion
    }
}
