using Microsoft.Maui.Dispatching;

namespace Microsoft.Maui.Handlers.WPF
{
    public partial class WPFDispatcherProvider : IDispatcherProvider
    {
        [ThreadStatic]
        static IDispatcher? s_dispatcherInstance;

        /// <inheritdoc/>
        public IDispatcher? GetForCurrentThread() =>
            s_dispatcherInstance ??= new WPFDispatcher(System.Windows.Threading.Dispatcher.CurrentDispatcher);
    }
}
