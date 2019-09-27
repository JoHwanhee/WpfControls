using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Controls
{
    public class User : INotifyPropertyChanged
    {
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public User(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        private bool _isBlink;
        public bool IsBlink
        {
            get => _isBlink;
            set
            {
                _isBlink = value;
                OnPropertyChanged(nameof(IsBlink));
            }
        }

        public string Name { get; set; }
    }
}
