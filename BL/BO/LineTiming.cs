using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTiming
    {
        static int running = 0;
        private int id = running++;
        public int ID { get => id; }
        public int LineID { get; set; }
        public int LineNum { get; set; }
        public int CurrentStationIndex { get; set; }
        public string LastStationName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan ArrivalTimeAtStation { get; set; }
    }
}
