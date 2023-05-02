using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCInventario.Models
{
   
        public class SalidaViewModel
        {
            public int Id { get; set; }
            public List<SALIDA> Salidas { get; set; }
            public SALIDA Salida { get; set; }

        //CATEGORIAS
            public SelectList NomL { get; set; }
            public SelectList ProL { get; set; }
        //BUSQUEDAS
            public string ClienteSearchString { get; set; }
            public string FechaSearchString { get; set; }

            public PRODUCTO Producto{ get; set; }

        }
    }
