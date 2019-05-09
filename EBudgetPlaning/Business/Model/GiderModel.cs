namespace EBudgetPlaning.Business.Model
{
    /// <summary>
    /// Gider Model
    /// </summary>
    public class GiderModel
    {
        private int _id;
        private string _giderAdi;
        private string _giderMiktari;
        private string _giderTarihi;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string GiderAdi
        {
            get { return _giderAdi; }
            set { _giderAdi = value; }
        }

        public string GiderMiktari
        {
            get { return _giderMiktari; }
            set { _giderMiktari = value; }
        }

        public string GiderTarihi
        {
            get { return _giderTarihi; }
            set { _giderTarihi = value; }
        }

    }
}
