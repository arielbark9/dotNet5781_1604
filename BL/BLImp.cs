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
        public BO.Bus busDoBoAdapter(DO.Bus busDo)
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
            if (newBus.LicenceNum.ToString().Length == 7 && newBus.StartDate.Year >= 2018)
                shouldAdd = false;
            else if (newBus.LicenceNum.ToString().Length == 8 && newBus.StartDate.Year <= 2018)
                shouldAdd = false;
            if (newBus.LicenceNum.ToString().Length != 7 && newBus.LicenceNum.ToString().Length != 8)
                shouldAdd = false;
            if (newBus.MileageSinceFuel > newBus.Mileage || newBus.MileageSinceMaintenance > newBus.Mileage)
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
            if (bus.LicenceNum.ToString().Length == 7 && bus.StartDate.Year >= 2018)
                shouldChange = false;
            else if (bus.LicenceNum.ToString().Length == 8 && bus.StartDate.Year <= 2018)
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
                throw new InvalidOperationException("Error! invalid change to bus start of operation year!");
        }
        #endregion

        #region Line
        public BO.Line LineDoBoAdapter(DO.Line lineDo)
        {
            BO.Line lineBo = new BO.Line();
            lineDo.CopyPropertiesTo(lineBo);
            // get the Line's Stations
            lineBo.Stations = (from s in dl.GetAllLineStations()
                                            where s.LineID == lineBo.ID
                                            select LineStationDoBoAdapter(s)).ToList();
            lineBo.LastStation = stationDoBoAdapter(dl.GetStation(lineBo.Stations.Last().StationCode));
            // get the Adjacent stations associated with the line
            lineBo.AdjStats = new List<BO.AdjacentStations>();
            for (int i = 0; i < lineBo.Stations.Count - 1; i++)
                lineBo.AdjStats.Add(AdjacentStationsDoBoAdapter(dl.GetAdjacentStations(lineBo.Stations[i].StationCode, lineBo.Stations[i + 1].StationCode)));

            return lineBo;
        }
        public IEnumerable<BO.Line> GetAllLines()
        {
            return from line in dl.GetAllLines()
                   select LineDoBoAdapter(line);
        }
        public IEnumerable<BO.Line> GetLinesThatGoThroughStation(int stationCode)
        {
            BO.Station s =  stationDoBoAdapter(dl.GetStation(stationCode));
            return from line in dl.GetAllLines() // foreach line
                   let lineBo = LineDoBoAdapter(line) // convert line
                   where lineBo.Stations.FirstOrDefault(x => x.StationCode == stationCode) != null //if line contains station
                   select lineBo;
        }
        public void AddLine(BO.Line newLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(BO.Line Line)
        {
            throw new NotImplementedException();
        }

        public void DeleteLine(BO.Line Line)
        {
            throw new NotImplementedException();
        }
        public void DeleteStationInLine(BO.Line line, int stationCode)
        {
            // update adjacent stations
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
                dl.AddAdjacentStation(newAdjStatDo);
                //update line
                line.AdjStats.RemoveAll(x => (x.Station1 == stationCode || x.Station2 == stationCode));
                line.AdjStats.Add(newAdjacentStations);
            }

            line.Stations.RemoveAll(x => x.StationCode == stationCode);
            line.LastStation = stationDoBoAdapter(dl.GetStation(line.Stations.Last().StationCode)); // update last station
        }

        #endregion

        #region Station
        public BO.Station stationDoBoAdapter(DO.Station stationDo)
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
            {
                DeleteStationInLine(line, station.StationCode);
            }
            try
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
        public BO.LineStation LineStationDoBoAdapter(DO.LineStation lineStationDo)
        {
            BO.LineStation lineStationBo = new BO.LineStation();
            lineStationDo.CopyPropertiesTo(lineStationBo);
            return lineStationBo;
        }
        public IEnumerable<BO.LineStation> GetAllLineStations()
        {
            throw new NotImplementedException();
        }

        public void AddLineStation(BO.LineStation newLineStation)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineStation(BO.LineStation LineStation)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineStation(BO.LineStation LineStation)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region AdjacentStations
        public BO.AdjacentStations AdjacentStationsDoBoAdapter(DO.AdjacentStations adjStatDo)
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
        private void DeleteAllAdjacentStationsAssociated(int stationCode)
        {
            dl.DeleteAdjacentStationsAssociated(stationCode);
        }
        #endregion
    }
}
