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
        void UpdateUser(DO.User user); // at least running num will be the same
        void DeleteUser(DO.User user);
        #endregion

        #region Bus
        IEnumerable<DO.Bus> GetAllBuses();
        void AddBus(DO.Bus newBus);
        void UpdateBus(DO.Bus bus); // at least licence number will be the same
        void DeleteBus(DO.Bus bus);
        #endregion

        #region Line
        void AddLine(DO.Line newLine);
        IEnumerable<DO.Line> GetAllLines();
        void UpdateLine(DO.Line Line); // at least running num will be the same
        void DeleteLine(DO.Line Line);
        #endregion

        #region Station
        void AddStation(DO.Station newStation);
        IEnumerable<DO.Station> GetAllStations();
        DO.Station GetStation(int stationCode);
        void UpdateStation(DO.Station Station); // at least running num will be the same
        void DeleteStation(DO.Station Station);
        #endregion

        #region LineStation
        void AddLineStation(DO.LineStation newLineStation);
        IEnumerable<DO.LineStation> GetAllLineStations();
        void UpdateLineStation(DO.LineStation LineStation); // at least running num will be the same
        void DeleteLineStation(DO.LineStation LineStation);
        #endregion

        #region AdjacentStations
        void AddAdjacentStation(DO.AdjacentStations newAdjacentStation);
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStations();
        void UpdateAdjacentStation(DO.AdjacentStations adjacentStation); // at least running num will be the same
        void DeleteAdjacentStation(DO.AdjacentStations adjacentStation);
        #endregion

    }
}
