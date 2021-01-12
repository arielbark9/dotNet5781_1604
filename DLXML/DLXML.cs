using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;

namespace DL
{
    sealed class DLXML : IDL
    {
        #region Singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion
        #region DS XML Files
        readonly string busesPath = @"BusesXml.xml"; //XElement
        readonly string stationsPath = @"StationsXml.xml"; //XMLSerializer
        readonly string lineStationsPath = @"LineStationsXml.xml"; //XMLSerializer
        readonly string linesPath = @"LinesXml.xml"; //XMLSerializer
        readonly string adjacentStationsPath = @"AdjacentStationsXml.xml"; //XMLSerializer
        readonly string usersPath = @"UsersXml.xml"; //XMLSerializer
        #endregion

        #region Bus
        // XElement in this entity
        public IEnumerable<Bus> GetAllBuses()
        {
            XElement busesRoot = XMLTools.LoadListFromXMLElement(busesPath);
            return (from bus in busesRoot.Elements()
                    where bool.Parse(bus.Element("Active").Value) == true
                    select new DO.Bus()
                    {
                        Active = true,
                        LicenceNum = Int32.Parse(bus.Element("LicenceNum").Value),
                        Mileage = Int32.Parse(bus.Element("Mileage").Value),
                        MileageSinceFuel = Int32.Parse(bus.Element("MileageSinceFuel").Value),
                        MileageSinceMaintenance = Int32.Parse(bus.Element("MileageSinceMaintenance").Value),
                        StartDate = DateTime.Parse(bus.Element("StartDate").Value),
                        DateSinceMaintenance = DateTime.Parse(bus.Element("DateSinceMaintenance").Value),
                        BusStatus = (Status)Enum.Parse(typeof(Status), bus.Element("BusStatus").Value),
                    });
        }
        public void UpdateBus(Bus busUpdate)
        {
            XElement busesRoot = XMLTools.LoadListFromXMLElement(busesPath);
            XElement bus = (from x in busesRoot.Elements()
                               where Int32.Parse(x.Element("LicenceNum").Value) == busUpdate.LicenceNum
                               select x).FirstOrDefault();
            if (bus != null)
            {
                bus.Element("StartDate").Value = busUpdate.StartDate.ToString();
                bus.Element("DateSinceMaintenance").Value = busUpdate.DateSinceMaintenance.ToString();
                bus.Element("Mileage").Value = busUpdate.Mileage.ToString();
                bus.Element("MileageSinceFuel").Value = busUpdate.MileageSinceFuel.ToString();
                bus.Element("MileageSinceMaintenance").Value = busUpdate.MileageSinceMaintenance.ToString();
                bus.Element("BusStatus").Value = busUpdate.BusStatus.ToString();
                bus.Element("Active").Value = "true";

                XMLTools.SaveListToXMLElement(busesRoot, busesPath);
            }
            else
                throw new ArgumentException("Invalid Bus");
        }
        public void DeleteBus(Bus busDelete)
        {
            XElement busesRoot = XMLTools.LoadListFromXMLElement(busesPath);
            XElement bus = (from x in busesRoot.Elements()
                            where Int32.Parse(x.Element("LicenceNum").Value) == busDelete.LicenceNum
                            select x).FirstOrDefault();
            if (bus != null)
            {
                bus.Element("Active").Value = false.ToString();
                XMLTools.SaveListToXMLElement(busesRoot, busesPath);
            }
            else
                throw new ArgumentException("Invalid Bus");
        }
        public void AddBus(Bus newBus)
        {
            XElement busesRoot = XMLTools.LoadListFromXMLElement(busesPath);
            XElement oldBus = (from bus in busesRoot.Elements()
                               where Int32.Parse(bus.Element("LicenceNum").Value) == newBus.LicenceNum
                               select bus).FirstOrDefault();
            if (oldBus == null)
            {
                XElement bus = new XElement("Bus",
                                               new XElement("LicenceNum", newBus.LicenceNum.ToString()),
                                               new XElement("StartDate", newBus.StartDate.ToString()),
                                               new XElement("DateSinceMaintenance", newBus.DateSinceMaintenance.ToString()),
                                               new XElement("Mileage", newBus.Mileage.ToString()),
                                               new XElement("MileageSinceFuel", newBus.MileageSinceFuel.ToString()),
                                               new XElement("BusStatus", newBus.BusStatus.ToString()),
                                               new XElement("Active", newBus.Active.ToString())
                                           );
                busesRoot.Add(bus);
                XMLTools.SaveListToXMLElement(busesRoot, busesPath);
            }
            else if (bool.Parse(oldBus.Element("Active").Value) == false)
                UpdateBus(newBus);
            else
                throw new InvalidOperationException("ERROR! That bus already exists!");
        }
        #endregion

        #region User
        public IEnumerable<User> GetAllUsers()
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            return from user in ListUsers
                   where user.Active == true
                   select user;
        }
        public void DeleteUser(int userID)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            User user = ListUsers.Find(x => x.ID == userID);
            if (user != null)
                user.Active = false;
            else
                throw new ArgumentException("ERROR! that user does not exist!");

            XMLTools.SaveListToXMLSerializer<User>(ListUsers, usersPath);
        }
        public void UpdateUser(User update)
        {
            update.Active = true;
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            User user = ListUsers.Find(x => x.ID == update.ID);
            if (user != null)
                update.CopyPropertiesTo(user);
            else
                throw new ArgumentException("ERROR! that user does not exist!");

            XMLTools.SaveListToXMLSerializer<User>(ListUsers, usersPath);
        }
        public void UpdateUser(int userID, Action<User> update)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            User user = ListUsers.Find(x => x.ID == userID);
            if (user != null)
                update(user);
            else
                throw new ArgumentException("ERROR! that user does not exist!");
            XMLTools.SaveListToXMLSerializer<User>(ListUsers, usersPath);
        }
        public void AddUser(User newUser)
        {
            newUser.Active = true;
            newUser.ID = XMLTools.GetAndIncrementRunningNum<User>();
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            User oldUser = ListUsers.FirstOrDefault(x => x.ID == newUser.ID);
            if (oldUser != null)
            {
                if (oldUser.Active == false)
                {
                    oldUser.Active = true;
                    UpdateUser(newUser);
                }
                else
                    throw new InvalidOperationException("ERROR! User Already Exists!");
            }
            else
                ListUsers.Add(newUser);

            XMLTools.SaveListToXMLSerializer<User>(ListUsers, usersPath);
        }
        #endregion

        #region Station
        public void AddStation(Station newStation)
        {
            newStation.Active = true;
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            Station oldStation = ListStations.FirstOrDefault(x => x.StationCode == newStation.StationCode);
            if (oldStation != null)
            {
                if (oldStation.Active == false)
                {
                    oldStation.Active = true;
                    UpdateStation(newStation);
                }
                else
                    throw new InvalidOperationException("ERROR! Station Already Exists!");
            }
            else
                ListStations.Add(newStation);

            XMLTools.SaveListToXMLSerializer<Station>(ListStations, stationsPath);
        }
        public IEnumerable<Station> GetAllStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            return from station in ListStations
                   where station.Active == true
                   select station;
        }
        public void DeleteStation(Station stationDel)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            Station station = ListStations.Find(x => x.StationCode == stationDel.StationCode);
            if (station != null)
                station.Active = false;
            else
                throw new ArgumentException("ERROR! that station does not exist!");

            XMLTools.SaveListToXMLSerializer<Station>(ListStations, stationsPath);
        }
        public Station GetStation(int stationCode)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            if (ListStations.FirstOrDefault(x => x.StationCode == stationCode && x.Active == true) != null)
                return ListStations.Find(x => x.StationCode == stationCode && x.Active == true);
            else
                throw new ArgumentException("ERROR! no such station");
        }
        public void UpdateStation(Station update)
        {
            update.Active = true;
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            Station station = ListStations.Find(x => x.StationCode == update.StationCode);
            if (station != null)
                update.CopyPropertiesTo(station);
            else
                throw new ArgumentException("ERROR! that station does not exist!");
            XMLTools.SaveListToXMLSerializer<Station>(ListStations, stationsPath);
        }
        #endregion

        #region Adjacent Stations
        public void DeleteAdjacentStation(AdjacentStations adjacentStation)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            AdjacentStations adjStat = ListAdjacentStations.Find(x => x.Station1 == adjacentStation.Station1 && x.Station2 == adjacentStation.Station2);
            if (adjStat != null)
                adjStat.Active = false;
            else
                throw new ArgumentException("ERROR! that adjacentStation does not exist!");
            XMLTools.SaveListToXMLSerializer<AdjacentStations>(ListAdjacentStations, adjacentStationsPath);
        }
        public AdjacentStations GetAdjacentStations(int stationCode1, int stationCode2)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            if (ListAdjacentStations.Where(x => x.Active == true).FirstOrDefault(x => x.Station1 == stationCode1 && x.Station2 == stationCode2) != null)
                return ListAdjacentStations.Where(x => x.Active == true).FirstOrDefault(x => x.Station1 == stationCode1 && x.Station2 == stationCode2);
            else
                throw new ArgumentException("ERROR! no such Adjacent stations entity");
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            return from adjacentStation in ListAdjacentStations
                   where adjacentStation.Active == true
                   select adjacentStation;
        }
        public void AddAdjacentStation(AdjacentStations newAdjacentStations)
        {
            newAdjacentStations.Active = true;
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            AdjacentStations oldAdjacentStations = ListAdjacentStations.FirstOrDefault(x => x.Station1 == newAdjacentStations.Station1 && x.Station2 == newAdjacentStations.Station2);
            if (oldAdjacentStations != null)
            {
                if (oldAdjacentStations.Active == false)
                {
                    oldAdjacentStations.Active = true;
                    UpdateAdjacentStation(newAdjacentStations);
                }
                else
                    throw new InvalidOperationException("ERROR! these adjacent stations Already Exist!");
            }
            else
                ListAdjacentStations.Add(newAdjacentStations);

            XMLTools.SaveListToXMLSerializer<AdjacentStations>(ListAdjacentStations, adjacentStationsPath);
        }
        public void UpdateAdjacentStation(AdjacentStations update)
        {
            update.Active = true;
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            AdjacentStations adjacentStation = ListAdjacentStations.Find(x => x.Station1 == update.Station1 && x.Station2 == update.Station2);
            if (adjacentStation != null)
                update.CopyPropertiesTo(adjacentStation);
            else
                throw new ArgumentException("ERROR! that pair does not exist!");

            XMLTools.SaveListToXMLSerializer<AdjacentStations>(ListAdjacentStations, adjacentStationsPath);
        }
        #endregion

        #region Line Stations
        public LineStation GetLineStation(int lineID, int stationCode)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationsPath);
            if (ListLineStations.Where(x => x.Active == true).FirstOrDefault(x => x.StationCode == stationCode && x.LineID == lineID) != null)
                return ListLineStations.Where(x => x.Active == true).FirstOrDefault(x => x.StationCode == stationCode && x.LineID == lineID);
            else
                throw new ArgumentException("ERROR! no such lineStation");
        }
        public void UpdateLineStation(LineStation update)
        {
            update.Active = true;
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationsPath);
            LineStation lineStation = ListLineStations.Find(x => x.LineID == update.LineID && x.StationCode == update.StationCode);
            if (lineStation != null)
                update.CopyPropertiesTo(lineStation);
            else
                throw new ArgumentException("ERROR! that lineStation does not exist!");
            XMLTools.SaveListToXMLSerializer<LineStation>(ListLineStations, lineStationsPath);
        }
        public void DeleteLineStation(LineStation lineStationDel)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationsPath);
            LineStation lineStation = ListLineStations.Find(x => x.LineID == lineStationDel.LineID && x.StationCode == lineStationDel.StationCode);
            if (lineStation != null)
                lineStation.Active = false;
            else
                throw new ArgumentException("ERROR! that lineStation does not exist!");
            XMLTools.SaveListToXMLSerializer<LineStation>(ListLineStations, lineStationsPath);
        }
        public void AddLineStation(LineStation newLineStation)
        {
            newLineStation.Active = true;
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationsPath);
            LineStation oldLineStation = ListLineStations.FirstOrDefault(x => x.LineID == newLineStation.LineID && x.StationCode == newLineStation.StationCode);
            if (oldLineStation != null)
            {
                if (oldLineStation.Active == false)
                {
                    oldLineStation.Active = true;
                    UpdateLineStation(newLineStation);
                }
                else
                    throw new InvalidOperationException("ERROR! LineStation Already Exists!");
            }
            else
                ListLineStations.Add(newLineStation);
            XMLTools.SaveListToXMLSerializer<LineStation>(ListLineStations, lineStationsPath);
        }
        public IEnumerable<LineStation> GetAllLineStations()
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationsPath);
            return from lineStation in ListLineStations
                   where lineStation.Active == true
                   select lineStation;
        }
        #endregion

        #region Line
        public void UpdateLine(int lineID, Action<Line> update)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            if (ListLines.FirstOrDefault(x => x.ID == lineID) != null)
                update(ListLines.Find(x => x.ID == lineID));
            else
                throw new ArgumentException("ERROR! no such line");

            XMLTools.SaveListToXMLSerializer<Line>(ListLines, linesPath);
        }
        public Line GetLine(int lineID)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            if (ListLines.Where(x => x.Active == true).FirstOrDefault(x => x.ID == lineID) != null)
                return ListLines.Where(x => x.Active == true).FirstOrDefault(x => x.ID == lineID);
            else
                throw new ArgumentException("ERROR! no such line");
        }
        public void AddLine(Line newLine)
        {
            newLine.Active = true;
            newLine.ID = XMLTools.GetAndIncrementRunningNum<Line>();
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            Line oldLine = ListLines.FirstOrDefault(x => x.ID == newLine.ID);
            if (oldLine != null)
            {
                if (oldLine.Active == false)
                {
                    oldLine.Active = true;
                    UpdateLine(newLine);
                }
                else
                    throw new InvalidOperationException("ERROR! Line Already Exists!");
            }
            else
                ListLines.Add(newLine);

            XMLTools.SaveListToXMLSerializer<Line>(ListLines, linesPath);
        }
        public IEnumerable<Line> GetAllLines()
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            return from line in ListLines
                   where line.Active == true
                   select line;
        }
        public void UpdateLine(Line update)
        {
            update.Active = true;
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            Line line = ListLines.Find(x => x.ID == update.ID);
            if (line != null)
                update.CopyPropertiesTo(line);
            else
                throw new ArgumentException("ERROR! that line does not exist!");
            XMLTools.SaveListToXMLSerializer<Line>(ListLines, linesPath);
        }
        public void DeleteLine(int lineID)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            Line line = ListLines.Find(x => x.ID == lineID);
            if (line != null)
                line.Active = false;
            else
                throw new ArgumentException("ERROR! that line does not exist!");
            XMLTools.SaveListToXMLSerializer<Line>(ListLines, linesPath);
        }
        #endregion
    }
}
