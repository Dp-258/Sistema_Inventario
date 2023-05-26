using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MVCInventario.Helper
{
    public class PageEventHelper : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            BaseFont _parrafo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1257, true);
            iTextSharp.text.Font parrafo = new iTextSharp.text.Font(_parrafo, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
            // Obtener el número de página actual
            int pageNumber = writer.PageNumber;

            // Crear un objeto PdfPTable para contener el número de página
            PdfPTable table = new PdfPTable(1);
            table.TotalWidth = document.Right - document.Left;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;

            // Crear un objeto PdfPCell para agregar el número de página
            PdfPCell cell = new PdfPCell(new Phrase(pageNumber.ToString(), parrafo));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;

            // Agregar el objeto PdfPCell a la tabla
            table.AddCell(cell);

            // Agregar la tabla al documento en la posición del pie de página
            table.WriteSelectedRows(0, -1, document.Left, document.Bottom, writer.DirectContent);
        }
    }
}

