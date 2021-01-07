using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;

namespace DL
{
    sealed class DalObject : IDL
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }  // static ctor to ensure instance init is done just before first usage
        private DalObject() { }
        public static DalObject Instance { get => instance; }// The public Instance property to use
        #endregion

        #region User
        public IEnumerable<User> GetAllUsers()
        {
            // Filter out non-active entities
            return from item in DataSource.ListUsers
                   where item.Active == true
                   select item.Clone();
        }
        public void AddUser(User newUser)
        {
            newUser.Active = true;
            if (DataSource.ListUsers.FirstOrDefault(x => x.UserName == newUser.UserName) == null)
                DataSource.ListUsers.Add(newUser.Clone());
            else
                throw new InvalidOperationException("ERROR! that username already exists!");
        }
        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bus
        public IEnumerable<Bus> GetAllBuses()
        {
            // Filter out non-active entities
            return from item in DataSource.ListBuses
                   where item.Active == true
                   select item.Clone();
        }
        public void AddBus(Bus newBus)
        {
            DO.Bus oldBus = DataSource.ListBuses.FirstOrDefault(x => x.LicenceNum == newBus.LicenceNum);
            newBus.Active = true;
            if (oldBus == null)
                DataSource.ListBuses.Add(newBus.Clone());
            else if (oldBus.Active == false)
            {
                oldBus.Active = true;
                newBus.CopyPropertiesTo(oldBus);
            }
            else
                throw new InvalidOperationException("ERROR! that bus already exists!");
        }
        public void DeleteBus(Bus bus)
        {
            if(DataSource.ListBuses.FirstOrDefault(b => b.LicenceNum == bus.LicenceNum) != null)
                DataSource.ListBuses.FirstOrDefault(b => b.LicenceNum == bus.LicenceNum).Active = false;
            else
                throw new ArgumentException("Invalid Bus Licence Number");
        }
        public void UpdateBus(Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(b => b.LicenceNum == bus.LicenceNum) != null)
            {
                DO.Bus busToUpdate = DataSource.ListBuses.FirstOrDefault(b => b.LicenceNum == bus.LicenceNum);
                bus.CopyPropertiesTo(busToUpdate);
            }
            else
                throw new ArgumentException("Invalid Bus Licence Number");
        }
        #endregion

        #region Line
        public void AddLine(Line newLine)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Line> GetAllLines()
        {
            // Filter out non-active entities
            return from item in DataSource.ListLines
                   where item.Active == true
                   select item.Clone();
        }
        public Line GetLine(int lineID)
        {
            if (DataSource.ListLines.Where(x => x.Active == true).FirstOrDefault(x => x.ID == lineID) != null)
                return DataSource.ListLines.Where(x => x.Active == true).FirstOrDefault(x => x.ID == lineID);
            else
                throw new ArgumentException("Invalid line ID");
        }
        public void UpdateLine(Line Line)
        {
            throw new NotImplementedException();
        }
        public void DeleteLine(Line Line)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Station
        public void AddStation(Station newStation)
        {
            newStation.Active = true;
            if (DataSource.ListStations.FirstOrDefault(x => x.StationCode == newStation.StationCode) == null)
                DataSource.ListStations.Add(newStation.Clone());
            else
                throw new InvalidOperationException("ERROR! that Station already exists!");
        }
        public IEnumerable<Station> GetAllStations()
        {
            // Filter out non-active entities
            return from item in DataSource.ListStations
                   where item.Active == true
                   select item.Clone();
        }
        public Station GetStation(int stationCode)
        {
            if (DataSource.ListStations.Where(x => x.Active == true).FirstOrDefault(x => x.StationCode == stationCode) != null)
                return DataSource.ListStations.Where(x => x.Active == true).FirstOrDefault(x => x.StationCode == stationCode);
            else
                throw new ArgumentException("Invalid Station Code");
        }
        public void UpdateStation(Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(x => x.StationCode == station.StationCode) != null)
            {
                DO.Station StationToUpdate = DataSource.ListStations.FirstOrDefault(x => x.StationCode == station.StationCode);
                station.CopyPropertiesTo(StationToUpdate);
            }
            else
                throw new ArgumentException("Invalid Station Code");
        }
        public void DeleteStation(Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(x => x.StationCode == station.StationCode) != null)
            {
                DataSource.ListStations.FirstOrDefault(x => x.StationCode == station.StationCode).Active = false;
                // deactivate entities using this station
                if(DataSource.ListLineStations.FirstOrDefault(x => x.StationCode == station.StationCode) != null)
                    DataSource.ListLineStations.FirstOrDefault(x => x.StationCode == station.StationCode).Active = false;

                if (DataSource.ListAdjacentStations.FirstOrDefault(x => x.Station1 == station.StationCode || x.Station2 == station.StationCode) != null)
                    foreach (var adjStat in DataSource.ListAdjacentStations.ToList().FindAll(x => x.Station1 == station.StationCode || x.Station2 == station.StationCode))
                    {
                        adjStat.Active = false;
                    }
            }
            else
                throw new ArgumentException("Invalid Station Code");
        }
        #endregion

        #region LineStation
        public void AddLineStation(LineStation newLineStation)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<LineStation> GetAllLineStations()
        {
            // Filter out non-active entities
            return from item in DataSource.ListLineStations
                   where item.Active == true
                   select item.Clone();
        }
        public LineStation GetLineStation(int lineID, int stationCode)
        {
            if (DataSource.ListLineStations.Where(x => x.Active == true).FirstOrDefault(x => (x.StationCode == stationCode && x.LineID == lineID)) != null)
                return DataSource.ListLineStations.Where(x => x.Active == true).FirstOrDefault(x => x.StationCode == stationCode && x.LineID == lineID);
            else
                throw new ArgumentException("Invalid Station and Line Code");
        }
        public void UpdateLineStation(LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(x => (x.StationCode == lineStation.StationCode && x.LineID == lineStation.LineID)) != null)
            {
                DO.LineStation LineStationToUpdate = DataSource.ListLineStations.FirstOrDefault(x => (x.StationCode == lineStation.StationCode && x.LineID == lineStation.LineID));
                lineStation.CopyPropertiesTo(LineStationToUpdate);
            }
            else
                throw new ArgumentException("Invalid Station Code");
        }
        public void DeleteLineStation(LineStation LineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(x => x.StationCode == LineStation.StationCode) != null)
            {
                DataSource.ListLineStations.FirstOrDefault(x => x.StationCode == LineStation.StationCode).Active = false;
                // deactivate entities using this LineStation
                if (DataSource.ListAdjacentStations.FirstOrDefault(x => x.Station1 == LineStation.StationCode || x.Station2 == LineStation.StationCode) != null)
                    foreach (var adjStat in DataSource.ListAdjacentStations.ToList().FindAll(x => x.Station1 == LineStation.StationCode || x.Station2 == LineStation.StationCode))
                    {
                        adjStat.Active = false;
                    }
            }
            else
                throw new ArgumentException("Invalid Station Code");
        }
        #endregion

        #region AdjacentStations
        public void AddAdjacentStation(AdjacentStations newAdjStat)
        {
            DO.AdjacentStations oldAdjStat = DataSource.ListAdjacentStations.FirstOrDefault(x => x.Station1 == newAdjStat.Station1 && x.Station2 == newAdjStat.Station2);
            newAdjStat.Active = true;
            if (oldAdjStat == null)
                DataSource.ListAdjacentStations.Add(newAdjStat.Clone());
            else if (oldAdjStat.Active == false)
            {
                oldAdjStat.Active = true;
                newAdjStat.CopyPropertiesTo(oldAdjStat);
            }
            else
                throw new InvalidOperationException("ERROR! that pair of stations already exists!");
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            // Filter out non-active entities
            return from item in DataSource.ListAdjacentStations
                   where item.Active == true
                   select item.Clone();
        }
        public AdjacentStations GetAdjacentStations(int stationCode1, int stationCode2)
        {
            if (DataSource.ListAdjacentStations.Where(x => x.Active == true).FirstOrDefault(x => (x.Station1 == stationCode1 && x.Station2 == stationCode2)) != null)
                return DataSource.ListAdjacentStations.Where(x => x.Active == true).FirstOrDefault(x => (x.Station1 == stationCode1 && x.Station2 == stationCode2));
            else
                throw new ArgumentException("Invalid Adjacent Station Pair");
        }
        public void UpdateAdjacentStation(AdjacentStations adjStat)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(x => x.Station1 == adjStat.Station1 && x.Station2 == adjStat.Station2) != null)
            {
                DO.AdjacentStations AdjStatToUpdate = DataSource.ListAdjacentStations.FirstOrDefault(x => x.Station1 == adjStat.Station1 && x.Station2 == adjStat.Station2);
                adjStat.CopyPropertiesTo(AdjStatToUpdate);
            }
            else
                throw new ArgumentException("Invalid AdjacentStations Entity");
        }
        public void DeleteAdjacentStation(AdjacentStations adjStat)
        {
            // only BL deletes Adjacent stations
            if (DataSource.ListAdjacentStations.FirstOrDefault(x => x.Station1 == adjStat.Station1 && x.Station2 == adjStat.Station2) != null)
                DataSource.ListAdjacentStations.FirstOrDefault(x => x.Station1 == adjStat.Station1 && x.Station2 == adjStat.Station2).Active = false;
            else
                throw new ArgumentException("Invalid AdjacentStations Entity");
        }
        public void DeleteAdjacentStationsAssociated(int stationCode)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(x => x.Station1 == stationCode || x.Station2 == stationCode) != null)
                DataSource.ListAdjacentStations.FirstOrDefault(x => x.Station1 == stationCode || x.Station2 == stationCode).Active = false;
            else
                throw new ArgumentException("Invalid AdjacentStations Code");
        }
        #endregion

    }
}
