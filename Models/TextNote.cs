using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace DXOrganizer
{
    public class TextNote
    {
        [Key()]
        public int NoteId { get; set; }

        [NotMapped]
        public Color Color { get; set; }

        [Column("Color")]
        public int ColorInt
        {
            get { return (Color.A << 24) | (Color.R << 16) | (Color.G << 8) | Color.B; }
            set
            {
                Color = Color.FromArgb((byte)(value >> 24),
                             (byte)(value >> 16),
                             (byte)(value >> 8),
                             (byte)(value));
            }
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Font { get; set; }
        public DateTime DateCreated { get; set; }
        public bool CreateComplet { get; set; }
        public bool ViewType { get; set; }
        public ICollection<Icon> Icons { get; set; }
    }

    public class Icon
    {
        public int IconId { get; set; }
        public string Name { get; set; }
        public byte[] IconImage { get; set; }
    }

    //enum Color
    //{
    //    Yellow = 0,
    //    Green = 1,
    //    Red = 2,
    //    Blue = 3,
    //    Magenta = 4,
    //    Gray = 5,
    //    Orange = 6
    //}
}
