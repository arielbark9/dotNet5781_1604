using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DO
{
    public class AdjacentStations
    {
        public int Station1 { get; set; } // IDENTIFIER 1
        public int Station2 { get; set; } // IDENTIFIER 2
        public double Distance { get; set; }

        [XmlIgnore]
        public TimeSpan Time { get; set; }
        // XmlSerializer does not support TimeSpan, so use this property for 
        // serialization instead.
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "Time")]
        public string TimeString
        {
            get
            {
                return XmlConvert.ToString(Time);
            }
            set
            {
                Time = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
    }
}
