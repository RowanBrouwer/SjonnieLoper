using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SjonnieLoper.Core;
using SjonnieLoper.DataBase;

namespace SjonnieLoper.Pages.WiskeyPages
{
    public class DeleteModel : PageModel
    {
        private readonly IWiskey context;

        public WhiskeyBase Whiskey { get; set; }
        public DeleteModel(IWiskey context)
        {
            this.context = context;
        }

        public async Task<IActionResult> OnGet(int whiskeyId)
        {
            Whiskey = await context.GetWhiskeyById(whiskeyId);
            if (Whiskey == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(int whiskeyId)
        {
            Whiskey = await context.DeleteWhiskey(whiskeyId);
            await context.Commit();
            if (Whiskey == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{Whiskey.Brand}{Whiskey.Name} deleted";
            return RedirectToPage("./Index");
        }
    }
}
