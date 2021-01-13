using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BO 
{
    public class Bus : INotifyPropertyChanged
    { // same as DO because there is an option to add a bus.
        private int mileage;
        private DateTime startDate;
        private DateTime dateSinceMaintenance;
        private int mileageSinceFuel;
        private int mileageSinceMaintenance;

        public int LicenceNum { get; set; } // IDENTIFIER
        public DateTime StartDate
        {
            get => startDate; set
            {
                startDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateSinceMaintenance
        {
            get => dateSinceMaintenance; set
            {
                dateSinceMaintenance = value;
                OnPropertyChanged();
            }
        }
        public int Mileage
        {
            get => mileage; set { mileage = value; OnPropertyChanged(); }
        }
        public int MileageSinceFuel
        {
            get => mileageSinceFuel; set
            {
                mileageSinceFuel = value;
                OnPropertyChanged();
            }
        }
        public int MileageSinceMaintenance
        {
            get => mileageSinceMaintenance; set
            {
                mileageSinceMaintenance = value;
                OnPropertyChanged();
            }
        }
        public BO.Status BusStatus { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            string res;
            res = "Licence Number: ";
            if (StartDate.Year >= 2018)
                res += LicenceNum.ToString().Substring(0, 3) + '-' + LicenceNum.ToString().Substring(3, 2) + '-' + LicenceNum.ToString().Substring(5, 3);
            else
                res += LicenceNum.ToString().Substring(0, 2) + '-' + LicenceNum.ToString().Substring(2, 3) + '-' + LicenceNum.ToString().Substring(5, 2);
            res += "    Mileage (in km): " + Mileage;
            return res;
        }
    }
}
