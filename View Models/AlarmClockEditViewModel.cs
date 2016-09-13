using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AlarmClock.Models;
using DXOrganizer.Migrations;
using DXOrganizer.Models;
using JetBrains.Annotations;

namespace DXOrganizer.View_Models
{
    public class AlarmClockEditViewModel : AlarmClocksViewModelSharingMethods
    {
        private MainOrganizerWindow _parentWindow;
        private DaysOfWeekCheck _selectedDaysOfWeekCheck;
        public AlarmClockEditViewModel(DatabaseContext context, DaysOfWeekCheck selected)
            : base(context)
        {
            _selectedDaysOfWeekCheck = selected;
            EditDaysOfWeekCheck();
        }
        public DaysOfWeekCheck SelectedDaysOfWeekCheck
        {
            get { return _selectedDaysOfWeekCheck; }
            set
            {
                if (Equals(value, _selectedDaysOfWeekCheck)) return;
                _selectedDaysOfWeekCheck = value;
                OnPropertyChanged();
            }
        }

        //Редактировать будильник
        public void EditDaysOfWeekCheck()
        {
            var s = SelectedDaysOfWeekCheck;
            AlarmClockName = s.AlarmClockName;
            AlarmClockId = s.AlarmClockId;s.DaysOfWeekChecked.ForEach(
                x => DaysOfWeekList.FirstOrDefault(e => e.DayOfWeek == x.DayOfWeek).Checked = x.Checked);
            Status = s.Status == "Вкл.";
            Repeat = s.Repeat;
            Start = s.Start;
            AlarmStatusImageId = s.AlarmStatusImageId;
            MelodyId = s.Melody.MelodyId;
            SelectedMelody = Melodies.FirstOrDefault(e => e.MelodyId == s.MelodyId);
            Hour = Start.Hours;
            Minutes = Start.Minutes;}
        //Собрать будильник
        public AlarmClock GetEditedAlarmClock()
        {
            var clock = _context.AlarmClocks.FirstOrDefault(e => e.AlarmClockId == AlarmClockId) ?? new AlarmClock
            {
                AlarmClockId = AlarmClockId
            };
            clock.AlarmClockName = AlarmClockName;
            clock.DaysOfWeek = GetDaysOfWeeks();
            clock.Status = Status;
            clock.Repeat = Repeat;
            clock.Start = new TimeSpan(Hour, Minutes, 0);
            clock.AlarmStatusImageId = AlarmStatusImageId;
            clock.MelodyId = SelectedMelody.MelodyId;
            clock.Melody = _context.Melodies.SingleOrDefault(e => e.MelodyId == SelectedMelody.MelodyId);
            clock.AlarmStatusImage = clock.AlarmStatusImageId == AlarmStatusImageId.On ? _context.AlarmStatusImages.SingleOrDefault(e => e.AlarmStatusImageId == AlarmStatusImageId.On) : _context.AlarmStatusImages.SingleOrDefault(e => e.AlarmStatusImageId == AlarmStatusImageId.Off);
            return clock;}
    }
}
