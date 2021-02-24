using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TimerTab.Command;
using TimerTab.Model;

namespace TimerTab.ViewModel
{
    public class TabContentViewModel : Tab, INotifyPropertyChanged
    {
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand PauseCommand { get; }
        public TimeControlViewModel timeControl { get; set; } 
        public volatile bool IsTimerStart;
        private TaskTimer _timer;
        private Dispatcher _dispatcher;
        
        public TabContentViewModel()
        {            
            PauseCommand = new ActionCommand(p => Pause());
            StartCommand = new ActionCommand(p => Start());
            StopCommand = new ActionCommand(p => Stop());
            timeControl = new TimeControlViewModel();
            PauseText = "Pause";
            StartVisibility = Visibility.Visible;
            StopPauseVisibility = Visibility.Hidden;
            StopEnabled = false;
        }

        private void Start()
        {
            _timer = new TaskTimer();
            IsTimerStart = true;
            _timer.Start(Properties.Settings.Default.UpDataTime);
            _timer.OnTimerChange += TimeCh;         
            _dispatcher=Dispatcher.CurrentDispatcher;
            StartVisibility = Visibility.Hidden;
            StopPauseVisibility = Visibility.Visible;
            StopEnabled = false;
            PauseText = "Pause";
        }
        private void Pause()
        {
            _timer.Pause();
            if (PauseText == "Pause")
            {
                PauseText = "Continuen";
                StopEnabled = true;
            }
            else
            { 
                PauseText = "Pause";
                StopEnabled = false;
            }
    }
        private void Stop()
        {
            _timer.Stop();
            IsTimerStart = false;
            _timer.OnTimerChange-= TimeCh;
            StartVisibility = Visibility.Visible;
            StopPauseVisibility = Visibility.Hidden;
        }
        private void TimeCh()
        {
            _dispatcher.BeginInvoke(new Action(() =>
           {               
               timeControl.Time = _timer.Time;              
           }));
        }
        
        private string _pausetext;
        public string PauseText
        {
            get { return _pausetext; }
            set
            {
                _pausetext = value;
                OnPropertyChanged(nameof(PauseText));
            }
        }
        private Visibility _startVisibility;
        public Visibility StartVisibility
        {
            get { return _startVisibility; }
            set
            {
                _startVisibility = value;
                OnPropertyChanged(nameof(StartVisibility));
            }
        }
        private Visibility _stopPauseVisibility;
        public Visibility StopPauseVisibility
        {
            get { return _stopPauseVisibility; }
            set
            {
                _stopPauseVisibility = value;
                OnPropertyChanged(nameof(StopPauseVisibility));
            }
        }
        private bool _stopEnabled;
        public bool StopEnabled
        {
            get { return _stopEnabled; }
            set
            {
                _stopEnabled = value;
                OnPropertyChanged(nameof(StopEnabled));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
    }
}
