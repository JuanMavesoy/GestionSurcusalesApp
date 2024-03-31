using GestionSurcusalesApp.ViewModel.Usuario;

namespace GestionSurcusalesApp.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(UserPageViewModel userPageViewModel)
	{
		InitializeComponent();
        this.BindingContext = userPageViewModel;
	}
}