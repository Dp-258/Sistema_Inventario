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
   
    public class CLIENTEController : Controller
    {
        private readonly MVCInventarioContext _context;

        public CLIENTEController(MVCInventarioContext context)
        {
            _context = context;
        }

        // GET: CLIENTE
        [Authorize(Roles = "Jefe, Operador")]
        public async Task<IActionResult> Index(ClienteViewModel modelo)
        {
            //Obtener la lista de Clientes
            var Clientes = _context.CLIENTE.Select(p => p);

            //Buqueda CEDULA de cliente
            if (!String.IsNullOrEmpty(modelo.CedString))
            {
                Clientes = Clientes.Where(p => p.CEDULACLIENTE.Contains(modelo.CedString));
            }

            //Buqueda Nombre de cliente
            if (!String.IsNullOrEmpty(modelo.NomString))
            {
                Clientes = Clientes.Where(p => p.NOMBRECLIENTE.Contains(modelo.NomString));
            }

            modelo.Clientes = await Clientes.ToListAsync();
            return View(modelo);
        }
        [Authorize(Roles = "Jefe, Operador")]
        // GET: CLIENTE/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cLIENTE = await _context.CLIENTE
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cLIENTE == null)
            {
                return NotFound();
            }

            return View(cLIENTE);
        }
        [Authorize(Roles = "Jefe, Operador")]
        // GET: CLIENTE/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CLIENTE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe, Operador")]
        public async Task<IActionResult> Create([Bind("Id,CEDULACLIENTE,NOMBRECLIENTE")] CLIENTE cLIENTE)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cLIENTE);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cLIENTE);
        }
        [Authorize(Roles = "Jefe, Operador")]
        // GET: CLIENTE/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cLIENTE = await _context.CLIENTE.FindAsync(id);
            if (cLIENTE == null)
            {
                return NotFound();
            }
            return View(cLIENTE);
        }

        // POST: CLIENTE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe, Operador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CEDULACLIENTE,NOMBRECLIENTE")] CLIENTE cLIENTE)
        {
            if (id != cLIENTE.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cLIENTE);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CLIENTEExists(cLIENTE.Id))
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
            return View(cLIENTE);
        }

        // GET: CLIENTE/Delete/5
        [Authorize(Roles = "Jefe")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cLIENTE = await _context.CLIENTE
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cLIENTE == null)
            {
                return NotFound();
            }

            return View(cLIENTE);
        }

        // POST: CLIENTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cLIENTE = await _context.CLIENTE.FindAsync(id);
            _context.CLIENTE.Remove(cLIENTE);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CLIENTEExists(int id)
        {
            return _context.CLIENTE.Any(e => e.Id == id);
        }
    }
}
