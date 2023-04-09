using System.Collections.Generic;

namespace MVCInventario.Models
{
    public class ProveedorViewModel
    {
        //Lista de clientes
        public List<PROVEEDOR> Proveedores { get; set; }
        public string CedString { get; set; }
        public string NomString { get; set; }
        public string DirString { get; set; }
        public string EmailString { get; set; }
        public string CiuString { get; set; }


    }
}
