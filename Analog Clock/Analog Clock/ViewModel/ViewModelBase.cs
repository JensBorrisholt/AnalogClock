using System;
using System.ComponentModel;

namespace Analog_Clock.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private readonly object notifyingObjectIsChangedSyncRoot = new object();
        private bool notifyingObjectIsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected ViewModelBase() => PropertyChanged += OnNotifiedOfPropertyChanged;

        public bool IsChanged
        {
            get
            {
                lock (notifyingObjectIsChangedSyncRoot)
                    return notifyingObjectIsChanged;
            }

            protected set
            {
                lock (notifyingObjectIsChangedSyncRoot)
                {
                    if (Equals(notifyingObjectIsChanged, value))
                        return;

                    notifyingObjectIsChanged = value;
                    OnPropertyChanged(nameof(IsChanged));
                }
            }
        }

        protected virtual void OnPropertyChanged(params string[] argsname)
        {
            foreach (var name in argsname)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnNotifiedOfPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e != null && !string.Equals(e.PropertyName, nameof(IsChanged), StringComparison.Ordinal))
                IsChanged = true;
        }
    }
}
