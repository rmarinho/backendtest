using Microsoft.Maui.Dispatching;
using System.Windows.Threading;

namespace Microsoft.Maui.Handlers.WPF
{
    public class WPFDispatchTimer : IDispatcherTimer
    {
        DispatcherTimer _dispatchTimer;

        public WPFDispatchTimer(WPFDispatcher wPFDispatcher)
        {
            _dispatchTimer = new DispatcherTimer(DispatcherPriority.Normal, wPFDispatcher.Dispatcher);
            _dispatchTimer.Tick += OnTick;
        }

        void OnTick(object? sender, EventArgs e)
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }

        public TimeSpan Interval
        {
            get => _dispatchTimer.Interval;
            set => _dispatchTimer.Interval = value;
        }
        public bool IsRepeating
        {
            get;
            set;
        }

        public bool IsRunning => throw new NotImplementedException();

        public event EventHandler? Tick;

        public void Start()
        {
            _dispatchTimer.Start();
        }

        public void Stop()
        {
            _dispatchTimer.Stop();
        }
    }
}
