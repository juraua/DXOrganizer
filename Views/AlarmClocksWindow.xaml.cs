using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using DevExpress.Xpo.Logger;
using DXOrganizer.Models;
using DXOrganizer.View_Models;
using NLog;
using LogManager = NLog.LogManager;

namespace DXOrganizer
{
    /// <summary>
    /// Interaction logic for AlarmClockWindow.xaml
    /// </summary>
    public partial class AlarmClocksWindow: UserControl
    {
        private readonly MainOrganizerWindow _parentOrganizer;
        private DatabaseContext _context;

        public new AlarmClocksViewModel DataContext
        {
            get { return base.DataContext as AlarmClocksViewModel; }
            set { base.DataContext = value; }
        }

        public AlarmClocksWindow(DatabaseContext context, MainOrganizerWindow parentOrganizer)
        {
            _parentOrganizer = parentOrganizer;
            _context = context;
            InitializeComponent();
            DataContext = new AlarmClocksViewModel(_context);
            DataContext.LoadData();
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = DataContext.SelectedDayOfWeekCheck.AlarmClockId;
                var alarm = _context.AlarmClocks.Find(id);
                var selected = DataContext.GetDaysOfWeekCheckFromAC(alarm);
                _parentOrganizer.ViewBox.Children.Clear();
                _parentOrganizer.ViewBox.Children.Add(new AlarmClockEditWindow(_context, _parentOrganizer, selected));
            }
            catch (NullReferenceException exception)
            {
                EditNullReferenceException(exception);}
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = DataContext.SelectedDayOfWeekCheck.AlarmClockId;
                var alarm = _context.AlarmClocks.Find(id);
                _context.AlarmClocks.Remove(alarm);
                _context.SaveChanges();
                DataContext = new AlarmClocksViewModel(_context);
                DataContext.LoadData();
            }
            catch (NullReferenceException exception)
            {
                DeleteNullReferenceException(exception);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
            

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            _parentOrganizer.ViewBox.Children.Clear();
            _parentOrganizer.ViewBox.Children.Add(new AlarmClockAddWindow(_context, _parentOrganizer));
        }

        private void DataGrid_OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }
        private void AlarmClocksWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext.LoadData();
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }
        private static void ProcessException(Exception exception)
        {
            MessageBox.Show("Виникла помилка при виконанні операції. Детальніше дивіться в звітах");
            var logger = LogManager.GetCurrentClassLogger();
            logger.Log(LogLevel.Fatal, exception, string.Format("Exception in main window: {0}", exception));
        }
        private static void EditNullReferenceException(Exception exception)
        {
            MessageBox.Show("Не вибрано жодного будильника для редагування");
            var logger = LogManager.GetCurrentClassLogger();
            logger.Log(LogLevel.Fatal, exception, string.Format("Exception in main window: {0}", exception));
        }
        private static void DeleteNullReferenceException(Exception exception)
        {
            MessageBox.Show("Не вибрано жодного будильника для видалення");
            var logger = LogManager.GetCurrentClassLogger();
            logger.Log(LogLevel.Fatal, exception, string.Format("Exception in main window: {0}", exception));
        }
    }
}
