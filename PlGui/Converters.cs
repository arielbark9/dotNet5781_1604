using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BLAPI;

namespace PlGui
{
    public class StationTimeToMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((TimeSpan)value == TimeSpan.Zero)
                return "Update Time!";
            else if (((TimeSpan)value).Ticks == 1)
                return "Last Station";
            else
                return (TimeSpan)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
