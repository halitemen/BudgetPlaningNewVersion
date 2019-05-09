using System;
using System.Windows;
using EBudgetPlaning.Business.ViewModel;

namespace EBudgetPlaning.Business.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window, IDisposable
    {
        public GelirViewModel gelirViewModel;
       
        public Window1()
        {
            gelirViewModel = new GelirViewModel();
            InitializeComponent();
            this.DataContext = gelirViewModel;           
            gelirViewModel.CloseGelirWindow += (s, e) => this.Close();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }

       
    }
}
