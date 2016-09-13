using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using AlarmClock.Models;
using DevExpress.Mvvm.Native;
using DevExpress.XtraPrinting.Native;
using DXOrganizer.Extentions;
using DXOrganizer.Models;
using JetBrains.Annotations;

namespace DXOrganizer.View_Models
{
    public class AlarmClocksViewModel : AlarmClocksViewModelSharingMethods
    {public AlarmClocksViewModel(DatabaseContext context): base(context)
        {
        }
        public void LoadData()
        {
            _context.AlarmClocks.Load();
            OnPropertyChanged("AlarmClocks");
        }
    }
}