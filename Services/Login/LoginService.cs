using GestionSurcusalesApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;


namespace GestionSurcusalesApp.Services
{
	public class LoginService : ILoginRepository
	{
        public async Task<UserInfo> Login(string username, string password)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                // Mostrar mensaje de falta de conectividad por ahora pongo un null 
                return null;
            }

            using (var client = new HttpClient())
            {
                var loginRequest = new { userName = username, password = password };
                var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://192.168.1.12:5038/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var Response = JsonConvert.DeserializeObject <UserInfo>(json);
                return new UserInfo { Token = Response.Token };
            }
        }

    }
}
