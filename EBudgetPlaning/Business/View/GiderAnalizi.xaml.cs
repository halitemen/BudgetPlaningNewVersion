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
    /// Interaction logic for GiderAnalizi.xaml
    /// </summary>
    public partial class GiderAnalizi : Window,IDisposable
    {
        GiderViewModel giderViewModel;
        public GiderAnalizi()
        {
            giderViewModel = new GiderViewModel();
            InitializeComponent();
            this.DataContext = giderViewModel;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
