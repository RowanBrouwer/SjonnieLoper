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

namespace SjonnieLoper.Pages.OrderPages
{
    public class IndexModel : PageModel
    {
        private readonly IWiskey context;

        [TempData]
        public string Message { get; set; }

        public IEnumerable<WhiskeyBase> whiskeys { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IndexModel(IWiskey context)
        {
            this.context = context;
        }

        public IList<OrdersAndReservations> OrdersAndReservations { get;set; }

        public async Task OnGetAsync()
        {
            
        }
    }
}
