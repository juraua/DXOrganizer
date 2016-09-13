using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DXOrganizer.Models;
using JetBrains.Annotations;

namespace DXOrganizer.View_Models
{
    public class TextNoteAddViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseContext _context;

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
