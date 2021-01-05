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
        public static List<Line> ListLines;
        public static List<AdjacentStations> ListAdjacentStations;
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
                    StationPlacement = 0,
                    StationBefore = 0,
                    StationAfter = 276
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 276,
                    LineNum = 6,
                    StationPlacement = 1,
                    StationBefore = 2524,
                    StationAfter = 277
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 277,
                    LineNum = 6,
                    StationPlacement = 2,
                    StationBefore = 276,
                    StationAfter = 2538
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2538,
                    LineNum = 6,
                    StationPlacement = 3,
                    StationBefore = 277,
                    StationAfter = 2944
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2944,
                    LineNum = 6,
                    StationPlacement = 4,
                    StationBefore = 2538,
                    StationAfter = 2539
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2539,
                    LineNum = 6,
                    StationPlacement = 5,
                    StationBefore = 2944,
                    StationAfter = 4216
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 4216,
                    LineNum = 6,
                    StationPlacement = 6,
                    StationBefore = 2539,
                    StationAfter = 4159
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 4159,
                    LineNum = 6,
                    StationPlacement = 7,
                    StationBefore = 4216,
                    StationAfter = 800
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 800,
                    LineNum = 6,
                    StationPlacement = 8,
                    StationBefore = 4159,
                    StationAfter = 3375
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 3375,
                    LineNum = 6,
                    StationPlacement = 9,
                    StationBefore = 800,
                    StationAfter = 221
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 221,
                    LineNum = 6,
                    StationPlacement = 10,
                    StationBefore = 3375,
                    StationAfter = 2531
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2531,
                    LineNum = 6,
                    StationPlacement = 11,
                    StationBefore = 221,
                    StationAfter = 973
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 973,
                    LineNum = 6,
                    StationPlacement = 12,
                    StationBefore = 2531,
                    StationAfter = 975
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 975,
                    LineNum = 6,
                    StationPlacement = 13,
                    StationBefore = 973,
                    StationAfter = 6094
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 6094,
                    LineNum = 6,
                    StationPlacement = 14,
                    StationBefore = 975,
                    StationAfter = 950
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 950,
                    LineNum = 6,
                    StationPlacement = 15,
                    StationBefore = 6094,
                    StationAfter = 1512
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 1512,
                    LineNum = 6,
                    StationPlacement = 16,
                    StationBefore = 950,
                    StationAfter = 1539
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 1539,
                    LineNum = 6,
                    StationPlacement = 17,
                    StationBefore = 1512,
                    StationAfter = 2398
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2398,
                    LineNum = 6,
                    StationPlacement = 18,
                    StationBefore = 1539,
                    StationAfter = 222
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 222,
                    LineNum = 6,
                    StationPlacement = 19,
                    StationBefore = 2398,
                    StationAfter = 3093
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 3093,
                    LineNum = 6,
                    StationPlacement = 20,
                    StationBefore = 222,
                    StationAfter = 0
                }
            };
            ListLines = new List<Line>
            {
                new Line
                {
                    Active = true,
                    LineNum = 6,
                    Region = Area.Jerusalem,
                    FirstStation = 2524,
                    LastStation = 3093
                }
            };
            ListAdjacentStations = new List<AdjacentStations>
            {
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 2524,
                    Station2 = 276,
                    Distance = 0.1,
                    Time = new TimeSpan(0, 1, 20)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 276,
                    Station2 = 277,
                    Distance = 0.1,
                    Time = new TimeSpan(0, 1, 20)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 277,
                    Station2 = 2538,
                    Distance = 0.2,
                    Time = new TimeSpan(0, 2, 40)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 2538,
                    Station2 = 2944,
                    Distance =  0.1,
                    Time = new TimeSpan(0, 1, 20)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 2944,
                    Station2 = 2539,
                    Distance = 0.05,
                    Time = new TimeSpan(0, 0, 40)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 2539,
                    Station2 = 4216,
                    Distance = 0.075,
                    Time = new TimeSpan(0, 1, 0)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 4216,
                    Station2 = 4159,
                    Distance = 0.15,
                    Time = new TimeSpan(0, 1, 40)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 4159,
                    Station2 = 800,
                    Distance = 0.17,
                    Time = new TimeSpan(0, 3, 0)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 800,
                    Station2 = 3375,
                    Distance = 0.1,
                    Time = new TimeSpan(0, 1, 0)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 3375,
                    Station2 = 221,
                    Distance = 0.07,
                    Time = new TimeSpan(0, 1, 0)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 221,
                    Station2 = 2531,
                    Distance = 1.2,
                    Time = new TimeSpan(0, 7, 0)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 2531,
                    Station2 = 973,
                    Distance = 0.07,
                    Time = new TimeSpan(0, 0, 50)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 973,
                    Station2 = 975,
                    Distance = 0.1,
                    Time = new TimeSpan(0, 1, 0)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 975,
                    Station2 = 6094,
                    Distance = 0.1,
                    Time = new TimeSpan(0, 1, 10)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 6094,
                    Station2 = 950,
                    Distance = 0.1,
                    Time = new TimeSpan(0, 1, 0)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 950,
                    Station2 = 1512,
                    Distance = 0.2,
                    Time = new TimeSpan(0, 1, 50)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 1512,
                    Station2 = 1539,
                    Distance = 0.7,
                    Time = new TimeSpan(0, 2, 30)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 1539,
                    Station2 = 2398,
                    Distance = 0.25,
                    Time = new TimeSpan(0, 2, 0)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 2398,
                    Station2 = 222,
                    Distance = 1.1,
                    Time = new TimeSpan(0, 5, 0)
                },
                new AdjacentStations
                {
                    Active = true,
                    Station1 = 222,
                    Station2 = 3093,
                    Distance = 0.3,
                    Time = new TimeSpan(0, 2, 0)
                }
            };
        }
    }
}
