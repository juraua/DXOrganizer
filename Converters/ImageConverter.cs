using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DXOrganizer.Extentions
{
    public class ImageConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var biteArray = value as byte[];
            if (biteArray != null)
            {
                MemoryStream stream = new MemoryStream(biteArray);
                stream.Seek(0, SeekOrigin.Begin);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageIn = value as Image;
            if (imageIn != null)
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, ImageFormat.Gif);
                return ms.ToArray();
            }
            return null;
        }
    }
}

//public byte[] ImageToByteArray(Image imageIn)
//{
//    MemoryStream ms = new MemoryStream();
//    imageIn.Save(ms, ImageFormat.Gif);
//    return ms.ToArray();
//}
//public Image ByteArrayToImage(byte[] byteArrayIn)
//{
//    MemoryStream ms = new MemoryStream(byteArrayIn);
//    Image returnImage = Image.FromStream(ms);
//    return returnImage;
//}