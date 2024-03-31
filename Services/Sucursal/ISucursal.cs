using GestionSurcusalesApp.Models;

namespace GestionSurcusalesApp.Services
{
   public  interface ISucursal
   {
        Task<PaginatedResponse<Sucursal>> LoadSucursal();

        Task<Response> CreateSucursal(Sucursal sucursal);

        Task<Response> UpdateSucursal(Sucursal sucursal);

        Task<Response> DeleteSucursal(int sucursalId);
   }
}
