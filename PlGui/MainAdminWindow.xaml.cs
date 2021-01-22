using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        BO.User user;
        IBL bl;
        ObservableCollection<BO.Bus> buses;
        ObservableCollection<BO.Line> lines;
        ObservableCollection<BO.Station> stations;
        ObservableCollection<BO.LineTiming> lineTimings;
        public MainAdminWindow(BO.User user, IBL bl)
        {
            InitializeComponent();
            this.user = user;
            gridViewUser.DataContext = user;
            this.bl = bl;
            labelGreeting.Content = $"Hello {user.UserName}! Welcome to Ariel's Bus handeling system";
            // init lists
            bl.InitializeDisplay(ref buses, ref lines, ref stations);
            lineTimings = new ObservableCollection<BO.LineTiming>();
            // buses
            busListView.DataContext = buses;
            // lines
            lineStationsListView.DataContext = lines[0].Stations;
            lineTripsListView.DataContext = bl.GetLineSchedule(lines[0]);
            cbLineNum.DataContext = lines;
            cbLineNum.SelectedItem = lines[0];
            // stations
            stationListView.DataContext = stations;
            lineTimingListView.DataContext = lineTimings;
            this.Closed += MainAdminWindow_Closed;
        }

        private void MainAdminWindow_Closed(object sender, EventArgs e)
        {
            bl.StopSimulation();
            if (simulationWorker != null && !simulationWorker.CancellationPending)
                simulationWorker.CancelAsync();
        }

        private void pbUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateUser(user);
            labelGreeting.Content = $"Hello {user.UserName}! Welcome to Ariel's Bus handeling system";
        }

        #region Buses View
        private void pbAddBus_Click(object sender, RoutedEventArgs e)
        {
            new AddBusWindow(bl, buses).ShowDialog();
        }
        private void pbDrive_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implement
        }
        private void pbDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure?", "Consent", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    bl.DeleteBus((sender as Button).DataContext as BO.Bus);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                buses.Remove((sender as Button).DataContext as BO.Bus);
            }
        }
        private void pbUpdate_Click(object sender, RoutedEventArgs e)
        {
            BO.Bus updateBus = new BO.Bus();
            ((sender as Button).DataContext as BO.Bus).CopyPropertiesTo(updateBus);
            new UpdateBusWindow(updateBus, buses, bl).ShowDialog();
        }
        private void busListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new BusDetailsWindow((sender as ListView).SelectedItem as BO.Bus, bl).Show();
        }
        #endregion

        #region Stations View
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new AddStationWindow(stations, bl).ShowDialog();
        }
        private void stationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new StationDetailsWindow((sender as ListView).SelectedItem as BO.Station, bl).Show();
        }
        private void pbDeleteStat_Click(object sender, RoutedEventArgs e)
        {
            BO.Station delStation = (sender as Button).DataContext as BO.Station;
            if (MessageBox.Show("Are you sure?", "Consent", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    bl.DeleteStation(delStation);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // Stations View Update
                UpdateStationsView(sender, e);
                // update linesView
                UpdateLinesView(sender, e);
            }
        }
        private void pbUpdateStat_Click(object sender, RoutedEventArgs e)
        {
            BO.Station updateStation = new BO.Station();
            ((sender as Button).DataContext as BO.Station).CopyPropertiesTo(updateStation);
            UpdateStationWindow updateStationWindow = new UpdateStationWindow(updateStation, stations, bl);
            updateStationWindow.ShowDialog();
            updateStationWindow.Closed += UpdateLinesView;
        }
        private void UpdateStationsView(object sender, EventArgs e)
        {
            // update stations view
            stations.Clear();
            foreach (var station in bl.GetAllStations())
                stations.Add(station);
        }
        BackgroundWorker stationSimWorker;
        private void stationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            linesByStationListView.DataContext = bl.GetLinesThatGoThroughStation(((sender as ListView).SelectedItem as BO.Station).StationCode);

            stationSimWorker = new BackgroundWorker();
            stationSimWorker.WorkerReportsProgress = true;
            stationSimWorker.WorkerSupportsCancellation = true;
            stationSimWorker.DoWork += StationSimWorker_DoWork;
            stationSimWorker.ProgressChanged += StationSimWorker_ProgressChanged;
            stationSimWorker.RunWorkerAsync(((sender as ListView).SelectedItem as BO.Station).StationCode);
        }

        private void StationSimWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BO.LineTiming newLineTiming = e.UserState as BO.LineTiming;

            if (newLineTiming != null)
            {
                if (newLineTiming.ArrivalTimeAtStation != TimeSpan.Zero)
                {
                    if (lineTimings.Any(x => x.ID == newLineTiming.ID))
                        lineTimings.Remove(lineTimings.FirstOrDefault(x => x.ID == newLineTiming.ID));
                    lineTimings.Add(newLineTiming);
                }
                else
                    gridViewLastLineToArrive.DataContext = newLineTiming;

                UpdateLineTimings();
            }
            else
                lineTimings.Clear();
        }

        private void StationSimWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            bl.SetStationPanel((int)e.Argument, (BO.LineTiming x) => stationSimWorker.ReportProgress(0, x));
            while (!stationSimWorker.CancellationPending) ;
        }

        private void UpdateLineTimings()
        {
            List<BO.LineTiming> newList = new List<BO.LineTiming>();
            
            newList = (from lineTiming in lineTimings
                              orderby lineTiming.ArrivalTimeAtStation
                              select lineTiming).ToList();

            lineTimings.Clear();
            foreach (var lineTiming in newList)
                if (lineTiming.ArrivalTimeAtStation != TimeSpan.Zero)
                    lineTimings.Add(lineTiming);
        }
        #endregion

        #region Lines View
        private void cbLineNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Line line = (sender as ComboBox).SelectedItem as BO.Line;
            if (line != null)
            {
                lineStationsListView.DataContext = bl.GetLine(line.ID).Stations;
                lineTripsListView.DataContext = bl.GetLineSchedule(line);
            }
        }
        private void UpdateLinesView(object sender, EventArgs e)
        {
            int selectedLineId = (cbLineNum.SelectedItem as BO.Line).ID;
            // update linesView
            lines.Clear();
            foreach (var line in bl.GetAllLines())
                lines.Add(line);
            BO.Line lineToDisplay = lines.FirstOrDefault(x => x.ID == selectedLineId);
            if (lineToDisplay != null)
            {
                cbLineNum.SelectedItem = lines.FirstOrDefault(x => x.ID == selectedLineId);
                lineStationsListView.DataContext = lineToDisplay.Stations;
                lineTripsListView.DataContext = bl.GetLineSchedule(lineToDisplay);
            }
            else
            {
                lineStationsListView.DataContext = null;
                lineTripsListView.DataContext = null;
            }
        }
        private void pbDeleteStationInLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteStationInLine(((sender as Button).DataContext as BO.LineStation).LineID, ((sender as Button).DataContext as BO.LineStation).StationCode);
                UpdateLinesView(sender, e);
            }
            catch (InvalidOperationException ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void pbDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            bl.DeleteLine(cbLineNum.SelectedItem as BO.Line);
            UpdateLinesView(sender, e);
        }
        private void pbAddLine_Click(object sender, RoutedEventArgs e)
        {
            AddLineWindow addLineWindow = new AddLineWindow(bl, stations.ToList());
            addLineWindow.Closed += UpdateLinesView;
            addLineWindow.ShowDialog();
        }
        private void pbDownButton_Click(object sender, RoutedEventArgs e)
        {
            BO.LineStation lineStation = ((sender as Button).DataContext as BO.LineStation);
            BO.Line line = cbLineNum.SelectedItem as BO.Line;
            bl.MoveStationDownInLine(line.ID, lineStation.StationCode);
            UpdateLinesView(sender, e);
        }
        private void pbUpButton_Click(object sender, RoutedEventArgs e)
        {
            BO.LineStation lineStation = ((sender as Button).DataContext as BO.LineStation);
            BO.Line line = cbLineNum.SelectedItem as BO.Line;
            bl.MoveStationUpInLine(line.ID, lineStation.StationCode);
            UpdateLinesView(sender, e);
        }
        private void lineStationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.AdjacentStations adjStat = (cbLineNum.SelectedItem as BO.Line).AdjStats.Find(x => x.Station1 == ((sender as ListView).SelectedItem as BO.LineStation).StationCode);
            UpdateAdjStatWindow updateAdjStat = new UpdateAdjStatWindow(adjStat, bl);
            updateAdjStat.Closed += UpdateLinesView;
            updateAdjStat.ShowDialog();
        }
        private void pbAddStationToLine_Click(object sender, RoutedEventArgs e)
        {
            AddStationToLineWindow addStationToLineWindow = new AddStationToLineWindow(bl, stations.ToList(), cbLineNum.SelectedItem as BO.Line);
            addStationToLineWindow.Closed += UpdateLinesView;
            addStationToLineWindow.ShowDialog();
        }
        private void pbAddLineSchedule_Click(object sender, RoutedEventArgs e)
        {
            LineScheduleWindow scheduleWindow = new LineScheduleWindow(bl, cbLineNum.SelectedItem as BO.Line);
            scheduleWindow.Closed += UpdateLinesView;
            scheduleWindow.ShowDialog();
        }
        #endregion

        #region Clock
        private void tbClockTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex("[^0-9:]+"); //regex that matches disallowed text
            e.Handled = _regex.IsMatch(e.Text);
        }
        private void tbClockRate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            e.Handled = _regex.IsMatch(e.Text);
        }

        BackgroundWorker simulationWorker;
        private void pbStartClock_Click(object sender, RoutedEventArgs e)
        {
            if (pbStartClock.Content.ToString() == "Start")
            {
                TimeSpan startTime = new TimeSpan();
                int rate = 0;
                if (TimeSpan.TryParse(tbClockTime.Text, out startTime) && int.TryParse(tbClockRate.Text, out rate) && rate != 0 && rate <= 1000)
                {
                    // take care of display options
                    tbClockRate.IsReadOnly = true;
                    tbClockTime.IsReadOnly = true;
                    pbStartClock.Content = "Stop";
                    // Activate Clock
                    simulationWorker = new BackgroundWorker();
                    simulationWorker.WorkerSupportsCancellation = true;
                    simulationWorker.WorkerReportsProgress = true;
                    simulationWorker.ProgressChanged += SimWorker_ProgressChanged;
                    simulationWorker.DoWork += SimWorker_DoWork;
                    simulationWorker.RunWorkerAsync(new object[] { startTime, rate });
                }
                else if (rate <= 1000)
                    MessageBox.Show("Invalid time or rate value!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show("rate can't be more than 1000", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else // Stop Clock
            {
                // take care of display options
                tbClockRate.IsReadOnly = false;
                tbClockTime.IsReadOnly = false;
                pbStartClock.Content = "Start";
                // Stop the Clock
                bl.StopSimulation();
                if (simulationWorker.WorkerSupportsCancellation == true)
                    simulationWorker.CancelAsync();
            }
        }
        void SimWorker_ProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            tbClockTime.Text = ((TimeSpan)args.UserState).ToString().Substring(0, 8); // this happens in UI Thread
        }
        void SimWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arguments = e.Argument as object[];
            bl.StartSimulation((TimeSpan)arguments[0], (int)arguments[1], (TimeSpan x) =>
            {
                if (!simulationWorker.CancellationPending)
                    simulationWorker.ReportProgress(0, x);
            });
            while (!simulationWorker.CancellationPending);
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource lineTimingViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lineTimingViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // lineTimingViewSource.Source = [generic data source]
        }
    }
}
