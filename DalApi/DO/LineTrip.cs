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
    public class LineTrip
    {
        public int LineID { get; set; } // IDENTIFIER (Every line will only have one schedule)
        public bool Active { get; set; }
        [XmlIgnore]
        public TimeSpan StartTime { get; set; }
        [XmlIgnore]
        public TimeSpan EndTime { get; set; }
        [XmlIgnore]
        public TimeSpan Frequency { get; set; }

        //start time
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "StartTime")]
        public string StartTimeString
        {
            get
            {
                return XmlConvert.ToString(StartTime);
            }
            set
            {
                StartTime = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
        //End Time
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "EndTime")]
        public string EndTimeString
        {
            get
            {
                return XmlConvert.ToString(EndTime);
            }
            set
            {
                EndTime = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
        //Frequency
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "Frequency")]
        public string FrequencyString
        {
            get
            {
                return XmlConvert.ToString(Frequency);
            }
            set
            {
                Frequency = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
    }
}
