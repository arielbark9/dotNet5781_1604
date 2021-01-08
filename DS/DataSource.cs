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
                    LineID = 0,
                    StationPlacement = 0,
                    StationBeforeCode = 0,
                    StationAfterCode = 276
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 276,
                    LineID = 0,
                    StationPlacement = 1,
                    StationBeforeCode = 2524,
                    StationAfterCode = 277
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 277,
                    LineID = 0,
                    StationPlacement = 2,
                    StationBeforeCode = 276,
                    StationAfterCode = 2538
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2538,
                    LineID = 0,
                    StationPlacement = 3,
                    StationBeforeCode = 277,
                    StationAfterCode = 2944
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2944,
                    LineID = 0,
                    StationPlacement = 4,
                    StationBeforeCode = 2538,
                    StationAfterCode = 2539
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2539,
                    LineID = 0,
                    StationPlacement = 5,
                    StationBeforeCode = 2944,
                    StationAfterCode = 4216
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 4216,
                    LineID = 0,
                    StationPlacement = 6,
                    StationBeforeCode = 2539,
                    StationAfterCode = 4159
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 4159,
                    LineID = 0,
                    StationPlacement = 7,
                    StationBeforeCode = 4216,
                    StationAfterCode = 800
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 800,
                    LineID = 0,
                    StationPlacement = 8,
                    StationBeforeCode = 4159,
                    StationAfterCode = 3375
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 3375,
                    LineID = 0,
                    StationPlacement = 9,
                    StationBeforeCode = 800,
                    StationAfterCode = 221
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 221,
                    LineID = 0,
                    StationPlacement = 10,
                    StationBeforeCode = 3375,
                    StationAfterCode = 2531
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2531,
                    LineID = 0,
                    StationPlacement = 11,
                    StationBeforeCode = 221,
                    StationAfterCode = 973
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 973,
                    LineID = 0,
                    StationPlacement = 12,
                    StationBeforeCode = 2531,
                    StationAfterCode = 975
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 975,
                    LineID = 0,
                    StationPlacement = 13,
                    StationBeforeCode = 973,
                    StationAfterCode = 6094
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 6094,
                    LineID = 0,
                    StationPlacement = 14,
                    StationBeforeCode = 975,
                    StationAfterCode = 950
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 950,
                    LineID = 0,
                    StationPlacement = 15,
                    StationBeforeCode = 6094,
                    StationAfterCode = 1512
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 1512,
                    LineID = 0,
                    StationPlacement = 16,
                    StationBeforeCode = 950,
                    StationAfterCode = 1539
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 1539,
                    LineID = 0,
                    StationPlacement = 17,
                    StationBeforeCode = 1512,
                    StationAfterCode = 2398
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2398,
                    LineID = 0,
                    StationPlacement = 18,
                    StationBeforeCode = 1539,
                    StationAfterCode = 222
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 222,
                    LineID = 0,
                    StationPlacement = 19,
                    StationBeforeCode = 2398,
                    StationAfterCode = 3093
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 3093,
                    LineID = 0,
                    StationPlacement = 20,
                    StationBeforeCode = 222,
                    StationAfterCode = 0
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 2398,
                    LineID = 1,
                    StationPlacement = 0,
                    StationBeforeCode = 1539,
                    StationAfterCode = 222
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 222,
                    LineID = 1,
                    StationPlacement = 1,
                    StationBeforeCode = 2398,
                    StationAfterCode = 3093
                },
                new LineStation
                {
                    Active = true,
                    StationCode = 3093,
                    LineID = 1,
                    StationPlacement = 2,
                    StationBeforeCode = 222,
                    StationAfterCode = 0
                }
            };
            ListLines = new List<Line>
            {
                new Line
                {
                    Active = true,
                    LineNum = 6,
                    ID = 0,
                    Region = Area.Jerusalem,
                    FirstStationCode = 2524,
                    LastStationCode = 3093
                },
                new Line
                {
                    Active = true,
                    LineNum = 18,
                    ID = 1,
                    Region = Area.General,
                    FirstStationCode = 2398,
                    LastStationCode = 3093
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
