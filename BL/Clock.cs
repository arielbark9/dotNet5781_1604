using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Clock
    {
        #region Singelton
        static readonly Clock instance = new Clock();
        static Clock() { }// static ctor to ensure instance init is done just before first usage
        Clock() { } // default => private
        public static Clock Instance { get => instance; }// The public Instance property to use
        #endregion

        private TimeSpan time;
        public int Rate { get; set; }
        public TimeSpan Time
        {
            get => time; set
            {
                time = value;
                onTimeChanged?.Invoke(value);
            }
        }

        public event Action<TimeSpan> onTimeChanged;
        public void ResetObservers()
        {
            onTimeChanged = null;
        }
    }
}
