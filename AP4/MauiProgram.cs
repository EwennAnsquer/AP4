using Microsoft.Extensions.Logging;

namespace AP4
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("InterBlack.ttf", "InterBlack");
                    fonts.AddFont("InterBold.ttf", "InterBold");
                    fonts.AddFont("InterExtraBold.ttf", "InterExtraBold");
                    fonts.AddFont("InterExtraLight.ttf", "InterExtraLight");
                    fonts.AddFont("InterLight.ttf", "InterLight");
                    fonts.AddFont("InterMedium.ttf", "InterMedium");
                    fonts.AddFont("InterRegular.ttf", "InterRegular");
                    fonts.AddFont("InterSemiBold.ttf", "InterSemiBold");
                    fonts.AddFont("InterThin.ttf", "Inter-Thin");
                });

            #if DEBUG
    		    builder.Logging.AddDebug();
            #endif

            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
