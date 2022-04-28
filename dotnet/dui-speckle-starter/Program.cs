using Avalonia;
using Avalonia.ReactiveUI;
using System.IO;

namespace Speckle.DesktopUIStarter
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {

            // to debug the VS previewer
            // 1. open another instanc of the project in VS
            // 2. uncomment the lines below
            // 3. close and reopen the problematic XAML window

            //Debugger.Launch();
            //while (!Debugger.IsAttached)
            //  Thread.Sleep(100);

            return AppBuilder.Configure<DesktopUI2.App>()
            .UsePlatformDetect()
            .With(new X11PlatformOptions { UseGpu = false })
            .With(new AvaloniaNativePlatformOptions { UseGpu = false, UseDeferredRendering = true })
            .With(new MacOSPlatformOptions { ShowInDock = false, DisableDefaultApplicationMenuItems = true, DisableNativeMenus = true })
            .With(new Win32PlatformOptions { AllowEglInitialization = true, EnableMultitouch = false })
            .With(new SkiaOptions { MaxGpuResourceSizeBytes = 8096000 })
            .LogToTrace()
            .UseReactiveUI();
        }

    }
}
