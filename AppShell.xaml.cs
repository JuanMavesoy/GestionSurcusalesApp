﻿using GestionSurcusalesApp.Views;

namespace GestionSurcusalesApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

        }
    }
}
