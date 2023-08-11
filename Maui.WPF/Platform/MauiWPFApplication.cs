using Microsoft.Maui;
using System.Windows;
using Microsoft.Maui.Platform;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;

namespace Microsoft.Maui.Handlers.WPF
{
    public abstract class MauiWPFApplication : System.Windows.Application, IPlatformApplication
    {

        public MauiWPFApplication()
        {
            System.Windows.Application.Current.Startup += OnLaunching;
            System.Windows.Application.Current.Exit += OnClosing;
        }

        private void OnClosing(object sender, ExitEventArgs e)
        {

        }

        private void OnLaunching(object sender, StartupEventArgs args)
        {
            // Windows running on a different thread will "launch" the app again
            //if (Application != null)
            //{
            //	Services.InvokeLifecycleEvents<WindowsLifecycle.OnLaunching>(del => del(this, args));
            //	Services.InvokeLifecycleEvents<WindowsLifecycle.OnLaunched>(del => del(this, args));
            //	return;
            //}

            IPlatformApplication.Current = this;
            var mauiApp = CreateMauiApp();

            var rootContext = new WPFMauiContext(mauiApp.Services);

            var applicationContext = rootContext.MakeApplicationScope(this);

            Services = applicationContext.Services;

            //Services.InvokeLifecycleEvents<WindowsLifecycle.OnLaunching>(del => del(this, args));

            Application = Services.GetRequiredService<IApplication>();

            this.SetApplicationHandler(Application, applicationContext);

            this.CreatePlatformWindow(Application, args);
        }

        public IServiceProvider Services { get; protected set; } = null!;

        public IApplication Application { get; protected set; } = null!;

        public static new MauiWPFApplication? Current => System.Windows.Application.Current as MauiWPFApplication;


        protected abstract MauiApp CreateMauiApp();


        //protected override void OnActivated(EventArgs args)
        //{

          
        //}

    }
}
