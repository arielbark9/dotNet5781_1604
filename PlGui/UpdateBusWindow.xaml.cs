using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                updateBus.CopyPropertiesTo(bus);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("ERROR!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }

        private void pbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region input check
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void mileageTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void mileageSinceFuelTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void mileageSinceMaintenanceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        #endregion
    }
}
