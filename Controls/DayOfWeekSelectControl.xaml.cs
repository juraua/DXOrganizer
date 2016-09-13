using System;
using System.Collections;
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

namespace DXOrganizer
{
    /// <summary>
    /// Interaction logic for DayOfWeekSelectControl.xaml
    /// </summary>
    public partial class DayOfWeekSelectControl : UserControl
    {
        public DayOfWeekSelectControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemSourseProperty = DependencyProperty.Register(
            "ItemSourse", typeof (IEnumerable), typeof (DayOfWeekSelectControl), new PropertyMetadata(default(IEnumerable)));

        public IEnumerable ItemSourse
        {
            get { return (IEnumerable)DayOfWeekSelectList.GetValue(InputScopeProperty); }
            set { DayOfWeekSelectList.SetValue(InputScopeProperty, value); }
        }

        private void DayOfWeekSelectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = DayOfWeekSelectList.SelectedIndex;
        }
    }
}