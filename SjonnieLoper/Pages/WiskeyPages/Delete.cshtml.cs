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

        public WhiskeyBase whiskey { get; set; }
        public DeleteModel(IWiskey context)
        {
            this.context = context;
        }

        public IActionResult OnGet(int whiskeyId)
        {
            whiskey = context.GetWhiskeyById(whiskeyId);
            if (whiskey == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int whiskeyId)
        {
            whiskey = context.DeleteWhiskey(whiskeyId);
            context.Commit();
            if (whiskey == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{whiskey.Brand}{whiskey.Name} deleted";
            return RedirectToPage("./Index");
        }
    }
}
