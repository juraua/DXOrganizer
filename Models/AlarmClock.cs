using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace DXOrganizer
{
    public class AlarmClock
    {
        public AlarmClock()
        {
            DaysOfWeek = new List<DayOfWeek>();
            Status = false;
            Repeat = false;
        }

        // ID будильника
        [Key()]
        public int AlarmClockId { get; set; }
        // название будильника
        [Required]
        public string AlarmClockName { get; set; }
        // время запуска будильника
        [Required]
        public TimeSpan Start { get; set; }
        // режим повтора
        [Required]
        public bool Repeat { get; set; }
        //Статус будильника (Вкл., Выкл.)
        [Required]
        public bool Status { get; set; }
        //Длительность активности будильника
        public int Duration { get; set; }
        //Дни недели в режиме повтора
        public string DaysOfWeekDb{
            get { return JsonConvert.SerializeObject(DaysOfWeek); }
            set { DaysOfWeek = JsonConvert.DeserializeObject<List<DayOfWeek>>(value) ?? new List<DayOfWeek>(); }
        }

        [NotMapped]
        public List<DayOfWeek> DaysOfWeek { get; set; }
        public MelodyName? MelodyId { get; set; }
        public virtual Melody Melody { get; set; }
        [Required]
        public AlarmStatusImageId AlarmStatusImageId { get; set; }
        public virtual AlarmStatusImage AlarmStatusImage { get; set; }
    }
    public class AlarmStatusImage
    {
        public AlarmStatusImageId AlarmStatusImageId { get; set; }
        public byte[] AlarmStatusImageInstance { get; set; }
    }

    public enum AlarmStatusImageId
    {
        On = 1,
        Off = 2
    };

    public class Melody
    {
        public MelodyName MelodyId { get; set; }
        public string MelodyName { get; set; }
        public byte[] MelodyInstance { get; set; }
    }
    public enum MelodyName
    {
        Chimes = 0,
        Xilophone = 1,
        Chords = 2,
        Tap = 3,
        Jingle = 4,
        Descending = 5,
        Bounce = 6,
        Echo = 7,
        Ascending = 8
    }
}
