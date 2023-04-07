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
    public class USUARIOController : Controller
    {
        private readonly MVCInventarioContext _context;

        public USUARIOController(MVCInventarioContext context)
        {
            _context = context;
        }

        // GET: USUARIO
        public async Task<IActionResult> Index()
        {
            return View(await _context.USUARIO.ToListAsync());
        }

        // GET: USUARIO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uSUARIO = await _context.USUARIO
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return View(uSUARIO);
        }

        // GET: USUARIO/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: USUARIO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NOMBREUSER,ROLUSER")] USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uSUARIO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uSUARIO);
        }

        // GET: USUARIO/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uSUARIO = await _context.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }
            return View(uSUARIO);
        }

        // POST: USUARIO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NOMBREUSER,ROLUSER")] USUARIO uSUARIO)
        {
            if (id != uSUARIO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uSUARIO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!USUARIOExists(uSUARIO.Id))
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
            return View(uSUARIO);
        }

        // GET: USUARIO/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uSUARIO = await _context.USUARIO
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return View(uSUARIO);
        }

        // POST: USUARIO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uSUARIO = await _context.USUARIO.FindAsync(id);
            _context.USUARIO.Remove(uSUARIO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool USUARIOExists(int id)
        {
            return _context.USUARIO.Any(e => e.Id == id);
        }
    }
}
