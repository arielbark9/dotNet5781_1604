using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLAPI;
using DLAPI;

namespace BL
{
    class BLImp : IBL // internal, use only through interface BLApi.
    {
        private IDL dl = DLFactory.GetDL();
        #region Singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        BLImp() { } // default => private
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion

        public void InitializeDisplay(ref ObservableCollection<BO.Bus> buses, ref ObservableCollection<BO.Line> lines, ref ObservableCollection<BO.Station> stations)
        {
            buses = new ObservableCollection<BO.Bus>(GetAllBuses());
            lines = new ObservableCollection<BO.Line>(GetAllLines());
            stations = new ObservableCollection<BO.Station>(GetAllStations());
        }

        #region User
        public IEnumerable<BO.User> GetAllUsers()
        {
            return from user in dl.GetAllUsers()
                   select userDoBoAdapter(user); 
        }
        public BO.User userDoBoAdapter(DO.User userDo)
        {
            BO.User userBo = new BO.User();
            userDo.CopyPropertiesTo(userBo);
            return userBo;
        }
        public void AddUser(BO.User newUser)
        {
            DO.User userDo = new DO.User();
            newUser.CopyPropertiesTo(userDo);
            dl.AddUser(userDo);
        }
        public void UpdateUser(BO.User user)
        {
            DO.User userDo = new DO.User();
            user.CopyPropertiesTo(userDo);
            dl.UpdateUser(userDo);
        }
        public void DeleteUser(BO.User User)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bus
        private BO.Bus busDoBoAdapter(DO.Bus busDo)
        {
            BO.Bus busBo = new BO.Bus();
            busDo.CopyPropertiesTo(busBo);
            return busBo;
        }
        public IEnumerable<BO.Bus> GetAllBuses()
        {
            return from bus in dl.GetAllBuses()
                   select busDoBoAdapter(bus);
        }
        public void AddBus(BO.Bus newBus)
        {
            bool shouldAdd = true;
            // check for validity first
            if ((newBus.LicenceNum.ToString().Length == 7 && newBus.StartDate.Year >= 2018)
                || (newBus.LicenceNum.ToString().Length == 8 && newBus.StartDate.Year <= 2018)
                || (newBus.LicenceNum.ToString().Length != 7 && newBus.LicenceNum.ToString().Length != 8)
                || (newBus.MileageSinceFuel > newBus.Mileage || newBus.MileageSinceMaintenance > newBus.Mileage))
                shouldAdd = false;

            // add bus
            if (shouldAdd)
            {
                DO.Bus newBusDo = new DO.Bus();
                newBus.CopyPropertiesTo(newBusDo);
                try
                {
                    dl.AddBus(newBusDo);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException("That bus already exists!", ex);
                }
            }
            else
                throw new ArgumentException("Provided bus is not valid!");
        }
        public void DeleteBus(BO.Bus bus)
        {
            DO.Bus busDo = new DO.Bus();
            bus.CopyPropertiesTo(busDo);
            try
            {
                dl.DeleteBus(busDo);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("BUS LIST AND DISPLAY LIST ARE NOT SYNCED", ex);    
            }
        }
        public void UpdateBus(BO.Bus bus)
        {
            DO.Bus busDo = new DO.Bus();
            bus.CopyPropertiesTo(busDo);
            bool shouldChange = true;
            // check for validity first
            if ((busDo.LicenceNum.ToString().Length == 7 && busDo.StartDate.Year >= 2018)
                 || (busDo.LicenceNum.ToString().Length == 8 && busDo.StartDate.Year <= 2018)
                 || (busDo.LicenceNum.ToString().Length != 7 && busDo.LicenceNum.ToString().Length != 8)
                 || (busDo.MileageSinceFuel > busDo.Mileage || busDo.MileageSinceMaintenance > busDo.Mileage))
                shouldChange = false;
            if (shouldChange)
            {
                try
                {
                    dl.UpdateBus(busDo);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException("BUS LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
                }
            }
            else
                throw new InvalidOperationException("Invalid change to bus!");
        }
        #endregion

        #region Line
        private BO.Line LineDoBoAdapter(DO.Line lineDo)
        {
            BO.Line lineBo = new BO.Line();
            lineDo.CopyPropertiesTo(lineBo);
            // get the Line's Stations
            lineBo.Stations = (from s in dl.GetAllLineStations()
                                            where s.LineID == lineBo.ID
                                            orderby s.StationPlacement
                                            select LineStationDoBoAdapter(s)).ToList();
            lineBo.LastStation = stationDoBoAdapter(dl.GetStation(lineBo.Stations.Last().StationCode));
            // get the Adjacent stations associated with the line
            lineBo.AdjStats = new List<BO.AdjacentStations>();
            for (int i = 0; i < lineBo.Stations.Count - 1; i++)
                lineBo.AdjStats.Add(AdjacentStationsDoBoAdapter(dl.GetAdjacentStations(lineBo.Stations[i].StationCode, lineBo.Stations[i + 1].StationCode)));
            lineBo.Trip = LineTripDoBoAdapter(dl.GetLineTrip(lineBo.ID));

            return lineBo;
        }
        public IEnumerable<BO.Line> GetAllLines()
        {
            return from line in dl.GetAllLines()
                   select LineDoBoAdapter(line);
        }
        public IEnumerable<BO.Line> GetLinesThatGoThroughStation(int stationCode)
        {
            return from line in dl.GetAllLines() // foreach line
                   let lineBo = LineDoBoAdapter(line) // convert line
                   where lineBo.Stations.FirstOrDefault(x => x.StationCode == stationCode) != null //if line contains station
                   select lineBo;
        }
        public BO.Line GetLine(int ID)
        {
            try
            {
                return LineDoBoAdapter(dl.GetLine(ID));
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("LINE LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
            }
        }
        public void AddLine(BO.Line newLine)
        {
            DO.Line newLineDo = new DO.Line();
            newLineDo.Active = true;
            newLineDo.FirstStationCode = newLine.Stations[0].StationCode;
            newLineDo.LastStationCode = newLine.Stations.Last().StationCode;
            newLineDo.LineNum = newLine.LineNum;
            newLineDo.Region = (DO.Area)newLine.Region;
            dl.AddLine(newLineDo);
            newLine.ID = newLineDo.ID;
            foreach (var ls in newLine.Stations)
                ls.LineID = newLineDo.ID;
            UpdateLine(newLine);
        }
        public void UpdateLine(BO.Line line)
        {
            for (int i = 0; i < line.Stations.Count; i++)
            {
                try
                {
                    if (i == 0)
                        dl.UpdateLineStation(LineStationBoDoAdapter(line.Stations[i], 0, line.Stations[i + 1].StationCode, i + 1));
                    else if (i == line.Stations.Count - 1)
                        dl.UpdateLineStation(LineStationBoDoAdapter(line.Stations[i], line.Stations[i - 1].StationCode, 0, i + 1));
                    else
                        dl.UpdateLineStation(LineStationBoDoAdapter(line.Stations[i], line.Stations[i - 1].StationCode, line.Stations[i + 1].StationCode, i + 1));
                }
                catch (ArgumentException)
                {
                    if (i == 0)
                        dl.AddLineStation(LineStationBoDoAdapter(line.Stations[i], 0, line.Stations[i + 1].StationCode, i + 1));
                    else if (i == line.Stations.Count - 1)
                        dl.AddLineStation(LineStationBoDoAdapter(line.Stations[i], line.Stations[i - 1].StationCode, 0, i + 1));
                    else
                        dl.AddLineStation(LineStationBoDoAdapter(line.Stations[i], line.Stations[i - 1].StationCode, line.Stations[i + 1].StationCode, i + 1));
                }
            }
            foreach (var adjStat in line.AdjStats)
            {
                DO.AdjacentStations adjacentStationsDo = new DO.AdjacentStations {Station1 = adjStat.Station1, Station2 = adjStat.Station2, Time = adjStat.Time };
                try
                {
                    dl.UpdateAdjacentStation(adjacentStationsDo);
                }
                catch (ArgumentException)
                {
                    dl.AddAdjacentStation(adjacentStationsDo);
                }
            }

            dl.UpdateLine(line.ID, (x) => 
            {
                x.Active = true;
                x.FirstStationCode = line.Stations[0].StationCode;
                x.LastStationCode = line.Stations.Last().StationCode;
                x.LineNum = line.LineNum;
                x.Region = (DO.Area)line.Region;
            });
        }
        public void DeleteLine(BO.Line line)
        {
            foreach (var ls in line.Stations)
                dl.DeleteLineStation(LineStationBoDoAdapter(ls, 0, 0, 0)); // dl only checks line id and station code
            dl.DeleteLine(line.ID);
        }
        public void DeleteStationInLine(int lineID, int stationCode)
        {
            BO.Line line = LineDoBoAdapter(dl.GetLine(lineID));
            if (line.Stations.Count > 2)
            {
                // update Line's adjacent stations
                if (line.Stations[0].StationCode == stationCode || line.Stations.Last().StationCode == stationCode) // if station is first or last in line
                    line.AdjStats.RemoveAll(x => (x.Station1 == stationCode || x.Station2 == stationCode));
                else // need to update adjacent stations
                {
                    BO.AdjacentStations oldAdjStatBefore = line.AdjStats.Find(x => x.Station2 == stationCode); // the station before
                    BO.AdjacentStations oldAdjStatAfter = line.AdjStats.Find(x => x.Station1 == stationCode); // the station after
                    BO.AdjacentStations newAdjacentStations =
                        new BO.AdjacentStations
                        {
                            Station1 = oldAdjStatBefore.Station1,
                            Station2 = oldAdjStatAfter.Station2,
                            Time = oldAdjStatBefore.Time + oldAdjStatAfter.Time
                        };
                    //Add the new pair
                    DO.AdjacentStations newAdjStatDo = new DO.AdjacentStations();
                    newAdjacentStations.CopyPropertiesTo(newAdjStatDo);
                    try
                    {
                        dl.AddAdjacentStation(newAdjStatDo);
                    }
                    catch (InvalidOperationException)
                    {
                        dl.UpdateAdjacentStation(newAdjStatDo);
                    }//just means it already exists which is fine, so just update

                    //update line
                    line.AdjStats.RemoveAll(x => (x.Station1 == stationCode || x.Station2 == stationCode));
                    line.AdjStats.Add(newAdjacentStations);
                }
                //update Line's Stations
                line.Stations.RemoveAll(x => x.StationCode == stationCode);
                DO.LineStation delLineStation = new DO.LineStation { LineID = line.ID, StationCode = stationCode };
                dl.DeleteLineStation(delLineStation);
                line.LastStation = stationDoBoAdapter(dl.GetStation(line.Stations.Last().StationCode)); // update last station
                // update line in DL
                UpdateLine(line);
            }
            else
                throw new InvalidOperationException("No line can have less than two stations");
        }
        public void MoveStationDownInLine(int lineID, int stationCode)
        {
            BO.Line line = LineDoBoAdapter(dl.GetLine(lineID));
            BO.LineStation station = LineStationDoBoAdapter(dl.GetLineStation(lineID, stationCode));
            int index = station.StationPlacement - 1;
            
            if(index == 0 && line.Stations.Count > 2)
            {
                BO.LineStation stationAfter = line.Stations[index + 1];
                BO.LineStation stationAfter2 = line.Stations[index + 2];
                // add
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = stationAfter.StationCode, Station2 = station.StationCode, Time = TimeSpan.Zero });
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = station.StationCode, Station2 = stationAfter2.StationCode, Time = TimeSpan.Zero });
                // remove
                line.AdjStats.RemoveAll(x => x.Station1 == station.StationCode && x.Station2 == stationAfter.StationCode);
                line.AdjStats.RemoveAll(x => x.Station1 == stationAfter.StationCode && x.Station2 == stationAfter2.StationCode);
                // update placement
                line.Stations[index].StationPlacement += 1;
                line.Stations[index + 1].StationPlacement -= 1;
                line.Stations = (from s in line.Stations orderby s.StationPlacement select s).ToList();
            }
            else if(index == 0)
            {
                BO.LineStation stationAfter = line.Stations[index + 1];
                // add
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = stationAfter.StationCode, Station2 = station.StationCode, Time = TimeSpan.Zero });
                // remove
                line.AdjStats.RemoveAll(x => x.Station1 == station.StationCode && x.Station2 == stationAfter.StationCode);
                // update placement
                line.Stations[index].StationPlacement += 1;
                line.Stations[index + 1].StationPlacement -= 1;
                line.Stations = (from s in line.Stations orderby s.StationPlacement select s).ToList();
            }
            else if(index == line.Stations.Count - 2)
            {
                BO.LineStation stationBefore = line.Stations[index - 1];
                BO.LineStation stationAfter = line.Stations[index + 1];
                // add
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = stationBefore.StationCode, Station2 = stationAfter.StationCode, Time = TimeSpan.Zero });
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = stationAfter.StationCode, Station2 = station.StationCode, Time = TimeSpan.Zero });
                // remove
                line.AdjStats.RemoveAll(x => x.Station1 == station.StationCode && x.Station2 == stationAfter.StationCode);
                line.AdjStats.RemoveAll(x => x.Station1 == stationBefore.StationCode && x.Station2 == station.StationCode);
                // update placement
                line.Stations[index].StationPlacement += 1;
                line.Stations[index + 1].StationPlacement -= 1;
                line.Stations = (from s in line.Stations orderby s.StationPlacement select s).ToList();
            }
            else if(index == line.Stations.Count - 1)
            {
                //Can't Go Down!
            }
            else
            {
                BO.LineStation stationBefore = line.Stations[index - 1];
                BO.LineStation stationAfter = line.Stations[index + 1];
                BO.LineStation stationAfter2 = line.Stations[index + 2];
                // add
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = stationBefore.StationCode, Station2 = stationAfter.StationCode, Time = TimeSpan.Zero });
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = stationAfter.StationCode, Station2 = station.StationCode, Time = TimeSpan.Zero });
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = station.StationCode, Station2 = stationAfter2.StationCode, Time = TimeSpan.Zero });
                // remove
                line.AdjStats.RemoveAll(x => x.Station1 == station.StationCode && x.Station2 == stationAfter.StationCode);
                line.AdjStats.RemoveAll(x => x.Station1 == stationBefore.StationCode && x.Station2 == station.StationCode);
                line.AdjStats.RemoveAll(x => x.Station1 == stationAfter.StationCode && x.Station2 == stationAfter2.StationCode);
                // update placement
                line.Stations[index].StationPlacement += 1;
                line.Stations[index+1].StationPlacement -= 1;
                line.Stations = (from s in line.Stations orderby s.StationPlacement select s).ToList();
            }
            // update line in DL
            UpdateLine(line);
        }
        public void MoveStationUpInLine(int lineID, int stationCode)
        {
            BO.Line line = LineDoBoAdapter(dl.GetLine(lineID));
            BO.LineStation station = LineStationDoBoAdapter(dl.GetLineStation(lineID, stationCode));
            int index = station.StationPlacement - 1;

            if (index == 0)
            {
                // Can't go up
            }
            else if (index == 1)
            {
                BO.LineStation stationBefore = line.Stations[index - 1];
                BO.LineStation stationAfter = line.Stations[index + 1];
                // add
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = station.StationCode, Station2 = stationBefore.StationCode, Time = TimeSpan.Zero });
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = stationBefore.StationCode, Station2 = stationAfter.StationCode, Time = TimeSpan.Zero });
                // remove
                line.AdjStats.RemoveAll(x => x.Station1 == stationBefore.StationCode && x.Station2 == station.StationCode);
                line.AdjStats.RemoveAll(x => x.Station1 == station.StationCode && x.Station2 == stationAfter.StationCode);
                // update placement
                line.Stations[index].StationPlacement -= 1;
                line.Stations[index - 1].StationPlacement += 1;
                line.Stations = (from s in line.Stations orderby s.StationPlacement select s).ToList();
            }
            else if (index == line.Stations.Count - 1 && line.Stations.Count > 2)
            {
                BO.LineStation stationBefore = line.Stations[index - 1];
                BO.LineStation stationBefore2 = line.Stations[index - 2];
                // add
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = stationBefore2.StationCode, Station2 = station.StationCode, Time = TimeSpan.Zero });
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = station.StationCode, Station2 = stationBefore.StationCode, Time = TimeSpan.Zero });
                // remove
                line.AdjStats.RemoveAll(x => x.Station1 == stationBefore2.StationCode && x.Station2 == stationBefore.StationCode);
                line.AdjStats.RemoveAll(x => x.Station1 == stationBefore.StationCode && x.Station2 == station.StationCode);
                // update placement
                line.Stations[index].StationPlacement -= 1;
                line.Stations[index - 1].StationPlacement += 1;
                line.Stations = (from s in line.Stations orderby s.StationPlacement select s).ToList();
            }
            else if(index == line.Stations.Count - 1)
            {
                BO.LineStation stationBefore = line.Stations[index - 1];
                // add
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = station.StationCode, Station2 = stationBefore.StationCode, Time = TimeSpan.Zero });
                // remove
                line.AdjStats.RemoveAll(x => x.Station1 == stationBefore.StationCode && x.Station2 == station.StationCode);
                // update placement
                line.Stations[index].StationPlacement -= 1;
                line.Stations[index - 1].StationPlacement += 1;
                line.Stations = (from s in line.Stations orderby s.StationPlacement select s).ToList();
            }
            else
            {
                BO.LineStation stationBefore = line.Stations[index - 1];
                BO.LineStation stationAfter = line.Stations[index + 1];
                BO.LineStation stationBefore2 = line.Stations[index - 2];
                // add
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = stationBefore2.StationCode, Station2 = station.StationCode, Time = TimeSpan.Zero });
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = station.StationCode, Station2 = stationBefore.StationCode, Time = TimeSpan.Zero });
                line.AdjStats.Add(new BO.AdjacentStations { Station1 = stationBefore.StationCode, Station2 = stationAfter.StationCode, Time = TimeSpan.Zero });
                // remove
                line.AdjStats.RemoveAll(x => x.Station1 == stationBefore2.StationCode && x.Station2 == stationBefore.StationCode);
                line.AdjStats.RemoveAll(x => x.Station1 == stationBefore.StationCode && x.Station2 == station.StationCode);
                line.AdjStats.RemoveAll(x => x.Station1 == station.StationCode && x.Station2 == stationAfter.StationCode);
                // update placement
                line.Stations[index].StationPlacement -= 1;
                line.Stations[index - 1].StationPlacement += 1;
                line.Stations = (from s in line.Stations orderby s.StationPlacement select s).ToList();
            }
            // update line in DL
            UpdateLine(line);
        }
        public List<TimeSpan> GetLineSchedule(BO.Line line)
        {
            List<TimeSpan> times = new List<TimeSpan>();
            BO.LineTrip trip = LineTripDoBoAdapter(dl.GetLineTrip(line.ID));
            if (trip == null)
                return times;
            if (trip.Frequency == TimeSpan.Zero)
            {
                times.Add(trip.StartTime);
                return times;
            }
            TimeSpan addTime = trip.StartTime;
            while(addTime <= trip.EndTime)
            {
                times.Add(addTime);
                addTime += trip.Frequency;
            }

            return times;
        }
        #endregion

        #region Line Trip
        private BO.LineTrip LineTripDoBoAdapter(DO.LineTrip tripDo)
        {
            if (tripDo != null)
            {
                BO.LineTrip tripBo = new BO.LineTrip();
                tripDo.CopyPropertiesTo(tripBo);
                return tripBo;
            }
            else
                return null;
        }
        public void AddLineTrip(BO.LineTrip newTripBo)
        {
            if (dl.GetLineTrip(newTripBo.LineID) != null)
                dl.DeleteLineTrip(newTripBo.LineID);
            if (newTripBo.StartTime <= newTripBo.EndTime)
            {
                DO.LineTrip newTripDo = new DO.LineTrip();
                newTripBo.CopyPropertiesTo(newTripDo);
                dl.AddLineTrip(newTripDo);
            }
            else
                throw new InvalidOperationException("start time was after end time!");
        }
        #endregion

        #region Station
        private BO.Station stationDoBoAdapter(DO.Station stationDo)
        {
            BO.Station stationBo = new BO.Station();
            stationDo.CopyPropertiesTo(stationBo);
            // get the Station's lines
            stationBo.LineStationsByStation = (from s in dl.GetAllLineStations()
                                                                         where s.StationCode == stationBo.StationCode
                                                                         select LineStationDoBoAdapter(s)).ToList();
            return stationBo;
        }
        public IEnumerable<BO.Station> GetAllStations()
        {
            return from item in dl.GetAllStations()
                   select stationDoBoAdapter(item);
        }
        public BO.Station GetStation(int stationCode)
        {
            try
            {
                return stationDoBoAdapter(dl.GetStation(stationCode));
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("FATAL ERROR!", ex);
            }
        }
        public void AddStation(BO.Station newStation)
        {
            bool shouldAdd = true;
            // check for validity first
            if (newStation.StationCode.ToString().Length < 3 || newStation.StationCode.ToString().Length > 6)
                shouldAdd = false;
            // add Station
            if (shouldAdd)
            {
                DO.Station newStationDo = new DO.Station();
                newStation.CopyPropertiesTo(newStationDo);
                try
                {
                    dl.AddStation(newStationDo);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException("That Station already exists!", ex);
                }
            }
            else
                throw new ArgumentException("Provided Station is not valid!");
        }
        public void UpdateStation(BO.Station station)
        {
            DO.Station stationDo = new DO.Station();
            station.CopyPropertiesTo(stationDo);
            try
            {
                dl.UpdateStation(stationDo);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("STATION LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
            }
        }
        public void DeleteStation(BO.Station station)
        {
            DO.Station stationDo = new DO.Station();
            station.CopyPropertiesTo(stationDo);
            //update all lines first
            foreach (var line in GetLinesThatGoThroughStation(station.StationCode))
                DeleteStationInLine(line.ID, station.StationCode);
            // delete all relevant entities
            try // Adjacent Stations
            {
                foreach (var stationDel in (from s in dl.GetAllAdjacentStations()
                                                               where s.Station1 == station.StationCode || s.Station2 == station.StationCode
                                                               select s))
                {
                    dl.DeleteAdjacentStation(stationDel);
                }
            }
            catch (ArgumentException) { }
            try // Line Stations
            {
                foreach (var stationDel in (from s in dl.GetAllLineStations()
                                                               where s.StationCode == station.StationCode
                                                               select s))
                {
                    dl.DeleteLineStation(stationDel);
                } 
            }
            catch (ArgumentException) { }
            try // Station
            {
                dl.DeleteStation(stationDo);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("STATION LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
            }
        }
        #endregion

        #region LineStation
        private BO.LineStation LineStationDoBoAdapter(DO.LineStation lineStationDo)
        {
            BO.LineStation lineStationBo = new BO.LineStation();
            lineStationDo.CopyPropertiesTo(lineStationBo);
            lineStationBo.StationName = dl.GetStation(lineStationBo.StationCode).StationName;
            if (lineStationDo.StationAfterCode != 0)
                lineStationBo.TimeToNext = dl.GetAdjacentStations(lineStationDo.StationCode, lineStationDo.StationAfterCode).Time;
            else
                lineStationBo.TimeToNext = new TimeSpan(1); // pseudo value for converter

            return lineStationBo;
        }
        private DO.LineStation LineStationBoDoAdapter(BO.LineStation lineStationBo, int prevSt, int nextSt, int statPlacement)
        {
            DO.LineStation lineStationDo = new DO.LineStation();
            lineStationBo.CopyPropertiesTo(lineStationDo);
            lineStationDo.Active = true;
            lineStationDo.StationBeforeCode = prevSt;
            lineStationDo.StationAfterCode = nextSt;
            lineStationDo.StationPlacement = statPlacement;
            return lineStationDo;
        }
        public IEnumerable<BO.LineStation> GetAllLineStations()
        {
            return from s in dl.GetAllLineStations()
                   select LineStationDoBoAdapter(s);
        }
        public void AddLineStation(BO.LineStation newLineStation)
        {
            BO.Line line = LineDoBoAdapter(dl.GetLine(newLineStation.LineID)); // associated line
            // add station to line
            newLineStation.StationPlacement = line.Stations.Count + 1;
            line.Stations.Add(newLineStation);
            // add Adjacent Stations entity
            int statIndex = newLineStation.StationPlacement - 1;
            line.AdjStats.Add(new BO.AdjacentStations { Station1 = line.Stations[statIndex - 1].StationCode, Station2 = newLineStation.StationCode, Time = TimeSpan.Zero });
            // Add Line Station entity in DL
            DO.LineStation lineStationDo = LineStationBoDoAdapter(newLineStation, line.Stations[statIndex - 1].StationCode, 0, newLineStation.StationPlacement);
            dl.AddLineStation(lineStationDo);
            // update Line
            UpdateLine(line);
        }
        #endregion

        #region AdjacentStations
        private BO.AdjacentStations AdjacentStationsDoBoAdapter(DO.AdjacentStations adjStatDo)
        {
            BO.AdjacentStations AdjacentStationsBo = new BO.AdjacentStations();
            adjStatDo.CopyPropertiesTo(AdjacentStationsBo);
            return AdjacentStationsBo;
        }
        public IEnumerable<BO.AdjacentStations> GetAllAdjacentStations()
        {
            return from AdjacentStations in dl.GetAllAdjacentStations()
                   select AdjacentStationsDoBoAdapter(AdjacentStations);
        }
        public void UpdateAdjacentStations(BO.AdjacentStations adjStat)
        {
            DO.AdjacentStations adjStatDo = new DO.AdjacentStations();
            adjStat.CopyPropertiesTo(adjStatDo);
            try
            {
                dl.UpdateAdjacentStation(adjStatDo);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("ADJACENT STATIONS LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
            }
        }
        private void AddAdjacentStations(BO.LineStation s1, BO.LineStation s2, TimeSpan time)
        {
            DO.AdjacentStations adjStatDo = new DO.AdjacentStations { Station1 = s1.StationCode, Station2 = s2.StationCode, Time = time};
            dl.AddAdjacentStation(adjStatDo);
        }
        public void DeleteAdjacentStations(BO.AdjacentStations adjStat)
        {
            DO.AdjacentStations adjStatDo = new DO.AdjacentStations() { Station1 = adjStat.Station1, Station2 = adjStat.Station2 };
            try
            {
                dl.DeleteAdjacentStation(adjStatDo);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Fatal Error!");
            }
        }
        #endregion

        #region Clock
        
        private volatile bool Canceled;
        public void StartSimulation(TimeSpan startTime, int rate, Action<TimeSpan> updateDispClock)
        {
            Canceled = false;
            Stopwatch stopwatch = new Stopwatch();
            Clock clock = Clock.Instance;
            Clock.onTimeChanged += (object sender, BO.TimeChangedEventArgs args) => updateDispClock(args.Time);
            TimeSpan sleepTime = new TimeSpan((1000 / rate) * TimeSpan.TicksPerMillisecond);
            // Run Clock simulation thread
            new Thread(() =>
            {
                stopwatch.Restart();
                while (!Canceled)
                {
                    Thread.Sleep(sleepTime);
                    clock.Time = startTime + new TimeSpan(stopwatch.ElapsedTicks * rate);
                }
                stopwatch.Stop();
            }).Start();
            // Run Line Dispatcher Thread
            //new Thread(() =>
            //{
            //    while(!Canceled)
            //    {

            //    }
            //}).Start();
        }
        public void StopSimulation()
        {
            Canceled = true;
            Clock.RemoveObservers();
        }
        #endregion
    }
}
