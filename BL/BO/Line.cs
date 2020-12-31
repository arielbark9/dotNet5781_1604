using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line
    {
        public int ID { get; set; }
        public int LineNum { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public BO.Area Region { get; set; }
        public override string ToString()
        {
            return $"Bus line: {LineNum}";
        }
    }
}
