using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace MVCInventario.Models
{
    public class ENTRADA
    {
        public int id { get; set; }
        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "Es necesario ingresar un proveedor  ")]

        public int IDPROVEEDOR { get; set; }
        [Display(Name = "Producto")]
        [Required(ErrorMessage = "Es necesario ingresar un producto ")]

        public int IDPRODUCTO { get; set; }
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Es necesario ingresar una fecha ")]
        [DataType(DataType.Date)]
        public DateTime FECHAREGISTROENTRADA { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Es necesario ingresar una cantidad")]

        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Ingrese un número entero mayor a 0")]
        public int CANTIDADPENTRADA { get; set; }
        [Display(Name = "Total")]
        //esto a 38
        [Column(TypeName = "decimal(38,2)")]
        //[RegularExpression(@"^(?=.[1-9])\d*(.\d{1,2})?$", ErrorMessage = "Ingrese una cantidad positiva")]

        public decimal MONTOTOTALENTRADA { get; set; }
        public int ANULARENTRADA { get; set; }


        [NotMapped]
        [Display(Name = "Proveedor")]
        public string proveedor { get; set; }


        [NotMapped]
        [Display(Name = "Producto")]
        public string producto { get; set; }

    }
}
