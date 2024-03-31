using GestionSurcusalesApp.Models;
using Newtonsoft.Json;
using System.Text;


namespace GestionSurcusalesApp.Services
{
    public class RegistraUsuarioService : IUsuario
    {
        public async Task<Response> CreateUser(string Identificacion, string Nombres, string Apellidos, string UserName, string Password)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                // Mostrar mensaje de falta de conectividad
                return null;
            }

            using (var client = new HttpClient())
            {
                var loginRequest = new { identificacion = Identificacion, nombres = Nombres, apellidos = Apellidos, username= UserName, password= Password };
                var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://192.168.1.12:5038/User/CrearUsuario", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<Response>(jsonResponse);
                    return new Response {Code=200, Message = responseObject.Message};
                }
                else
                {
                    // Manejar la respuesta no exitosa aquí
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return new Response { Code = 0, Message = $"Error al crear usuario: {errorResponse}" };
                }

            }
        }
    }
}
