﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        BO.User user;
        IBL bl;
        ObservableCollection<BO.Bus> buses;
        public MainAdminWindow(BO.User user, IBL bl)
        {
            InitializeComponent();
            this.user = user;
            this.bl = bl;
            // using more than one BL request to Bind all listviews
            buses = new ObservableCollection<BO.Bus>(from item in bl.GetAllBuses() select item);
            busListView.DataContext = buses;
        }
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
                bl.DeleteBus((sender as Button).DataContext as BO.Bus); // No exceptions because if it is a part of the list view it can bel deleted.
                // assuming that our binded collection is synced with DATASOURCE
                buses.Remove((sender as Button).DataContext as BO.Bus);
                
            }
        }

        private void pbUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void busListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new BusDetailsWindow((sender as ListView).SelectedItem as BO.Bus, bl).Show();
        }
    }
}
