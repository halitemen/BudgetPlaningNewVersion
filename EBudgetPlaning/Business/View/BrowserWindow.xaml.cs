using System;
using System.Windows;
using Gecko;
namespace EBudgetPlaning.Business.View
{
    /// <summary>
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window, IDisposable
    {
        public BrowserWindow()
        {

            InitializeComponent();
            Xpcom.Initialize("Firefox");
            Browser.Navigate("http://bigpara.hurriyet.com.tr/altin");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
