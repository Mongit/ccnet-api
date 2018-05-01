namespace BO.Proveedor
{
    public class Proveedor : BaseBO
    {
        public string Empresa { get; set; }
        public string Contacto { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string HorarioAtencion { get; set; }
    }
}