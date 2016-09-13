using System.ComponentModel;
using System.Runtime.CompilerServices;
using DXOrganizer;
using JetBrains.Annotations;

namespace DXOrganizer.View_Models
{
    public class MelodyViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private MelodyName? _melodyId;
        private string _melodyName;
        private byte[] _melodyInstance;

        public MelodyName? MelodyId
        {
            get { return _melodyId; }
            set
            {
                if (value == _melodyId) return;
                _melodyId = value;
                OnPropertyChanged();
            }
        }

        public string MelodyName
        {
            get { return _melodyName; }
            set
            {
                if (value == _melodyName) return;
                _melodyName = value;
                OnPropertyChanged();
            }
        }
        public byte[] MelodyInstance
        {
            get { return _melodyInstance; }
            set
            {
                if (value == _melodyInstance) return;
                _melodyInstance = value;
                OnPropertyChanged();
            }
        }
    }
}

