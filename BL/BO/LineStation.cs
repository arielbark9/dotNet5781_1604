using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation
    {
        public int LineID { get; set; }
        public Station Station { get; set; }
        public Station BeforeStation { get; set; }
        public Station AfterStation { get; set; }
        public int StationPlacement { get; set; }
    }
}
