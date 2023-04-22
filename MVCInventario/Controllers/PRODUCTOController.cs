using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCInventario.Data;
using MVCInventario.Models;

namespace MVCInventario.Controllers
{
    public class PRODUCTOController : Controller
    {
        private readonly MVCInventarioContext _context;

        public PRODUCTOController(MVCInventarioContext context)
        {
            _context = context;
        }

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
            foreach (Models.PRODUCTO item in Productos)
            {
                var entrada = _context.ENTRADA.Where(p => p.IDPRODUCTO == item.id);
                var salida = _context.SALIDA.Where(p => p.IDPRODUCTO == item.id);
                foreach (var e in entrada)
                {
                    item.STOCKPRODUCTO += e.CANTIDADPENTRADA;
                }
                foreach (var e in salida)
                {
                    item.STOCKPRODUCTO -= e.CANTIDADSALIDA;
                }
            }
            modelo.Productos = await Productos.ToListAsync();
            return View(modelo);
        }

        // GET: PRODUCTO/Details/5
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

            var entrada = _context.ENTRADA.Where(p => p.IDPRODUCTO == pRODUCTO.id);
            var salida = _context.SALIDA.Where(p => p.IDPRODUCTO == pRODUCTO.id);
            foreach (var e in entrada)
            {
                pRODUCTO.STOCKPRODUCTO += e.CANTIDADPENTRADA;
            }
            foreach (var e in salida)
            {
                pRODUCTO.STOCKPRODUCTO += e.CANTIDADSALIDA;
            }

            return View(pRODUCTO);
        }

        // GET: PRODUCTO/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PRODUCTO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,CODIGOPRODUCTO,NOMBREPRODUCTO,DESCRIPCIONPRODUCTO,STOCKPRODUCTO,PVPPRODUCTO,CATEGORIAPRODUCTO,FOTOPRODUCTO")] PRODUCTO pRODUCTO)
        {
            if (ModelState.IsValid)
            {
                pRODUCTO.STOCKPRODUCTO = 0;
                _context.Add(pRODUCTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pRODUCTO);
        }

        // GET: PRODUCTO/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("id,CODIGOPRODUCTO,NOMBREPRODUCTO,DESCRIPCIONPRODUCTO,STOCKPRODUCTO,PVPPRODUCTO,CATEGORIAPRODUCTO,FOTOPRODUCTO")] PRODUCTO pRODUCTO)
        {
            if (id != pRODUCTO.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
            return View(pRODUCTO);
        }

        // GET: PRODUCTO/Delete/5
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
            var entrada = _context.ENTRADA.Where(p => p.IDPRODUCTO == pRODUCTO.id);
            var salida = _context.SALIDA.Where(p => p.IDPRODUCTO == pRODUCTO.id);
            foreach (var e in entrada)
            {
                pRODUCTO.STOCKPRODUCTO += e.CANTIDADPENTRADA;
            }
            foreach (var e in salida)
            {
                pRODUCTO.STOCKPRODUCTO += e.CANTIDADSALIDA;
            }
            return View(pRODUCTO);
        }

        // POST: PRODUCTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pRODUCTO = await _context.PRODUCTO.FindAsync(id);
            _context.PRODUCTO.Remove(pRODUCTO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PRODUCTOExists(int id)
        {
            return _context.PRODUCTO.Any(e => e.id == id);
        }
    }
}
