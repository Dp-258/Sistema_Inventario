using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCInventario.Models;

namespace MVCInventario.Data
{
    public class MVCInventarioContext : IdentityDbContext
    {
        public MVCInventarioContext (DbContextOptions<MVCInventarioContext> options)
            : base(options)
        {
        }

        public DbSet<MVCInventario.Models.CLIENTE> CLIENTE { get; set; }

        public DbSet<MVCInventario.Models.PROVEEDOR> PROVEEDOR { get; set; }

        public DbSet<MVCInventario.Models.USUARIO> USUARIO { get; set; }
        public DbSet<MVCInventario.Models.PRODUCTO> PRODUCTO { get; set; }
        public DbSet<MVCInventario.Models.ENTRADA> ENTRADA { get; set; }
        public DbSet<MVCInventario.Models.SALIDA> SALIDA { get; set; }
        public DbSet<MVCInventario.Models.AplicacionUsuarios> AplicacionUsuarios { get; set; }

    }
}



