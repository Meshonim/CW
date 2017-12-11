using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CW.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document
        [Authorize]
        public void Generate(string vlk)
        {
            using (MemoryStream ms = new MemoryStream())
            using (Document document = new Document(PageSize.A4, 25, 25, 30, 30))
            using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
            {
                document.Open();
                //document.Add(new Paragraph("Hello World"));

                PdfPTable table = new PdfPTable(3);

                PdfPCell cell = new PdfPCell(new Phrase(vlk));
                cell.Colspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                table.AddCell("Row 2, Col 1");
                table.AddCell("Row 2, Col 1");
                table.AddCell("Row 2, Col 1");

                table.AddCell("Row 3, Col 1");
                table.AddCell("Row 3, Col 1");
                table.AddCell("Row 3, Col 1");

                table.AddCell("Row 4, Col 1");
                table.AddCell("Row 4, Col 1");
                table.AddCell("Row 4, Col 1");

                document.Add(table);


                document.Close();
                writer.Close();
                ms.Close();
                Response.ContentType = "pdf/application";
                Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            }
        }
    }
}