using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO 
{
    public class Bus
    { // same as DO because there is an option to add a bus.
        public int LicenceNum { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DateSinceMaintenance { get; set; }
        public int Mileage { get; set; }
        public int MileageSinceFuel { get; set; }
        public int MileageSinceMaintenance { get; set; }
        public BO.Status BusStatus { get; set; }
        public override string ToString()
        {
            string res;
            res = "Licence Number: ";
            if (StartDate.Year >= 2018)
                res += LicenceNum.ToString().Substring(0, 3) + '-' + LicenceNum.ToString().Substring(3, 2) + '-' + LicenceNum.ToString().Substring(5, 3);
            else
                res += LicenceNum.ToString().Substring(0, 2) + '-' + LicenceNum.ToString().Substring(2, 3) + '-' + LicenceNum.ToString().Substring(5, 2);
            res += "    Mileage (in km): " + Mileage;
            return res;
        }
    }
}
