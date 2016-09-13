using System;
using System.Collections.Generic;
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
using DXOrganizer.Models;
using DXOrganizer.View_Models;
using NLog;
using LogManager = NLog.LogManager;

namespace DXOrganizer.Controls
{
    /// <summary>
    /// Interaction logic for AlarmClockActivationWindow.xaml
    /// </summary>
    public partial class AlarmWindow : Window
    {
        private readonly DatabaseContext _context;

        public new AlarmClockActivationViewModel DataContext
        {
            get { return base.DataContext as AlarmClockActivationViewModel; }
            set { base.DataContext = value; }
        }

        public AlarmWindow(DatabaseContext context, int alarmId)
        {
            _context = context;
            InitializeComponent();
            DataContext = new AlarmClockActivationViewModel(context, alarmId);
        }
        private void AlarmWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DataContext.SetSuspendTime();
            DataContext.SetStatusOff();
            DataContext.SaveChanges();
            Close();
        }
        private void AlarmMediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("Медіа файл не знайдено");
        }
    }
}
