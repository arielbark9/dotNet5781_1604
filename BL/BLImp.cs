using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using BO;
using DLAPI;

namespace BL
{
    class BLImp : IBL // internal, use only through interface BLApi.
    {
        private IDL dl = DLFactory.GetDL();

        #region User
        public IEnumerable<BO.User> GetAllUsers()
        {
            return from user in dl.GetAllUsers()
                   select userDoBoAdapter(user); 
        }
        public BO.User userDoBoAdapter(DO.User userDo)
        {
            BO.User userBo = new BO.User();
            userDo.CopyPropertiesTo(userBo);
            return userBo;
        }
        public void AddUser(BO.User newUser)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(BO.User User)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(BO.User User)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bus
        public BO.Bus busDoBoAdapter(DO.Bus busDo)
        {
            BO.Bus busBo = new BO.Bus();
            busDo.CopyPropertiesTo(busBo);
            return busBo;
        }
        public IEnumerable<BO.Bus> GetAllBuses()
        {
            return from bus in dl.GetAllBuses()
                   select busDoBoAdapter(bus);
        }
        public void AddBus(BO.Bus newBus)
        {
            bool shouldAdd = true;
            // check for validity first
            if (newBus.LicenceNum.ToString().Length == 7 && newBus.StartDate.Year >= 2018)
                shouldAdd = false;
            else if (newBus.LicenceNum.ToString().Length == 8 && newBus.StartDate.Year <= 2018)
                shouldAdd = false;

            if (newBus.LicenceNum.ToString().Length != 7 && newBus.LicenceNum.ToString().Length != 8)
                shouldAdd = false;

            // add bus
            if (shouldAdd)
            {
                DO.Bus newBusDo = new DO.Bus();
                newBus.CopyPropertiesTo(newBusDo);
                try
                {
                    dl.AddBus(newBusDo);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException("Error! that bus already exists!", ex);
                }
            }
            else
                throw new ArgumentException("Provided bus is not valid!");
        }
        public void DeleteBus(BO.Bus bus)
        {
            DO.Bus busDo = new DO.Bus();
            bus.CopyPropertiesTo(busDo);
            try
            {
                dl.DeleteBus(busDo);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("FATAL ERROR! BUS LIST AND DISPLAY LIST ARE NOT SYNCED", ex);    
            }
        }
        public void UpdateBus(BO.Bus bus)
        {
            DO.Bus busDo = new DO.Bus();
            bus.CopyPropertiesTo(busDo);
            try
            {
                dl.UpdateBus(busDo);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("FATAL ERROR! BUS LIST AND DISPLAY LIST ARE NOT SYNCED", ex);
            }
        }
        #endregion

        #region Line
        public BO.Line LineDoBoAdapter(DO.Line LineDo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Line> GetAllLinees()
        {
            throw new NotImplementedException();
        }

        public void AddLine(BO.Line newLine)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(BO.Line Line)
        {
            throw new NotImplementedException();
        }

        public void DeleteLine(BO.Line Line)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Station
        public BO.Station StationDoBoAdapter(DO.Station StationDo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Station> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public void AddStation(BO.Station newStation)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(BO.Station Station)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(BO.Station Station)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region LineStation
        public BO.LineStation LineStationDoBoAdapter(DO.LineStation LineStationDo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.LineStation> GetAllLineStations()
        {
            throw new NotImplementedException();
        }

        public void AddLineStation(BO.LineStation newLineStation)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineStation(BO.LineStation LineStation)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineStation(BO.LineStation LineStation)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
