﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation
    {
        public int LineID { get; set; } // IDENTIFIER 1
        public int StationCode { get; set; } // IDENTIFIER 2
        public string StationName { get; set; }
        public TimeSpan TimeToNext { get; set; }
        public int StationPlacement { get; set; }
    }
}
