﻿using System;
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
    public class SALIDAController : Controller
    {
        private readonly MVCInventarioContext _context;

        public SALIDAController(MVCInventarioContext context)
        {
            _context = context;
        }

        // GET: SALIDA
        [Authorize(Roles = "Jefe, Operador")]
        public async Task<IActionResult> Index(SalidaViewModel model)
        {
            var salida = _context.SALIDA.Select(p => p);
            bool busqueda = false;

            if (!string.IsNullOrEmpty(model.ClienteSearchString))
            {
                var cliTemp = await _context.CLIENTE.Where(p => p.NOMBRECLIENTE.Contains(model.ClienteSearchString)).ToListAsync();
                if (cliTemp != null)
                {
                    var cliIds = cliTemp.Select(p => p.Id).ToList();
                    salida = salida.Where(p => cliIds.Contains(p.IDCLIENTE));
                    busqueda = true;
                }
            }
            if (!string.IsNullOrEmpty(model.FechaSearchString))
            {
                DateTime fechaBusqueda = DateTime.Parse(model.FechaSearchString);
                salida = salida.Where(p => p.FECHAREGISTROSALIDA == fechaBusqueda);
                busqueda = true;    
            }
            model.NomL = new SelectList(_context.CLIENTE.OrderBy(p => p.NOMBRECLIENTE).Select(p => p.NOMBRECLIENTE));
            model.ProL = new SelectList(_context.PRODUCTO.OrderBy(p => p.NOMBREPRODUCTO).Select(p => p.NOMBREPRODUCTO));

            if (!salida.Any() && busqueda)
            {
                TempData["ErrorMessage"] = "No existe una salida con estos datos.";
                salida = _context.SALIDA.Select(p => p);
            }
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
        [Authorize(Roles = "Jefe, Operador")]
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

        // GET: SALIDA/Create
        [Authorize(Roles = "Jefe, Operador")]
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
        [Authorize(Roles = "Jefe, Operador")]
        public async Task<IActionResult> Create(string Idnouse, SalidaViewModel model)
        {
            if (model.Salida.IDCLIENTE == 0 || model.Salida.IDPRODUCTO == 0)
            {
                TempData["ErrorMessage"] = "No se puede editar la salida debido a que uno de los datos ingresados no existe.";
                // Retorna a la vista anterior
                return RedirectToAction("Create");
            }
            if (ModelState.IsValid)
            {

                PRODUCTO prod = await _context.PRODUCTO.Where(p => p.id == model.Salida.IDPRODUCTO).SingleOrDefaultAsync();


                if (prod.STOCKPRODUCTO <= 0 || prod.STOCKPRODUCTO < model.Salida.CANTIDADSALIDA)
                {
                    ModelState.AddModelError("", "La cantidad de la salida no puede hacer que el stock sea negativo.");
                    model.NomL = new SelectList(_context.CLIENTE.ToList(), "Id", "NOMBRECLIENTE");
                    model.ProL = new SelectList(_context.PRODUCTO.ToList(), "id", "NOMBREPRODUCTO");
                    model.Salida = await _context.SALIDA.FindAsync(model.Id);
                    return View(model);
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
        [Authorize(Roles = "Jefe, Operador")]
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
        [Authorize(Roles = "Jefe, Operador")]
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
                    SALIDA salActual = await _context.SALIDA
                        .AsNoTracking()
                        .SingleOrDefaultAsync(e => e.id == model.Salida.id);

                    PRODUCTO prod = await _context.PRODUCTO
                        .SingleOrDefaultAsync(p => p.id == model.Salida.IDPRODUCTO);

                    int stockActual = prod.STOCKPRODUCTO + salActual.CANTIDADSALIDA;
                    int stockFinal = stockActual - model.Salida.CANTIDADSALIDA;

                    if (stockFinal < 0)
                    {
                        ModelState.AddModelError("", "La cantidad de la salida no puede hacer que el stock sea negativo.");
                        model.NomL = new SelectList(_context.CLIENTE.ToList(), "Id", "NOMBRECLIENTE");
                        model.ProL = new SelectList(_context.PRODUCTO.ToList(), "id", "NOMBREPRODUCTO");
                        model.Salida = await _context.SALIDA.FindAsync(model.Id);
                        return View(model);
                    }

                    prod.STOCKPRODUCTO = stockFinal;

                    _context.Update(prod);
                    _context.Update(model.Salida);
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
        [Authorize(Roles = "Jefe")]
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
        [Authorize(Roles = "Jefe")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salida = await _context.SALIDA.FindAsync(id);
            var producto = await _context.PRODUCTO.FindAsync(salida.IDPRODUCTO);

            if (salida == null)
            {
                return NotFound();
            }

            
                producto.STOCKPRODUCTO += salida.CANTIDADSALIDA;
                _context.Update(producto);

                _context.SALIDA.Remove(salida);
                await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool SALIDAExists(int id)
        {
            return _context.SALIDA.Any(e => e.id == id);
        }

        //CARGAR-CLEINTE
        public IActionResult GetCliInfo(string id)
        {
            var user = _context.CLIENTE.FirstOrDefault(u => u.CEDULACLIENTE == id);

            if (user == null)
            {
                return Json(null);
            }

            return Json(new { name = user.NOMBRECLIENTE, id = user.Id });
        }
        //Mostrar-Cliente
        public IActionResult LoadCliInfo(string id)
        {
            var user = _context.CLIENTE.FirstOrDefault(u => u.Id == Convert.ToInt32(id));

            if (user == null)
            {
                return Json(null);
            }

            return Json(new { name = user.NOMBRECLIENTE, ced = user.CEDULACLIENTE });
        }
        //CARGAR-PRODUCTO
        public IActionResult GetProdInfo(string id)
        {
            var prod = _context.PRODUCTO.FirstOrDefault(u => u.CODIGOPRODUCTO == id);

            if (prod == null)
            {
                return Json(null);
            }

            return Json(new { name = prod.NOMBREPRODUCTO, prod.id }); //AQUI SE PUSO EL ENVIO DIRECTO PORQUE EL NOMBRE ES IGUAL id = prod.id
        }
        //Mostrar-producto
        public IActionResult LoadProdInfo(string idprod)
        {
            var user = _context.PRODUCTO.FirstOrDefault(u => u.id == Convert.ToInt32(idprod));

            if (user == null)
            {
                return Json(null);
            }

            return Json(new { name = user.NOMBREPRODUCTO, ced = user.CODIGOPRODUCTO });
        }
    }
}
