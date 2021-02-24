using System.ComponentModel;

namespace TimerTab.ViewModel
{
    public class TimeControlViewModel : INotifyPropertyChanged
    {
        public long Time
        {           
            set
            { 
                Milliseconds= value % 1000;
                Seconds = ((value % 60000)  - Milliseconds)/1000;
                Minutes = ((value % 3600000) - Seconds - Milliseconds) / 60000;
                Hours = ((value% 216000000) - Minutes - Seconds - Milliseconds)/ 3600000;
            }
         }
        private long _hours;
        public long Hours
        {
            get { return _hours; }
            set
            {
                _hours = value;
                OnPropertyChanged(nameof(Hours));
            }
        }
       
        private long _minutes;
        public long Minutes
        {
            get { return _minutes; }
            set
            {
                _minutes = value;
                OnPropertyChanged(nameof(Minutes));
            }
        }

        private long _seconds;
        public long Seconds
        {
            get { return _seconds; }
            set
            {
                _seconds = value;
                OnPropertyChanged(nameof(Seconds));
            }
        }
        private long _milliseconds;
        public long Milliseconds
        {
            get { return _milliseconds; }
            set
            {
                _milliseconds = value;
                OnPropertyChanged(nameof(Milliseconds));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));

    }
}
