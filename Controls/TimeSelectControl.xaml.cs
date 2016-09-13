using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using JetBrains.Annotations;

namespace DXOrganizer
{
    /// <summary>
    /// Interaction logic for TimeSelectControl.xaml
    /// </summary>
    public partial class TimeSelectControl : UserControl, INotifyPropertyChanged
    {
        public TimeSelectControl()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
        }

        /// <summary>
        /// Returns the set hours as an integer
        /// </summary>
        /// <returns></returns>
        public int Hour
        {
            get { return (int)GetValue(HourProperty); }
            set
            {
                SetValue(HourProperty, value);
                OnPropertyChanged();
            }
        }

        #region DependencyProperty Hour

        public static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof (int), typeof (TimeSelectControl),
                new UIPropertyMetadata(default(int), new PropertyChangedCallback(HourChanged)),
                new ValidateValueCallback(ValidateHour));

        public static bool ValidateHour(object value)
        {
            int x;
            if(!int.TryParse(value.ToString(), out x)) 
                return false;
            return x >= 0 && x <= 23;
        }

        private static void HourChanged(DependencyObject depObj,
            DependencyPropertyChangedEventArgs args)
        {
            TimeSelectControl s = (TimeSelectControl)depObj;
            s.Hour = Convert.ToInt32(args.NewValue);
        }
        
        #endregion

        /// <summary>
        /// Returns the set minutes as an integer
        /// </summary>
        /// <returns></returns>
        
        public int Minutes
        {
            get { return (int)GetValue(MinutesProperty); }
            set
            {
                SetValue(MinutesProperty, value);
                OnPropertyChanged();
            }
        }
        
        #region DependencyProperty Minutes

        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof (int), typeof (TimeSelectControl),
                new UIPropertyMetadata(default(int), new PropertyChangedCallback(MinutesChanged)),
                new ValidateValueCallback(ValidateMinutes));

        public static bool ValidateMinutes(object value)
        {
            int x;
            if (!int.TryParse(value.ToString(), out x)) return false;
            return x >= 0 && x <= 59;
        }

        private static void MinutesChanged(DependencyObject depObj,
            DependencyPropertyChangedEventArgs args)
        {
            TimeSelectControl s = (TimeSelectControl)depObj;
            s.Minutes = Convert.ToInt32(args.NewValue);
        }

        #endregion
        
        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
