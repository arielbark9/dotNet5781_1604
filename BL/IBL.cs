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
        #endregion
        #region Bus
        BO.Bus busDoBoAdapter(DO.Bus busDo);
        IEnumerable<BO.Bus> GetAllBuses();
        #endregion
    }
}
