using GestionSurcusalesApp.Models;
using GestionSurcusalesApp.Services;
using GestionSurcusalesApp.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace GestionSurcusalesApp.ViewModel.Sucursal
{
    public partial class SucursalPageViewModel : BaseViewModel
    {
        private readonly ISucursal _SucursalRepository = new SucursaleService();


        [ObservableProperty]
        private string _nombre;

        [ObservableProperty]
        private string _direccion;

        [ObservableProperty]
        private string _telefono;

        [ObservableProperty]
        private string _correoElectronico;

        [ObservableProperty]
        private string _horarioAtencion;

        [ObservableProperty]
        private string _GerenteSucursal;

        [ObservableProperty]
        private int _moneda;

        [ICommand]
        public async void SaveSucursal()
        {
            if (string.IsNullOrWhiteSpace(Nombre) ||
               string.IsNullOrWhiteSpace(Direccion) ||
               string.IsNullOrWhiteSpace(Telefono) ||
               string.IsNullOrWhiteSpace(CorreoElectronico) ||
               string.IsNullOrWhiteSpace(HorarioAtencion) ||
               string.IsNullOrWhiteSpace(GerenteSucursal))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Por favor, introduzca la información completa", "Ok");
                return;
            }

                var newSucursal = new GestionSurcusalesApp.Models.Sucursal
            {
                Nombre = this.Nombre,
                Direccion = this.Direccion,
                Telefono = this.Telefono,
                CorreoElectronico = this.CorreoElectronico,
                HorarioAtencion = this.HorarioAtencion,
                GerenteSucursal = this.GerenteSucursal,
                Moneda = (Moneda)this.Moneda // Asegúrate de que esto coincida con tus definiciones de enum
            };

            Response res = await _SucursalRepository.CreateSucursal (newSucursal);


            if (res.Code == 200)
            {
                Nombre = string.Empty;
                Direccion = string.Empty;
                Telefono = string.Empty;
                CorreoElectronico = string.Empty;
                HorarioAtencion = string.Empty;
                GerenteSucursal = string.Empty;

                await Application.Current.MainPage.DisplayAlert("Éxito", "Sucursal registrada con éxito.", "OK");
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                return;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al registrar la sucursal: {res.Message}", "OK");
                return;
            }
        }

    }
}
