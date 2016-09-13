using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AlarmClock.Models;
using DXOrganizer.Models;
using DXOrganizer.View_Models;
using JetBrains.Annotations;

namespace DXOrganizer
{
    /// <summary>
    /// Interaction logic for AlarmClockWindow.xaml
    /// </summary>
    public partial class AlarmClockEditWindow : UserControl
    {
        private readonly DatabaseContext _context;
        private readonly MainOrganizerWindow _parentMainOrganizer;

        public AlarmClockEditWindow(DatabaseContext context, MainOrganizerWindow parentMainOrganizer, DaysOfWeekCheck selected)
        {
            _parentMainOrganizer = parentMainOrganizer;
            _context = context;
            InitializeComponent();
            DataContext = new AlarmClockEditViewModel(context, selected);
            if (DataContext.SelectedDaysOfWeekCheck.Repeat)
                DayOfWeekSelectList.Visibility = Visibility.Visible;}

        public new AlarmClockEditViewModel DataContext
        {
            get { return base.DataContext as AlarmClockEditViewModel; }
            set { base.DataContext = value; }
        }
       
        private void Create_OnClick(object sender, RoutedEventArgs e)
        {
            //Собрать отредактированный будильник GetAlarmClock()
            AlarmClock editedAlarmClock = DataContext.GetEditedAlarmClock();
            _context.Entry(editedAlarmClock).State = EntityState.Modified;
            _context.SaveChanges();
            _parentMainOrganizer.ViewBox.Children.Clear();
            _parentMainOrganizer.ViewBox.Children.Add(new AlarmClocksWindow(_context, _parentMainOrganizer));
        }

        private void Cansel_OnClick(object sender, RoutedEventArgs e)
        {
            _parentMainOrganizer.ViewBox.Children.Clear();
            _parentMainOrganizer.ViewBox.Children.Add(new AlarmClocksWindow(_context, _parentMainOrganizer));
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
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

        private void RepeatButton_OnClick(object sender, RoutedEventArgs e)
        {
            DayOfWeekSelectList.Visibility = Visibility.Visible;
        }

        private void OneTimeButton_OnClick(object sender, RoutedEventArgs e)
        {
            DayOfWeekSelectList.Visibility = Visibility.Hidden;
            for (int i = 0; i < 7; i++)
            {
                DataContext.DaysOfWeekList[i].Checked = false;
            }
            DataContext.Repeat = false;
        }

        private void StatusBox_OnChecked(object sender, RoutedEventArgs e)
        {
            DataContext.Status = true;
            DataContext.AlarmStatusImageId = AlarmStatusImageId.On;}

        private void StatusBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataContext.Status = false;
            DataContext.AlarmStatusImageId = AlarmStatusImageId.Off;
        }
    }
}