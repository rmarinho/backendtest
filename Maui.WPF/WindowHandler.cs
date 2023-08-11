using Microsoft.Maui.Handlers;
using Microsoft.Maui;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Handlers.WPF
{


    public partial class WindowHandler : ElementHandler<IWindow, Window>
    {
        public WindowHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
        {
        }

        public static void MapTitle(IWindowHandler handler, IWindow window)
        {
        }

        public static void MapContent(IWindowHandler handler, IWindow window)
        {
            //_ = handler.MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

            //var nativeContent = window.Content.ToPlatform(handler.MauiContext);

            //handler.PlatformView.Child = nativeContent;
        }

        [MissingMapper]
        public static void MapRequestDisplayDensity(IWindowHandler handler, IWindow window, object? args) { }

        public static void MapToolbar(IWindowHandler handler, IWindow view)
        {
            //if (view is IToolbarElement tb)
            //    ViewHandler.MapToolbar(handler, tb);
        }

        protected override Window CreatePlatformElement()
        {
            return MauiContext?.Services.GetService<Window>() ?? throw new InvalidOperationException($"MauiContext did not have a valid window.");

        }
    }
}
