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
        [Required(ErrorMessage = "Es necesario ingresar un código")]
        public string CODIGOPRODUCTO { get; set; }
        [Display(Name = "Nombre")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Ingrese un nombre válido")]
        [Required(ErrorMessage = "Es necesario ingresar un nombre")]
        public string NOMBREPRODUCTO { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Es necesario ingresar una descripción")]
        public string DESCRIPCIONPRODUCTO { get; set; }
        [Display(Name = "Stock")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese un stock válido")]
        public int STOCKPRODUCTO { get; set; } 
        [Display(Name = "Precio")]
        [RegularExpression(@"^[0-9.]+$", ErrorMessage = "Ingrese un precio válido")]
        public decimal PVPPRODUCTO { get; set; }
        [Display(Name = "Categoría")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ingrese una categoría válida")]
        [RegularExpression(@"^[a-záéíóúüñçA-Z''-'\s]{3,40}$", ErrorMessage = "Ingrese una categoría válida")]
        [Required(ErrorMessage = "Es necesario ingresar una categoría")]
        public string CATEGORIAPRODUCTO { get; set; }
        [Display(Name = "Imagen")]
        public string FOTOPRODUCTO { get; set; }

    }
}
