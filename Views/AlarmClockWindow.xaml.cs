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
using DXOrganizer.Models;

namespace DXOrganizer
{
    /// <summary>
    /// Interaction logic for AlarmClockWindow.xaml
    /// </summary>
    public partial class AlarmClockWindow: UserControl
    {
        private readonly MainOrganizer _parent;
        private DatabaseContext _context;

        public new AlarmClockViewModel DataContext
        {
            get { return base.DataContext as AlarmClockViewModel; }
            set { base.DataContext = value; }
        }
        public AlarmClockWindow(DatabaseContext context, MainOrganizer parent)
        {
            _parent = parent;
            _context = context;
            InitializeComponent();
            DataContext = new AlarmClockViewModel();
        }
        
        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            _parent.ViewBox.Children.Clear();
            _parent.ViewBox.Children.Add(new AlarmClockAdd(_context));
        }
    }
}
