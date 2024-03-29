﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station
    {
        public int StationCode { get; set; } // IDENTIFIER
        public string StationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Active { get; set; }
        public override string ToString()
        {
            return $"Station name: {StationName}, code: {StationCode}";
        }
    }
}
