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
    /// Interaction logic for UpdateStationWindow.xaml
    /// </summary>
    public partial class UpdateStationWindow : Window
    {
        BO.Station updateStat;
        ObservableCollection<BO.Station> stations;
        IBL bl;
        public UpdateStationWindow(BO.Station _updateStat, ObservableCollection<BO.Station> _stations, IBL _bl)
        {
            InitializeComponent();
            updateStat = _updateStat;
            stations = _stations;
            bl = _bl;
            gridViewStat.DataContext = updateStat;
        }
        private void pbUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateStation(updateStat); // will stop here if an exception is thrown
                BO.Station station = stations.FirstOrDefault(x => x.StationCode == updateStat.StationCode);
                updateStat.CopyPropertiesTo(station);
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
        #region Input Check
        private void latitudeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void longitudeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void stationNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private static readonly Regex _regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
        #endregion
    }
}
