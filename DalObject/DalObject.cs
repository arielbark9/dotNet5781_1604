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
                   select item;
        }
        public void AddUser(User newUser)
        {
            newUser.Active = true;
            if (DataSource.ListUsers.FirstOrDefault(x => x.UserName == newUser.UserName) == null)
                DataSource.ListUsers.Add(newUser.Clone());
            else
                throw new InvalidOperationException("ERROR! that username already exists!");
        }
        public void UpdateUser(int userID, Action<User> update)
        {
            throw new NotImplementedException();
        }
        public void DeleteUser(int userID)
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
            bus.Active = true;
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
            DO.Line oldLine = DataSource.ListLines.FirstOrDefault(x => x.ID == newLine.ID);
            newLine.Active = true;
            if (oldLine == null)
                DataSource.ListLines.Add(newLine);
            else if (oldLine.Active == false)
            {
                oldLine.Active = true;
                newLine.CopyPropertiesTo(oldLine);
            }
            else
                throw new InvalidOperationException("ERROR! that Line already exists!");
        }
        public IEnumerable<Line> GetAllLines()
        {
            // Filter out non-active entities
            return from item in DataSource.ListLines
                   where item.Active == true
                   select item;
        }
        public Line GetLine(int lineID)
        {
            if (DataSource.ListLines.Where(x => x.Active == true).FirstOrDefault(x => x.ID == lineID) != null)
                return DataSource.ListLines.Where(x => x.Active == true).FirstOrDefault(x => x.ID == lineID);
            else
                throw new ArgumentException("Invalid line ID");
        }
        public void UpdateLine(int lineID, Action<Line> update)
        {
            if (DataSource.ListLines.Where(x => x.Active).FirstOrDefault(x => x.ID == lineID) != null)
            {
                DO.Line lineToUpdate = DataSource.ListLines.FirstOrDefault(x => x.ID == lineID);
                update(lineToUpdate);
            }
            else
                throw new ArgumentException("Invalid Line ID Number");
        }
        public void DeleteLine(int lineID)
        {
            if (DataSource.ListLines.FirstOrDefault(x => x.ID == lineID) != null)
                DataSource.ListLines.FirstOrDefault(x => x.ID == lineID).Active = false;
            else
                throw new ArgumentException("Invalid Line");
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
            station.Active = true;
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
            }
            else
                throw new ArgumentException("Invalid Station Code");
        }
        #endregion

        #region LineStation
        public void AddLineStation(LineStation newLineStation)
        {
            DO.LineStation oldLineStation = DataSource.ListLineStations.FirstOrDefault(x => (x.LineID == newLineStation.LineID && x.StationCode == newLineStation.StationCode));
            newLineStation.Active = true;
            if (oldLineStation == null)
                DataSource.ListLineStations.Add(newLineStation.Clone());
            else if (oldLineStation.Active == false)
            {
                oldLineStation.Active = true;
                newLineStation.CopyPropertiesTo(oldLineStation);
            }
            else
                throw new InvalidOperationException("ERROR! that LineStation already exists!");
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
            lineStation.Active = true;
            if (DataSource.ListLineStations.FirstOrDefault(x => (x.StationCode == lineStation.StationCode && x.LineID == lineStation.LineID)) != null)
            {
                DO.LineStation LineStationToUpdate = DataSource.ListLineStations.FirstOrDefault(x => (x.StationCode == lineStation.StationCode && x.LineID == lineStation.LineID));
                lineStation.CopyPropertiesTo(LineStationToUpdate);
            }
            else
                throw new ArgumentException("Invalid Station Code");
        }
        public void DeleteLineStation(LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(x => (x.StationCode == lineStation.StationCode && x.LineID == lineStation.LineID)) != null)
            {
                DataSource.ListLineStations.FirstOrDefault(x => (x.StationCode == lineStation.StationCode && x.LineID == lineStation.LineID)).Active = false;
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
            adjStat.Active = true;
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
        #endregion

    }
}
