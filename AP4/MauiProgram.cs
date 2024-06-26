﻿using AP4.Controls;
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
                .UseLocalNotification()
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

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
            {
                if(view is BorderlessEntry)
                {
#if ANDROID
                    handler.PlatformView.Background = null;
                    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.White);
#endif
                }
            });

            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
            {
                if (view is BorderlessDatePicker)
                {
#if ANDROID
                    handler.PlatformView.Background = null;
                    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.White);
#endif
                }
            });

#if DEBUG
                builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<LoginService>();
            builder.Services.AddSingleton<RegisterService>();
            builder.Services.AddSingleton<CommandeService>();
            builder.Services.AddSingleton<CategorieService>();
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<UserService>();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<ConnectionViewModel>();
            builder.Services.AddSingleton<InscriptionViewModel>();
            builder.Services.AddSingleton<CommandeViewModel>();
            builder.Services.AddSingleton<PlusViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ConnectionView>();
            builder.Services.AddTransient<InscriptionView>();
            builder.Services.AddSingleton<CommandeView>();
            builder.Services.AddSingleton<ProductPriceView>();
            builder.Services.AddSingleton<AchatView>();
            builder.Services.AddSingleton<PlusView>();

            return builder.Build();
        }
    }
}
