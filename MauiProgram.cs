using GestionSurcusalesApp.ViewModel;
using GestionSurcusalesApp.ViewModel.Sucursal;
using GestionSurcusalesApp.ViewModel.Usuario;
using GestionSurcusalesApp.Views;
using Microsoft.Extensions.Logging;

namespace GestionSurcusalesApp
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
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<UserPage>();
            builder.Services.AddSingleton<AddSucursalPage>();
            builder.Services.AddSingleton<RegisterPage>();


            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<UserPageViewModel>();
            builder.Services.AddSingleton<SucursalPageViewModel>();

            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
