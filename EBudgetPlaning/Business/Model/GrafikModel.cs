using System.Collections.Generic;
using System.ComponentModel;

namespace EBudgetPlaning.Business.Model
{
    public class GrafikModel : INotifyPropertyChanged
    {
        private string key;

        private int values;

        public string Key
        {
            get { return key; }
            set { key = value; OnPropertyChanged(nameof(Key)); }
        }

        public int Values
        {
            get { return values; }
            set { values = value; OnPropertyChanged(nameof(Values)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(prop));
        }
    }
    public class BindModel : INotifyPropertyChanged
    {
        private List<GrafikModel> _DataList;
        public List<GrafikModel> DataList
        {
            get { return _DataList; }
            set
            {
                _DataList = value; OnPropertyChanged(nameof(DataList));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(prop));
        }
    }
}
