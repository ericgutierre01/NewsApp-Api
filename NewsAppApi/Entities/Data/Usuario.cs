using System.ComponentModel.DataAnnotations;

namespace NewsAppApi.Entities.Data
{
    public class Usuario
    {
        [Key]
        public int UsuId { get; set; }
        public string UsuSesion { get; set; }
        public string UsuPass { get; set; }
        public string UsuNombre { get; set; }
        public string UsuEmail { get; set; }
        public string UsuApellido { get; set; }
        public string UsuFoto { get; set; }
        public int UsuEstado { get; set; }
    }
}
