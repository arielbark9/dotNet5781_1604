using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        BO.Line newLine = new BO.Line();

        IBL bl;
        List<BO.Station> stations;
        public AddLineWindow(IBL _bl, List<BO.Station> _stations)
        {
            InitializeComponent();
            bl = _bl;
            stations = _stations;
            newLine.AdjStats = new List<BO.AdjacentStations>();
            newLine.Stations = new List<BO.LineStation>();
            newLine.LineNum = 0;
            newLine.LastStation = new BO.Station();
            newLine.ID = -1;
            newLine.Region = BO.Area.General;
            gridViewLine.DataContext = newLine;
            cbFirstStation.DataContext = stations;
            cbFirstStation.SelectedItem = null;
            cbLastStation.IsEnabled = false;
        }
        private void pbAddLine_Click(object sender, RoutedEventArgs e)
        {
            newLine.Region = (BO.Area)regionComboBox.SelectedItem;
            bl.AddLine(newLine);
            this.Close();
        }

        private void pbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbFirstStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbLastStation.DataContext = stations.Where(x => x.StationCode != (cbFirstStation.SelectedItem as BO.Station).StationCode);
            cbLastStation.IsEnabled = true;
            cbFirstStation.IsEnabled = false;
            if(newLine.Stations.Count == 0)
                newLine.Stations.Add(new BO.LineStation { StationCode = (cbFirstStation.SelectedItem as BO.Station).StationCode, StationPlacement = 1});
            else
                newLine.Stations[0] = new BO.LineStation { StationCode = (cbFirstStation.SelectedItem as BO.Station).StationCode, StationPlacement = 1 };
        }

        private void cbLastStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbLastStation.IsEnabled = false;
            if (newLine.Stations.Count == 1)
                newLine.Stations.Add(new BO.LineStation { StationCode = (cbLastStation.SelectedItem as BO.Station).StationCode, StationPlacement = 2 });
            else
                newLine.Stations[1] = new BO.LineStation { StationCode = (cbLastStation.SelectedItem as BO.Station).StationCode, StationPlacement = 2 };
            newLine.AdjStats.Add(new BO.AdjacentStations { Station1 = newLine.Stations[0].StationCode, Station2 = newLine.Stations[1].StationCode, Time = TimeSpan.Zero });
            newLine.LastStation = bl.GetStation(newLine.Stations[1].StationCode);
        }
        #region input check
        private void lineNumTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        #endregion
    }
}
