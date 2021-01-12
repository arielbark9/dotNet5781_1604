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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        BO.Station newStat = new BO.Station();
        ObservableCollection<BO.Station> stations;
        IBL bl;
        public AddStationWindow(ObservableCollection<BO.Station> _stations, IBL _bl)
        {
            InitializeComponent();
            stations = _stations;
            bl = _bl;
            gridViewStat.DataContext = newStat;
        }

        private void pbAddStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddStation(newStat);
                stations.Add(newStat); //if bl failed this wont execute
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error! you tried to add a Station that already exists!");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error! you tried to add a Station that is invalid!");
            }
        }

        private void pbCancelStation_Click(object sender, RoutedEventArgs e)
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
        private void stationCodeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void stationNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
             Regex regex = new Regex("[^a-z^A-Z/]+");
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
