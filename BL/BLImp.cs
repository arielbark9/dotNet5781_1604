using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using DLAPI;

namespace BL
{
    class BLImp : IBL // internal, use only through interface BLApi.
    {
        private IDL dl = DLFactory.GetDL();

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
            throw new NotImplementedException();
        }

        public void UpdateUser(BO.User User)
        {
            throw new NotImplementedException();
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
                    throw new InvalidOperationException("Error! that bus already exists!", ex);
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
                throw new ArgumentException("FATAL ERROR! BUS LIST AND DISPLAY LIST ARE NOT SYNCED", ex);    
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
                    throw new ArgumentException("FATAL ERROR! BUS LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
                }
            }
            else
                throw new InvalidOperationException("Error! invalid change to bus!");
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

            return lineBo;
        }
        private DO.Line LineBoDoAdapter(BO.Line lineBo)
        {
            DO.Line lineDo = new DO.Line();
            lineBo.CopyPropertiesTo(lineDo);
            lineDo.FirstStationCode = lineBo.Stations[0].StationCode;
            lineDo.LastStationCode = lineBo.Stations.Last().StationCode;
            lineDo.Active = true;
            return lineDo;
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
                throw new ArgumentException("FATAL ERROR! LINE LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
            }
        }
        public void AddLine(BO.Line newLine)
        {
            throw new NotImplementedException();
        }
        public void UpdateLine(BO.Line line)
        {
            DO.Line lineDo = LineBoDoAdapter(line);
            for (int i = 0; i < line.Stations.Count; i++)
            {
                if (i == 0)
                    dl.UpdateLineStation(LineStationBoDoAdapter(line.Stations[i], 0, line.Stations[i + 1].StationCode, i + 1));
                else if (i == line.Stations.Count - 1)
                    dl.UpdateLineStation(LineStationBoDoAdapter(line.Stations[i], line.Stations[i - 1].StationCode, 0, i + 1));
                else
                    dl.UpdateLineStation(LineStationBoDoAdapter(line.Stations[i], line.Stations[i - 1].StationCode, line.Stations[i + 1].StationCode, i + 1));
            }
            //foreach(var adjStat in line.AdjStats)
            //{
            //    DO.AdjacentStations adjacentStationsDo = new DO.AdjacentStations { Active = true, Station1 = adjStat.Station1, Station2 = adjStat.Station2, Time = adjStat.Time };
            //    dl.UpdateAdjacentStation(adjacentStationsDo);
            //}
            
            dl.UpdateLine(lineDo);
        }
        public void DeleteLine(BO.Line Line)
        {
            throw new NotImplementedException();
        }
        public void DeleteStationInLine(int lineID, int stationCode)
        {
            BO.Line line = LineDoBoAdapter(dl.GetLine(lineID));
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
                    throw new InvalidOperationException("Error! that Station already exists!", ex);
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
                throw new ArgumentException("FATAL ERROR! STATION LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
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
                throw new ArgumentException("FATAL ERROR! STATION LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
            }
        }
        #endregion

        #region LineStation
        private BO.LineStation LineStationDoBoAdapter(DO.LineStation lineStationDo)
        {
            BO.LineStation lineStationBo = new BO.LineStation();
            lineStationDo.CopyPropertiesTo(lineStationBo);
            return lineStationBo;
        }
        private DO.LineStation LineStationBoDoAdapter(BO.LineStation lineStationBo, int prevSt, int nextSt, int placement)
        {
            DO.LineStation lineStationDo = new DO.LineStation();
            lineStationBo.CopyPropertiesTo(lineStationDo);
            lineStationDo.Active = true;
            lineStationDo.StationBeforeCode = prevSt;
            lineStationDo.StationAfterCode = nextSt;
            lineStationDo.StationPlacement = placement;
            return lineStationDo;
        }
        public IEnumerable<BO.LineStation> GetAllLineStations()
        {
            return from s in dl.GetAllLineStations()
                   select LineStationDoBoAdapter(s);
        }
        public void AddLineStation(BO.LineStation newLineStation)
        {
            BO.Line line = LineDoBoAdapter(dl.GetLine(newLineStation.LineID));
            int statIndex = line.Stations.FindIndex(x => x.StationCode == newLineStation.StationCode);
            DO.LineStation lineStationDo = LineStationBoDoAdapter(newLineStation, line.Stations[statIndex - 1].StationCode, line.Stations[statIndex + 1].StationCode, statIndex+1);
            dl.AddLineStation(lineStationDo);
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
                throw new ArgumentException("FATAL ERROR! ADJACENT STATIONS LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
            }
        }
        private void AddAdjacentStations(BO.LineStation s1, BO.LineStation s2, TimeSpan time)
        {
            DO.AdjacentStations adjStatDo = new DO.AdjacentStations { Active = true, Station1 = s1.StationCode, Station2 = s2.StationCode, Time = time};
            dl.AddAdjacentStation(adjStatDo);
        }
        #endregion
    }
}
