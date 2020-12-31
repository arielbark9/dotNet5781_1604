using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
//using DO;
using DS;

namespace DL
{
    sealed class DalObject : IDL
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }  // static ctor to ensure instance init is done just before first usage
        private DalObject() { }
        public static DalObject Instance { get => instance; }// The public Instance property to use
        #endregion
        #region User
        public IEnumerable<User> GetAllUsers()
        {
            // Filter out non-active entities
            return from item in DataSource.ListUsers
                   where item.Active == true
                   select item.Clone();
        }
        #endregion
        #region Bus
        public IEnumerable<Bus> GetAllBuses()
        {
            // Filter out non-active entities
            return from item in DataSource.ListBuses
                   where item.Active == true
                   select item.Clone();
        }

        public void AddBus(Bus newBus)
        {
            newBus.Active = true;
            if (!DataSource.ListBuses.Exists(x => x.LicenceNum == newBus.LicenceNum))
                DataSource.ListBuses.Add(newBus);
            else
                throw new InvalidOperationException(); // bl takes care of messages
        }
        #endregion
    }
}
