using System;
using System.Windows;
using EBudgetPlaning.Business.VİewModel;

namespace EBudgetPlaning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        UsersViewModel usersViewModel;
        public MainWindow()
        {
            usersViewModel = new UsersViewModel(this);
            InitializeComponent();
            this.DataContext = usersViewModel;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
