using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DXOrganizer.Extentions
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            return new BitmapImage(
                new Uri(
                    System.IO.Directory.GetCurrentDirectory() + "\\" + (string)value));
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            var biteArray = parameter as byte[];
            if (biteArray != null)
            {
                MemoryStream ms = new MemoryStream(biteArray);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
            return null;
        }
    }
}
