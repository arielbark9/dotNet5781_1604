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
        IEnumerable<DO.User> GetAllUsers();
        #endregion
        #region Bus
        IEnumerable<DO.Bus> GetAllBuses();
        void AddBus(DO.Bus newBus);
        void DeleteBus(DO.Bus bus);
        #endregion
    }
}
