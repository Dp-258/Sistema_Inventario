using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace MVCInventario.Models
{
    public class SALIDA
    {
        public int id { get; set; }

        [Display(Name = "CLIENTE")]
        [Required(ErrorMessage = "Es necesario ingresar un proveedor  ")]

        public int IDCLIENTE { get; set; }
        [Display(Name = "Producto")]
        [Required(ErrorMessage = "Es necesario ingresar un producto ")]

        public int IDPRODUCTO { get; set; }
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Es necesario ingresar una fecha ")]
        [DataType(DataType.Date)]
        public DateTime FECHAREGISTROSALIDA { get; set; }
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Es necesario ingresar una cantidad")]

        public int CANTIDADSALIDA { get; set; }
        [Display(Name = "Total")]
        [Column(TypeName = "decimal(8,2)")]

        public decimal MONTOTOTALSALIDA { get; set; } 


        [NotMapped]
        [Display(Name = "Cliente")]
        public string cliente { get; set; }
       
        [NotMapped]
        [Display(Name = "Producto")]
        public string producto { get; set; }
       
        
       
    }
}
