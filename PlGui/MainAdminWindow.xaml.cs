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
                UpdateStationsView(sender, e);
                // update linesView
                UpdateLinesView(sender, e);
                // update adjacent stations view
                UpdateAdjacentStationsView(sender, e);
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
        private void UpdateStationsView(object sender, EventArgs e)
        {
            // update stations view
            stations.Clear();
            foreach (var station in bl.GetAllStations())
                stations.Add(station);
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
        private void UpdateAdjacentStationsView(object sender, EventArgs e)
        {
            // update adjacent stations view
            adjStats.Clear();
            foreach (var adjStat in bl.GetAllAdjacentStations())
                adjStats.Add(adjStat);
        }
        #endregion

        #region Lines View
        private void cbLineNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Line line = (sender as ComboBox).SelectedItem as BO.Line;
            if (line != null)
                lineStationsListView.DataContext = bl.GetLine(line.ID).Stations;
        }
        private void UpdateLinesView(object sender, EventArgs e)
        {
            int selectedLineId = (cbLineNum.SelectedItem as BO.Line).ID;
            // update linesView
            lines.Clear();
            foreach (var line in bl.GetAllLines())
                lines.Add(line);
            BO.Line lineToDisplay = lines.FirstOrDefault(x => x.ID == selectedLineId);
            if (lineToDisplay != null)
            {
                cbLineNum.SelectedItem = lines.FirstOrDefault(x => x.ID == selectedLineId);
                lineStationsListView.DataContext = lineToDisplay.Stations;
            }
            else
                lineStationsListView.DataContext = null;
        }
        private void pbDeleteStationInLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteStationInLine(((sender as Button).DataContext as BO.LineStation).LineID, ((sender as Button).DataContext as BO.LineStation).StationCode);
                UpdateLinesView(sender, e);
                UpdateAdjacentStationsView(sender, e);
            }
            catch (InvalidOperationException ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pbDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            bl.DeleteLine(cbLineNum.SelectedItem as BO.Line);
            UpdateLinesView(sender, e);
        }
        private void pbAddLine_Click(object sender, RoutedEventArgs e)
        {
            AddLineWindow addLineWindow = new AddLineWindow(bl, stations.ToList());
            addLineWindow.Closed += UpdateLinesView;
            addLineWindow.ShowDialog();
        }
        private void pbDownButton_Click(object sender, RoutedEventArgs e)
        {
            BO.LineStation lineStation = ((sender as Button).DataContext as BO.LineStation);
            BO.Line line = cbLineNum.SelectedItem as BO.Line;
            bl.MoveStationDownInLine(line.ID, lineStation.StationCode);
            UpdateLinesView(sender, e);
            UpdateAdjacentStationsView(sender, e);
        }
        private void pbUpButton_Click(object sender, RoutedEventArgs e)
        {
            BO.LineStation lineStation = ((sender as Button).DataContext as BO.LineStation);
            BO.Line line = cbLineNum.SelectedItem as BO.Line;
            bl.MoveStationUpInLine(line.ID, lineStation.StationCode);
            UpdateLinesView(sender, e);
            UpdateAdjacentStationsView(sender,e);
        }
        private void lineStationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.AdjacentStations adjStat = (cbLineNum.SelectedItem as BO.Line).AdjStats.Find(x => x.Station1 == ((sender as ListView).SelectedItem as BO.LineStation).StationCode);
            UpdateAdjStatWindow updateAdjStat = new UpdateAdjStatWindow(adjStat, adjStats, bl);
            updateAdjStat.Closed += UpdateAdjacentStationsView;
            updateAdjStat.Closed += UpdateLinesView;
            updateAdjStat.ShowDialog();
        }
        private void pbAddStationToLine_Click(object sender, RoutedEventArgs e)
        {
            AddStationToLineWindow addStationToLineWindow = new AddStationToLineWindow(bl, stations.ToList(), cbLineNum.SelectedItem as BO.Line);
            addStationToLineWindow.Closed += UpdateLinesView;
            addStationToLineWindow.Closed += UpdateAdjacentStationsView;
            addStationToLineWindow.ShowDialog();
        }
        #endregion


    }
}
