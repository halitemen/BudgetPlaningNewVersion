using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EBudgetPlaning.Business.ViewModel;

namespace EBudgetPlaning.Business.View
{
    /// <summary>
    /// Interaction logic for GelirAnalizi.xaml
    /// </summary>
    public partial class GelirAnalizi : Window, IDisposable
    {
        GelirViewModel gelirViewModel;
        public GelirAnalizi()
        {
            gelirViewModel = new GelirViewModel();
            InitializeComponent();
            this.DataContext = gelirViewModel;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
