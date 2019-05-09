using System;
using System.Windows;
using EBudgetPlaning.Business.ViewModel;

namespace EBudgetPlaning.Business.View
{
    /// <summary>
    /// Interaction logic for AnaSayfa.xaml
    /// </summary>
    public partial class AnaSayfa : Window, IDisposable
    {
        AnaSayfaViewModel anasayfaViewModel;
        public AnaSayfa()
        {
            anasayfaViewModel = new AnaSayfaViewModel();
            InitializeComponent();
            this.DataContext = anasayfaViewModel;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }      
    }
}
