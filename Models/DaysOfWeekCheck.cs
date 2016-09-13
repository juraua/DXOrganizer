using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlarmClock.Models
{
    public class DaysOfWeekCheck
    {
        public DaysOfWeekCheck()
        {
            DaysOfWeek = Enumerable.Range(0, 7).Select(e => false).ToList();
        }

        public int AlarmClockId { get; set; }
        // название будильника
        public string Name { get; set; }
        // время запуска будильника
        public TimeSpan Start { get; set; }
        // режим повтора
        public bool Repeat { get; set; }
        //Статус будильника (Вкл., Выкл.)
        public bool Status { get; set; }
        //Длительность активности будильника

        public int Duration { get; set; }
        
        public List<bool> DaysOfWeek { get; set; }
    }
}