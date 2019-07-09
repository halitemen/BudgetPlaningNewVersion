using System;
using System.Windows;
using EBudgetPlaning.Business.ViewModel;

namespace EBudgetPlaning.Business.View
{
    /// <summary>
    /// Interaction logic for GiderWindow.xaml
    /// </summary>
    public partial class GiderWindow : Window ,IDisposable
    {
        public GiderViewModel giderViewModel;
        public GiderWindow()
        {
            giderViewModel = new GiderViewModel();
            InitializeComponent();
            this.DataContext = giderViewModel;
            giderViewModel.CloseGiderWindow += (s, e) => this.Close();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }

    }
}
