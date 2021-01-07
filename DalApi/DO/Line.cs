﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Line
    {
        private static int running = 0;
        int id = running++;
        public int ID { get => id; set => id = value; }
        public int LineNum { get; set; }
        public int FirstStationCode { get; set; }
        public int LastStationCode { get; set; }
        public Area Region { get; set; }
        public bool Active { get;set; }
        
        public override string ToString()
        {
            return $"Bus line: {LineNum}";
        }
    }
}
