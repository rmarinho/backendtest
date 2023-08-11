using Microsoft.Maui;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Maui.Handlers.WPF
{
    public partial class ApplicationHandler : ElementHandler<IApplication, Application>
    {
        public ApplicationHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
        {
        }

        public static void MapTerminate(ApplicationHandler handler, IApplication application, object? args)
        {
            handler.PlatformView.Shutdown();
        }

        public static void MapOpenWindow(ApplicationHandler handler, IApplication application, object? args)
        {
            //handler.PlatformView?.Windows =  CreatePlatformWindow(application, args as OpenWindowRequest);
        }

        public static void MapCloseWindow(ApplicationHandler handler, IApplication application, object? args)
        {
            if (args is IWindow window)
            {
                (window.Handler?.PlatformView as Window)?.Close();
            }
        }
        protected override Application CreatePlatformElement()
        {
            return MauiContext?.Services.GetService<Application>() ?? throw new InvalidOperationException($"MauiContext did not have a valid window.");
        }
    }
}
