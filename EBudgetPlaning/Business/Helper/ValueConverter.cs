using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EBudgetPlaning.Business.Helper
{
    public class ValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush solidColor = null;
            if (value.ToString() == "Maaş")
            {
                solidColor = new SolidColorBrush(Colors.Green);
            }
            else
            {
                solidColor = new SolidColorBrush(Colors.Red);
            }
            return solidColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
