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
        public LineScheduleWindow(IBL bl, BO.Line _line)
        {
            InitializeComponent();
            this.bl = bl;
            line = _line;
            TopLabel.Content = $"Upcoming trips for line {line.LineNum}";
            lineListBox.DataContext = bl.GetLineSchedule(line);
        }
    }
}
