using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using EBudgetPlaning.Business.Model;

namespace EBudgetPlaning.Business.ViewModel
{
    public class GrafikGelirViewModel
    {
        public GrafikGelirViewModel(GroupBox groupBox)
        {
            GelirDbAccess gelirDbAccess = new GelirDbAccess();
            List<GrafikModel> tempList = new List<GrafikModel>();
            tempList = gelirDbAccess.getTotalGelir();

            Chart dynamicChart = new Chart();

            dynamicChart.Title = "YILLIK GELİR GRAFİĞİ";
            LineSeries lineseries = new LineSeries();
            lineseries.ItemsSource = tempList;
            lineseries.DependentValuePath = "Values";
            lineseries.IndependentValuePath = "Key";
            dynamicChart.Series.Add(lineseries);
            groupBox.Content = dynamicChart;
        }
    }
}
