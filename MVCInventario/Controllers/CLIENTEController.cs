﻿using System;
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
    public class CLIENTEController : Controller
    {
        private readonly MVCInventarioContext _context;

        public CLIENTEController(MVCInventarioContext context)
        {
            _context = context;
        }

        // GET: CLIENTE
        public async Task<IActionResult> Index()
        {
            return View(await _context.CLIENTE.ToListAsync());
        }

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