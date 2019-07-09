using System;
using System.Windows;
using EBudgetPlaning.Business.ViewModel;

namespace EBudgetPlaning.Business.View
{
    /// <summary>
    /// Interaction logic for GrafikWindowGelir.xaml
    /// </summary>
    public partial class GrafikWindowGelir : Window,IDisposable
    {
        public GrafikWindowGelir()
        {
            InitializeComponent();
            this.DataContext = new GrafikGelirViewModel(GroupBoxDynamicChart);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
