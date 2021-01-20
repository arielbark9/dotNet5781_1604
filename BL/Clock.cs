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
        public TimeSpan Time
        {
            get => time; set
            {
                time = value;
                onTimeChanged?.Invoke(this, new BO.TimeChangedEventArgs(value));
            }
        }

        public delegate void UpdateEventHandler(object sender, BO.TimeChangedEventArgs args);
        public static event UpdateEventHandler onTimeChanged;
        public static void RemoveObservers()
        {
            onTimeChanged = null;
        }
    }
}
