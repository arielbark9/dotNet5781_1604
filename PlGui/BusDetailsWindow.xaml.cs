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
    /// Interaction logic for BusDetailsWindow.xaml
    /// </summary>
    public partial class BusDetailsWindow : Window
    {
        BO.Bus displayBus;
        IBL bl;
        public BusDetailsWindow(BO.Bus displayBus, IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            this.displayBus = displayBus;
            gridViewBus.DataContext = displayBus;
        }

        private void pbFuel_Click(object sender, RoutedEventArgs e)
        {
            //TODO: implement
        }

        private void pbRepair_Click(object sender, RoutedEventArgs e)
        {
            //TODO: implement
        }
    }
}
