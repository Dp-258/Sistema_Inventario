using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCInventario.Models
{
    public class CLIENTE
    {
        public int Id { get; set; }

        [Display(Name = "Cédula")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Ingrese un número de cédula válido")]
        [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage = "Ingrese un número de cédula válido")]
        [Required(ErrorMessage = "Es necesario ingresar una cédula de identidad válida")]
        public string CEDULACLIENTE { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Ingrese un nombre válido")]
        [RegularExpression(@"^[a-záéíóúüñçA-Z''-'\s]{10,40}$", ErrorMessage = "Ingrese un nombre válido")]
        [Required(ErrorMessage = "Es necesario ingresar un nombre")]
        public string NOMBRECLIENTE { get; set; }


    }
}
