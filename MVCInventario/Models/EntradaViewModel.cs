using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCInventario.Models
{
    public class EntradaViewModel
    {
        public List<ENTRADA> Entradas { get; set; }
        public ENTRADA Entrada { get; set; }


        //Lista de categorias
        public SelectList ListaProveedores { get; set; }
        public SelectList ListaProductos { get; set; }

        //BUSQUEDAS
        public string ProveedorSearchString { get; set; }
        public string FechaSearchString { get; set; }
        
        
        public int Id { get; set; }
    }
}
