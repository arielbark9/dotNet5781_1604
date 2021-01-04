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
        public static List<LineStation> ListLineStations;
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
                },
            }; 
            ListStations = new List<Station>
            {
                new Station
                {
                    Active = true,
                    StationCode = 2524,
                    StationName = "Masof Eged/Kanfey Nesharim",
                    Latitude = 31.787175,
                    Longitude = 35.181048
                },
                new Station
                {
                    Active = true,
                    StationCode = 276,
                    StationName = "Beit Inbar/Kanfey Nesharim",
                    Latitude = 31.787528,
                    Longitude = 35.184601
                },
                new Station
                {
                    Active = true,
                    StationCode = 277,
                    StationName = "Mercaz Shetner/Kanfey Nesharim",
                    Latitude = 31.78751,
                    Longitude = 35.188178
                },
                new Station
                {
                    Active = true,
                    StationCode = 2538,
                    StationName = "Yemin Avot",
                    Latitude = 31.78751,
                    Longitude = 35.193736
                },
                new Station
                {
                    Active = true,
                    StationCode = 2944,
                    StationName = "Tzvi Yehuda/Hameiri",
                    Latitude = 31.787508,
                    Longitude = 35.19541
                },
                new Station
                {
                    Active = true,
                    StationCode = 2539,
                    StationName = "Merkaz Harav/Tzvi Yehuda",
                    Latitude = 31.787352,
                    Longitude = 35.1969
                },
                new Station
                {
                    Active = true,
                    StationCode = 4216,
                    StationName = "Gesher Hameytarim/Herzel",
                    Latitude = 31.787171,
                    Longitude = 35.199532
                },
                new Station
                {
                    Active = true,
                    StationCode = 4159,
                    StationName = "Mercazit Jerusalem (B1-B2)",
                    Latitude = 31.788603,
                    Longitude = 35.202593
                },
                new Station
                {
                    Active = true,
                    StationCode = 800,
                    StationName = "Binyaney Hauma/Hanasi Hashishi",
                    Latitude = 31.78634,
                    Longitude = 35.203701
                },
                new Station
                {
                    Active = true,
                    StationCode = 3375,
                    StationName = "Haleom/Hanasi Hashishi",
                    Latitude = 31.783139,
                    Longitude = 35.203037
                },
                new Station
                {
                    Active = true,
                    StationCode = 221,
                    StationName = "Misrad Hahutz/Rabin",
                    Latitude = 31.782889,
                    Longitude = 35.202207
                },
                new Station
                {
                    Active = true,
                    StationCode = 2531,
                    StationName = "Shachal/Heler",
                    Latitude = 31.765953,
                    Longitude = 35.194083
                },
                new Station
                {
                    Active = true,
                    StationCode = 973,
                    StationName = "Mercaz Mischari/Shachal",
                    Latitude = 31.764457,
                    Longitude = 35.194399
                },
                new Station
                {
                    Active = true,
                    StationCode = 975,
                    StationName = "Shachal Alef",
                    Latitude = 31.763277,
                    Longitude = 35.19555
                },
                new Station
                {
                    Active = true,
                    StationCode = 6094,
                    StationName = "Shachal/Harav Gold",
                    Latitude = 31.761532,
                    Longitude = 35.196755
                },
                new Station
                {
                    Active = true,
                    StationCode = 950,
                    StationName = "Shachal Bet",
                    Latitude = 31.760286,
                    Longitude = 35.197129
                },
                new Station
                {
                    Active = true,
                    StationCode = 1512,
                    StationName = "Harav Hertzog/Shachal",
                    Latitude = 31.761109,
                    Longitude = 35.199936
                },
                new Station
                {
                    Active = true,
                    StationCode = 1539,
                    StationName = "Pat/Golomov",
                    Latitude = 31.757465,
                    Longitude = 35.197119
                },
                new Station
                {
                    Active = true,
                    StationCode = 2398,
                    StationName = "Golomov/San Martin",
                    Latitude = 31.756807,
                    Longitude = 35.194807
                },
                new Station
                {
                    Active = true,
                    StationCode = 222,
                    StationName = "Ted/A.S. Beitar",
                    Latitude = 31.751345,
                    Longitude = 35.188474
                },
                new Station
                {
                    Active = true,
                    StationCode = 3093,
                    StationName = "Masof Eged/Benbenisti",
                    Latitude = 31.748797,
                    Longitude = 35.190945
                }
            };
            ListLineStations = new List<LineStation>
            {
                new LineStation
                {
                    Active = true,
                    StationCode = 2524,
                    LineNum = 6,
                    StationPlacement = 0
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 276,
                    LineNum = 6,
                    StationPlacement = 1
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 277,
                    LineNum = 6,
                    StationPlacement = 2
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2538,
                    LineNum = 6,
                    StationPlacement = 3
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2944,
                    LineNum = 6,
                    StationPlacement = 4
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2539,
                    LineNum = 6,
                    StationPlacement = 5
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 4216,
                    LineNum = 6,
                    StationPlacement = 6
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 4159,
                    LineNum = 6,
                    StationPlacement = 7
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 800,
                    LineNum = 6,
                    StationPlacement = 8
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 3375,
                    LineNum = 6,
                    StationPlacement = 9
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 221,
                    LineNum = 6,
                    StationPlacement = 10
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2531,
                    LineNum = 6,
                    StationPlacement = 11
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 973,
                    LineNum = 6,
                    StationPlacement = 12
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 975,
                    LineNum = 6,
                    StationPlacement = 13
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 6094,
                    LineNum = 6,
                    StationPlacement = 14
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 950,
                    LineNum = 6,
                    StationPlacement = 15
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 1512,
                    LineNum = 6,
                    StationPlacement = 16
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 1539,
                    LineNum = 6,
                    StationPlacement = 17
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2398,
                    LineNum = 6,
                    StationPlacement = 18
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 222,
                    LineNum = 6,
                    StationPlacement = 19
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 3093,
                    LineNum = 6,
                    StationPlacement = 20
                }
            };
        }
    }
}
