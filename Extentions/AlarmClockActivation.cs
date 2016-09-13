using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using System.Windows.Threading;
using DevExpress.Mvvm.Native;
using DXOrganizer;
using DXOrganizer.Models;
using DXOrganizer.View_Models;
using JetBrains.Annotations;

namespace AlarmClock.Models
{
    public class AlarmClockActivation
    {
        private readonly DispatcherTimer _timer;

        public event Action<int> Alarm;

        public AlarmClockActivation()
        {
            _timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(1)};
            _timer.Tick += TimerOnTick;
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }
        protected virtual void OnAlarm(int alarmClockId)
        {
            var handler = Alarm;
            if (handler != null) handler(alarmClockId);
        }
        
        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            var context = new DatabaseContext();
            context.AlarmClocks.Load();
            var timeOfDay = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes,
                    DateTime.Now.TimeOfDay.Seconds);
            var alarmClock = context.AlarmClocks.Local.FirstOrDefault(e =>
                ((e.Repeat && e.DaysOfWeek.Contains(DateTime.Now.DayOfWeek) ||
                 (!e.Repeat && e.Status)) &&
                 e.Start == timeOfDay));
            if (alarmClock != null) OnAlarm(alarmClock.AlarmClockId);
        }
    }
}