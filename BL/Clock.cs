using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

        internal volatile bool Cancel;
        private TimeSpan time;
        public int Rate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Time
        {
            get => time; set
            {
                time = value;
                onTimeChanged?.Invoke(value);
            }
        }

        public event Action<TimeSpan> onTimeChanged;
        /// <summary>
        /// Start clock running by rate and start time.
        /// </summary>
        public void StartClock()
        {
            Cancel = false;
            Stopwatch stopwatch = new Stopwatch();
            TimeSpan sleepTime = new TimeSpan((1000 / Rate) * TimeSpan.TicksPerMillisecond);
            // Run Clock simulation thread
            new Thread(() =>
            {
                stopwatch.Restart();
                while (!Cancel)
                {
                    Thread.Sleep(sleepTime);
                    Time = StartTime + new TimeSpan(stopwatch.ElapsedTicks * Rate);
                }
                stopwatch.Stop();
            }).Start();
        }
        public void StopClock()
        {
            Cancel = true;
            Rate = 0;
            onTimeChanged = null;
            StartTime = TimeSpan.Zero;
            time = TimeSpan.Zero;
        }
    }
}
