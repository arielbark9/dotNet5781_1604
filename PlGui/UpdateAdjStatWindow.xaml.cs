using BLAPI;
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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for UpdateAdjStatWindow.xaml
    /// </summary>
    public partial class UpdateAdjStatWindow : Window
    {
        BO.AdjacentStations updateAdjStat;
        ObservableCollection<BO.AdjacentStations> dispAdjStat;
        IBL bl;
        public UpdateAdjStatWindow(BO.AdjacentStations updateAdjStat, ObservableCollection<BO.AdjacentStations> dispAdjStat, IBL bl)
        {
            InitializeComponent();
            this.updateAdjStat = updateAdjStat;
            this.dispAdjStat = dispAdjStat;
            this.bl = bl;
            gridViewAdjStat.DataContext = this.updateAdjStat;
        }
        private void pbUpdate_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan check = new TimeSpan();
            if (TimeSpan.TryParse(timeTextBox.Text, out check))
            {
                try
                {
                    bl.UpdateAdjacentStations(updateAdjStat); // will stop here if an exception is thrown
                    BO.AdjacentStations adjStat = dispAdjStat.FirstOrDefault(x => x.Station1 == updateAdjStat.Station1 && x.Station2 == updateAdjStat.Station2);
                    updateAdjStat.CopyPropertiesTo(adjStat);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("ERROR!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Time value must be of correct format", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            this.Close();
        }

        private void pbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Input Check
        private void timeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9:]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}
