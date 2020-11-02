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
    public class DetailsModel : PageModel
    {
        private readonly IWiskey context;

        public WhiskeyBase Whiskey { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailsModel(IWiskey context)
        {
            this.context = context;
        }

        public IActionResult OnGet(int whiskeyId)
        {
            Whiskey = context.GetWhiskeyById(whiskeyId);
            if (Whiskey == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}
