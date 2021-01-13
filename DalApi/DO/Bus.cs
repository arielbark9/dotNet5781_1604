using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus
    {
        public int LicenceNum { get; set; } // IDENTIFIER
        public DateTime StartDate { get; set; }
        public DateTime DateSinceMaintenance { get; set; }
        public int Mileage { get; set; }
        public int MileageSinceFuel { get; set; }
        public int MileageSinceMaintenance { get; set; }
        public Status BusStatus { get; set; }
        public bool Active { get; set; }
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
