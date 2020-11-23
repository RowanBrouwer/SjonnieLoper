using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SjonnieLoper.Core;
using SjonnieLoper.DataBase.Services.Interfaces;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace SjonnieLoper.Pages.OrderPages
{
    public class ConvertToPdfModel : PageModel
    {
        private readonly IOrders orderContext;

        #region public properties
        public Order order { get; set; }

        [TempData]
        public string Message { get; set; }
        #endregion

        public ConvertToPdfModel(IOrders orderContext)
        {
            this.orderContext = orderContext;
        }

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            order = await orderContext.GetOrderById(orderId);
            if (order == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int orderId)
        {
            order = await orderContext.GetOrderById(orderId);
            if (order == null)
            {
                return RedirectToPage("./NotFound");
            }

            PdfDocument document = new PdfDocument();

            PdfPage page = document.Pages.Add();

            PdfGrid pdfGrid = new PdfGrid();

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Whiskey Id");
            dataTable.Columns.Add("Whiskey Name");
            dataTable.Columns.Add("Amount of whiskey");

            foreach (var OIT in order.OrderItems)
            {
                dataTable.Rows.Add(new object[] { $"{OIT.Id}", $"{OIT.Whiskey.Name}", $"{OIT.Amount}" });
            }

            pdfGrid.DataSource = dataTable;

            pdfGrid.Draw(page, new PointF(10, 10));

            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            stream.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(stream, "SjonnieLoper/Pdf");

            fileStreamResult.FileDownloadName = $"{order.Id}.pdf";

            return fileStreamResult;
        }
    }
}
