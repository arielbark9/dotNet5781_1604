using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLAPI
{
    public interface IBL
    {
        #region User
        BO.User userDoBoAdapter(DO.User userDo);
        IEnumerable<BO.User> GetAllUsers();
        void AddUser(BO.User newUser);
        void UpdateUser(BO.User User);
        void DeleteUser(BO.User User);
        #endregion

        #region Bus
        BO.Bus busDoBoAdapter(DO.Bus busDo);
        IEnumerable<BO.Bus> GetAllBuses();
        void AddBus(BO.Bus newBus);
        void UpdateBus(BO.Bus bus);
        void DeleteBus(BO.Bus bus);
        #endregion

        #region Line
        BO.Line LineDoBoAdapter(DO.Line LineDo);
        IEnumerable<BO.Line> GetAllLines();
        void AddLine(BO.Line newLine);
        void UpdateLine(BO.Line Line);
        void DeleteLine(BO.Line Line);
        #endregion

        #region Station
        BO.Station stationDoBoAdapter(DO.Station StationDo);
        IEnumerable<BO.Station> GetAllStations();
        void AddStation(BO.Station newStation);
        void UpdateStation(BO.Station Station);
        void DeleteStation(BO.Station Station);
        #endregion

        #region LineStation
        BO.LineStation LineStationDoBoAdapter(DO.LineStation LineStationDo);
        IEnumerable<BO.LineStation> GetAllLineStations();
        void AddLineStation(BO.LineStation newLineStation);
        void UpdateLineStation(BO.LineStation LineStation);
        void DeleteLineStation(BO.LineStation LineStation);
        #endregion

        #region AdjacentStations 
        // no option for user to delete
        BO.AdjacentStations AdjacentStationsDoBoAdapter(DO.AdjacentStations adjStatDo);
        IEnumerable<BO.AdjacentStations> GetAllAdjacentStations();
        void UpdateAdjacentStations(BO.AdjacentStations AdjStat);
        #endregion
    }
}
