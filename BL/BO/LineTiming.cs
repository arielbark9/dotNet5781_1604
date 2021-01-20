using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTiming
    {
        public int LineID { get; set; }
        public int LineNum { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan ArrivalTimeAtStation { get; set; }
    }
}
