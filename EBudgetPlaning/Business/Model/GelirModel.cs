namespace EBudgetPlaning.Business.Model
{
    /// <summary>
    /// Gelir Model
    /// </summary>
    public class GelirModel
    {
        private int _id;
        private string _gelirAdi;
        private string _gelirMiktari;
        private string _gelirTarihi;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string GelirAdi
        {
            get { return _gelirAdi; }
            set { _gelirAdi = value; }
        }

        public string GelirMiktari
        {
            get { return _gelirMiktari; }
            set { _gelirMiktari = value; }
        }

        public string GelirTarihi
        {
            get { return _gelirTarihi; }
            set { _gelirTarihi = value; }
        }

    }
}
