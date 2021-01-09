using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        BO.User user;
        IBL bl;
        ObservableCollection<BO.Bus> buses;
        ObservableCollection<BO.Line> lines;
        ObservableCollection<BO.Station> stations;
        ObservableCollection<BO.AdjacentStations> adjStats;
        public MainAdminWindow(BO.User user, IBL bl)
        {
            InitializeComponent();
            this.user = user;
            this.bl = bl;
            labelGreeting.Content = $"Hello {user.UserName}! Welcome to Ariel's Bus handeling system";
            // using more than one BL request to Bind all listviews With Hirearchial Data Context
            buses = new ObservableCollection<BO.Bus>(from item in bl.GetAllBuses() select item);
            busListView.DataContext = buses;
            lines = new ObservableCollection<BO.Line>(from item in bl.GetAllLines() select item);
            lineStationsListView.DataContext = lines[0].Stations;
            cbLineNum.DataContext = lines;
            cbLineNum.SelectedItem = lines[0];
            stations = new ObservableCollection<BO.Station>(from item in bl.GetAllStations() select item);
            stationListView.DataContext = stations;
            adjStats = new ObservableCollection<BO.AdjacentStations>(from item in bl.GetAllAdjacentStations() select item);
            adjacentStationsListView.DataContext = adjStats;
        }
        #region Buses View
        private void pbAddBus_Click(object sender, RoutedEventArgs e)
        {
            new AddBusWindow(bl, buses).ShowDialog();
        }

        private void pbDrive_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implement
        }

        private void pbDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure?", "Consent", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    bl.DeleteBus((sender as Button).DataContext as BO.Bus);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                buses.Remove((sender as Button).DataContext as BO.Bus);
            }
        }

        private void pbUpdate_Click(object sender, RoutedEventArgs e)
        {
            BO.Bus updateBus = new BO.Bus();
            ((sender as Button).DataContext as BO.Bus).CopyPropertiesTo(updateBus);
            new UpdateBusWindow(updateBus, buses, bl).ShowDialog();
        }

        private void busListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new BusDetailsWindow((sender as ListView).SelectedItem as BO.Bus, bl).Show();
        }
        #endregion

        #region Stations View
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new AddStationWindow(stations, bl).ShowDialog();
        }
        private void stationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new StationDetailsWindow((sender as ListView).SelectedItem as BO.Station, bl).Show();
        }
        private void pbDeleteStat_Click(object sender, RoutedEventArgs e)
        {
            BO.Station delStation = (sender as Button).DataContext as BO.Station;
            if (MessageBox.Show("Are you sure?", "Consent", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    bl.DeleteStation(delStation);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // Stations View Update
                stations.Remove(delStation);
                // update linesView
                UpdateLinesView(this, new EventArgs());
                // update adjacent stations view
                adjStats.Clear();
                foreach (var adjStat in bl.GetAllAdjacentStations())
                    adjStats.Add(adjStat);
            }
        }
        private void pbUpdateStat_Click(object sender, RoutedEventArgs e)
        {
            BO.Station updateStation = new BO.Station();
            ((sender as Button).DataContext as BO.Station).CopyPropertiesTo(updateStation);
            UpdateStationWindow updateStationWindow = new UpdateStationWindow(updateStation, stations, bl);
            updateStationWindow.ShowDialog();
            updateStationWindow.Closed += UpdateLinesView;
        }

        private void UpdateLinesView(object sender, EventArgs e)
        {
            // update linesView
            lines.Clear();
            foreach (var line in bl.GetAllLines())
                lines.Add(line);

            cbLineNum.SelectedItem = lines[0];
            lineStationsListView.DataContext = lines[0].Stations;
        }
        #endregion

        #region AdjacentStations View
        private void adjacentStationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new AdjStatDetailsWindow((sender as ListView).SelectedItem as BO.AdjacentStations).Show();
        }
        private void pbUpdateAdjStat_Click(object sender, RoutedEventArgs e)
        {
            UpdateAdjStatWindow updateAdjStat = new UpdateAdjStatWindow((sender as Button).DataContext as BO.AdjacentStations, adjStats, bl);
            updateAdjStat.Closed += UpdateLinesView;
            updateAdjStat.ShowDialog();
        }
        #endregion

        #region Lines View
        private void cbLineNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Line line = (sender as ComboBox).SelectedItem as BO.Line;
            if (line != null)
                lineStationsListView.DataContext = bl.GetLine(line.ID).Stations;
        }
        #endregion


    }
}
