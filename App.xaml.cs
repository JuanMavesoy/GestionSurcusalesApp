using GestionSurcusalesApp.Models;

namespace GestionSurcusalesApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
