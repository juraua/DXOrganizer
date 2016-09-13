using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Media;

namespace DXOrganizer
{
    class ScheduledEvent
    {
        public int ScheduledEventId { get; set; }
        //public Color Color { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public DateTime MappedStartTime { get; set; }
        public DateTime MappedEndTime { get; set; }
    }
    //---------To Syncfusion Project----------
    //public class MappedAppointment
    //{
    //    public string MappedSubject { get; set; }
    //    public DateTime MappedStartTime { get; set; }
    //    public DateTime MappedEndTime { get; set; }
    //}
}
