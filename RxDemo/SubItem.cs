using RxDemo.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace RxDemo
{
    public class SubItem : INotifyPropertyChanged
    {
        private int _value;
        private IObservable<int> _observable;

        public SubItem(IObservable<int> obs)
        {
            obs.Subscribe(p => Value = p, ex => MessageBox.Show(ex.Message));
            _observable = obs;
        }

        public int Value
        {
            get { return _value; }
            private set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public IObservable<int> Observable
        {
            get { return _observable; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
