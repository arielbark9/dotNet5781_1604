using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AdjacentStations : INotifyPropertyChanged
    {
        private TimeSpan time;

        public int Station1 { get; set; } // IDENTIFIER 1
        public int Station2 { get; set; } // IDENTIFIER 2
        public TimeSpan Time
        {
            get => time; 
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
