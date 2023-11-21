using CommunityToolkit.Maui;
using Mensa_App.MVVM.Models;
using Mensa_App.MVVM.Services;
using Mensa_App.MVVM.View;
using Mensa_App.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mensa_App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()   
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().UseMauiCommunityToolkit().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}