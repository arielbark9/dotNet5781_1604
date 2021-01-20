using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TimeChangedEventArgs : EventArgs
    {
        public TimeSpan Time { get; private set; }

        public TimeChangedEventArgs(TimeSpan t) : base()
        {
            Time = t;
        }
    }
}
