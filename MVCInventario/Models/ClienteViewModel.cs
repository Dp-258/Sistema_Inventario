using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCInventario.Models
{
    public class ClienteViewModel
    {
        //Lista de clientes
        public List<CLIENTE> Clientes { get; set; }
        public string CedString { get; set; }
    }
}
