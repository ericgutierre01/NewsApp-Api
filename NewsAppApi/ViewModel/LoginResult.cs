
namespace NewsAppApi.ViewModel
{
    public class LoginResult
    {
        public int UsuId { get; set; }
        public string UsuSesion { get; set; }
        public string UsuNombre { get; set; }
        public string UsuEmail { get; set; }
        public string UsuApellido { get; set; }
        public string UsuFoto { get; set; }
        public int UsuEstado { get; set; }
        public string Token { get; set; }
    }
}
