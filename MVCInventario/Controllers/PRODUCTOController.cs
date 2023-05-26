using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCInventario.Data;
using MVCInventario.Models;
using MVCInventario.Views.Shared.Components.Buscador;

namespace MVCInventario.Controllers
{
    [Authorize]
    public class PRODUCTOController : Controller
    {
        private readonly MVCInventarioContext _context;
        public async Task<IActionResult> LeerArchivoCSVAsync(IFormFile archivo)
        {
            List<PRODUCTO> productos = new List<PRODUCTO>();

            if (archivo != null && archivo.Length > 0)
            {
                using (var reader = new StreamReader(archivo.OpenReadStream()))
                {
                    // Ignoramos la primera línea si contiene encabezados
                    var encabezados = reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var valores = linea.Split(',');

                        var codigo = valores[0];
                        var nombre = valores[1];
                        var descripcion = valores[2];
                        var stock = 0;
                        var precio = Convert.ToDecimal(valores[4]);
                        var categoria = valores[5];
                        var imagen = valores[6];
                        if (codigo != null)
                        {
                            PRODUCTO val = await _context.PRODUCTO.SingleOrDefaultAsync(p => p.CODIGOPRODUCTO == codigo);
                            if (val != null)
                            {

                            }
                        }
                        var producto = new PRODUCTO
                        {
                            CODIGOPRODUCTO = codigo,
                            NOMBREPRODUCTO = nombre,
                            DESCRIPCIONPRODUCTO = descripcion,
                            STOCKPRODUCTO = stock,
                            PVPPRODUCTO = precio,
                            CATEGORIAPRODUCTO = categoria,
                            FOTOPRODUCTO = imagen
                        };

                        productos.Add(producto);
                    }
                }

                // Guardar los productos en la base de datos
                foreach (var producto in productos)
                {
                    _context.PRODUCTO.Add(producto);
                }

                _context.SaveChanges();
            }

            // Construir el modelo ProductoViewModel
            ProductoViewModel viewModel = new ProductoViewModel
            {
                Productos = productos
            };

            // Pasar el modelo a la vista
            return View(viewModel);
        }
        public PRODUCTOController(MVCInventarioContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Jefe, Operador, Administrador")]        // GET: PRODUCTO
        public async Task<IActionResult> Index(ProductoViewModel modelo)
        {
            var Productos = _context.PRODUCTO.Select(p => p);
            if (!String.IsNullOrEmpty(modelo.codigoString))
            {
                Productos = Productos.Where(p => p.CODIGOPRODUCTO.Contains(modelo.codigoString));
            }

            if (!String.IsNullOrEmpty(modelo.nombreString))
            {
                Productos = Productos.Where(p => p.NOMBREPRODUCTO.Contains(modelo.nombreString));
            }

            if (!String.IsNullOrEmpty(modelo.categoriaString))
            {
                Productos = Productos.Where(p => p.CATEGORIAPRODUCTO.Contains(modelo.categoriaString));
            }
            
            modelo.Productos = await Productos.ToListAsync();
            return View(modelo);
        }

        // GET: PRODUCTO/Details/5
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pRODUCTO = await _context.PRODUCTO
                .FirstOrDefaultAsync(m => m.id == id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }

           

            return View(pRODUCTO);
        }

        // GET: PRODUCTO/Create
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public IActionResult Create()
        {

            return View();
        }

        // POST: PRODUCTO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public async Task<IActionResult> Create([Bind("id,CODIGOPRODUCTO,NOMBREPRODUCTO,DESCRIPCIONPRODUCTO,STOCKPRODUCTO,PVPPRODUCTO,CATEGORIAPRODUCTO,FOTOPRODUCTO")] PRODUCTO pRODUCTO)
        {
            if (ModelState.IsValid)
            {
                PRODUCTO val = await _context.PRODUCTO
                        .SingleOrDefaultAsync(p => p.CODIGOPRODUCTO == pRODUCTO.CODIGOPRODUCTO);
                if (val == null)
                {
                    _context.Add(pRODUCTO);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Si hay un error, muestra una ventana emergente con el mensaje de error
                    TempData["ErrorMessage"] = "No se puede crear el producto, debido a que ya existe un producto con esta clave.";
                    // Retorna a la vista anterior
                    return RedirectToAction("Create");
                }


            }
            return View(pRODUCTO);
        }

        // GET: PRODUCTO/Edit/5
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pRODUCTO = await _context.PRODUCTO.FindAsync(id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }
            return View(pRODUCTO);
        }

        // POST: PRODUCTO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("id,CODIGOPRODUCTO,NOMBREPRODUCTO,DESCRIPCIONPRODUCTO,STOCKPRODUCTO,PVPPRODUCTO,CATEGORIAPRODUCTO,FOTOPRODUCTO")] PRODUCTO pRODUCTO)
        {
            if (id != pRODUCTO.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var val = await _context.PRODUCTO.AsNoTracking()
                        .Where(p => p.CODIGOPRODUCTO == pRODUCTO.CODIGOPRODUCTO && p.id!= pRODUCTO.id).ToListAsync();
                if (val.Count == 0)
                { 
                        try
                    {
                        _context.Update(pRODUCTO);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PRODUCTOExists(pRODUCTO.id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Si hay un error, muestra una ventana emergente con el mensaje de error
                    TempData["ErrorMessage"] = "No se puede editar el producto, debido a que ya existe un producto con esta clave.";
                    // Retorna a la vista anterior
                    return RedirectToAction("Edit");
                }
            }
            return View(pRODUCTO);
        }

        // GET: PRODUCTO/Delete/5
        [Authorize(Roles = "Jefe, Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pRODUCTO = await _context.PRODUCTO
                .FirstOrDefaultAsync(m => m.id == id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }
            
            return View(pRODUCTO);
        }

        // POST: PRODUCTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe,Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var pRODUCTO = await _context.PRODUCTO.FindAsync(id);
                _context.PRODUCTO.Remove(pRODUCTO);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                string errorMessage = "No se puede eliminar el producto, debido a que se encuentra siendo utilizado en otra tabla.";
                TempData["ErrorMessage"] = errorMessage;
                // Retorna a la vista anterior
                return RedirectToAction("Delete");
            }


            return RedirectToAction(nameof(Index));
        }

        private bool PRODUCTOExists(int id)
        {
            return _context.PRODUCTO.Any(e => e.id == id);
        }

        public IActionResult Invente(string ParametroBusc = "", int pg = 1)
        {
            List<PRODUCTO> Productos;
            bool busqueda = false;
            if (ParametroBusc != "" && ParametroBusc != null)
            {
                Productos = _context.PRODUCTO.Where(p => p.CODIGOPRODUCTO.Contains(ParametroBusc)).ToList();
                busqueda = true;
            }
            else
                Productos = _context.PRODUCTO.ToList();

            if (!Productos.Any() && busqueda)
            {
                TempData["ErrorMessage"] = "No existe un producto con estos datos.";
                Productos = _context.PRODUCTO.ToList();
            }

            const int TamanoPagina = 5;
            if (pg < 1)
                pg = 1;

            int recsCont = Productos.Count();

            var pager = new Pager(recsCont, pg, TamanoPagina);

            int recSkip = (pg - 1) * TamanoPagina;

            List<PRODUCTO> retProductos = Productos.Skip(recSkip).Take(pager.iTamanoPagina).ToList();

            this.ViewBag.Pager = pager;

            SPager SearchPager = new SPager() { Accion = "Invente", Controlador = "PRODUCTO", TextoBusqueda = "ParametroBusc" };

            ViewBag.SearchPager = SearchPager;

            return View(retProductos);
        }
        public IActionResult GetUserInfo(string id)
        {
            var user = _context.PRODUCTO.FirstOrDefault(u => u.CODIGOPRODUCTO == id);

            if (user == null)
            {
                return Json(null);
            }

            return Json(new { name = user.CODIGOPRODUCTO });
        }
    }
}
