using System.Collections.Generic;

namespace MVCInventario.Models
{
    public class ProductoViewModel
    {
        public List<PRODUCTO> Productos { get; set; }
        public string codigoString { get; set; }
        public string nombreString { get; set; }
        public int stockInt { get; set; }
        public decimal precioDecimal { get; set; }
        public string categoriaString { get; set; }
    }
}
