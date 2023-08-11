using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using PlatformApplication = System.Windows.Application;
using PlatformWindow = System.Windows.Window;

namespace Microsoft.Maui.Handlers.WPF
{
    public static class ApplicationExtensions
    {
        public static void CreatePlatformWindow(this PlatformApplication platformApplication, IApplication application, System.Windows.StartupEventArgs? args) =>
            platformApplication.CreatePlatformWindow(application, new OpenWindowRequest(new WPFPersistedState(args)));

        public static void CreatePlatformWindow(this PlatformApplication platformApplication, IApplication application, OpenWindowRequest? args)
        {
            if (application.Handler?.MauiContext is not IMauiContext applicationContext)
                return;

            var winuiWindow = new MauiWPFWindow();

            var mauiContext = applicationContext!.MakeWindowScope(winuiWindow, out var windowScope);

         //   applicationContext.Services.InvokeLifecycleEvents<WindowsLifecycle.OnMauiContextCreated>(del => del(mauiContext));

            var activationState = args?.State is not null
                ? new ActivationState(mauiContext, args.State)
                : new ActivationState(mauiContext);

            var window = application.CreateWindow(activationState);

            winuiWindow.SetWindowHandler(window, mauiContext);

           // applicationContext.Services.InvokeLifecycleEvents<WindowsLifecycle.OnWindowCreated>(del => del(winuiWindow));
          //  winuiWindow.SetWindow(window);
            winuiWindow.Show();
        }
    }
}
