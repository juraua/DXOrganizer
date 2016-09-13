using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Core.Native;
using DXOrganizer.Models;
using JetBrains.Annotations;

namespace DXOrganizer
{
    class AlarmClockAddViewModel: INotifyPropertyChanged
    {
        private readonly DatabaseContext _context;
        private readonly MainOrganizer _parentMainOrganizer;

        public AlarmClockAddViewModel()
        {
            _context = new DatabaseContext();
        }
        public void LoadData()
        {
            _context.AlarmClocks.Load();
            OnPropertyChanged("AlarmClocks");
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
