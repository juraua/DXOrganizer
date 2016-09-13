using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlarmClock.Models;
using DXOrganizer.Models;
using JetBrains.Annotations;

namespace DXOrganizer.View_Models
{
    public class AlarmClockActivationViewModel : AlarmClocksViewModelSharingMethods
    {
        private ObservableCollection<ComboBoxItem> _suspendItems;
        private DaysOfWeekCheck _alarmClockActive;
        private ComboBoxItem _selectedSuspendItem;

        public AlarmClockActivationViewModel(DatabaseContext context, int alarmId):base(context)
        {
            SuspendItems = new ObservableCollection<ComboBoxItem>
            {
                new ComboBoxItem {Name = "5 минут", Time = new TimeSpan(00, 05, 00)},
                new ComboBoxItem {Name = "10 минут", Time = new TimeSpan(00, 10, 00)},
                new ComboBoxItem {Name = "15 минут", Time = new TimeSpan(00, 15, 00)},
                new ComboBoxItem {Name = "20 минут", Time = new TimeSpan(00, 20, 00)},
                new ComboBoxItem {Name = "30 минут", Time = new TimeSpan(00, 30, 00)},
                new ComboBoxItem {Name = "1 час", Time = new TimeSpan(01, 00, 00)},
            };
            GetAlarmClockActive(alarmId);
        }

        public DaysOfWeekCheck AlarmClockActive
        {
            get { return _alarmClockActive; }
            set
            {
                if (Equals(value, _alarmClockActive)) return;
                _alarmClockActive = value;
                OnPropertyChanged();
            }}

        public ObservableCollection<ComboBoxItem> SuspendItems
        {
            get { return _suspendItems; }
            set
            {
                if (Equals(value, _suspendItems)) return;
                _suspendItems = value;
                OnPropertyChanged();
            }
        }

        public ComboBoxItem SelectedSuspendItem
        {
            get { return _selectedSuspendItem; }
            set
            {
                if (Equals(value, _selectedSuspendItem)) return;
                _selectedSuspendItem = value;
                OnPropertyChanged();
            }
        }

        #region Activation Logic

        private void GetAlarmClockActive(int alarmId)
        {
            AlarmClock singleOrDefault = _context.AlarmClocks.SingleOrDefault(e => e.AlarmClockId == alarmId);
            AlarmClockActive = GetDaysOfWeekCheckFromAC(singleOrDefault);
        }
        public void SetSuspendTime()
        {
            if (SelectedSuspendItem != null)
            {
                var suspendTime = SelectedSuspendItem.Time;
                foreach (var alarmClock in AlarmClocks.Where(alarmClock => alarmClock.AlarmClockId == AlarmClockActive.AlarmClockId))
                {
                    alarmClock.Start += suspendTime;
                }
            }
        }

        public void SetStatusOff()
        {
            if (SelectedSuspendItem == null)
            {
                foreach (var alarmClock in AlarmClocks.Where(alarmClock => alarmClock.AlarmClockId == AlarmClockActive.AlarmClockId).Where(alarmClock => !alarmClock.Repeat))
                {
                    alarmClock.Status = false;
                }
            }
        }
        #endregion
    }
    public class ComboBoxItem
    {
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
    }
}



 