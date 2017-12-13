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
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using System.Data;

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

        [Authorize(Roles = "Admin")]
        public void GenerateExcel(int? templateId)
        {
            var template = db.LibraryReportTemplates.Find(templateId);
            if (template == null)
            {
                template = new LibraryReportTemplate()
                {
                    TemplateName = "Default",
                    PrintHeaders = false,
                    Autosized = false,
                    MaxCount = 100
                };
            }

            var lorders = db.LibraryOrders.Take(template.MaxCount).ToList();
            IWorkbook workbook;
            workbook = new XSSFWorkbook();

            ISheet sheet = workbook.CreateSheet("Library orders"); 
            
            var st = workbook.CreateCellStyle();
            st.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            st.FillPattern = FillPattern.SolidForeground;

            IRow row1 = sheet.CreateRow(0);
            var columnNames = new string[] { "Status", "Title", "Count" };

            if (template.PrintHeaders)
            {
                for (int j = 0; j < 3; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    String columnName = columnNames[j];
                    cell.CellStyle = st;
                    cell.SetCellValue(columnName);
                }
            }       

            for (int i = 0; i < lorders.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                ICell cell = null;
                cell = row.CreateCell(0);
                var boolValue = "Inactive";
                if (lorders[i].LibraryOrderStatus)
                {
                    boolValue = "Active";
                }
                cell.SetCellValue(boolValue);
                cell = row.CreateCell(1);
                cell.SetCellValue(lorders[i].Edition.EditionTitle);
                cell = row.CreateCell(2);
                cell.SetCellValue(lorders[i].LibraryOrderCount);
            }

            if (template.Autosized)
            {
                sheet.AutoSizeColumn(0);
                sheet.AutoSizeColumn(1);
                sheet.AutoSizeColumn(2);
            }          

            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "Library orders report.xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }
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
            if (userId != 0)
            {
                var marginTop = template.PrintBackground ? 250 : 30;
                using (MemoryStream ms = new MemoryStream())
                using (Document document = new Document(PageSize.A4, 25, 25, marginTop, 30))
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

                    iTextSharp.text.pdf.BaseFont baseFont =
                        iTextSharp.text.pdf.BaseFont.CreateFont
                        (
                            path,
                            iTextSharp.text.pdf.BaseFont.IDENTITY_H,
                            iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED
                            );
                    iTextSharp.text.Font f = new iTextSharp.text.Font(baseFont, 12);
                    iTextSharp.text.Font paragraphFont = new iTextSharp.text.Font(baseFont, 20, iTextSharp.text.Font.BOLD);

                    document.Open();

                    if (template.PrintBackground)
                    {
                        iTextSharp.text.Image jpg = null;
                        string imageFilePath = Server.MapPath(@"~/images/pdf.jpg");
                        jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
                        jpg.SetAbsolutePosition(0, 0);
                        jpg.ScaleToFit(600, 950);
                        jpg.Alignment = iTextSharp.text.Image.UNDERLYING;
                        document.Add(jpg);
                    }

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
            else
            {
                Response.Write("<h2>Error!</h2>");
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