using GestionSurcusalesApp.Models;
using GestionSurcusalesApp.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace GestionSurcusalesApp.ViewModel.Usuario
{
    public partial class UserPageViewModel : BaseViewModel
    {
        private readonly IUsuario _UsuserioRepository = new RegistraUsuarioService();

        [ObservableProperty]
        private string _identificacion;

        [ObservableProperty]
        private string _nombres;

        [ObservableProperty]
        private string _apellidos;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [ICommand]
        public async void Register()
        {
            if (string.IsNullOrWhiteSpace(Identificacion) || string.IsNullOrWhiteSpace(Nombres) || string.IsNullOrWhiteSpace(Apellidos) || string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Por favor, introduzca la información completa", "Ok");
                return;
            }

            Response res = await _UsuserioRepository.CreateUser(Identificacion, Nombres, Apellidos, UserName, Password);

            if (res.Code == 200)
            {
                // Limpiar los campos después de un registro exitoso
                Identificacion = string.Empty;
                Nombres = string.Empty;
                Apellidos = string.Empty;
                UserName = string.Empty;
                Password = string.Empty;

                await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario registrado con éxito.", "OK");
                return;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al registrar usuario: {res.Message}", "OK");
                return;
            }

        }
    }
}
