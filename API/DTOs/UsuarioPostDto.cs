using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UsuarioPostDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
