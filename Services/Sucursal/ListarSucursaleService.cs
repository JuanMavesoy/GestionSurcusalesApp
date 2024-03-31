using GestionSurcusalesApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GestionSurcusalesApp.Services
{
    public class SucursaleService : ISucursal
    {
        private readonly string _baseUri = "http://192.168.1.12:5038";

        public async Task<PaginatedResponse<Sucursal>> LoadSucursal()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    Debug.WriteLine("No hay acceso a Internet.");
                    return null;
                }

                using (var client = new HttpClient())
                {
                    var token = await SecureStorage.GetAsync("auth_token");
                    if (string.IsNullOrEmpty(token))
                    {
                        Debug.WriteLine("Token no disponible.");
                        return null;
                    }

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = await client.GetAsync($"{_baseUri}/Sucursal");

                    if (!response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine($"Respuesta no exitosa: {response.StatusCode}");
                        return null;
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var paginatedResponse = JsonConvert.DeserializeObject<PaginatedResponse<Sucursal>>(json);
                    return paginatedResponse;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar sucursales: {ex.Message}");
                return null;
            }
        }


        public async Task<Response> CreateSucursal(Sucursal sucursal)
        {

            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    return new Response { Code = 500, Message = "No hay acceso a Internet." };
                }

                using (var client = new HttpClient())
                {
                    var token = await SecureStorage.GetAsync("auth_token");
                    if (string.IsNullOrEmpty(token))
                    {
                        return new Response { Code = 500, Message = "Token no disponible." };
                    }

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var json = JsonConvert.SerializeObject(sucursal);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{_baseUri}/Sucursal/CrearSucursal", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        return new Response { Code = 500, Message = error };
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    return new Response { Code = 200, Message = "Sucursal creada con éxito." };
                }
            }
            catch (Exception ex)
            {
                return new Response { Code = 500, Message = ex.Message };
            }
        
        }

        public async Task<Response> UpdateSucursal(Sucursal sucursal)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    return new Response { Code = 500, Message = "No hay acceso a Internet." };
                }

                using (var client = new HttpClient())
                {
                    var token = await SecureStorage.GetAsync("auth_token");
                    if (string.IsNullOrEmpty(token))
                    {
                        return new Response { Code = 500, Message = "Token no disponible." };
                    }

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var json = JsonConvert.SerializeObject(sucursal);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"{_baseUri}/Sucursal/ActualizarSucursal", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        return new Response { Code = (int)response.StatusCode, Message = error };
                    }

                    return new Response { Code = (int)response.StatusCode, Message = "Sucursal actualizada con éxito." };
                }
            }
            catch (Exception ex)
            {
                return new Response { Code = 500, Message = ex.Message };
            }
        }

        public async Task<Response> DeleteSucursal(int sucursalId)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    return new Response { Code = 500, Message = "No hay acceso a Internet." };
                }

                using (var client = new HttpClient())
                {
                    var token = await SecureStorage.GetAsync("auth_token");
                    if (string.IsNullOrEmpty(token))
                    {
                        return new Response { Code = 500, Message = "Token no disponible." };
                    }

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = await client.DeleteAsync($"{_baseUri}/Sucursal/EliminarSucursal/{sucursalId}");

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        return new Response { Code = (int)response.StatusCode, Message = error };
                    }

                    return new Response { Code = (int)response.StatusCode, Message = "Sucursal eliminada con éxito." };
                }
            }
            catch (Exception ex)
            {
                return new Response { Code = 500, Message = ex.Message };
            }
        }


    }
}
