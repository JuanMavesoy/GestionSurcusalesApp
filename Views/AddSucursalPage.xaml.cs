using GestionSurcusalesApp.ViewModel.Sucursal;

namespace GestionSurcusalesApp.Views;

public partial class AddSucursalPage : ContentPage
{
    private readonly SucursalPageViewModel sucursalPageViewModel = new SucursalPageViewModel();
    public AddSucursalPage()
	{
		InitializeComponent();
		this.BindingContext = sucursalPageViewModel;
	}
}