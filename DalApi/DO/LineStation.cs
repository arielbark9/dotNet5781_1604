using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation
    {
        public bool Active { get; set; }
        public int LineID { get; set; } // IDENTIFIER 1
        public int StationCode { get; set; } // IDENTIFIER 2
        public int StationBeforeCode { get; set; }
        public int StationAfterCode { get; set; }
        public int StationPlacement { get; set; }
    }
}
