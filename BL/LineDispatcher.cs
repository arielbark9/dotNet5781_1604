using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLAPI;

namespace BL
{
    public class LineDispatcher
    {
        IBL bl = BLFactory.GetBL("1");
        #region Singelton
        static readonly LineDispatcher instance = new LineDispatcher();
        static LineDispatcher() { }// static ctor to ensure instance init is done just before first usage
        LineDispatcher() { } // default => private
        public static LineDispatcher Instance { get => instance; }// The public Instance property to use
        #endregion
        private Clock clock = Clock.Instance;
        public int SecondsToUpdate { get; set; }
        public int DisplayStationCode { get; set; } = -1;
        public event Action<BO.LineTiming> OnLineTimingChanged;
        // Helper Class
        private class LeavingLine
        {
            public BO.Line Line { get; set; }
            public TimeSpan LeaveTime { get; set; }
        }

        public void StartDispatch()
        {
            List<LeavingLine> startTimes = new List<LeavingLine>(); // line id, time of leaving

            foreach (var line in bl.GetAllLines())
                foreach (var time in bl.GetLineSchedule(line))
                    startTimes.Add(new LeavingLine { Line = line, LeaveTime = time });
            startTimes = (from item in startTimes
                          orderby item.LeaveTime
                          select item).ToList();

            new Thread(() =>
            {
                int index = 0;
                List<LeavingLine> linesLeavingNow = new List<LeavingLine>();
                while (!clock.Cancel)
                {
                    while (linesLeavingNow.Count == 0)
                        linesLeavingNow = startTimes.FindAll(x => x.LeaveTime == new TimeSpan(clock.Time.Hours, clock.Time.Minutes, clock.Time.Seconds));
                    index = startTimes.IndexOf(linesLeavingNow.Last());

                    foreach (var leavingLine in linesLeavingNow)
                        SendLine(leavingLine);

                    linesLeavingNow.Clear(); // reset lines leaving

                    if (index < startTimes.Count - 1)
                        Thread.Sleep(TimeSpan.FromTicks((startTimes[index + 1].LeaveTime - startTimes[index].LeaveTime).Ticks / clock.Rate));
                }
            }).Start();
        }
        public void StopDispatch()
        {
            OnLineTimingChanged = null;
        }
        private void SendLine(LeavingLine leavingLine)
        {
            Thread newLineLeaving = new Thread(() =>
            {
                BO.LineTiming lineOnTrip = new BO.LineTiming() { LineID = leavingLine.Line.ID, StartTime = leavingLine.LeaveTime };
                BO.Line line = leavingLine.Line;
                lineOnTrip.LineNum = line.LineNum;
                lineOnTrip.CurrentStationIndex = 0;
                lineOnTrip.LastStationName = line.LastStation.StationName;
                TimeSpan TimeSinceStation = TimeSpan.Zero;
                Random r = new Random();

                while (!clock.Cancel)
                {
                    if (this.DisplayStationCode != -1 && lineOnTrip.CurrentStationIndex <= line.Stations.FindIndex(x => x.StationCode == this.DisplayStationCode))
                    {
                        BO.LineStation station1 = new BO.LineStation { StationCode = line.Stations[lineOnTrip.CurrentStationIndex].StationCode, LineID = line.ID };
                        BO.LineStation station2 = new BO.LineStation { StationCode = this.DisplayStationCode, LineID = line.ID };
                        lineOnTrip.ArrivalTimeAtStation = bl.TimeBetweenLineStations(station1, station2, line) - TimeSinceStation;
                        OnLineTimingChanged?.Invoke(lineOnTrip);
                        if (lineOnTrip.ArrivalTimeAtStation == TimeSpan.Zero)
                            break; // Arrived
                        
                        Thread.Sleep(SecondsToUpdate*(1000 / clock.Rate));
                        TimeSinceStation += TimeSpan.FromSeconds(SecondsToUpdate);
                        if (TimeSinceStation >= line.Stations[lineOnTrip.CurrentStationIndex].TimeToNext)
                        {
                            TimeSinceStation = TimeSpan.Zero;
                            if (lineOnTrip.CurrentStationIndex++ == line.Stations.Count - 1)
                                break;
                        }
                    }
                    else if (DisplayStationCode == -1)
                    {
                        Thread.Sleep(TimeSpan.FromTicks((line.Stations[lineOnTrip.CurrentStationIndex].TimeToNext).Ticks / clock.Rate));
                        if (lineOnTrip.CurrentStationIndex++ == line.Stations.Count - 1) // arrived at last station
                            break;
                    }
                    else
                        break;
                }
            });
            newLineLeaving.Start();
        }
        public void ResetObservers()
        {
            OnLineTimingChanged = null;
        }
    }
}
