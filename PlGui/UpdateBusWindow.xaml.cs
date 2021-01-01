using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UpdateBusWindow.xaml
    /// </summary>
    public partial class UpdateBusWindow : Window
    {
        BO.Bus updateBus;
        IBL bl;
        ObservableCollection<BO.Bus> buses;
        public UpdateBusWindow(BO.Bus upBus, ObservableCollection<BO.Bus> displayBuses, IBL bl)
        {
            this.bl = bl;
            updateBus = upBus;
            buses = displayBuses;
            InitializeComponent();
            gridViewBus.DataContext = updateBus;
        }

        private void pbUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateBus(updateBus); // will stop here if an exception is thrown
                BO.Bus bus = buses.FirstOrDefault(x => x.LicenceNum == updateBus.LicenceNum);
                buses.Remove(bus);
                buses.Add(updateBus);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("ERROR!");
            }
            this.Close();
        }

        private void pbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
