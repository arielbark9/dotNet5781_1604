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
    /// Interaction logic for StationDetailsWindow.xaml
    /// </summary>
    public partial class StationDetailsWindow : Window
    {
        BO.Station displayStat;
        IBL bl;
        public StationDetailsWindow(BO.Station station, IBL bl)
        {
            displayStat = station;
            this.bl = bl;
            InitializeComponent();
            gridViewStat.DataContext = displayStat;
            linesByStationListView.ItemsSource = bl.GetLinesThatGoThroughStation(displayStat.StationCode);
        }
    }
}
