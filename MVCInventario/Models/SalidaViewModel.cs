using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCInventario.Models
{
   
        public class SalidaViewModel
        {
            public List<SALIDA> Salidas { get; set; }
            public SALIDA Salida { get; set; }
            public SelectList NomL { get; set; }
            public SelectList ProL { get; set; }
            public string Productos { get; set; }
            public string Clientes { get; set; }

            public int Id { get; set; }

            public PRODUCTO Producto{ get; set; }

        }
    }
