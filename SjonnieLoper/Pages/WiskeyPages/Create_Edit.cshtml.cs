using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SjonnieLoper.Core;
using SjonnieLoper.Core.Models;
using SjonnieLoper.DataBase;

namespace SjonnieLoper.Pages.WiskeyPages
{
    public class Create_EditModel : PageModel
    {
        private readonly IWiskey context;
        private readonly IHtmlHelper HtmlHelper;

        [BindProperty(SupportsGet = true )]
        public WhiskeyBase Whiskey { get; set; }

        public IEnumerable<SelectListItem> countrys { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Images { get; set; }


        [BindProperty(SupportsGet = true)]
        public bool NewCountry { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NewCountryString { get; set; }

        public Create_EditModel(IWiskey context, IHtmlHelper htmlHelper)
        {
            this.context = context;
            this.HtmlHelper = htmlHelper;
        }


        public async Task<IActionResult> OnGet(int? whiskeyId)
        {
            if (NewCountry)
            {

            }
            else
            {
                countrys = new SelectList(await context.GetAllCountrys(), "Id", "Country");
            }
            Types = HtmlHelper.GetEnumSelectList<WhiskeyType>();
            Images = new SelectList(new List<string> { Core.Helpers.ImageNames.Img1, Core.Helpers.ImageNames.Img2, Core.Helpers.ImageNames.Img3 });

            if (whiskeyId.HasValue)
            {
                Whiskey = await context.GetWhiskeyById(whiskeyId.Value);
            }
            else
            {
                Whiskey = new WhiskeyBase();
            }
            if (Whiskey == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                countrys = new SelectList(await context.GetAllCountrys(), "CountryId", "CountryName", Whiskey.CountryOfOrigin);
                Types = HtmlHelper.GetEnumSelectList<WhiskeyType>();
                return Page();
            }
            if (Whiskey.Id > 0)
            {
                context.UpdateWiskey(Whiskey, NewCountry, NewCountryString);
            }
            else
            {
                await context.AddWhiskey(Whiskey, NewCountry, NewCountryString);
            }
            await context.Commit();
            TempData["Message"] = "Whiskey saved!";
            return RedirectToPage("./Details", new { whiskeyId = Whiskey.Id });
        }
    }
}
