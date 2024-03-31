namespace GestionSurcusalesApp.Models
{
    public class Sucursal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string HorarioAtencion { get; set; }
        public string GerenteSucursal { get; set; }
        public Moneda Moneda { get; set; }
    }

    public enum Moneda
    {
        USD, // Dólar estadounidense
        COP  // Peso colombiano
    }



}
