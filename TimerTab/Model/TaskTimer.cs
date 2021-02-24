using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TimerTab.Model
{
    public class TaskTimer
    {
        public delegate void TimerChange();
        public event TimerChange OnTimerChange;
        public long Time { get; set; }
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;
        private Stopwatch _stopwatch;

        public void Start(int updata)
        {
            _cancelTokenSource = new CancellationTokenSource();
            _token = _cancelTokenSource.Token;
            var timerTask = Task.Run(() => SwTimer(updata));
        }

        public void Stop()
        {
            if (_stopwatch == null)
                return;
            
            _cancelTokenSource.Cancel();
            Time = 0;
            _stopwatch.Reset();
            OnTimerChange?.Invoke();
        }

        public void Pause()
        {
            if (_stopwatch == null)
                return;

            if (_stopwatch.IsRunning)
                _stopwatch.Stop();
            else
                _stopwatch.Start();
        }

        private void SwTimer(int UdDatePeriud)
        {
            _stopwatch = Stopwatch.StartNew();
            while (true)
            {                
                Time = _stopwatch.ElapsedMilliseconds;               
                OnTimerChange?.Invoke();
                if (_token.IsCancellationRequested)
                    break;
                Thread.Sleep(UdDatePeriud);
            }
            
        }
    }
}
