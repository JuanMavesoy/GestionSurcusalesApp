namespace GestionSurcusalesApp.Views;

public partial class UserPage : ContentPage
{
	public UserPage()
	{
		InitializeComponent();
	}

    private async void CerrarSesionClicked(object sender, EventArgs e)
    {

        SecureStorage.Remove("auth_token");

        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}