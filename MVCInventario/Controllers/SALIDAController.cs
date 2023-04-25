﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCInventario.Data;
using MVCInventario.Models;

namespace MVCInventario.Controllers
{
    public class SALIDAController : Controller
    {
        private readonly MVCInventarioContext _context;

        public SALIDAController(MVCInventarioContext context)
        {
            _context = context;
        }

        // GET: ENTRADA
        public async Task<IActionResult> Index(SalidaViewModel model)
        {
            var salida = _context.SALIDA.Select(p => p);

            if (!string.IsNullOrEmpty(model.Clientes))
            {
                CLIENTE CLiTemp = await _context.CLIENTE.Where(p => p.NOMBRECLIENTE == model.Clientes).SingleOrDefaultAsync();
                if (CLiTemp != null)
                {
                    salida = salida.Where(p => p.IDCLIENTE == CLiTemp.Id);
                }
            }
            model.NomL = new SelectList(_context.CLIENTE.OrderBy(p => p.NOMBRECLIENTE).Select(p => p.NOMBRECLIENTE));
            if (!string.IsNullOrEmpty(model.Productos))
            {
                PRODUCTO proTemp = await _context.PRODUCTO.Where(p => p.NOMBREPRODUCTO == model.Productos).SingleOrDefaultAsync();
                if (proTemp != null)
                {
                    salida = salida.Where(p => p.IDPRODUCTO == proTemp.id);
                }
            }
            model.ProL = new SelectList(_context.PRODUCTO.OrderBy(p => p.NOMBREPRODUCTO).Select(p => p.NOMBREPRODUCTO));

            foreach (var item in salida)
            {
                CLIENTE cliTemp = await _context.CLIENTE.Where(p => p.Id == item.IDCLIENTE).SingleOrDefaultAsync();
                item.cliente = cliTemp.NOMBRECLIENTE;

                PRODUCTO provTemp1 = await _context.PRODUCTO.Where(p => p.id == item.IDPRODUCTO).SingleOrDefaultAsync();
                item.producto = provTemp1.NOMBREPRODUCTO;

            }
            model.Salidas = await salida.ToListAsync();
            return View(model);
        }

        // GET: ENTRADA/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salida = await _context.SALIDA
                 .FirstOrDefaultAsync(m => m.id == id);
            CLIENTE CliTemp = await _context.CLIENTE.Where(p => p.Id == salida.IDCLIENTE).SingleOrDefaultAsync();
            salida.cliente = CliTemp.NOMBRECLIENTE;
            PRODUCTO proTemp1 = await _context.PRODUCTO.Where(p => p.id == salida.IDPRODUCTO).SingleOrDefaultAsync();
            salida.producto = proTemp1.NOMBREPRODUCTO;

            if (salida == null)
            {
                return NotFound();
            }

            return View(salida);
        }

        // GET: ENTRADA/Create
        public IActionResult Crear(SALIDA salida)
        {
            // Guardar la salida en la base de datos
            _context.SALIDA.Add(salida);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Create(SalidaViewModel model)
        {
            model.NomL = new SelectList(_context.CLIENTE.ToList(), "Id", "NOMBRECLIENTE");
            model.ProL = new SelectList(_context.PRODUCTO.ToList(), "id", "NOMBREPRODUCTO");

            return View(model);
        }

        // POST: ENTRADA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Idnouse, SalidaViewModel model)
        {

            if (ModelState.IsValid)
            {

                PRODUCTO prod = await _context.PRODUCTO.Where(p => p.id == model.Salida.IDPRODUCTO).SingleOrDefaultAsync();

                if (prod.STOCKPRODUCTO<=0) {
                    //Arreglar esta cosa
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                model.Salida.MONTOTOTALSALIDA = (model.Salida.CANTIDADSALIDA * prod.PVPPRODUCTO);
                    prod.STOCKPRODUCTO -= model.Salida.CANTIDADSALIDA;
                    _context.Update(prod);
                _context.Add(model.Salida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            }
            return View(model.Salida);
        }

        // GET: ENTRADA/Edit/5
        public async Task<IActionResult> Edit(SalidaViewModel model)
        {
            model.NomL = new SelectList(_context.CLIENTE.ToList(), "Id", "NOMBRECLIENTE");
            model.ProL = new SelectList(_context.PRODUCTO.ToList(), "id", "NOMBREPRODUCTO");

            if (model == null)
            {
                return NotFound();
            }

            model.Salida = await _context.SALIDA.FindAsync(model.Id);
            if (model.Salida == null)
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
        public async Task<IActionResult> Edit(int id, SalidaViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SALIDA entr = model.Salida;
                    entr.id = model.Id;
                    _context.Update(entr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SALIDAExists(model.Salida.id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sALIDA = await _context.SALIDA
                .FirstOrDefaultAsync(m => m.id == id);
            CLIENTE CliTemp = await _context.CLIENTE.Where(p => p.Id == sALIDA.IDCLIENTE).SingleOrDefaultAsync();
            sALIDA.cliente = CliTemp.NOMBRECLIENTE;
            PRODUCTO proTemp1 = await _context.PRODUCTO.Where(p => p.id == sALIDA.IDPRODUCTO).SingleOrDefaultAsync();
            sALIDA.producto = proTemp1.NOMBREPRODUCTO;
            if (sALIDA == null)
            {
                return NotFound();
            }

            return View(sALIDA);
        }

        // POST: ENTRADA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sALIDA = await _context.SALIDA.FindAsync(id);
            _context.SALIDA.Remove(sALIDA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SALIDAExists(int id)
        {
            return _context.SALIDA.Any(e => e.id == id);
        }
    }
}