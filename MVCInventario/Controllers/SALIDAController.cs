using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCInventario.Data;
using MVCInventario.Models;
using MVCInventario.Helper;

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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public async Task<IActionResult> Pdf(string parametro, string fecha1, string fecha2)
        {
            Document doc = new Document(iTextSharp.text.PageSize.Letter);

            doc.SetMargins(70.8661f, 70.8661f, 35.0394f, 35.0394f);
            MemoryStream file = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, file);

            PageEventHelper eventHandler = new PageEventHelper();
            writer.PageEvent = eventHandler;

            doc.AddAuthor("StockUp");
            doc.AddTitle("Reporte Salida");
            doc.Open();

            BaseFont _titulo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font titulo = new iTextSharp.text.Font(_titulo, 18f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

            BaseFont _subtitulo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, true);
            iTextSharp.text.Font subtitulo = new iTextSharp.text.Font(_subtitulo, 14f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

            //BaseFont _parrafo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1257, true);
            //iTextSharp.text.Font parrafo = new iTextSharp.text.Font(_parrafo, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
           
            Font parrafo = FontFactory.GetFont("Courier", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 12);
            if (fecha1 == null && fecha2 == null)
            {
                TempData["ErrorMessage"] = "No se puede generar el pdf, elija un rango de fechas en ambos calendarios";
                // Retorna a la vista anterior
                return RedirectToAction("Index");
            }
            else if (fecha1 == null)
            {
                TempData["ErrorMessage"] = "No se puede generar el pdf, elija un rango de fechas en ambos calendarios";
                // Retorna a la vista anterior
                return RedirectToAction("Index");
            }
            else if (fecha2 == null)
            {
                TempData["ErrorMessage"] = "No se puede generar el pdf, elija un rango de fechas en ambos calendarios";
                // Retorna a la vista anterior
                return RedirectToAction("Index");
            }

            //BaseFont _blanco = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            //iTextSharp.text.Font blanco = new iTextSharp.text.Font(_parrafo, 10f, iTextSharp.text.Font.NORMAL, new BaseColor(255, 255, 255));



            //doc.Add(new Phrase("Reporte", titulo));
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "stockUpLogo.png");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
            float ancho = logo.Width;
            float alto = logo.Height;
            float proporcion = alto / ancho;
            logo.ScaleAbsoluteWidth(50);
            logo.ScaleAbsoluteHeight(50 * proporcion);
            //float pageHeight = doc.PageSize.Height;
            logo.SetAbsolutePosition(10, 732);

            doc.Add(logo);

            doc.Add(new Chunk("\n"));
            doc.Add(new Phrase("\n"));

            var table = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100 };
            table.AddCell(new PdfPCell(new Phrase("Informe de Salidas", titulo)) { Border = 0, Rowspan = 3, VerticalAlignment = Element.ALIGN_LEFT });
            table.AddCell(new PdfPCell(new Phrase("Informe por: " + parametro, parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
            table.AddCell(new PdfPCell(new Phrase(DateTime.Parse(fecha1).ToString("dd/MM/yyyy") + " -  " + DateTime.Parse(fecha2).ToString("dd/MM/yyyy"), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 0 });
            table.AddCell(new PdfPCell(new Phrase("Emitido el: " + DateTime.Now.ToString("dd/MM/yyyy"), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
            doc.Add(table);


            doc.Add(new Chunk("\n"));
            doc.Add(new Phrase("\n"));


            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Path.Combine());
            var tbl = new PdfPTable(new float[] { 10f, 30f, 32f, 17f, 16f, 15f }) { WidthPercentage = 120 };

            // Configurar estilo de las celdas
            var cellStyle = new PdfPCell(new Phrase()) { Padding = 5f, BackgroundColor = BaseColor.White }; cellStyle.BorderWidth = 0.5f;
            cellStyle.BorderColor = new BaseColor(0, 69, 161);



            // ...

            // Configurar estilo de las celdas de datos


            var c1 = new PdfPCell(new Phrase("ID", subtitulo)) { BackgroundColor = BaseColor.White, BorderWidthBottom = 2f, PaddingBottom = 10f };
            c1.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
            c1.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
            c1.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior

            var c2 = new PdfPCell(new Phrase("CLIENTE" ,subtitulo)) { BackgroundColor = BaseColor.White, BorderWidthBottom = 2f, PaddingBottom = 10f };
            c2.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
            c2.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
            c2.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior

            var c3 = new PdfPCell(new Phrase("PRODUCTO" ,subtitulo)) { BackgroundColor = BaseColor.White, BorderWidthBottom = 2f, PaddingBottom = 10f };
            c3.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
            c3.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
            c3.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior

            var c4 = new PdfPCell(new Phrase("FECHA" , subtitulo)) { BackgroundColor = BaseColor.White, BorderWidthBottom = 2f, PaddingBottom = 10f };
            c4.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
            c4.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
            c4.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior

            var c5 = new PdfPCell(new Phrase("CANTIDAD", subtitulo)) { BackgroundColor = BaseColor.White, BorderWidthBottom = 2f, PaddingBottom = 10f };
            c5.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
            c5.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
            c5.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
            var c6 = new PdfPCell(new Phrase("TOTAL", subtitulo)) { BackgroundColor = BaseColor.White, BorderWidthBottom = 2f, PaddingBottom = 10f };
            c6.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
            c6.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
            c6.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior



            // Configurar estilo de las celdas de encabezado


            tbl.AddCell(c1);
            tbl.AddCell(c2);
            tbl.AddCell(c3);
            tbl.AddCell(c4);
            tbl.AddCell(c5);
            tbl.AddCell(c6);

            var salidas = new List<SALIDA>();
            int i = 0;
            if (fecha1 == null && fecha2 == null)
            {
                TempData["ErrorMessage"] = "No se puede generar el pdf, elija un rango de fechas en ambos calendarios";
                // Retorna a la vista anterior
                return RedirectToAction("Index");
            }
            else if (fecha1 == null)
            {
                TempData["ErrorMessage"] = "No se puede generar el pdf, elija un rango de fechas en ambos calendarios";
                // Retorna a la vista anterior
                return RedirectToAction("Index");
            }
            else if (fecha2 == null)
            {
                TempData["ErrorMessage"] = "No se puede generar el pdf, elija un rango de fechas en ambos calendarios";
                // Retorna a la vista anterior
                return RedirectToAction("Index");
            }
            //}else if (!string.IsNullOrEmpty(fecha1)&& !string.IsNullOrEmpty(fecha2)) {
            //    DateTime fechaBusqueda = DateTime.Parse(model.FechaSearchString);
            //}
            else
            {
                DateTime fechaBusqueda1 = DateTime.Parse(fecha1);
                DateTime fechaBusqueda2 = DateTime.Parse(fecha2);
                if (fechaBusqueda1 > fechaBusqueda2)
                {
                    TempData["ErrorMessage"] = "No se puede generar el pdf, elija un rango en el cual la primera fecha sea mayor que la segunda";
                    // Retorna a la vista anterior
                    return RedirectToAction("Index");
                }
                else
                {
                    if (parametro == "monto")
                    {
                        salidas = await _context.SALIDA.OrderByDescending(e => e.MONTOTOTALSALIDA).ToListAsync();
                        salidas = salidas.Where(p=>p.FECHAREGISTROSALIDA >= fechaBusqueda1 && p.FECHAREGISTROSALIDA <= fechaBusqueda2).ToList();
                    }
                    
                    else if (parametro == "cantidad")
                    {
                        salidas = await _context.SALIDA.OrderByDescending(e => e.CANTIDADSALIDA).ToListAsync();
                        salidas = salidas.Where(p => p.FECHAREGISTROSALIDA >= fechaBusqueda1 && p.FECHAREGISTROSALIDA <= fechaBusqueda2).ToList();
                    }
                }

            }
            if (!(salidas.Any()))
            {
                // Si hay un error, muestra una ventana emergente con el mensaje de error
                TempData["ErrorMessage"] = "No se puede generar el pdf porque no existe ninguna salida";

                // Retorna a la vista anterior
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var salida in salidas)
                {
                    if (salida.ANULARSALIDA == 0)
                    {
                        CLIENTE provTemp = await _context.CLIENTE.Where(p => p.Id == salida.IDCLIENTE).SingleOrDefaultAsync();
                        salida.cliente = provTemp.NOMBRECLIENTE;
                        PRODUCTO provTemp1 = await _context.PRODUCTO.Where(p => p.id == salida.IDPRODUCTO).SingleOrDefaultAsync();
                        salida.producto = provTemp1.NOMBREPRODUCTO;

                        // Configurar estilo de las celdas de datos
                        c1 = new PdfPCell(new Phrase(salida.id.ToString(),parrafo));
                        c1.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c1.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c1.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
                        c2 = new PdfPCell(new Phrase(salida.cliente.ToString(),parrafo));
                        c2.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c2.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c2.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
                        c3 = new PdfPCell(new Phrase(salida.producto.ToString(), parrafo));
                        c3.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c3.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c3.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
                        c4 = new PdfPCell(new Phrase(salida.FECHAREGISTROSALIDA.ToShortDateString(), parrafo));
                        c4.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c4.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c4.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
                        c5 = new PdfPCell(new Phrase(salida.CANTIDADSALIDA.ToString(), parrafo));
                        c5.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c5.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c5.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
             
                        c6 = new PdfPCell(new Phrase(salida.MONTOTOTALSALIDA.ToString(), parrafo));
                        c6.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c6.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c6.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
                        if (i % 2 == 0)
                        {
                            c1.BackgroundColor = new BaseColor(204, 204, 204);
                            c2.BackgroundColor = new BaseColor(204, 204, 204);
                            c3.BackgroundColor = new BaseColor(204, 204, 204);
                            c4.BackgroundColor = new BaseColor(204, 204, 204);
                            c5.BackgroundColor = new BaseColor(204, 204, 204);
                            c6.BackgroundColor = new BaseColor(204, 204, 204);

                        }
                        else
                        {
                            c1.BackgroundColor = BaseColor.White;
                            c2.BackgroundColor = BaseColor.White;
                            c3.BackgroundColor = BaseColor.White;
                            c4.BackgroundColor = BaseColor.White;
                            c5.BackgroundColor = BaseColor.White;
                            c6.BackgroundColor = BaseColor.White;

                        }

                        tbl.AddCell(c1);
                        tbl.AddCell(c2);
                        tbl.AddCell(c3);
                        tbl.AddCell(c4);
                        tbl.AddCell(c5);
                        tbl.AddCell(c6);
                        i++;
                    }
                   
                }

            }

            doc.Add(tbl);

            writer.Close();
            doc.Close();
            doc.Dispose();


            return File(file.ToArray(), "application/pdf");
        }
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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public async Task<IActionResult> Create(string Idnouse, SalidaViewModel model)
        {
            if (model.Salida.IDCLIENTE == 0 && model.Salida.IDPRODUCTO == 0)
            {
                TempData["ErrorMessage"] = "Por favor ingrese un dato ya registrado";
                TempData["Stock"] = "Stock negativo";
                // Retorna a la vista anterior
                return RedirectToAction("Create");
            }
            if (model.Salida.IDCLIENTE == 0 || model.Salida.IDPRODUCTO == 0)
            {
                TempData["ErrorMessage"] = "Por favor ingrese un dato ya registrado";
                TempData["Stock"] = "Stock negativo";
                // Retorna a la vista anterior
                return RedirectToAction("Create");
            }
            if (ModelState.IsValid)
            {

                PRODUCTO prod = await _context.PRODUCTO.Where(p => p.id == model.Salida.IDPRODUCTO).SingleOrDefaultAsync();


                if (prod.STOCKPRODUCTO <= 0 || prod.STOCKPRODUCTO < model.Salida.CANTIDADSALIDA)
                {
                    ModelState.AddModelError("", "La cantidad de la salida no puede hacer que el stock sea negativo.\n " +
                        "El stock actual del producto " + prod.NOMBREPRODUCTO.ToLower() + ", es de: " + prod.STOCKPRODUCTO);
                    model.NomL = new SelectList(_context.CLIENTE.ToList(), "Id", "NOMBRECLIENTE");
                    model.ProL = new SelectList(_context.PRODUCTO.ToList(), "id", "NOMBREPRODUCTO");
                    model.Salida = await _context.SALIDA.FindAsync(model.Id);
                    TempData["Stock"] = "Stock negativo";
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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
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
        [Authorize(Roles = "Jefe, Administrador")]
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
        [Authorize(Roles = "Jefe, Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salida = await _context.SALIDA.FindAsync(id);
            var producto = await _context.PRODUCTO.FindAsync(salida.IDPRODUCTO);

            if (salida == null)
            {
                return NotFound();
            }

            if (salida.ANULARSALIDA == 0)
            {
                producto.STOCKPRODUCTO += salida.CANTIDADSALIDA;
                _context.Update(producto);

                salida.ANULARSALIDA = 1;
                _context.Update(salida);

            }
            ///else
            //{
            //    if (producto.STOCKPRODUCTO <= 0 || producto.STOCKPRODUCTO < salida.CANTIDADSALIDA)
            //    {
            //        producto.STOCKPRODUCTO -= salida.CANTIDADSALIDA;
            //        _context.Update(producto);

            //        salida.ANULARSALIDA = 0;
            //        _context.Update(salida);

            //    }
            //}/
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
