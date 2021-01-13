using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line : INotifyPropertyChanged
    {
        private int lineNum;
        private List<LineStation> stations;
        private Area region;

        public int ID { get; set; } // IDENTIFIER
        public int LineNum
        {
            get => lineNum;
            set
            {
                lineNum = value;
                OnPropertyChanged();
            }
        }
        public List<LineStation> Stations
        {
            get => stations;
            set
            {
                stations = value;
                OnPropertyChanged();
            }
        }
        public Station LastStation { get; set; }
        public  List<BO.AdjacentStations> AdjStats { get; set; }
        public BO.Area Region
        {
            get => region;
            set
            {
                region = value;
                OnPropertyChanged();
            }
        }
        public BO.LineTrip Trip { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return $"Bus line: {LineNum}";
        }
    }
}
