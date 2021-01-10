using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI
{
    //CRUD Logic:
    // Create - add new instance
    // Request - ask for an instance or for a collection
    // Update - update properties of an instance
    // Delete - delete an instance
    public interface IDL
    {
        #region User
        void AddUser(DO.User newUser);
        IEnumerable<DO.User> GetAllUsers();
        void UpdateUser(int userID, Action<User> update);
        void DeleteUser(int userID);
        #endregion

        #region Bus
        IEnumerable<DO.Bus> GetAllBuses();
        void AddBus(DO.Bus newBus);
        void UpdateBus(DO.Bus bus); 
        void DeleteBus(DO.Bus bus);
        #endregion

        #region Line
        void AddLine(DO.Line newLine);
        IEnumerable<DO.Line> GetAllLines();
        DO.Line GetLine(int lineID);
        void UpdateLine(int lineID , Action<Line> update);
        void DeleteLine(int lineID);
        #endregion

        #region Station
        void AddStation(DO.Station newStation);
        IEnumerable<DO.Station> GetAllStations();
        DO.Station GetStation(int stationCode);
        void UpdateStation(DO.Station Station);
        void DeleteStation(DO.Station Station);
        #endregion

        #region LineStation
        void AddLineStation(DO.LineStation newLineStation);
        IEnumerable<DO.LineStation> GetAllLineStations();
        DO.LineStation GetLineStation(int lineID, int stationCode);
        void UpdateLineStation(DO.LineStation LineStation);
        void DeleteLineStation(DO.LineStation LineStation);
        #endregion

        #region AdjacentStations
        void AddAdjacentStation(DO.AdjacentStations newAdjacentStation);
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStations();
        DO.AdjacentStations GetAdjacentStations(int stationCode1, int stationCode2);
        void UpdateAdjacentStation(DO.AdjacentStations adjacentStation);
        void DeleteAdjacentStation(DO.AdjacentStations adjacentStation);
        #endregion

    }
}
