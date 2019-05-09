using System;
using System.Windows;
using EBudgetPlaning.Business.ViewModel;

namespace EBudgetPlaning.Business.View
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window, IDisposable
    {
        GiderViewModel giderViewModel;
        public Window2()
        {
            giderViewModel = new GiderViewModel();
            InitializeComponent();
            DataContext = giderViewModel;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
