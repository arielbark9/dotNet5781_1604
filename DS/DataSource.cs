using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public static class DataSource
    {
        #region Declerations
        public static List<User> ListUsers;
        public static List<Bus> ListBuses;
        public static List<Station> ListStations;
        #endregion
        static DataSource()
        {
            InitAllLists();
        }
        private static void InitAllLists()
        {
            ListUsers = new List<User>
            {
                new User
                {
                    UserName = "user",
                    Password = "user",
                    Admin = false,
                    Active = true
                },
                new User
                {
                    UserName = "admin",
                    Password = "admin",
                    Admin = true,
                    Active = true
                }
            };
            ListBuses = new List<Bus>
            {
                new Bus
                {
                    Active = true,
                    LicenceNum = 22399223,
                    StartDate = DateTime.Parse("12.23.2019"),
                    BusStatus = Status.READY,
                    Mileage = 2400,
                    MileageSinceFuel = 0,
                    MileageSinceMaintenance = 2400,
                    DateSinceMaintenance = DateTime.Parse("12.23.2019")
                }
            }; 

            ListStations = new List<Station>
            {
                new Station
                {
                    Active = true,
                    StationCode = 38833,
                    StationName = "Hanachshol/Hadayagim",
                    Latitude = 31.984553,
                    Longitude = 34.782828
                }
            };
        }
    }
}
