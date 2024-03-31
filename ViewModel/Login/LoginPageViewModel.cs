using GestionSurcusalesApp.Models;
using GestionSurcusalesApp.Services;
using GestionSurcusalesApp.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;


namespace GestionSurcusalesApp.ViewModel
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly ILoginRepository _loginRepository = new LoginService();

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

  
        [ICommand]
        public async void Login()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Por favor, introduzca nombre de usuario y contraseña", "Ok");
                return;
            }

            UserInfo userInfo = await _loginRepository.Login(UserName, Password);

            if (userInfo == null)
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Por favor, introduzca nombre de usuario y contraseña válidos", "Ok");
                return;
            }

            await SecureStorage.SetAsync("auth_token", userInfo.Token);
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            UserName = string.Empty; 
            Password = string.Empty;
        }

        [ICommand]
        private async void NavigateToRegisterPage()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

    }
}
