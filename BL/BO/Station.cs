using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        public int StationCode { get; set; }
        public string StationName { get; set; }
        public override string ToString()
        {
            return $"Station name: {StationName}, code: {StationCode}";
        }
    }
}
