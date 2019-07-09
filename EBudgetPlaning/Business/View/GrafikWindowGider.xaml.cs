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
    /// Interaction logic for GrafikWindowGider.xaml
    /// </summary>
    public partial class GrafikWindowGider : Window,IDisposable
    {
        public GrafikWindowGider()
        {
            InitializeComponent();
            this.DataContext = new GrafikGiderViewModel(GroupBoxDynamicChart); 
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
