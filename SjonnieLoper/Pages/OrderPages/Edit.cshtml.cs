using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Data;

namespace SjonnieLoper.Pages.OrderPages
{
    public class EditModel : PageModel
    {
        private readonly SjonnieLoper.Data.ApplicationDbContext _context;

        public EditModel(SjonnieLoper.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrdersAndReservations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersAndReservationsExists(OrdersAndReservations.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrdersAndReservationsExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
