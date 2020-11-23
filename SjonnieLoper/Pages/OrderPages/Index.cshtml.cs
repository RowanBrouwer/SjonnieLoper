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

namespace SjonnieLoper.Pages.OrderPages
{
    public class IndexModel : PageModel
    {
        private readonly IWiskey context;
        private readonly IGeneral general;
        private readonly IOrdersAndReservations orderContext;

        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<OrdersAndReservations> OrdersAndReservations { get; set; }

        public IndexModel(IWiskey context, IGeneral general, IOrdersAndReservations orderContext)
        {
            this.context = context;
            this.general = general;
            this.orderContext = orderContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            OrdersAndReservations = await orderContext.GetAllOrdersAndReservations(SearchTerm);
            return Page();
        }
    }
}
