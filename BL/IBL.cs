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
        /// <summary>
        /// Initialize display lists by display needs.
        /// </summary>
        void InitializeDisplay(ref ObservableCollection<BO.Bus> buses, ref ObservableCollection<BO.Line> lines, ref ObservableCollection<BO.Station> stations);

        /*
         * CRUD logic 
         * Create - Add
         * Request - Get, GetAll
         * Update - Update
         * Delete - Delete
         * 
         * Entity DoBo Adapter - Convert entity from DO format to BO Format.
         * Param - a DO type entity
         * Returns - the BO type entity corresponding to the parameter.
         * 
         * Entity BoBo Adapter - Convert entity from BO format to DO Format.
         * Param - a BO type entity
         * Returns - the DO type entity corresponding to the parameter.
        */

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
        /// <summary>
        /// Get all lines that their trips go through a specified station
        /// </summary>
        /// <param name="stationCode"> station to get lines by </param>
        /// <returns> IEnumerable of type BO.Line </returns>
        IEnumerable<BO.Line> GetLinesThatGoThroughStation(int stationCode);
        BO.Line GetLine(int ID);
        void AddLine(BO.Line newLine);
        void UpdateLine(BO.Line Line);
        void DeleteLine(BO.Line Line);
        /// <summary>
        /// Delete specified station in specified line
        /// </summary>
        /// <param name="lineID"> Line to delete station in </param>
        /// <param name="stationCode"> Station to delete </param>
        void DeleteStationInLine(int lineID, int stationCode);
        /// <summary>
        /// Move station one spot down in line's stations.
        /// </summary>
        /// <param name="lineID"> Line </param>
        /// <param name="stationCode"> Station to move </param>
        void MoveStationDownInLine(int lineID, int stationCode);
        /// <summary>
        /// Move station one spot up in line's stations.
        /// </summary>
        /// <param name="lineID"> Line </param>
        /// <param name="stationCode"> Station to move </param>
        void MoveStationUpInLine(int lineID, int stationCode);
        /// <summary>
        /// Get specified Line's schedule
        /// </summary>
        /// <param name="line"> Line </param>
        /// <returns> List of Type TimeSpan of all leaving Times from first station </returns>
        List<TimeSpan> GetLineSchedule(BO.Line line);
        /// <summary>
        /// Add a new Trip to line
        /// </summary>
        /// <param name="newTrip"> New Trip</param>
        void AddLineTrip(LineTrip newTrip);
        #endregion

        #region Station
        IEnumerable<BO.Station> GetAllStations();
        BO.Station GetStation(int stationCode);
        void AddStation(BO.Station newStation);
        void UpdateStation(BO.Station Station);
        void DeleteStation(BO.Station Station);
        /// <summary>
        /// Set station panel data contex to a specified station
        /// </summary>
        /// <param name="station"> station </param>
        /// <param name="updateDisplay"> an Action that takes LineTiming and updates the display </param>
        void SetStationPanel(int station, Action<LineTiming> updateDisplay);
        #endregion

        #region LineStation
        IEnumerable<BO.LineStation> GetAllLineStations();
        void AddLineStation(BO.LineStation newLineStation);
        /// <summary>
        /// Get the time it takes to get from one station to another in a specified line
        /// </summary>
        /// <param name="station1"> first station </param>
        /// <param name="station2"> second station </param>
        /// <param name="line"> specified line </param>
        /// <returns> the time it tatkes to get from station one to station two </returns>
        TimeSpan TimeBetweenLineStations(BO.LineStation station1, BO.LineStation station2, BO.Line line);
        #endregion

        #region AdjacentStations 
        // no option for user to delete or add
        IEnumerable<BO.AdjacentStations> GetAllAdjacentStations();
        void UpdateAdjacentStations(BO.AdjacentStations AdjStat);
        void DeleteAdjacentStations(AdjacentStations adjStat);
        BO.AdjacentStations GetAdjacentStations(int stationOneCode, int stationTwoCode);
        #endregion

        #region Clock
        /// <summary>
        /// start Simulation of clock and lines leaving 
        /// </summary>
        /// <param name="time"> Initial time </param>
        /// <param name="rate"> clock rate </param>
        /// <param name="updateDispClock"> An Action taking TimeSpan that updates display clock </param>
        void StartSimulation(TimeSpan time, int rate, Action<TimeSpan> updateDispClock);
        /// <summary>
        /// Stop the simulation of clock and lines leaving.
        /// </summary>
        void StopSimulation();
        #endregion
    }
}
