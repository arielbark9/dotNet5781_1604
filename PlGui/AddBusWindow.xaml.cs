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
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        BO.Bus newBus;
        ObservableCollection<BO.Bus> buses; 
        IBL bl;
        public AddBusWindow(IBL bl, ObservableCollection<BO.Bus> buses)
        {
            InitializeComponent();
            this.bl = bl;
            this.buses = buses;
            newBus = new BO.Bus();
            gridViewBus.DataContext = newBus;
        }

        private void pbAddBus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddBus(newBus);
                buses.Add(newBus); //if bl failed this wont execute
                this.Close();
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show("You tried to add a bus that already exists!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show("You tried to add a bus that is invalid!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void pbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region input check
        private void licenceNumPreviewTextInput(object sender, TextCompositionEventArgs e) // allow only for digits to be entered
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
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
