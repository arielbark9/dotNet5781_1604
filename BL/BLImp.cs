using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using BO;
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
        public BO.Line LineDoBoAdapter(DO.Line LineDo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Line> GetAllLines()
        {
            throw new NotImplementedException();
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
        #endregion

        #region Station
        public BO.Station stationDoBoAdapter(DO.Station stationDo)
        {
            BO.Station stationBo = new BO.Station();
            stationDo.CopyPropertiesTo(stationBo);
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
            if (newStation.StationCode.ToString().Length != 5 && newStation.StationCode.ToString().Length != 6)
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
        public BO.LineStation LineStationDoBoAdapter(DO.LineStation LineStationDo)
        {
            throw new NotImplementedException();
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
        #endregion
    }
}
