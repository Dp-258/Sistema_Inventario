using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCInventario.Models
{
    public class EntradaViewModel
    {
        public List<ENTRADA> Entradas { get; set; }
        public ENTRADA Entrada { get; set; }
        public SelectList ListaProveedores { get; set; }

        //Lista de categorias
        public SelectList ListaProductos { get; set; }

        public string SearchString { get; set; }
        public string Proveedores { get; set; }
        public string Productos { get; set; }
        
        
        public int Id { get; set; }
    }
}
