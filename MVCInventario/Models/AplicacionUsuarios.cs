using Microsoft.AspNetCore.Identity;

namespace MVCInventario.Models
{
    public class AplicacionUsuarios : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

    }
}
