﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User
    {
        // for use with DALObject
        //private static int running = 0;
        //int id = running++;
        //public int ID { get => id; set => id = value; } // IDENTIFIER

        public int ID { get; set; } // IDENTIFIER
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public bool Active { get; set; }
        public override string ToString()
        {
            return $"User: {UserName}, Pass: {Password}";
        }
    }
}
