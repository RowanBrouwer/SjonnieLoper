using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SjonnieLoper.Core;
using SjonnieLoper.DataBase;
using SjonnieLoper.DataBase.Services.Interfaces;

namespace SjonnieLoper.Pages.WiskeyPages
{
    [Authorize(Roles = "Employee")]
    public class DeleteModel : PageModel
    {
        private readonly IWiskey context;
        private readonly IGeneral general;

        public WhiskeyBase Whiskey { get; set; }
        public DeleteModel(IWiskey context, IGeneral general)
        {
            this.context = context;
            this.general = general;
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
            await general.Commit();
            if (Whiskey == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{Whiskey.Brand} {Whiskey.Name} deleted";
            return RedirectToPage("./Index");
        }
    }
}
