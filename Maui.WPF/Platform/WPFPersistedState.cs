using System.Windows;

namespace Microsoft.Maui.Handlers.WPF
{
    public class WPFPersistedState : PersistedState
    {
        public WPFPersistedState(StartupEventArgs? startupEventArgs)
        {
            StartupEventArgs = startupEventArgs;
        }

        public StartupEventArgs? StartupEventArgs { get; }
    }
}
