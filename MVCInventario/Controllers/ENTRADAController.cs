using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using iText.Kernel.Colors;

using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MVCInventario.Data;
using MVCInventario.Helper;
using MVCInventario.Models;
using NuGet.Versioning;
using static iTextSharp.text.pdf.AcroFields;

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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public async Task<IActionResult> Pdf(string parametro, string fecha1, string fecha2)
        {
            Document doc = new Document(iTextSharp.text.PageSize.Letter);

            doc.SetMargins(70.8661f, 70.8661f, 35.0394f, 40.0394f);
            MemoryStream file = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, file);

            PageEventHelper eventHandler = new PageEventHelper();
            writer.PageEvent = eventHandler;


            //pe.Title = "REPORTE";
            doc.AddAuthor("StockUp");
            doc.AddTitle("Reporte Entrada");
            doc.Open();

            BaseFont _titulo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font titulo = new iTextSharp.text.Font(_titulo, 18f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

            BaseFont _subtitulo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, true);
            iTextSharp.text.Font subtitulo = new iTextSharp.text.Font(_subtitulo, 14f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

            //BaseFont _parrafo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1257, true);
            //iTextSharp.text.Font parrafo = new iTextSharp.text.Font(_parrafo, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

            //BaseFont _blanco = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            //iTextSharp.text.Font blanco = new iTextSharp.text.Font(_parrafo, 10f, iTextSharp.text.Font.NORMAL, new BaseColor(255, 255, 255));

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
            doc.Add(new Paragraph(" "));

            var table = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100 };
            table.AddCell(new PdfPCell(new Phrase("Informe de Entradas", titulo)) { Border = 0, Rowspan = 4, VerticalAlignment = Element.ALIGN_MIDDLE });

            table.AddCell(new PdfPCell(new Phrase("Informe por: " + parametro, parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
            table.AddCell(new PdfPCell(new Phrase(DateTime.Parse(fecha1).ToString("dd/MM/yyyy") + " -  " + DateTime.Parse(fecha2).ToString("dd/MM/yyyy"), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 0 });
            table.AddCell(new PdfPCell(new Phrase("Emitido el: " + DateTime.Now.ToString("dd/MM/yyyy"), parrafo)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
            doc.Add(table);


            doc.Add(new Chunk("\n"));
            doc.Add(new Phrase("\n"));
            //Agregar la imagen al documento


            //tabla de datos
            var tbl = new PdfPTable(new float[] { 10f, 30f, 32f, 17f, 16f, 15f }) { WidthPercentage = 120 };

            // Configurar estilo de las celdas
            var cellStyle = new PdfPCell(new Phrase()) { Padding = 5f, BackgroundColor = BaseColor.White }; cellStyle.BorderWidth = 0.5f;
            cellStyle.BorderColor = new BaseColor(0, 69, 161);


            // Configurar estilo de las celdas de datos

            var c1 = new PdfPCell(new Phrase("ID", subtitulo)) { BackgroundColor = BaseColor.White, BorderWidthBottom = 2f, PaddingBottom = 10f };
            c1.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
            c1.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
            c1.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior

            var c2 = new PdfPCell(new Phrase("PROVEEDOR", subtitulo)) { BackgroundColor = BaseColor.White, BorderWidthBottom = 2f, PaddingBottom = 10f };
            c2.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
            c2.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
            c2.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior

            var c3 = new PdfPCell(new Phrase("PRODUCTO", subtitulo)) { BackgroundColor = BaseColor.White, BorderWidthBottom = 2f, PaddingBottom = 10f };
            c3.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
            c3.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
            c3.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior

            var c4 = new PdfPCell(new Phrase("FECHA", subtitulo)) { BackgroundColor = BaseColor.White, BorderWidthBottom = 2f, PaddingBottom = 10f };
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


            var entradas = new List<ENTRADA>();

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
                        entradas = await _context.ENTRADA.OrderByDescending(e => e.MONTOTOTALENTRADA).ToListAsync();
                        entradas = entradas.Where(p => p.FECHAREGISTROENTRADA >= fechaBusqueda1 && p.FECHAREGISTROENTRADA <= fechaBusqueda2).ToList();
                    }

                    else if (parametro == "cantidad")
                    {
                        entradas = await _context.ENTRADA.OrderByDescending(e => e.CANTIDADPENTRADA).ToListAsync();
                        entradas = entradas.Where(p => p.FECHAREGISTROENTRADA >= fechaBusqueda1 && p.FECHAREGISTROENTRADA <= fechaBusqueda2).ToList();
                    }
                }

            }
            if (!(entradas.Any()))
            {
                // Si hay un error, muestra una ventana emergente con el mensaje de error
                TempData["ErrorMessage"] = "No se puede generar el pdf porque no existe ninguna entrada";

                // Retorna a la vista anterior
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var entrada in entradas)
                {
                    if (entrada.ANULARENTRADA == 0)
                    {
                        PROVEEDOR provTemp = await _context.PROVEEDOR.Where(p => p.Id == entrada.IDPROVEEDOR).SingleOrDefaultAsync();
                        entrada.proveedor = provTemp.NOMBREPROVEEDOR;
                        PRODUCTO provTemp1 = await _context.PRODUCTO.Where(p => p.id == entrada.IDPRODUCTO).SingleOrDefaultAsync();
                        entrada.producto = provTemp1.NOMBREPRODUCTO;

                        // Configurar estilo de las celdas de datos
                        c1 = new PdfPCell(new Phrase(entrada.id.ToString(), parrafo));

                        c1.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c1.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c1.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
                        c2 = new PdfPCell(new Phrase(entrada.proveedor.ToString(), parrafo));
                        c2.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c2.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c2.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
                        c3 = new PdfPCell(new Phrase(entrada.producto.ToString(), parrafo));
                        c3.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c3.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c3.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
                        c4 = new PdfPCell(new Phrase(entrada.FECHAREGISTROENTRADA.ToShortDateString(), parrafo));
                        c4.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c4.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c4.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior
                        c5 = new PdfPCell(new Phrase(entrada.CANTIDADPENTRADA.ToString(), parrafo));
                        c5.BorderWidthLeft = 0.5f;  // Ajustar ancho del borde izquierdo
                        c5.BorderWidthRight = 0.5f;  // Ajustar ancho del borde derecho
                        c5.BorderWidthTop = 0.5f;  // Ajustar ancho del borde superior

                        c6 = new PdfPCell(new Phrase(entrada.MONTOTOTALENTRADA.ToString(), parrafo));
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

            //nuevo codigo


        }
        public async Task<IActionResult> Index(EntradaViewModel model)
        {
            var entrada = _context.ENTRADA.Select(p => p);
            bool busqueda = false;
          
            if (!string.IsNullOrEmpty(model.ProveedorSearchString))
            {
                var provTemp = await _context.PROVEEDOR.Where(p => p.NOMBREPROVEEDOR.Contains(model.ProveedorSearchString)).ToListAsync();
                if (provTemp != null)
                {
                    var provIds = provTemp.Select(p => p.Id).ToList();
                    entrada = entrada.Where(p => provIds.Contains(p.IDPROVEEDOR));
                    busqueda = true;
                }
            }
            if (!string.IsNullOrEmpty(model.FechaSearchString))
            {
                DateTime fechaBusqueda = DateTime.Parse(model.FechaSearchString);
                entrada = entrada.Where(p => p.FECHAREGISTROENTRADA == fechaBusqueda);
                busqueda = true;
            }

            model.ListaProveedores = new SelectList(_context.PROVEEDOR.OrderBy(p => p.NOMBREPROVEEDOR).Select(p => p.NOMBREPROVEEDOR));
            model.ListaProductos = new SelectList(_context.PRODUCTO.OrderBy(p => p.NOMBREPRODUCTO).Select(p => p.NOMBREPRODUCTO));

            if (!entrada.Any() && busqueda)
            {
                TempData["ErrorMessage"] = "No existe una entrada con estos datos.";
                entrada = _context.ENTRADA.Select(p => p);
            }
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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        [Authorize(Roles = "Jefe, Operador, Administrador")]
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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public async Task<IActionResult> Create(string Idnouse, EntradaViewModel model)
        {
            if (model.Entrada.IDPRODUCTO == 0 || model.Entrada.IDPROVEEDOR == 0)
            {
                TempData["ErrorMessage"] = "Por favor complete los campos con datos ya registrados.";
                TempData["Stock"] = "Stock negativo";
                // Retorna a la vista anterior
                return RedirectToAction("Create");
            }
            if (ModelState.IsValid)
            {

                PRODUCTO prod = await _context.PRODUCTO.Where(p => p.id == model.Entrada.IDPRODUCTO).SingleOrDefaultAsync();
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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
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
        [Authorize(Roles = "Jefe, Operador, Administrador")]
        public async Task<IActionResult> Edit(int id, EntradaViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (model.Entrada.IDPRODUCTO == 0 || model.Entrada.IDPROVEEDOR == 0)
            {
                TempData["ErrorMessage"] = "No se puede editar la entrada debido a que uno de los datos ingresados no existe.";
                // Retorna a la vista anterior
                return RedirectToAction("Edit");
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
        [Authorize(Roles = "Jefe, Administrador")]
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
        [Authorize(Roles = "Jefe,Administrador")]
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

            if (entrada.ANULARENTRADA == 0)
            {

                if (producto.STOCKPRODUCTO < entrada.CANTIDADPENTRADA)
                {
                    // Si hay un error, muestra una ventana emergente con el mensaje de error
                    TempData["ErrorMessage"] = "No se puede anular la entrada debido a que el producto " +
                        "no tiene suficiente stock para cubrir las salidas registradas.";

                    // Retorna a la vista anterior
                    return RedirectToAction("Delete");
                }
                else
                {
                    producto.STOCKPRODUCTO -= entrada.CANTIDADPENTRADA;
                    _context.Update(producto);

                    entrada.ANULARENTRADA = 1;
                    _context.Update(entrada);

                    await _context.SaveChangesAsync();

                }
            }
            ///else
            //{
            //    producto.STOCKPRODUCTO += entrada.CANTIDADPENTRADA;
            //    _context.Update(producto);

            //    entrada.ANULARENTRADA = 0;
            //    _context.Update(entrada);

            //    await _context.SaveChangesAsync();
            //}/
            return RedirectToAction(nameof(Index));

        }
        private bool ENTRADAExists(int id)
        {
            return _context.ENTRADA.Any(e => e.id == id);
        }

        //CARGAR-PROVEEDOR
        public IActionResult GetUserInfo(string id)
        {
            var user = _context.PROVEEDOR.FirstOrDefault(u => u.CEDULAPROVEEDOR == id);

            if (user == null)
            {
                return Json(null);
            }

            return Json(new { name = user.NOMBREPROVEEDOR, id = user.Id });
        }
        //Mostrar-proveedor
        public IActionResult LoadUserInfo(string id)
        {
            var user = _context.PROVEEDOR.FirstOrDefault(u => u.Id == Convert.ToInt32(id));

            if (user == null)
            {
                return Json(null);
            }

            return Json(new { name = user.NOMBREPROVEEDOR, ced = user.CEDULAPROVEEDOR });
        }
        //CARGAR-PRODUCTO
        [HttpGet]
        public IActionResult BuscarProductos(string searchTerm)
        {
            var productos = _context.PRODUCTO
                .Where(p => p.NOMBREPRODUCTO.Contains(searchTerm))
                .Select(p => new { Id = p.id, Nombre = p.NOMBREPRODUCTO })
                .ToList();

            return Json(productos);
        }
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
