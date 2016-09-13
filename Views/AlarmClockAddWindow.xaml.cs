using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlarmClock.Models;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DXOrganizer;
using DXOrganizer.Models;
using DXOrganizer.View_Models;
using JetBrains.Annotations;

namespace DXOrganizer
{
    /// <summary>
    /// Interaction logic for AlarmClockWindow.xaml
    /// </summary>
    public partial class AlarmClockAddWindow : UserControl
    {
        private readonly DatabaseContext _context;
        private readonly MainOrganizerWindow _parentMainOrganizer;

        public AlarmClockAddWindow (DatabaseContext context, MainOrganizerWindow parentMainOrganizer)
        {
            _parentMainOrganizer = parentMainOrganizer;
            _context = context;
            InitializeComponent();
           DataContext = new AlarmClockAddViewModel(context);
        }
        
       public new AlarmClockAddViewModel DataContext
        {
            get { return base.DataContext as AlarmClockAddViewModel; }
            set { base.DataContext = value; }
        }
       
        private void Create_OnClick(object sender, RoutedEventArgs e)
        {
            //Собрать будильник GetAlarmClock()
            AlarmClock newAlarmClock = DataContext.GetAlarmClock();
            _context.AlarmClocks.Add(newAlarmClock);
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

        private void DayOfWeekSelectList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                int index = DayOfWeekSelectList.SelectedIndex;
                DataContext.DaysOfWeekList[i].Checked = index == i;
            }
        }
    }
}