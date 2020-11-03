using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Data;

namespace SjonnieLoper.Pages.OrderPages
{
    public class DeleteModel : PageModel
    {
        private readonly SjonnieLoper.Data.ApplicationDbContext _context;

        public DeleteModel(SjonnieLoper.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrdersAndReservations OrdersAndReservations { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrdersAndReservations = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);

            if (OrdersAndReservations == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrdersAndReservations = await _context.Orders.FindAsync(id);

            if (OrdersAndReservations != null)
            {
                _context.Orders.Remove(OrdersAndReservations);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
