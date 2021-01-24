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
        public static List<LineTrip> ListLineTrips;
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
                    UserName = "arielbark9",
                    Password = "!Ab850850850",
                    Admin = true,
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
                },
                new Station
                {
                Active = true,
                StationCode = 123,
                StationName = "fake stat",
                Latitude = 2,
                Longitude = 20
                },
                new Station
                {
                Active = true,
                StationCode = 47962,
                StationName = "Mercazit Hadera",
                Latitude = 32.45962,
                Longitude = 34.914021
                },
                new Station
                {
                Active = true,
                StationCode = 41788,
                StationName = "Ehad Haam/Rotchild",
                Latitude = 32.434276,
                Longitude = 34.920598
                },
                new Station
                {
                Active = true,
                StationCode = 40200,
                StationName = "Hagiborim/Haaliya Harishona",
                Latitude = 32.436167,
                Longitude = 34.922872
                },
                new Station
                {
                Active = true,
                StationCode = 40227,
                StationName = "Vaytzman/Jerusalem",
                Latitude = 32.437864,
                Longitude = 34.92632
                },
                new Station
                {
                Active = true,
                StationCode = 40255,
                StationName = "Hevrat Hashmal/Tzahal",
                Latitude = 32.441546,
                Longitude = 34.932949
                },
                new Station
                {
                Active = true,
                StationCode = 40420,
                StationName = "Tzomet Granot",
                Latitude = 32.447713,
                Longitude = 34.957584
                },
                new Station
                {
                Active = true,
                StationCode = 40458,
                StationName = "Tzomet Talmey Elazar",
                Latitude = 32.449809,
                Longitude = 34.963141
                },
                new Station
                {
                Active = true,
                StationCode = 40557,
                StationName = "Tzomet Mahane Shmonim",
                Latitude = 32.457376,
                Longitude = 34.984447
                },
                new Station
                {
                Active = true,
                StationCode = 46132,
                StationName = "Tzomet Karkur",
                Latitude = 32.4654,
                Longitude = 34.994606
                },
                new Station
                {
                Active = true,
                StationCode = 46133,
                StationName = "Tzomet Mishmar Hagvul",
                Latitude = 32.481259,
                Longitude = 35.026274
                },
                new Station
                {
                Active = true,
                StationCode = 640,
                StationName = "Mehlaf Hemed",
                Latitude = 31.80033,
                Longitude = 35.126238
                },
                new Station
                {
                Active = true,
                StationCode = 61157,
                StationName = "Harav Pardes/Meir",
                Latitude = 31.844981,
                Longitude = 35.242001
                },
                new Station
                {
                Active = true,
                StationCode = 730,
                StationName = "Golda Meir/Hamshorer",
                Latitude = 31.825302,
                Longitude = 35.188624
                },
                new Station
                {
                Active = true,
                StationCode = 103,
                StationName = "Golda/Hartom",
                Latitude = 31.8,
                Longitude = 35.214106
                },
                new Station
                {
                Active = true,
                StationCode = 127,
                StationName = "Panim Meirot",
                Latitude = 31.796655,
                Longitude = 35.199964
                },
                new Station
                {
                Active = true,
                StationCode = 152,
                StationName = "Rakanti/Idelzon",
                Latitude = 31.817802,
                Longitude = 35.190391
                },
                new Station
                {
                Active = true,
                StationCode = 166,
                StationName = "Zolti",
                Latitude = 31.810445,
                Longitude = 35.215754
                },
                new Station
                {
                Active = true,
                StationCode = 590,
                StationName = "Nahal Luz/Nahal Refaim",
                Latitude = 31.710922,
                Longitude = 34.995945
                },
                new Station
                {
                Active = true,
                StationCode = 609,
                StationName = "Nahar Hayarden/Rabi Yehoshoua",
                Latitude = 31.73176,
                Longitude = 34.993684
                },
                new Station
                {
                Active = true,
                StationCode = 615,
                StationName = "Ben Zeev/Rabin",
                Latitude = 31.742856,
                Longitude = 34.992317
                },
                new Station
                {
                Active = true,
                StationCode = 664,
                StationName = "Horev/Golda Meir",
                Latitude = 31.799833,
                Longitude = 35.2156
                },
                new Station
                {
                Active = true,
                StationCode = 731,
                StationName = "Tzomet Hazeytim",
                Latitude = 31.783507,
                Longitude = 35.257738
                }
            };
            ListLineStations = new List<LineStation>
            {
                new LineStation
                {
                Active = true,
                StationCode = 2524,
                LineID = 0,
                StationBeforeCode = 0,
                StationAfterCode = 277,
                StationPlacement = 1
                },
                new LineStation
                {
                Active = true,
                StationCode = 276,
                LineID = 0,
                StationBeforeCode = 277,
                StationAfterCode = 2538,
                StationPlacement = 3
                },
                new LineStation
                {
                Active = true,
                StationCode = 277,
                LineID = 0,
                StationBeforeCode = 2524,
                StationAfterCode = 276,
                StationPlacement = 2
                },
                new LineStation
                {
                Active = true,
                StationCode = 2538,
                LineID = 0,
                StationBeforeCode = 276,
                StationAfterCode = 2944,
                StationPlacement = 4
                },
                new LineStation
                {
                Active = true,
                StationCode = 2944,
                LineID = 0,
                StationBeforeCode = 2538,
                StationAfterCode = 2539,
                StationPlacement = 5
                },
                new LineStation
                {
                Active = true,
                StationCode = 2539,
                LineID = 0,
                StationBeforeCode = 2944,
                StationAfterCode = 4216,
                StationPlacement = 6
                },
                new LineStation
                {
                Active = true,
                StationCode = 4216,
                LineID = 0,
                StationBeforeCode = 2539,
                StationAfterCode = 4159,
                StationPlacement = 7
                },
                new LineStation
                {
                Active = true,
                StationCode = 4159,
                LineID = 0,
                StationBeforeCode = 4216,
                StationAfterCode = 800,
                StationPlacement = 8
                },
                new LineStation
                {
                Active = true,
                StationCode = 800,
                LineID = 0,
                StationBeforeCode = 4159,
                StationAfterCode = 3375,
                StationPlacement = 9
                },
                new LineStation
                {
                Active = true,
                StationCode = 3375,
                LineID = 0,
                StationBeforeCode = 800,
                StationAfterCode = 221,
                StationPlacement = 10
                },
                new LineStation
                {
                Active = true,
                StationCode = 221,
                LineID = 0,
                StationBeforeCode = 3375,
                StationAfterCode = 2531,
                StationPlacement = 11
                },
                new LineStation
                {
                Active = true,
                StationCode = 2531,
                LineID = 0,
                StationBeforeCode = 221,
                StationAfterCode = 973,
                StationPlacement = 12
                },
                new LineStation
                {
                Active = true,
                StationCode = 973,
                LineID = 0,
                StationBeforeCode = 2531,
                StationAfterCode = 975,
                StationPlacement = 13
                },
                new LineStation
                {
                Active = true,
                StationCode = 975,
                LineID = 0,
                StationBeforeCode = 973,
                StationAfterCode = 6094,
                StationPlacement = 14
                },
                new LineStation
                {
                Active = true,
                StationCode = 6094,
                LineID = 0,
                StationBeforeCode = 975,
                StationAfterCode = 950,
                StationPlacement = 15
                },
                new LineStation
                {
                Active = true,
                StationCode = 950,
                LineID = 0,
                StationBeforeCode = 6094,
                StationAfterCode = 1512,
                StationPlacement = 16
                },
                new LineStation
                {
                Active = true,
                StationCode = 1512,
                LineID = 0,
                StationBeforeCode = 950,
                StationAfterCode = 1539,
                StationPlacement = 17
                },
                new LineStation
                {
                Active = true,
                StationCode = 1539,
                LineID = 0,
                StationBeforeCode = 1512,
                StationAfterCode = 2398,
                StationPlacement = 18
                },
                new LineStation
                {
                Active = true,
                StationCode = 2398,
                LineID = 0,
                StationBeforeCode = 1539,
                StationAfterCode = 222,
                StationPlacement = 19
                },
                new LineStation
                {
                Active = true,
                StationCode = 222,
                LineID = 0,
                StationBeforeCode = 2398,
                StationAfterCode = 3093,
                StationPlacement = 20
                },
                new LineStation
                {
                Active = true,
                StationCode = 3093,
                LineID = 0,
                StationBeforeCode = 222,
                StationAfterCode = 0,
                StationPlacement = 21
                },
                new LineStation
                {
                Active = true,
                StationCode = 2398,
                LineID = 1,
                StationBeforeCode = 0,
                StationAfterCode = 222,
                StationPlacement = 1
                },
                new LineStation
                {
                Active = true,
                StationCode = 222,
                LineID = 1,
                StationBeforeCode = 2398,
                StationAfterCode = 3093,
                StationPlacement = 2
                },
                new LineStation
                {
                Active = true,
                StationCode = 3093,
                LineID = 1,
                StationBeforeCode = 222,
                StationAfterCode = 800,
                StationPlacement = 3
                },
                new LineStation
                {
                Active = true,
                StationCode = 47962,
                LineID = 2,
                StationBeforeCode = 0,
                StationAfterCode = 41788,
                StationPlacement = 1
                },
                new LineStation
                {
                Active = true,
                StationCode = 4159,
                LineID = 2,
                StationBeforeCode = 640,
                StationAfterCode = 0,
                StationPlacement = 12
                },
                new LineStation
                {
                Active = true,
                StationCode = 640,
                LineID = 2,
                StationBeforeCode = 46133,
                StationAfterCode = 4159,
                StationPlacement = 11
                },
                new LineStation
                {
                Active = true,
                StationCode = 46133,
                LineID = 2,
                StationBeforeCode = 46132,
                StationAfterCode = 640,
                StationPlacement = 10
                },
                new LineStation
                {
                Active = true,
                StationCode = 46132,
                LineID = 2,
                StationBeforeCode = 40557,
                StationAfterCode = 46133,
                StationPlacement = 9
                },
                new LineStation
                {
                Active = true,
                StationCode = 40557,
                LineID = 2,
                StationBeforeCode = 40458,
                StationAfterCode = 46132,
                StationPlacement = 8
                },
                new LineStation
                {
                Active = true,
                StationCode = 40458,
                LineID = 2,
                StationBeforeCode = 40420,
                StationAfterCode = 40557,
                StationPlacement = 7
                },
                new LineStation
                {
                Active = true,
                StationCode = 40420,
                LineID = 2,
                StationBeforeCode = 40255,
                StationAfterCode = 40458,
                StationPlacement = 6
                },
                new LineStation
                {
                Active = true,
                StationCode = 40255,
                LineID = 2,
                StationBeforeCode = 40227,
                StationAfterCode = 40420,
                StationPlacement = 5
                },
                new LineStation
                {
                Active = true,
                StationCode = 40227,
                LineID = 2,
                StationBeforeCode = 40200,
                StationAfterCode = 40255,
                StationPlacement = 4
                },
                new LineStation
                {
                Active = true,
                StationCode = 40200,
                LineID = 2,
                StationBeforeCode = 41788,
                StationAfterCode = 40227,
                StationPlacement = 3
                },
                new LineStation
                {
                Active = true,
                StationCode = 41788,
                LineID = 2,
                StationBeforeCode = 47962,
                StationAfterCode = 40200,
                StationPlacement = 2
                },
                new LineStation
                {
                Active = true,
                StationCode = 800,
                LineID = 1,
                StationBeforeCode = 3093,
                StationAfterCode = 4159,
                StationPlacement = 4
                },
                new LineStation
                {
                Active = true,
                StationCode = 4159,
                LineID = 1,
                StationBeforeCode = 800,
                StationAfterCode = 4216,
                StationPlacement = 5
                },
                new LineStation
                {
                Active = true,
                StationCode = 4216,
                LineID = 1,
                StationBeforeCode = 4159,
                StationAfterCode = 2539,
                StationPlacement = 6
                },
                new LineStation
                {
                Active = true,
                StationCode = 2539,
                LineID = 1,
                StationBeforeCode = 4216,
                StationAfterCode = 0,
                StationPlacement = 7
                },
                new LineStation
                {
                Active = true,
                StationCode = 40420,
                LineID = 3,
                StationBeforeCode = 0,
                StationAfterCode = 664,
                StationPlacement = 1
                },
                new LineStation
                {
                Active = true,
                StationCode = 664,
                LineID = 3,
                StationBeforeCode = 40420,
                StationAfterCode = 152,
                StationPlacement = 2
                },
                new LineStation
                {
                Active = true,
                StationCode = 152,
                LineID = 3,
                StationBeforeCode = 664,
                StationAfterCode = 166,
                StationPlacement = 3
                },
                new LineStation
                {
                Active = true,
                StationCode = 166,
                LineID = 3,
                StationBeforeCode = 152,
                StationAfterCode = 609,
                StationPlacement = 4
                },
                new LineStation
                {
                Active = true,
                StationCode = 609,
                LineID = 3,
                StationBeforeCode = 166,
                StationAfterCode = 590,
                StationPlacement = 5
                },
                new LineStation
                {
                Active = true,
                StationCode = 590,
                LineID = 3,
                StationBeforeCode = 609,
                StationAfterCode = 615,
                StationPlacement = 6
                },
                new LineStation
                {
                Active = true,
                StationCode = 615,
                LineID = 3,
                StationBeforeCode = 590,
                StationAfterCode = 731,
                StationPlacement = 7
                },
                new LineStation
                {
                Active = true,
                StationCode = 731,
                LineID = 3,
                StationBeforeCode = 615,
                StationAfterCode = 973,
                StationPlacement = 8
                },
                new LineStation
                {
                Active = true,
                StationCode = 973,
                LineID = 3,
                StationBeforeCode = 731,
                StationAfterCode = 2531,
                StationPlacement = 9
                },
                new LineStation
                {
                Active = true,
                StationCode = 2531,
                LineID = 3,
                StationBeforeCode = 973,
                StationAfterCode = 221,
                StationPlacement = 10
                },
                new LineStation
                {
                Active = true,
                StationCode = 221,
                LineID = 3,
                StationBeforeCode = 2531,
                StationAfterCode = 800,
                StationPlacement = 11
                },
                new LineStation
                {
                Active = true,
                StationCode = 800,
                LineID = 3,
                StationBeforeCode = 221,
                StationAfterCode = 4159,
                StationPlacement = 12
                },
                new LineStation
                {
                Active = true,
                StationCode = 4159,
                LineID = 3,
                StationBeforeCode = 800,
                StationAfterCode = 2539,
                StationPlacement = 13
                },
                new LineStation
                {
                Active = true,
                StationCode = 2539,
                LineID = 3,
                StationBeforeCode = 4159,
                StationAfterCode = 4216,
                StationPlacement = 14
                },
                new LineStation
                {
                Active = true,
                StationCode = 4216,
                LineID = 3,
                StationBeforeCode = 2539,
                StationAfterCode = 40255,
                StationPlacement = 15
                },
                new LineStation
                {
                Active = true,
                StationCode = 40255,
                LineID = 3,
                StationBeforeCode = 4216,
                StationAfterCode = 40227,
                StationPlacement = 16
                },
                new LineStation
                {
                Active = true,
                StationCode = 40227,
                LineID = 3,
                StationBeforeCode = 40255,
                StationAfterCode = 40200,
                StationPlacement = 17
                },
                new LineStation
                {
                Active = true,
                StationCode = 40200,
                LineID = 3,
                StationBeforeCode = 40227,
                StationAfterCode = 3093,
                StationPlacement = 18
                },
                new LineStation
                {
                Active = true,
                StationCode = 3093,
                LineID = 3,
                StationBeforeCode = 40200,
                StationAfterCode = 41788,
                StationPlacement = 19
                },
                new LineStation
                {
                Active = true,
                StationCode = 41788,
                LineID = 3,
                StationBeforeCode = 3093,
                StationAfterCode = 0,
                StationPlacement = 20
                },
                new LineStation
                {
                Active = false,
                StationCode = 47962,
                LineID = 3,
                StationBeforeCode = 41788,
                StationAfterCode = 123,
                StationPlacement = 21
                },
                new LineStation
                {
                Active = false,
                StationCode = 123,
                LineID = 3,
                StationBeforeCode = 41788,
                StationAfterCode = 0,
                StationPlacement = 21
                },
                new LineStation
                {
                Active = true,
                StationCode = 4159,
                LineID = 4,
                StationBeforeCode = 0,
                StationAfterCode = 3093,
                StationPlacement = 1
                },
                new LineStation
                {
                Active = true,
                StationCode = 3093,
                LineID = 4,
                StationBeforeCode = 4159,
                StationAfterCode = 47962,
                StationPlacement = 2
                },
                new LineStation
                {
                Active = true,
                StationCode = 47962,
                LineID = 4,
                StationBeforeCode = 3093,
                StationAfterCode = 2531,
                StationPlacement = 3
                },
                new LineStation
                {
                Active = true,
                StationCode = 2531,
                LineID = 4,
                StationBeforeCode = 47962,
                StationAfterCode = 973,
                StationPlacement = 4
                },
                new LineStation
                {
                Active = true,
                StationCode = 973,
                LineID = 4,
                StationBeforeCode = 2531,
                StationAfterCode = 975,
                StationPlacement = 5
                },
                new LineStation
                {
                Active = true,
                StationCode = 975,
                LineID = 4,
                StationBeforeCode = 973,
                StationAfterCode = 3375,
                StationPlacement = 6
                },
                new LineStation
                {
                Active = true,
                StationCode = 3375,
                LineID = 4,
                StationBeforeCode = 975,
                StationAfterCode = 2524,
                StationPlacement = 7
                },
                new LineStation
                {
                Active = true,
                StationCode = 2524,
                LineID = 4,
                StationBeforeCode = 3375,
                StationAfterCode = 276,
                StationPlacement = 8
                },
                new LineStation
                {
                Active = true,
                StationCode = 276,
                LineID = 4,
                StationBeforeCode = 2524,
                StationAfterCode = 277,
                StationPlacement = 9
                },
                new LineStation
                {
                Active = true,
                StationCode = 277,
                LineID = 4,
                StationBeforeCode = 276,
                StationAfterCode = 2538,
                StationPlacement = 10
                },
                new LineStation
                {
                Active = true,
                StationCode = 2538,
                LineID = 4,
                StationBeforeCode = 277,
                StationAfterCode = 800,
                StationPlacement = 11
                },
                new LineStation
                {
                Active = true,
                StationCode = 800,
                LineID = 4,
                StationBeforeCode = 2538,
                StationAfterCode = 2944,
                StationPlacement = 12
                },
                new LineStation
                {
                Active = true,
                StationCode = 2944,
                LineID = 4,
                StationBeforeCode = 800,
                StationAfterCode = 2539,
                StationPlacement = 13
                },
                new LineStation
                {
                Active = true,
                StationCode = 2539,
                LineID = 4,
                StationBeforeCode = 2944,
                StationAfterCode = 4216,
                StationPlacement = 14
                },
                new LineStation
                {
                Active = true,
                StationCode = 4216,
                LineID = 4,
                StationBeforeCode = 2539,
                StationAfterCode = 1539,
                StationPlacement = 15
                },
                new LineStation
                {
                Active = true,
                StationCode = 1539,
                LineID = 4,
                StationBeforeCode = 4216,
                StationAfterCode = 1512,
                StationPlacement = 16
                },
                new LineStation
                {
                Active = true,
                StationCode = 1512,
                LineID = 4,
                StationBeforeCode = 1539,
                StationAfterCode = 950,
                StationPlacement = 17
                },
                new LineStation
                {
                Active = true,
                StationCode = 950,
                LineID = 4,
                StationBeforeCode = 1512,
                StationAfterCode = 6094,
                StationPlacement = 18
                },
                new LineStation
                {
                Active = true,
                StationCode = 6094,
                LineID = 4,
                StationBeforeCode = 950,
                StationAfterCode = 222,
                StationPlacement = 19
                },
                new LineStation
                {
                Active = true,
                StationCode = 222,
                LineID = 4,
                StationBeforeCode = 6094,
                StationAfterCode = 40200,
                StationPlacement = 20
                },
                new LineStation
                {
                Active = true,
                StationCode = 40200,
                LineID = 4,
                StationBeforeCode = 222,
                StationAfterCode = 0,
                StationPlacement = 21
                },
                new LineStation
                {
                Active = true,
                StationCode = 4216,
                LineID = 5,
                StationBeforeCode = 0,
                StationAfterCode = 800,
                StationPlacement = 1
                },
                new LineStation
                {
                Active = true,
                StationCode = 800,
                LineID = 5,
                StationBeforeCode = 4216,
                StationAfterCode = 2524,
                StationPlacement = 2
                },
                new LineStation
                {
                Active = true,
                StationCode = 2524,
                LineID = 5,
                StationBeforeCode = 800,
                StationAfterCode = 221,
                StationPlacement = 3
                },
                new LineStation
                {
                Active = true,
                StationCode = 221,
                LineID = 5,
                StationBeforeCode = 2524,
                StationAfterCode = 4159,
                StationPlacement = 4
                },
                new LineStation
                {
                Active = true,
                StationCode = 4159,
                LineID = 5,
                StationBeforeCode = 221,
                StationAfterCode = 3375,
                StationPlacement = 5
                },
                new LineStation
                {
                Active = true,
                StationCode = 3375,
                LineID = 5,
                StationBeforeCode = 4159,
                StationAfterCode = 2539,
                StationPlacement = 6
                },
                new LineStation
                {
                Active = true,
                StationCode = 2539,
                LineID = 5,
                StationBeforeCode = 3375,
                StationAfterCode = 2944,
                StationPlacement = 7
                },
                new LineStation
                {
                Active = true,
                StationCode = 2944,
                LineID = 5,
                StationBeforeCode = 2539,
                StationAfterCode = 276,
                StationPlacement = 8
                },
                new LineStation
                {
                Active = true,
                StationCode = 276,
                LineID = 5,
                StationBeforeCode = 2944,
                StationAfterCode = 277,
                StationPlacement = 9
                },
                new LineStation
                {
                Active = true,
                StationCode = 277,
                LineID = 5,
                StationBeforeCode = 276,
                StationAfterCode = 2538,
                StationPlacement = 10
                },
                new LineStation
                {
                Active = true,
                StationCode = 2538,
                LineID = 5,
                StationBeforeCode = 277,
                StationAfterCode = 0,
                StationPlacement = 11
                }
            };
            ListLines = new List<Line>
            {
                new Line
                {
                Active = true,
                ID = 0,
                LineNum = 6,
                Region = Area.Jerusalem,
                FirstStationCode = 2524,
                LastStationCode = 3093
                },
                new Line
                {
                Active = true,
                ID = 1,
                LineNum = 18,
                Region = Area.Jerusalem,
                FirstStationCode = 2398,
                LastStationCode = 2539
                },
                new Line
                {
                Active = true,
                ID = 2,
                LineNum = 942,
                Region = Area.General,
                FirstStationCode = 47962,
                LastStationCode = 4159
                },
                new Line
                {
                Active = true,
                ID = 3,
                LineNum = 5,
                Region = Area.Jerusalem,
                FirstStationCode = 40420,
                LastStationCode = 41788
                },
                new Line
                {
                Active = true,
                ID = 4,
                LineNum = 1,
                Region = Area.Jerusalem,
                FirstStationCode = 4159,
                LastStationCode = 40200
                },
                new Line
                {
                Active = true,
                ID = 5,
                LineNum = 15,
                Region = Area.Jerusalem,
                FirstStationCode = 4216,
                LastStationCode = 2538
                }
            };
            ListLineTrips = new List<LineTrip>
            {
                new LineTrip
                {
                Active = true,
                LineID = 0,
                StartTime = new TimeSpan(5,30,0),
                EndTime = new TimeSpan(23,30,0),
                Frequency = new TimeSpan(0,15,0)
                },
                new LineTrip
                {
                Active = true,
                LineID = 1,
                StartTime = new TimeSpan(5,30,0),
                EndTime = new TimeSpan(12,30,0),
                Frequency = new TimeSpan(0,5,0)
                },
                new LineTrip
                {
                Active = true,
                LineID = 2,
                StartTime = new TimeSpan(7,0,0),
                EndTime = new TimeSpan(12,0,0),
                Frequency = new TimeSpan(0,50,0)
                },
                new LineTrip
                {
                Active = true,
                LineID = 3,
                StartTime = new TimeSpan(10,0,0),
                EndTime = new TimeSpan(23,30,0),
                Frequency = new TimeSpan(0,20,0)
                },
                new LineTrip
                {
                Active = true,
                LineID = 4,
                StartTime = new TimeSpan(0,0,0),
                EndTime = new TimeSpan(23,0,0),
                Frequency = new TimeSpan(0,30,0)
                },
                new LineTrip
                {
                Active = true,
                LineID = 5,
                StartTime = new TimeSpan(12,0,0),
                EndTime = new TimeSpan(23,30,0),
                Frequency = new TimeSpan(0,30,0)
                }
            };
            ListAdjacentStations = new List<AdjacentStations>
            {
                new AdjacentStations
                {
                Station1 = 2524,
                Station2 = 276,
                Time = new TimeSpan(0,20,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 276,
                Station2 = 277,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 277,
                Station2 = 2538,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2538,
                Station2 = 2944,
                Time = new TimeSpan(0,1,20),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2944,
                Station2 = 2539,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2539,
                Station2 = 4216,
                Time = new TimeSpan(0,3,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 4216,
                Station2 = 4159,
                Time = new TimeSpan(0,1,40),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 4159,
                Station2 = 800,
                Time = new TimeSpan(0,3,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 800,
                Station2 = 3375,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 3375,
                Station2 = 221,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 221,
                Station2 = 2531,
                Time = new TimeSpan(0,7,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2531,
                Station2 = 973,
                Time = new TimeSpan(0,3,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 973,
                Station2 = 975,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 975,
                Station2 = 6094,
                Time = new TimeSpan(0,1,10),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 6094,
                Station2 = 950,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 950,
                Station2 = 1512,
                Time = new TimeSpan(0,1,50),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 1512,
                Station2 = 1539,
                Time = new TimeSpan(0,2,30),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 1539,
                Station2 = 2398,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2398,
                Station2 = 222,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 222,
                Station2 = 3093,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 640,
                Station2 = 4159,
                Time = new TimeSpan(0,12,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 41788,
                Station2 = 40200,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 47962,
                Station2 = 41788,
                Time = new TimeSpan(0,3,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 40200,
                Station2 = 40227,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 40227,
                Station2 = 40255,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 40255,
                Station2 = 40420,
                Time = new TimeSpan(0,4,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 40420,
                Station2 = 40458,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 40458,
                Station2 = 40557,
                Time = new TimeSpan(0,3,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 40557,
                Station2 = 46132,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 46132,
                Station2 = 46133,
                Time = new TimeSpan(0,3,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 46133,
                Station2 = 640,
                Time = new TimeSpan(1,8,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 3093,
                Station2 = 800,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 800,
                Station2 = 4159,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 4159,
                Station2 = 4216,
                Time = new TimeSpan(0,4,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 4216,
                Station2 = 2539,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2524,
                Station2 = 277,
                Time = new TimeSpan(0,1,20),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 277,
                Station2 = 276,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 276,
                Station2 = 2538,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 40420,
                Station2 = 664,
                Time = new TimeSpan(0,0,15),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 664,
                Station2 = 152,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 152,
                Station2 = 166,
                Time = new TimeSpan(0,6,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 166,
                Station2 = 609,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 609,
                Station2 = 590,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 590,
                Station2 = 615,
                Time = new TimeSpan(0,3,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 615,
                Station2 = 731,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 731,
                Station2 = 973,
                Time = new TimeSpan(0,10,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 973,
                Station2 = 2531,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2531,
                Station2 = 221,
                Time = new TimeSpan(0,15,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 221,
                Station2 = 800,
                Time = new TimeSpan(0,6,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 4159,
                Station2 = 2539,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 4216,
                Station2 = 40255,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 40255,
                Station2 = 40227,
                Time = new TimeSpan(0,6,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 40227,
                Station2 = 40200,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 40200,
                Station2 = 3093,
                Time = new TimeSpan(0,1,30),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 3093,
                Station2 = 41788,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 41788,
                Station2 = 47962,
                Time = new TimeSpan(0,10,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 47962,
                Station2 = 123,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 41788,
                Station2 = 123,
                Time = new TimeSpan(0,11,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 4159,
                Station2 = 3093,
                Time = new TimeSpan(0,5,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 3093,
                Station2 = 47962,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 47962,
                Station2 = 2531,
                Time = new TimeSpan(0,3,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 975,
                Station2 = 3375,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 3375,
                Station2 = 2524,
                Time = new TimeSpan(0,1,20),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2538,
                Station2 = 800,
                Time = new TimeSpan(0,10,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 800,
                Station2 = 2944,
                Time = new TimeSpan(0,1,1),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 4216,
                Station2 = 1539,
                Time = new TimeSpan(0,1,30),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 1539,
                Station2 = 1512,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 1512,
                Station2 = 950,
                Time = new TimeSpan(0,3,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 950,
                Station2 = 6094,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 6094,
                Station2 = 222,
                Time = new TimeSpan(0,1,10),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 222,
                Station2 = 40200,
                Time = new TimeSpan(0,10,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 4216,
                Station2 = 800,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 800,
                Station2 = 2524,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2524,
                Station2 = 221,
                Time = new TimeSpan(0,1,20),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 221,
                Station2 = 4159,
                Time = new TimeSpan(0,3,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 4159,
                Station2 = 3375,
                Time = new TimeSpan(0,4,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 3375,
                Station2 = 2539,
                Time = new TimeSpan(0,2,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2539,
                Station2 = 2944,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                },
                new AdjacentStations
                {
                Station1 = 2944,
                Station2 = 276,
                Time = new TimeSpan(0,1,0),
                Distance = 0
                }
            };
        }
    }
}
