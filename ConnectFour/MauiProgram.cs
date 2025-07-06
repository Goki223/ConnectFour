using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace ConnectFour
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif


            builder.ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
                events.AddWindows(windows =>
                {
                    windows.OnWindowCreated(window =>
                    {
                        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
                        var appWindow = AppWindow.GetFromWindowId(windowId);

                        
                        appWindow.Resize(new SizeInt32
                        {
                            Width = 800,
                            Height = 1100
                        });
                    });
                });
#endif
            });

            return builder.Build();
        }
    }
}
