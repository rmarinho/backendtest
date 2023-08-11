using System.Windows;
using Microsoft.Maui.Platform;

namespace Microsoft.Maui.Handlers.WPF
{
    public static class ApplicationExtensions
    {
        public static void CreatePlatformWindow(this Application platformApplication, IApplication application, EventArgs? args) =>
            platformApplication.CreatePlatformWindow(application, new OpenWindowRequest(null));

        public static void CreatePlatformWindow(this Application platformApplication, IApplication application, OpenWindowRequest? args)
        {
            if (application.Handler?.MauiContext is not IMauiContext applicationContext)
                return;

            var winuiWindow = new MauiWPFWindow();

            var mauiContext = applicationContext!.MakeWindowScope(winuiWindow, out var windowScope);

         //   applicationContext.Services.InvokeLifecycleEvents<WindowsLifecycle.OnMauiContextCreated>(del => del(mauiContext));

            var activationState = args?.State is not null
                ? new ActivationState(mauiContext, args.State)
                : new ActivationState(mauiContext, null);

            var window = application.CreateWindow(activationState);

            winuiWindow.SetWindowHandler(window, mauiContext);

           // applicationContext.Services.InvokeLifecycleEvents<WindowsLifecycle.OnWindowCreated>(del => del(winuiWindow));
            winuiWindow.SetWindow(window);
            winuiWindow.Show();
        }
    }
}
