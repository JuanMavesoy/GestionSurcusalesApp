using GestionSurcusalesApp.Models;
using GestionSurcusalesApp.Services;

namespace GestionSurcusalesApp.Views;

public partial class HomePage : ContentPage
{
    private readonly ISucursal _sucursalService;

    public HomePage()
    {
        InitializeComponent();
        _sucursalService = new SucursaleService();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadSucursales();
    }

    private async Task LoadSucursales()
    {
        var paginatedResponse = await _sucursalService.LoadSucursal();
        if (paginatedResponse != null)
        {
            SucursalesListView.ItemsSource = paginatedResponse.Data;
        }
        else
        {
            await DisplayAlert("Warning", "No Hay sucursales creadas", "Ok");
        }
    }


    private async void OnSucursalSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Sucursal selectedSucursal)
        {
            string action = await DisplayActionSheet(
            "Acciones",
            "Cancelar",
            null,
            "Editar",
            "Eliminar");

            switch (action)
            {
                case "Editar":
                    await Navigation.PushAsync(new EditSucursalPage(selectedSucursal));
                    break;
                case "Eliminar":
                    bool isConfirmed = await DisplayAlert(
                        "Confirmar",
                        "¿Deseas eliminar la sucursal?",
                        "Sí", "No");
                    if (isConfirmed)
                    {
                        var response = await _sucursalService.DeleteSucursal(selectedSucursal.Id);
                        if (response.Code == 200)
                        {
                            await LoadSucursales(); 
                        }
                        else
                        {
                            await DisplayAlert("Error", response.Message, "OK");
                        }
                    }
                    break;
            }
        }
        ((ListView)sender).SelectedItem = null;
    }

    private async void OnAddSucursalClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddSucursalPage());
    }
}