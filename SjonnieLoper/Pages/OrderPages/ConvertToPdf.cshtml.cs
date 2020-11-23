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

            PdfGraphics graphics = page.Graphics;

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            DataTable dtCsv = new DataTable();

            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            stream.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(stream, "SjonnieLoper/Pdf");

            fileStreamResult.FileDownloadName = $"{order.Id}.pdf";

            RedirectToPage("./Index");
            return fileStreamResult;
        }
    }
}
