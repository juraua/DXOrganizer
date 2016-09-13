using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using DXOrganizer.Extentions;
using DXOrganizer.Models;
using NLog;

namespace DXOrganizer.View_Models
{
    public class AlarmClockAddViewModel : AlarmClocksViewModelSharingMethods
    {
        private readonly MainOrganizerWindow _parentMainOrganizer;

        public AlarmClockAddViewModel(DatabaseContext context)
            : base(context){}
        
        //Собрать будильник
        public AlarmClock GetAlarmClock()
        {
            var clock = new AlarmClock
            {AlarmClockName = AlarmClockName,
                DaysOfWeek = GetDaysOfWeeks(),
                Status = true,
                Repeat = Repeat,
                Start = new TimeSpan(Hour, Minutes, 0),
                AlarmStatusImage =
                    _context.AlarmStatusImages.SingleOrDefault(e => e.AlarmStatusImageId == AlarmStatusImageId.On),
                AlarmStatusImageId = AlarmStatusImageId.On
            };
            try
            {
                clock.Melody = _context.Melodies.SingleOrDefault(e => e.MelodyId == SelectedMelody.MelodyId);
            }
            catch (NullReferenceException e)
            {
                MelodyNullReferenceException(e);
            }
            finally
            {
                if (clock.Melody == null)
                {
                    clock.Melody = _context.Melodies.SingleOrDefault(e => e.MelodyId == MelodyName.Chimes);
                }
            }return clock;
        }
        private static void MelodyNullReferenceException(Exception exception)
        {
            MessageBox.Show("Не було вибрано мелодії для будильника, встановлено мелодію за замовчуванням.");var logger = LogManager.GetCurrentClassLogger();
            logger.Log(LogLevel.Fatal, exception, string.Format("Exception in main window: {0}", exception));
        }
    }
}

