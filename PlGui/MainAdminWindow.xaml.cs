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
        ObservableCollection<BO.Station> stations;
        public MainAdminWindow(BO.User user, IBL bl)
        {
            InitializeComponent();
            this.user = user;
            this.bl = bl;
            // using more than one BL request to Bind all listviews
            buses = new ObservableCollection<BO.Bus>(from item in bl.GetAllBuses() select item);
            busListView.DataContext = buses;
            stations = new ObservableCollection<BO.Station>(from item in bl.GetAllStations() select item);
            stationListView.DataContext = stations;
        }
        #region Buses View
        private void pbAddBus_Click(object sender, RoutedEventArgs e)
        {
            new AddBusWindow(bl, buses).Show();
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
            new UpdateBusWindow(updateBus, buses, bl).Show();
        }

        private void busListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new BusDetailsWindow((sender as ListView).SelectedItem as BO.Bus, bl).Show();
        }
        #endregion

        #region Stations View
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new AddStationWindow(stations, bl).Show();
        }
        private void stationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new StationDetailsWindow((sender as ListView).SelectedItem as BO.Station).Show();
        }
        private void pbDeleteStat_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Consent", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    bl.DeleteStation((sender as Button).DataContext as BO.Station);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                stations.Remove((sender as Button).DataContext as BO.Station);
            }
        }
        private void pbUpdateStat_Click(object sender, RoutedEventArgs e)
        {
            BO.Station updateStation = new BO.Station();
            ((sender as Button).DataContext as BO.Station).CopyPropertiesTo(updateStation);
            new UpdateStationWindow(updateStation, stations, bl).Show();
        }
        #endregion
    }
}
