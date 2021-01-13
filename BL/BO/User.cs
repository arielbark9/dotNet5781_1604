using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        public int ID { get; set; } // IDENTIFIER
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public override string ToString()
        {
            return $"User: {UserName}, Pass: {Password}";
        }
    }
}
