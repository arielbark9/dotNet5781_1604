using System;
using System.Collections.Generic;
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
    /// Interaction logic for LineScheduleWindow.xaml
    /// </summary>
    public partial class LineScheduleWindow : Window
    {
        IBL bl;
        BO.Line line;
        BO.LineTrip newTrip = new BO.LineTrip() { StartTime = TimeSpan.Zero, EndTime = TimeSpan.Zero, Frequency = TimeSpan.Zero};
        public LineScheduleWindow(IBL bl, BO.Line _line)
        {
            InitializeComponent();
            this.bl = bl;
            line = _line;
            newTrip.LineID = line.ID;
            if (line.Trip != null)
                line.Trip.CopyPropertiesTo(newTrip);
            TopLabel.Content = $"Add trip for line {line.LineNum}";
            gridViewTrip.DataContext = newTrip;
        }

        private void pbAdd_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan temp = new TimeSpan();
            if (TimeSpan.TryParse(startTimeTextBox.Text, out temp) &&
                TimeSpan.TryParse(endTimeTextBox.Text, out temp) &&
                TimeSpan.TryParse(frequencyTextBox.Text, out temp))
            {
                try
                {
                    bl.AddLineTrip(newTrip);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                this.Close();
            }
            else
                MessageBox.Show("Time value must be of correct format", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void pbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
