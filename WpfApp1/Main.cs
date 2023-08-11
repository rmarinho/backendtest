using Microsoft.Maui.Handlers;
using Microsoft.Maui.Handlers.WPF;
using Microsoft.Maui.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Program
    {
        [STAThread]

        public static void Main()
        {
            RunApp();
        }

        // Ensure the method is not inlined, so you don't
        // need to load any WPF dll in the Main method
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        static void RunApp()
        {
            var app = new NativeApp();
            //  app.InitializeComponent();
            app.Run();
        }

    }

    public class NativeApp : MauiWPFApplication
    {
        protected override MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder()
                        .UseMauiAppWPF<TheApp>();
            return builder.Build();
        }
    }

    public class TheApp : Microsoft.Maui.Controls.Application
    {
        public TheApp()
        {
            MainPage = new Microsoft.Maui.Controls.ContentPage { Content = new Microsoft.Maui.Controls.Button { Text = "Hello World" } };
        }
    }
}
