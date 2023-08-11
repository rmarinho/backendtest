using System.Windows;

namespace Microsoft.Maui.Handlers.WPF
{
    public class MauiWPFWindow : Window
    {
        internal void SetWindow(IWindow window)
        {
            Window = window;
        }

        internal IWindow? Window { get; private set; }
    }
}
