using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using EBudgetPlaning.Business.Model;

namespace EBudgetPlaning.Business.ViewModel
{
    public class GrafikGiderViewModel
    {
        public GrafikGiderViewModel(GroupBox groupBox)
        {
            GiderDbAccess db = new GiderDbAccess();
            List<GrafikModel> tempList = new List<GrafikModel>();
            tempList = db.ForGraphic();
           
            Chart dynamicChart = new Chart();
            dynamicChart.Title = "YILLIK GİDER GRAFİĞİ";
            LineSeries lineseries = new LineSeries();
            lineseries.ItemsSource = tempList;
            lineseries.DependentValuePath = "Values";
            lineseries.IndependentValuePath = "Key";
            dynamicChart.Series.Add(lineseries);
            groupBox.Content = dynamicChart;
        }

    }
}
