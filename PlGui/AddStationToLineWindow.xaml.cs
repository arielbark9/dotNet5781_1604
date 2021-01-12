using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for AddStationToLineWindow.xaml
    /// </summary>
    public partial class AddStationToLineWindow : Window
    {
        List<int> stationsToAdd = new List<int>();
        IBL bl;
        List<BO.Station> stations;
        BO.Line line;
        public AddStationToLineWindow(IBL _bl, List<BO.Station> _stations, BO.Line _line)
        {
            InitializeComponent();
            bl = _bl;
            stations = _stations;
            line = _line;
            stations.RemoveAll(x => line.Stations.Exists(y => y.StationCode == x.StationCode)); // only stations not already in the line
            stationListView.DataContext = stations;
        }

        private void checkBoxAdd_Checked(object sender, RoutedEventArgs e)
        {
            stationsToAdd.Insert(0, ((sender as CheckBox).DataContext as BO.Station).StationCode);
        }
        private void checkBoxAdd_Unchecked(object sender, RoutedEventArgs e)
        {
            stationsToAdd.Remove(((sender as CheckBox).DataContext as BO.Station).StationCode);
        }
        private void pbAddStations_Click(object sender, RoutedEventArgs e)
        {
            foreach (int stationCode in stationsToAdd)
            {
                BO.LineStation newLineStation = new BO.LineStation { LineID = line.ID, StationCode = stationCode };
                bl.AddLineStation(newLineStation);
            }
            this.Close();
        }
        private void pbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
