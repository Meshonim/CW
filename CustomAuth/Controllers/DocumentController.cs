using DalToWeb.Models;
using DalToWeb.Repositories;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Globalization;

namespace CW.Controllers
{
    public class DocumentController : Controller
    {
        private MainContext db = new MainContext();

        [NonAction]
        public int GetCurrentUserId()
        {
            var email = User.Identity.Name;
            var userId = 0;
            if (email != null)
            {
                var user = db.Users.Where(u => u.Email == email).FirstOrDefault();
                if (user != null)
                    userId = user.Id;
            }
            return userId;
        }

        // GET: Document
        [Authorize]
        public void Generate(int? templateId)
        {
            var template = db.OrderReportTemplates.Find(templateId);
            if (template == null)
            {
                template = new OrderReportTemplate()
                {
                    TemplateName = "Default",
                    PrintBackground = false,
                    PrintDayOfWeek = false,
                    PrintEditionTitle = false,
                    MaxCount = 100
                };
            }
            var userId = GetCurrentUserId();
            if (userId == 0)
                return;
            using (MemoryStream ms = new MemoryStream())
            using (Document document = new Document(PageSize.A4, 25, 25, 250, 30))
            using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
            {
                var orders = db.ReaderOrders
                    .Include(o => o.Exemplar)
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.ReaderOrderDateOfIssue)
                    .Take(template.MaxCount)
                    .ToList();
                var title = "Order report for " + User.Identity.Name;
                var dateFormat = template.PrintDayOfWeek ? "D" : "d";
                var columnCount = template.PrintEditionTitle ? 5 : 4;
                string path = Server.MapPath(@"~/fonts/ARIALUNI.TTF");

                iTextSharp.text.Image jpg = null;
                if (template.PrintBackground)
                {
                    string imageFilePath = Server.MapPath(@"~/images/pdf.jpg");
                    jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
                    jpg.SetAbsolutePosition(0, 0);
                    jpg.ScaleToFit(600, 950);
                    jpg.Alignment = iTextSharp.text.Image.UNDERLYING;
                }

                BaseFont baseFont = BaseFont.CreateFont(path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font f = new Font(baseFont, 12);
                Font paragraphFont = new Font(baseFont, 20, Font.BOLD);

                document.Open();

                document.Add(jpg);

                var paragraph = new Paragraph(title, paragraphFont);
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.SpacingAfter = 20;
                document.Add(paragraph);
                PdfPTable table = new PdfPTable(columnCount);

                PdfPCell cell = null;

                cell = new PdfPCell(new Phrase("Order Id"));
                cell.BackgroundColor = BaseColor.GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Exemplar Id"));
                cell.BackgroundColor = BaseColor.GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                if (template.PrintEditionTitle)
                {
                    cell = new PdfPCell(new Phrase("Edition title"));
                    cell.BackgroundColor = BaseColor.GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }                  

                cell = new PdfPCell(new Phrase("Date of issue"));
                cell.BackgroundColor = BaseColor.GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Expiry date"));
                cell.BackgroundColor = BaseColor.GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                foreach (var order in orders)
                {
                    table.AddCell(order.ReaderOrderId.ToString());
                    table.AddCell(order.ExemplarId.ToString());
                    if (template.PrintEditionTitle)
                    {
                        table.AddCell(new Phrase(order.Exemplar.Edition.EditionTitle, f));
                    }
                    table.AddCell(order.ReaderOrderDateOfIssue.ToString(dateFormat, new CultureInfo("en-US")));
                    table.AddCell(order.ReaderOrderExpiryDate.ToString(dateFormat, new CultureInfo("en-US")));
                }

                document.Add(table);
                document.Close();
                writer.Close();
                ms.Close();
                Response.ContentType = "pdf/application";
                Response.AddHeader("content-disposition", "attachment;filename=Order report.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}