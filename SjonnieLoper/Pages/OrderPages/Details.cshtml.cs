using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Data;
using SjonnieLoper.DataBase;
using SjonnieLoper.DataBase.Services.Interfaces;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;

namespace SjonnieLoper.Pages.OrderPages
{
    public class DetailsModel : PageModel
    {
        private readonly IWiskey _whiskeyContext;
        private readonly IGeneral _GeneralContext;
        private readonly IOrders _orderContext;

        #region public properties
        public Order Order { get; set; }

        [TempData]
        public string Message { get; set; }
        #endregion

        public DetailsModel(IWiskey context, IGeneral general, IOrders orderContext)
        {
            _whiskeyContext = context;
            _GeneralContext = general;
            _orderContext = orderContext;
        }

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            Order = await _orderContext.GetOrderById(orderId);
            if (Order == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public Task<IActionResult> ConvertToPdf(int orderId)
        {
            PdfDocument document = new PdfDocument();

            PdfPage page = document.Pages.Add();

            PdfGraphics graphics = page.Graphics;

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            graphics.DrawString($"{Order.Id}", font, PdfBrushes.Black, new PointF(0, 0));

            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            stream.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(stream, "SjonnieLoper/Pdf");

            fileStreamResult.FileDownloadName = $"{Order.Id}.pdf";

            return fileStreamResult;
        }
    }
}
