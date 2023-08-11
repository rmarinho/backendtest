using Microsoft.Maui.Dispatching;
using Dispatcher = Microsoft.Maui.Dispatching.Dispatcher;

namespace Microsoft.Maui.Handlers.WPF
{
    public partial class WPFDispatcher : IDispatcher
    {
        public static System.Windows.Threading.Dispatcher? ReplacHack { get; set; }

        readonly System.Windows.Threading.Dispatcher _dispatcherQueue;
        public System.Windows.Threading.Dispatcher Dispatcher => ReplacHack ?? _dispatcherQueue;

        internal WPFDispatcher(System.Windows.Threading.Dispatcher dispatcherQueue)
        {
            _dispatcherQueue = dispatcherQueue ?? throw new ArgumentNullException(nameof(dispatcherQueue));
        }

        public bool IsDispatchRequired
        {
            get
            {
                var result = System.Windows.Threading.Dispatcher.FromThread(Thread.CurrentThread);
                return result != Dispatcher;
            }
        }

        public bool Dispatch(Action action)
        {
            Dispatcher.BeginInvoke(action, null);
            return true;
        }

        public bool DispatchDelayed(TimeSpan delay, Action action)
        {
            throw new NotImplementedException();
        }

        public IDispatcherTimer CreateTimer()
        {
            return new WPFDispatchTimer(this);
        }
    }
}
