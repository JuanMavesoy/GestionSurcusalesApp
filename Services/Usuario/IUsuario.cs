using GestionSurcusalesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSurcusalesApp.Services
{
    public interface IUsuario
    {
        Task<Response> CreateUser(string Identificacion, string Nombres, string Apellidos, string UserName, string Password);
    }
}
