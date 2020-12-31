using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class LineStation
    {
        private static int running = 0;
        int id = running++; 
        public int ID { get => id; }
        public int LineNum { get; set; }
        public int StationCode { get; set; }
        public int StationPlacement { get; set; }
    }
}
