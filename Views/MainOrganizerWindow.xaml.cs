using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlarmClock.Models;
using DXOrganizer.Controls;
using DXOrganizer.Models;
using DXOrganizer.View_Models;

namespace DXOrganizer
{
    /// <summary>
    /// Interaction logic for MainOrganizer.xaml
    /// </summary>
    public partial class MainOrganizerWindow : Window
    {
        private readonly DatabaseContext _context;
        private AlarmClockActivation _activation;
        public MainOrganizerWindow()
        {
            InitializeComponent();
            _context = new DatabaseContext();
            
            _activation = new AlarmClockActivation();
            _activation.Alarm += (alarmId) => Dispatcher.Invoke(() => ShowAlarm(alarmId));
            _activation.StartTimer();
        }
        private void MainOrganizer_OnLoaded(object sender, RoutedEventArgs e)
        {
            CalendarButton_OnClick(sender, e);
        }

        private void ShowAlarm(int alarmId)
        {
            var alarmWindow = new AlarmWindow(_context, alarmId);
            alarmWindow.ShowDialog();
        }

        private void MainOrganizerWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _activation.StopTimer();
        }
        private void CalendarButton_OnClick(object sender, RoutedEventArgs e)
        {
            EventsArrowRightImage.Visibility = Visibility.Hidden;
            NotesArrowRightImage.Visibility = Visibility.Hidden;
            AlarmClockArrowRightImage.Visibility = Visibility.Hidden;
            OptionsArrowRightImage.Visibility = Visibility.Hidden;
            ThemeArrowRightImage.Visibility = Visibility.Hidden;
            CalendarArrowRightImage.Visibility = Visibility.Visible;
            ViewBox.Children.Clear();
            ViewBox.Children.Add(new SchedulerWindow(_context));
        }

        private void EventsButton_OnClick(object sender, RoutedEventArgs e)
        {
            CalendarArrowRightImage.Visibility = Visibility.Hidden;
            NotesArrowRightImage.Visibility = Visibility.Hidden;
            AlarmClockArrowRightImage.Visibility = Visibility.Hidden;
            OptionsArrowRightImage.Visibility = Visibility.Hidden;
            ThemeArrowRightImage.Visibility = Visibility.Hidden;
            EventsArrowRightImage.Visibility = Visibility.Visible;
            ViewBox.Children.Clear();
            ViewBox.Children.Add(new SchedulerWindow(_context));
        }

        private void NotesButton_OnClick(object sender, RoutedEventArgs e)
        {
            CalendarArrowRightImage.Visibility = Visibility.Hidden;
            EventsArrowRightImage.Visibility = Visibility.Hidden;
            AlarmClockArrowRightImage.Visibility = Visibility.Hidden;
            OptionsArrowRightImage.Visibility = Visibility.Hidden;
            ThemeArrowRightImage.Visibility = Visibility.Hidden;
            NotesArrowRightImage.Visibility = Visibility.Visible;
            ViewBox.Children.Clear();
            ViewBox.Children.Add(new TextNotesWindow(_context));
        }

        private void AlarmClockButton_OnClick(object sender, RoutedEventArgs e)
        {
            CalendarArrowRightImage.Visibility = Visibility.Hidden;
            EventsArrowRightImage.Visibility = Visibility.Hidden;
            NotesArrowRightImage.Visibility = Visibility.Hidden;
            OptionsArrowRightImage.Visibility = Visibility.Hidden;
            ThemeArrowRightImage.Visibility = Visibility.Hidden;
            AlarmClockArrowRightImage.Visibility = Visibility.Visible;
            ViewBox.Children.Clear();
            ViewBox.Children.Add(new AlarmClocksWindow(_context, this));}

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            CalendarArrowRightImage.Visibility = Visibility.Hidden;
            EventsArrowRightImage.Visibility = Visibility.Hidden;
            NotesArrowRightImage.Visibility = Visibility.Hidden;
            AlarmClockArrowRightImage.Visibility = Visibility.Hidden;
            ThemeArrowRightImage.Visibility = Visibility.Hidden;
            OptionsArrowRightImage.Visibility = Visibility.Visible;   
        }

        private void ThemeButton_OnClick(object sender, RoutedEventArgs e)
        {
            CalendarArrowRightImage.Visibility = Visibility.Hidden;
            EventsArrowRightImage.Visibility = Visibility.Hidden;
            NotesArrowRightImage.Visibility = Visibility.Hidden;
            AlarmClockArrowRightImage.Visibility = Visibility.Hidden;
            OptionsArrowRightImage.Visibility = Visibility.Hidden;
            ThemeArrowRightImage.Visibility = Visibility.Visible;
        }
    }
}
