using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MVCInventario.Models
{
    public class PRODUCTO
    {
        public int id { get; set; }
        [Display(Name = "Código")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Ingrese un código válido")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese un código válido")]
        [Required(ErrorMessage = "Es necesario ingresar un código")]
        public string CODIGOPRODUCTO { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Ingrese un nombre válido")]
        [Required(ErrorMessage = "Es necesario ingresar un nombre")]
        public string NOMBREPRODUCTO { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Es necesario ingresar una descripción")]
        public string DESCRIPCIONPRODUCTO { get; set; }

        [Display(Name = "Stock")]
        public int STOCKPRODUCTO { get; set; }

        [Display(Name = "Precio")]
        [RegularExpression(@"^\d+(.\d+)?$", ErrorMessage = "Ingrese un precio que sea un número con dos decimales")]
        [Required(ErrorMessage = "Es necesario ingresar un precio")]
        public decimal PVPPRODUCTO { get; set; }

        [Display(Name = "Categoría")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ingrese una categoría válida")]
        [RegularExpression(@"^[a-záéíóúüñçA-Z''-'\s]{3,40}$", ErrorMessage = "Ingrese una categoría válida ")]
        [Required(ErrorMessage = "Es necesario ingresar una categoría")]
        public string CATEGORIAPRODUCTO { get; set; }

        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "Es necesario ingresar una imagen")]
        public string FOTOPRODUCTO { get; set; }

    }
}
