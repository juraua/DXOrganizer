using System.Collections.Generic;
using System.Windows.Media;

namespace DXOrganizer
{
    public class ColorCollection:List<Color>
    {
        private static readonly ColorCollection InternalCollection = new ColorCollection
        {
            Color.FromArgb(0xFF, 0xA2, 0xC1, 0x39),
            Color.FromArgb(0xFF, 0xD8, 0x00, 0x73),
            Color.FromArgb(0xFF, 0x1B, 0xA1, 0xE2),
            Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8),
            Color.FromArgb(0xFF, 0xF0, 0x96, 0x09),
            Color.FromArgb(0xFF, 0x33, 0x99, 0x33),
            Color.FromArgb(0xFF, 0x00, 0xAB, 0xA9),
            Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8)
        };

        private ColorCollection() { }

        public static ColorCollection Collection
        {
            get
            {
                return InternalCollection;
            }
        }
    }
}