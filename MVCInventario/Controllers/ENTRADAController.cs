using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    public class ENTRADAController : Controller
    {
        private readonly MVCInventarioContext _context;

        public ENTRADAController(MVCInventarioContext context)
        {
            _context = context;
        }

        // GET: ENTRADA
        [Authorize(Roles = "Jefe, Operador")]
        public async Task<IActionResult> Index(EntradaViewModel model)
        {
            var entrada = _context.ENTRADA.Select(p => p);
          
            if (!string.IsNullOrEmpty(model.Proveedores))
            {
                PROVEEDOR provTemp = await _context.PROVEEDOR.Where(p => p.NOMBREPROVEEDOR == model.Proveedores).SingleOrDefaultAsync();
                if (provTemp != null)
                {
                    entrada = entrada.Where(p => p.IDPROVEEDOR == provTemp.Id);
                }
            }
            model.ListaProveedores = new SelectList(_context.PROVEEDOR.OrderBy(p => p.NOMBREPROVEEDOR).Select(p => p.NOMBREPROVEEDOR));
            if (!string.IsNullOrEmpty(model.Productos))
            {
                PRODUCTO provTemp = await _context.PRODUCTO.Where(p => p.NOMBREPRODUCTO == model.Productos).SingleOrDefaultAsync();
                if (provTemp != null)
                {
                    entrada = entrada.Where(p => p.IDPRODUCTO == provTemp.id);
                }
            }
            model.ListaProductos = new SelectList(_context.PRODUCTO.OrderBy(p => p.NOMBREPRODUCTO).Select(p => p.NOMBREPRODUCTO));

            foreach (var item in entrada)
            {
                PROVEEDOR provTemp = await _context.PROVEEDOR.Where(p => p.Id == item.IDPROVEEDOR).SingleOrDefaultAsync();
                item.proveedor = provTemp.NOMBREPROVEEDOR;


                PRODUCTO provTemp1 = await _context.PRODUCTO.Where(p => p.id == item.IDPRODUCTO).SingleOrDefaultAsync();
                item.producto = provTemp1.NOMBREPRODUCTO;

               
            }
            model.Entradas = await entrada.ToListAsync();
            return View(model);
        }

        // GET: ENTRADA/Details/5
        [Authorize(Roles = "Jefe, Operador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrada = await _context.ENTRADA
                 .FirstOrDefaultAsync(m => m.id == id);
            PROVEEDOR provTemp = await _context.PROVEEDOR.Where(p => p.Id == entrada.IDPROVEEDOR).SingleOrDefaultAsync();
            entrada.proveedor = provTemp.NOMBREPROVEEDOR;
            PRODUCTO provTemp1 = await _context.PRODUCTO.Where(p => p.id == entrada.IDPRODUCTO).SingleOrDefaultAsync();
            entrada.producto = provTemp1.NOMBREPRODUCTO;
            
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // GET: ENTRADA/Create
        [Authorize(Roles = "Jefe, Operador")]
        public IActionResult Create(EntradaViewModel model)
        {
            model.ListaProveedores = new SelectList(_context.PROVEEDOR.ToList(), "Id", "NOMBREPROVEEDOR");
            model.ListaProductos = new SelectList(_context.PRODUCTO.ToList(), "id", "NOMBREPRODUCTO");
            return View(model);
        }

        // POST: ENTRADA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe, Operador")]
        public async Task<IActionResult> Create(string Idnouse, EntradaViewModel model)
        {

            if (ModelState.IsValid)
            {

                PRODUCTO prod = await _context.PRODUCTO.Where(p => p.id== model.Entrada.IDPRODUCTO).SingleOrDefaultAsync();
                

                
                    model.Entrada.MONTOTOTALENTRADA = (model.Entrada.CANTIDADPENTRADA * prod.PVPPRODUCTO);
                    prod.STOCKPRODUCTO += model.Entrada.CANTIDADPENTRADA;
                    
                    _context.Update(prod);       
                _context.Add(model.Entrada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            return View(model.Entrada);
        }

        // GET: ENTRADA/Edit/5
        [Authorize(Roles = "Jefe, Operador")]
        public async Task<IActionResult> Edit(EntradaViewModel model)
        {
            model.ListaProveedores = new SelectList(_context.PROVEEDOR.ToList(), "Id", "NOMBREPROVEEDOR");
            model.ListaProductos = new SelectList(_context.PRODUCTO.ToList(), "id", "NOMBREPRODUCTO");
            
            if (model == null)
            {
                return NotFound();
            }

            model.Entrada = await _context.ENTRADA.FindAsync(model.Id);
            if (model.Entrada == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: ENTRADA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe, Operador")]
        public async Task<IActionResult> Edit(int id, EntradaViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ENTRADA entraActual = await _context.ENTRADA
                        .AsNoTracking()
                        .SingleOrDefaultAsync(e => e.id == model.Entrada.id);

                    PRODUCTO prod = await _context.PRODUCTO
                        .SingleOrDefaultAsync(p => p.id == model.Entrada.IDPRODUCTO);

                    prod.STOCKPRODUCTO = prod.STOCKPRODUCTO - entraActual.CANTIDADPENTRADA + model.Entrada.CANTIDADPENTRADA;
                    model.Entrada.MONTOTOTALENTRADA = model.Entrada.CANTIDADPENTRADA * prod.PVPPRODUCTO;

                    _context.Update(prod);
                    _context.Update(model.Entrada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ENTRADAExists(model.Entrada.id))
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

            return View(model);
        }

        // GET: ENTRADA/Delete/5
        [Authorize(Roles = "Jefe")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eNTRADA = await _context.ENTRADA
                .FirstOrDefaultAsync(m => m.id == id);
            PROVEEDOR provTemp = await _context.PROVEEDOR.Where(p => p.Id== eNTRADA.IDPROVEEDOR).SingleOrDefaultAsync();
            eNTRADA.proveedor = provTemp.NOMBREPROVEEDOR;
            PRODUCTO provTemp1 = await _context.PRODUCTO.Where(p => p.id == eNTRADA.IDPRODUCTO).SingleOrDefaultAsync();
            eNTRADA.producto = provTemp1.NOMBREPRODUCTO;
            if (eNTRADA == null)
            {
                return NotFound();
            }

            return View(eNTRADA);
        }

        // POST: ENTRADA/Delete/5
        [Authorize(Roles = "Jefe")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrada = await _context.ENTRADA.FindAsync(id);
            var producto = await _context.PRODUCTO.FindAsync(entrada.IDPRODUCTO);

            if (entrada == null)
            {
                return NotFound();
            }
            

            if (producto.STOCKPRODUCTO < entrada.CANTIDADPENTRADA)
            {
                //Arreglar esta cosa
                
                return RedirectToAction(nameof(Index));

            }
            else
            {
                producto.STOCKPRODUCTO -= entrada.CANTIDADPENTRADA;
                _context.Update(producto);

                _context.ENTRADA.Remove(entrada);
                await _context.SaveChangesAsync();

            }
                return RedirectToAction(nameof(Index));
           
        }

        private bool ENTRADAExists(int id)
        {
            return _context.ENTRADA.Any(e => e.id == id);
        }
    }
}
