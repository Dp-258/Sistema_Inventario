using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCInventario.Data;
using MVCInventario.Models;

namespace MVCInventario.Controllers
{
    [Authorize]
    public class PRODUCTOController : Controller
    {
        private readonly MVCInventarioContext _context;

        public PRODUCTOController(MVCInventarioContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Jefe, Operador")]
        // GET: PRODUCTO
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
        [Authorize(Roles = "Jefe, Operador")]
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
        [Authorize(Roles = "Jefe, Operador")]
        public IActionResult Create()
        {

            return View();
        }

        // POST: PRODUCTO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe, Operador")]
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
        [Authorize(Roles = "Jefe, Operador")]
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
        [Authorize(Roles = "Jefe, Operador")]
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
        [Authorize(Roles = "Jefe")]
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
        [Authorize(Roles = "Jefe")]
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

        public IActionResult Invente(int pg = 1)
        {
            List<PRODUCTO> Productos = _context.PRODUCTO.ToList();

            const int TamanoPagina = 5;
            if (pg < 1)
                pg = 1;

            int recsCont = Productos.Count();

            var pager = new Pager(recsCont, pg, TamanoPagina);

            int recSkip = (pg - 1) * TamanoPagina;

            var data = Productos.Skip(recSkip).Take(pager.iTamanoPagina).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }
    }
}
