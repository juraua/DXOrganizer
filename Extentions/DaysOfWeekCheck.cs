using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using DXOrganizer;
using DXOrganizer.View_Models;

namespace AlarmClock.Models
{
    public class DaysOfWeekCheck
    {
        public DaysOfWeekCheck()
        {
            //DaysOfWeek = Enumerable.Range(0, 7).Select(e => false).ToList();
            DaysOfWeekChecked = Enumerable.Range(0, 7).Select(i => new CheckItem { Checked = false }).ToList();
        }

        public int AlarmClockId { get; set; }
        // название будильника
        public string AlarmClockName { get; set; }
        // время запуска будильника
        public TimeSpan Start { get; set; }
        // режим повтора
        public bool Repeat { get; set; }
        //Статус будильника (Вкл., Выкл.)
        public string Status { get; set; }
        //Длительность активности будильника
        public int Duration { get; set; }
        public string DaysOfWeek { get; set; }
        public List<CheckItem> DaysOfWeekChecked { get; set; }
        public MelodyName? MelodyId { get; set; }
        public virtual Melody Melody { get; set; }
        [Required]
        public AlarmStatusImageId AlarmStatusImageId { get; set; }
        public byte[] AlarmStatusImage { get; set; }
    }
}