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
    public class StationCodeToTimeToNextConverter : IMultiValueConverter
    {
        IBL bl = BLFactory.GetBL("1");
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            // value[0] = LineNum, value[1] = StationCode
            BO.Line line = bl.GetLine(int.Parse(value[0].ToString()));
            int stationOneIndex = line.Stations.FindIndex(x => x.StationCode == int.Parse(value[1].ToString()));
            if (stationOneIndex < line.Stations.Count - 1)
            {
                int stationOneCode = line.Stations[stationOneIndex].StationCode;
                int stationTwoCode = line.Stations[stationOneIndex + 1].StationCode;
                return line.AdjStats.Find(x => (x.Station1 == stationOneCode && x.Station2 == stationTwoCode)).Time.ToString() ;
            }
            else
                return null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class StationCodeToStationNameConverter : IValueConverter
    {
        IBL bl = BLFactory.GetBL("1");
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return bl.GetStation(int.Parse(value.ToString())).StationName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
