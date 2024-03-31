using GestionSurcusalesApp.ViewModel;
using GestionSurcusalesApp.Views;

namespace GestionSurcusalesApp;

public partial class LoginPage : ContentPage
{

	public LoginPage(LoginPageViewModel loginPageViewModel)
	{
		InitializeComponent();
        this.BindingContext = loginPageViewModel;
	}
}