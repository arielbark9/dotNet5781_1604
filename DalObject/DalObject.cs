using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
//using DO;
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
                newBus.CopyPropertiesTo(newBus);
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
            throw new NotImplementedException();
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
                DataSource.ListStations.FirstOrDefault(x => x.StationCode == station.StationCode).Active = false;
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
            throw new NotImplementedException();
        }

        public void UpdateLineStation(LineStation LineStation)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineStation(LineStation LineStation)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
