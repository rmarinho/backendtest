using System.Windows;
using Window = System.Windows.Window;

namespace Microsoft.Maui.Handlers.WPF
{
    public class MauiWPFWindow : Window
    {
        internal void SetWindow(IWindow window)
        {
            Window = window;
        }

        internal IWindow? Window { get; private set; }

        protected override void OnActivated(EventArgs args)
        {
            base.OnActivated(args);
         //   MauiWPFApplication.Current.Services?.InvokeLifecycleEvents<WPFLifecycle.OnActivated>(del => del(this, args));
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
        }
    }
}
