using Microsoft.Maui.Handlers;
using Microsoft.Maui;
using Microsoft.Extensions.DependencyInjection;
using Window = System.Windows.Window;
using Microsoft.Maui.Platform;
using System.Windows;
using Microsoft.Maui.Platform.WPF;
using PlatformView = System.Windows.Window;

namespace Microsoft.Maui.Handlers.WPF
{

    public partial class WindowHandler : ElementHandler<IWindow, Window>, IWindowHandler
    {

        public static IPropertyMapper<IWindow, IWindowHandler> Mapper = new PropertyMapper<IWindow, IWindowHandler>(ElementHandler.ElementMapper)
        {
            [nameof(IWindow.Title)] = MapTitle,
            [nameof(IWindow.Content)] = MapContent,
            //[nameof(IWindow.X)] = MapX,
            //[nameof(IWindow.Y)] = MapY,
            //[nameof(IWindow.Width)] = MapWidth,
            //[nameof(IWindow.Height)] = MapHeight,
            //[nameof(IWindow.MaximumWidth)] = MapMaximumWidth,
            //[nameof(IWindow.MaximumHeight)] = MapMaximumHeight,
            //[nameof(IWindow.MinimumWidth)] = MapMinimumWidth,
            //[nameof(IWindow.MinimumHeight)] = MapMinimumHeight,
            //[nameof(IToolbarElement.Toolbar)] = MapToolbar,
            //[nameof(IMenuBarElement.MenuBar)] = MapMenuBar,
            //[nameof(IWindow.FlowDirection)] = MapFlowDirection,
        };

        public static CommandMapper<IWindow, IWindowHandler> CommandMapper = new(ElementCommandMapper)
        {
            //[nameof(IWindow.RequestDisplayDensity)] = MapRequestDisplayDensity,
        };

        public WindowHandler()
            : base(Mapper, CommandMapper)
        {
        }

        public WindowHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
        {
        }

        protected override void ConnectHandler(PlatformView platformView)
        {
            base.ConnectHandler(platformView);

            if (platformView.Content is null)
                platformView.Content = new WindowRootViewContainer();

            // update the platform window with the user size/position
            //platformView.UpdatePosition(VirtualView);
            //platformView.UpdateSize(VirtualView);

            //var appWindow = platformView.GetAppWindow();
            //if (appWindow is not null)
            //{
            //	// then pass the actual size back to the user
            //	UpdateVirtualViewFrame(appWindow);

            //	// THEN attach the event to reduce churn
            //	appWindow.Changed += OnWindowChanged;
            //}
        }

        protected override void DisconnectHandler(PlatformView platformView)
        {
            //MauiContext
            //	?.GetNavigationRootManager()
            //	?.Disconnect();

            //if (platformView.Content is WindowRootViewContainer container)
            //{
            //	container.Children.Clear();
            //	platformView.Content = null;
            //}

            //var appWindow = platformView.GetAppWindow();
            //if (appWindow is not null)
            //	appWindow.Changed -= OnWindowChanged;

            base.DisconnectHandler(platformView);
        }

        public static void MapTitle(IWindowHandler handler, IWindow window)
        {
        }

        public static void MapContent(IWindowHandler handler, IWindow window)
        {
            _ = handler.MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

            //var previousRootView = windowManager.RootView;


            if ((handler.PlatformView as PlatformView)?.Content is WindowRootViewContainer container)
            {
                //if (previousRootView != null && previousRootView != windowManager.RootView)
                //	container.RemovePage(previousRootView);

                container.AddPage((FrameworkElement)handler.VirtualView.Content.ToPlatform(handler.MauiContext));
            }

            //if (window.VisualDiagnosticsOverlay != null)
            //	window.VisualDiagnosticsOverlay.Initialize();
        }

        protected override Window CreatePlatformElement()
        {
            return MauiContext?.Services.GetService<Window>() ?? throw new InvalidOperationException($"MauiContext did not have a valid window.");

        }
    }
}
