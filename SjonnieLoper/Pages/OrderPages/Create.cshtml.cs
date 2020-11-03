using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SjonnieLoper.Core;
using SjonnieLoper.Data;

namespace SjonnieLoper.Pages.OrderPages
{
    public class CreateModel : PageModel
    {
        private readonly SjonnieLoper.Data.ApplicationDbContext _context;

        public CreateModel(SjonnieLoper.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public OrdersAndReservations OrdersAndReservations { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Orders.Add(OrdersAndReservations);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
