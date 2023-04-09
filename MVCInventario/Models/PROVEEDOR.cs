using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MVCInventario.Models
{
    public class PROVEEDOR
    {
        public int Id { get; set; }

        [Display(Name = "Cédula")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Ingrese un número de cédula válido")]
        [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage = "Ingrese un número de cédula válido")]
        [Required(ErrorMessage = "Es necesario ingresar una cédula de indentidad")]
        public string CEDULAPROVEEDOR { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Ingrese un nombre válido")]
        [RegularExpression(@"^[a-záéíóúüñçA-Z''-'\s]{10,40}$", ErrorMessage = "Ingrese un nombre válido")]
        [Required(ErrorMessage = "Es necesario ingresar un nombre")]
        public string NOMBREPROVEEDOR { get; set; }
        [Display(Name = "Dirección")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Por favor, detalle un poco más la dirección")]
        [Required(ErrorMessage = "Es necesario ingresar una dirección")]
        public string DIRECCIONPROVEEDOR { get; set; }
        [Display(Name = "Email")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "Ingrese un correo válido")]
        [Required(ErrorMessage = "Es necesario ingresar el corrreo electronico del proovedor")]
        public string CORREOPROVEEDOR { get; set; }
        [Display(Name = "Ciudad")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Por favor, ingrese una ciudad válida")]
        [Required(ErrorMessage = "Es necesario ingresar la ciudad del proveedor")]
        public string CIUDADPROVEEDOR { get; set; }

    }
}
