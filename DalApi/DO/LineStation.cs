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
        public int LineID { get; set; }
        public int StationCode { get; set; }
        public int StationBeforeCode { get; set; }
        public int StationAfterCode { get; set; }
        public int StationPlacement { get; set; }
    }
}
