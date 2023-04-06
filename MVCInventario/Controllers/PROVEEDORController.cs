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
    public class PROVEEDORController : Controller
    {
        private readonly MVCInventarioContext _context;

        public PROVEEDORController(MVCInventarioContext context)
        {
            _context = context;
        }

        // GET: PROVEEDOR
        public async Task<IActionResult> Index()
        {
            return View(await _context.PROVEEDOR.ToListAsync());
        }

        // GET: PROVEEDOR/Details/5
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

        // GET: PROVEEDOR/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PROVEEDOR/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CEDULAPROVEEDOR,NOMBREPROVEEDOR,DIRECCIONPROVEEDOR,CORREOPROVEEDOR,CIUDADPROVEEDOR")] PROVEEDOR pROVEEDOR)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pROVEEDOR);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pROVEEDOR);
        }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,CEDULAPROVEEDOR,NOMBREPROVEEDOR,DIRECCIONPROVEEDOR,CORREOPROVEEDOR,CIUDADPROVEEDOR")] PROVEEDOR pROVEEDOR)
        {
            if (id != pROVEEDOR.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
            return View(pROVEEDOR);
        }

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

        // POST: PROVEEDOR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pROVEEDOR = await _context.PROVEEDOR.FindAsync(id);
            _context.PROVEEDOR.Remove(pROVEEDOR);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PROVEEDORExists(int id)
        {
            return _context.PROVEEDOR.Any(e => e.Id == id);
        }
    }
}