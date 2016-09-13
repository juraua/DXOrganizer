using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AlarmClock.Models;
using DevExpress.Xpf.Core.Native;
using DXOrganizer.Models;
using JetBrains.Annotations;

namespace DXOrganizer.View_Models
{
    public class AlarmClocksViewModelSharingMethods: INotifyPropertyChanged
    {
        protected readonly DatabaseContext _context;
        private ObservableCollection<DaysOfWeekCheck> _daysOfWeekChecks;
        private DaysOfWeekCheck _selectedDayOfWeekCheck;
        private MelodyViewModel _selectedMelody;
        private Melody _melody;
        private TimeSpan _start;
        private AlarmStatusImageId _alarmStatusImageId;
        private MelodyName? _melodyId;
        private string _alarmClockName;
        private int _alarmClockId;
        private bool _repeat;
        private bool _status;
        private ObservableCollection<AlarmClock> _alarmClocks;
        private int _hour;
        private int _minutes;

        public AlarmClocksViewModelSharingMethods(DatabaseContext context)
        {
            _context = context;
            DaysOfWeekList = new List<CheckItem>
            {
                new CheckItem {Name = "Вс.", FullName = "Воскресенье", DayOfWeek = DayOfWeek.Sunday},
                new CheckItem {Name = "Пн.", FullName = "Понедельник", DayOfWeek = DayOfWeek.Monday}, 
                new CheckItem {Name = "Вт.", FullName = "Вторник", DayOfWeek = DayOfWeek.Tuesday},
                new CheckItem {Name = "Ср.", FullName = "Среда", DayOfWeek = DayOfWeek.Wednesday},
                new CheckItem {Name = "Чт.", FullName = "Четверг", DayOfWeek = DayOfWeek.Tuesday},
                new CheckItem {Name = "Пт.", FullName = "Пятница", DayOfWeek = DayOfWeek.Friday},
                new CheckItem {Name = "Сб.", FullName = "Суббота", DayOfWeek = DayOfWeek.Saturday},
            };
            Melodies = new ObservableCollection<MelodyViewModel>(_context.Melodies.Select(x => new MelodyViewModel
            {
                MelodyId = x.MelodyId,
                MelodyInstance = x.MelodyInstance,
                MelodyName = x.MelodyName
            }));
            DaysOfWeekChecks =
               new ObservableCollection<DaysOfWeekCheck>(_context.AlarmClocks.Select(GetDaysOfWeekCheckFromAC));
            AlarmClocks = new ObservableCollection<AlarmClock>(_context.AlarmClocks);
        }

        # region AlarmClock Parameters
        public string AlarmClockName
        {
            get { return _alarmClockName; }
            set
            {
                if (value == _alarmClockName) return;
                _alarmClockName = value;
                OnPropertyChanged();
            }
        }
        public int AlarmClockId
        {
            get { return _alarmClockId; }
            set
            {
                if (value == _alarmClockId) return;
                _alarmClockId = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan Start
        {
            get { return _start; }
            set
            {
                if (value == _start) return;
                _start = value;
                OnPropertyChanged();
            }
        }
        public bool Repeat
        {
            get { return _repeat; }
            set
            {
                if (value == _repeat) return;
                _repeat = value;
                OnPropertyChanged();
            }
        }
        public bool Status
        {
            get { return _status; }
            set
            {
                if (value == _status) return;
                _status = value;
                OnPropertyChanged();
            }
        }
        public MelodyName? MelodyId
        {
            get { return _melodyId; }
            set
            {
                if (value == _melodyId) return;
                _melodyId = value;
                OnPropertyChanged();
            }
        }
        public Melody Melody
        {
            get { return _melody; }
            set
            {
                if (value == _melody) return;
                _melody = value;
                OnPropertyChanged();
            }
        }

        public AlarmStatusImageId AlarmStatusImageId
        {
            get { return _alarmStatusImageId; }
            set
            {
                if (value == _alarmStatusImageId) return;
                _alarmStatusImageId = value;
                OnPropertyChanged();
            }
        }

        # endregion

        #region AlarmWindow Parameters

        public int  Hour
        {
            get { return _hour; }
            set
            {
                if (value == _hour) return;
                _hour = value % 24;
                OnPropertyChanged();
            }
        }

        public int Minutes
        {
            get { return _minutes; }
            set
            {
                if (value == _minutes) return;
                if (_minutes > 60)
                    Hour += value / 60;
                _minutes = value % 60;
                OnPropertyChanged();
            }
        }

        #endregion
        public List<CheckItem> DaysOfWeekList { get; set; }
        public ObservableCollection<DaysOfWeekCheck> DaysOfWeekChecks
        {
            get { return _daysOfWeekChecks; }
            set
            {
                if (Equals(value, _daysOfWeekChecks)) return;
                _daysOfWeekChecks = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AlarmClock> AlarmClocks
        {
            get { return _alarmClocks; }
            set
            {
                if (Equals(value, _alarmClocks)) return;
                _alarmClocks = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<MelodyViewModel> Melodies { get; set; }
        public DaysOfWeekCheck GetDaysOfWeekCheckFromAC(AlarmClock alarm)
        {
            var check = new DaysOfWeekCheck
            {
                AlarmClockId = alarm.AlarmClockId,
                Duration = alarm.Duration,
                AlarmClockName = alarm.AlarmClockName,
                Repeat = alarm.Repeat,
                Start = alarm.Start,
                AlarmStatusImageId = alarm.AlarmStatusImageId,
                AlarmStatusImage = alarm.AlarmStatusImage.AlarmStatusImageInstance,
                Status = alarm.Status ? "Вкл." : "Выкл.",
                MelodyId = alarm.MelodyId,
                Melody = alarm.Melody
            };
            for (int i = 0; i < 7; i++)
            {
                if (alarm.DaysOfWeek.Contains((DayOfWeek)i))
                {
                    check.DaysOfWeekChecked[i].Checked = true;
                    DaysOfWeekList[i].Checked = true;
                    check.DaysOfWeekChecked[i].Name = DaysOfWeekList[i].Name;
                    check.DaysOfWeek += DaysOfWeekList[i].Name;
                }
                if (check.DaysOfWeekChecked.All(e => e.Checked))
                {
                    check.DaysOfWeek = "Ежедневно";
                }
                else if (!check.Repeat)
                {
                    check.DaysOfWeek = "Без повтора";
                }}
            return check;
        }

        protected List<DayOfWeek> GetDaysOfWeeks()
        {
            var list = new List<DayOfWeek>();
            for (int i = 0; i < DaysOfWeekList.Count; i++)
            {
                if (DaysOfWeekList[i].Checked)
                    list.Add((DayOfWeek)i);
            }
            return list;
        }
        public AlarmClock GetAlarmClockFromDOWC(DaysOfWeekCheck check)
        {
            var alarm = new AlarmClock
            {
                AlarmClockId = check.AlarmClockId,
                Duration = check.Duration,
                AlarmClockName = AlarmClockName,
                Repeat = check.Repeat,
                Start = check.Start,
                AlarmStatusImageId = check.AlarmStatusImageId,
                Status = check.Status == "Вкл."
            };
            for (int i = 0; i < 7; i++)
            {
                if (check.DaysOfWeekChecked[i].Checked)
                    alarm.DaysOfWeek.Add((DayOfWeek)i);
            }
            return alarm;
        }

        public DaysOfWeekCheck SelectedDayOfWeekCheck
        {
            get { return _selectedDayOfWeekCheck; }
            set
            {
                if (Equals(value, _selectedDayOfWeekCheck)) return;
                _selectedDayOfWeekCheck = value;
                OnPropertyChanged();
            }
        }
        public MelodyViewModel SelectedMelody
        {
            get { return _selectedMelody; }
            set
            {
                if (value == _selectedMelody) return;
                _selectedMelody = value;
                OnPropertyChanged();
            }
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        
    }
    public class CheckItem
    {
        public bool Checked { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}

