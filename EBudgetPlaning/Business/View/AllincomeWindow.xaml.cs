using System;
using System.Windows;
using EBudgetPlaning.Business.ViewModel;

namespace EBudgetPlaning.Business.View
{
    /// <summary>
    /// Interaction logic for AllincomeWindow.xaml
    /// </summary>
    public partial class AllincomeWindow : Window, IDisposable
    {
        GelirViewModel gelirViewModel;
        public AllincomeWindow()
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
