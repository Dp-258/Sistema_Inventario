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
    public class PROVEEDORController : Controller
    {
        private readonly MVCInventarioContext _context;

        public PROVEEDORController(MVCInventarioContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Jefe, Operador, Administrador")]        // GET: PROVEEDOR
        public async Task<IActionResult> Index(ProveedorViewModel modelo)
        {
            //Obtener la lista de Proveedores
            var Proveedores = _context.PROVEEDOR.Select(p => p);
            bool busqueda = false;

            //Buqueda CEDULA de Proveedor
            if (!String.IsNullOrEmpty(modelo.CedString))
            {
                Proveedores = Proveedores.Where(p => p.CEDULAPROVEEDOR.Contains(modelo.CedString));
                busqueda = true;
            }

            //Buqueda Nombre de Proveedor
            if (!String.IsNullOrEmpty(modelo.NomString))
            {
                Proveedores = Proveedores.Where(p => p.NOMBREPROVEEDOR.Contains(modelo.NomString));
                busqueda = true;
            }

            //Buqueda Direccion de Proveedor
            if (!String.IsNullOrEmpty(modelo.DirString))
            {
                Proveedores = Proveedores.Where(p => p.DIRECCIONPROVEEDOR.Contains(modelo.DirString));
                busqueda = true;
            }

            //Buqueda Email de Proveedor
            if (!String.IsNullOrEmpty(modelo.EmailString))
            {
                Proveedores = Proveedores.Where(p => p.CORREOPROVEEDOR.Contains(modelo.EmailString));
                busqueda = true;
            }

            //Buqueda Ciudad de Proveedor
            if (!String.IsNullOrEmpty(modelo.CiuString))
            {
                Proveedores = Proveedores.Where(p => p.CIUDADPROVEEDOR.Contains(modelo.CiuString));
                busqueda = true;
            }

            if (!Proveedores.Any() && busqueda)
            {
                TempData["ErrorMessage"] = "No existe un proveedor con estos datos.";
                Proveedores = _context.PROVEEDOR.Select(p => p);
            }

            modelo.Proveedores = await Proveedores.ToListAsync();
            return View(modelo);
        }
        [Authorize(Roles = "Jefe, Operador, Administrador")]        // GET: PROVEEDOR/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pROVEEDOR = await _context.PROVEEDOR
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pROVEEDOR == null)
            {
                return NotFound();
            }

            return View(pROVEEDOR);
        }
        [Authorize(Roles = "Jefe, Operador, Administrador")]        // GET: PROVEEDOR/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PROVEEDOR/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public async Task<IActionResult> Create([Bind("Id,CEDULAPROVEEDOR,NOMBREPROVEEDOR,DIRECCIONPROVEEDOR,CORREOPROVEEDOR,CIUDADPROVEEDOR")] PROVEEDOR pROVEEDOR)
        {
            if (ModelState.IsValid)
            {
                PROVEEDOR val = await _context.PROVEEDOR
                        .SingleOrDefaultAsync(p => p.CEDULAPROVEEDOR == pROVEEDOR.CEDULAPROVEEDOR);
                if (val == null)
                {
                    _context.Add(pROVEEDOR);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Si hay un error, muestra una ventana emergente con el mensaje de error
                    TempData["ErrorMessage"] = "No se puede crear el proveedor, debido a que este RUC ya se encuentra registrado.";
                    // Retorna a la vista anterior
                    return RedirectToAction("Create");
                }
            }
            return View(pROVEEDOR);
        }
        [Authorize(Roles = "Jefe, Administrador")]
        // GET: PROVEEDOR/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pROVEEDOR = await _context.PROVEEDOR.FindAsync(id);
            if (pROVEEDOR == null)
            {
                return NotFound();
            }
            return View(pROVEEDOR);
        }

        // POST: PROVEEDOR/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Jefe,Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CEDULAPROVEEDOR,NOMBREPROVEEDOR,DIRECCIONPROVEEDOR,CORREOPROVEEDOR,CIUDADPROVEEDOR")] PROVEEDOR pROVEEDOR)
        {
            if (id != pROVEEDOR.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var val = await _context.PROVEEDOR.AsNoTracking()
                        .Where(p => p.CEDULAPROVEEDOR == pROVEEDOR.CEDULAPROVEEDOR && p.Id != pROVEEDOR.Id).ToListAsync();
                if (val.Count == 0)
                {
                    try
                    {
                        _context.Update(pROVEEDOR);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PROVEEDORExists(pROVEEDOR.Id))
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
                    TempData["ErrorMessage"] = "No se puede editar el proveedor, debido a que este RUC ya se encuentra registrado.";
                    // Retorna a la vista anterior
                    return RedirectToAction("Edit");
                }
            }
            return View(pROVEEDOR);
        }
        [Authorize(Roles = "Jefe,Administrador")]
        // GET: PROVEEDOR/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pROVEEDOR = await _context.PROVEEDOR
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pROVEEDOR == null)
            {
                return NotFound();
            }

            return View(pROVEEDOR);
        }
        [Authorize(Roles = "Jefe,Administrador")]
        // POST: PROVEEDOR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var pROVEEDOR = await _context.PROVEEDOR.FindAsync(id);
                _context.PROVEEDOR.Remove(pROVEEDOR);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                string errorMessage = "No se puede eliminar el proveedor, debido a que se encuentra siendo utilizado en otra tabla.";
                TempData["ErrorMessage"] = errorMessage;
                // Retorna a la vista anterior
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index");
        }

        private bool PROVEEDORExists(int id)
        {
            return _context.PROVEEDOR.Any(e => e.Id == id);
        }
    }
}
