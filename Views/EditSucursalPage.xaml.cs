using GestionSurcusalesApp.Models;
using GestionSurcusalesApp.Services;

namespace GestionSurcusalesApp.Views;

public partial class EditSucursalPage : ContentPage
{
    private Sucursal currentSucursal;
    private readonly ISucursal _sucursalService;
    public EditSucursalPage(Sucursal sucursal)
	{
		InitializeComponent();
        currentSucursal = sucursal;
        _sucursalService = new SucursaleService();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        NombreEntry.Text = currentSucursal.Nombre;
        DireccionEntry.Text = currentSucursal.Direccion;
        TelefonoEntry.Text = currentSucursal.Telefono;
        CorreoElectronicoEntry.Text = currentSucursal.CorreoElectronico;
        HorarioAtencionEntry.Text = currentSucursal.HorarioAtencion;
        GerenteSucursalEntry.Text = currentSucursal.GerenteSucursal;
        MonedaPicker.SelectedItem = currentSucursal.Moneda.ToString();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {

        currentSucursal.Nombre = NombreEntry.Text;
        currentSucursal.Direccion = DireccionEntry.Text;
        currentSucursal.Telefono = TelefonoEntry.Text;
        currentSucursal.CorreoElectronico = CorreoElectronicoEntry.Text;
        currentSucursal.HorarioAtencion = HorarioAtencionEntry.Text;
        currentSucursal.GerenteSucursal = GerenteSucursalEntry.Text;
        currentSucursal.Moneda = (Moneda)Enum.Parse(typeof(Moneda), MonedaPicker.SelectedItem.ToString());


        Response response = await _sucursalService.UpdateSucursal(currentSucursal);
        if (response.Code == 200) 
        {
            await DisplayAlert("Éxito", "Sucursal actualizada con éxito", "OK");
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");

        }
        else
        {
            await DisplayAlert("Error", $"No se pudo actualizar la sucursal: {response.Message}", "OK");
        }
    }
}