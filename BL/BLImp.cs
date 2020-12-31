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
        public void DeleteBus(Bus bus)
        {
            DO.Bus busDo = new DO.Bus();
            bus.CopyPropertiesTo(busDo);
            dl.DeleteBus(busDo);
        }
        #endregion
    }
}
