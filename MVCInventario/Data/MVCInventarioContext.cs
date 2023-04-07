using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCInventario.Models;

namespace MVCInventario.Data
{
    public class MVCInventarioContext : DbContext
    {
        public MVCInventarioContext (DbContextOptions<MVCInventarioContext> options)
            : base(options)
        {
        }

        public DbSet<MVCInventario.Models.CLIENTE> CLIENTE { get; set; }

        public DbSet<MVCInventario.Models.PROVEEDOR> PROVEEDOR { get; set; }

        public DbSet<MVCInventario.Models.USUARIO> USUARIO { get; set; }
    }
}
