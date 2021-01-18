using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLAPI
{
    public interface IBL
    {
        void InitializeDisplay(ref ObservableCollection<BO.Bus> buses, ref ObservableCollection<BO.Line> lines, ref ObservableCollection<BO.Station> stations, ref ObservableCollection<BO.AdjacentStations> adjStats);
        #region User
        BO.User userDoBoAdapter(DO.User userDo);
        IEnumerable<BO.User> GetAllUsers();
        void AddUser(BO.User newUser);
        void UpdateUser(BO.User User);
        void DeleteUser(BO.User User);
        #endregion

        #region Bus
        IEnumerable<BO.Bus> GetAllBuses();
        void AddBus(BO.Bus newBus);
        void UpdateBus(BO.Bus bus);
        void DeleteBus(BO.Bus bus);
        #endregion

        #region Line
        IEnumerable<BO.Line> GetAllLines();
        IEnumerable<BO.Line> GetLinesThatGoThroughStation(int stationCode);
        BO.Line GetLine(int ID);
        void AddLine(BO.Line newLine);
        void UpdateLine(BO.Line Line);
        void DeleteLine(BO.Line Line);
        void DeleteStationInLine(int lineID, int stationCode);
        void MoveStationDownInLine(int lineID, int stationCode);
        void MoveStationUpInLine(int lineID, int stationCode);
        List<TimeSpan> GetLineSchedule(BO.Line line);
        void AddLineTrip(LineTrip newTrip);
        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
        BO.Station GetStation(int stationCode);
        void AddStation(BO.Station newStation);
        void UpdateStation(BO.Station Station);
        void DeleteStation(BO.Station Station);
        #endregion

        #region LineStation
        IEnumerable<BO.LineStation> GetAllLineStations();
        void AddLineStation(BO.LineStation newLineStation);
        #endregion

        #region AdjacentStations 
        // no option for user to delete or add
        IEnumerable<BO.AdjacentStations> GetAllAdjacentStations();
        void UpdateAdjacentStations(BO.AdjacentStations AdjStat);
        void DeleteAdjacentStations(AdjacentStations adjStat);
        #endregion

        #region Clock
        void StartSimulation(TimeSpan time, int rate, Action<TimeSpan> updateDispClock);
        void StopSimulation();
        #endregion
    }
}
