using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station : INotifyPropertyChanged
    {
        private double latitude;
        private string stationName;
        private double longitude;
        private List<LineStation> lineStationsByStation;

        public int StationCode { get; set; }
        public string StationName
        {
            get => stationName;
            set
            {
                stationName = value;
                OnPropertyChanged();
            }
        }
        public double Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }
        public double Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }
        public List<LineStation> LineStationsByStation
        {
            get => lineStationsByStation; 
            set
            {
                lineStationsByStation = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Station name: {StationName}, code: {StationCode}";
        }
    }
}
