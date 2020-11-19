using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SjonnieLoper.Core;
using SjonnieLoper.Core.Models;
using SjonnieLoper.DataBase;
using SjonnieLoper.DataBase.Services.Interfaces;

namespace SjonnieLoper.Pages.WiskeyPages
{
    [Authorize(Roles = "Employee")]
    public class Create_EditModel : PageModel
    {
        private readonly IWiskey context;
        private readonly IHtmlHelper HtmlHelper;
        private readonly IGeneral general;

        [BindProperty(SupportsGet = true )]
        public WhiskeyBase Whiskey { get; set; }

        public IEnumerable<SelectListItem> CountriesList { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Images { get; set; }


        [BindProperty(SupportsGet = true)]
        public bool AddNewCountry { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NewCountryName { get; set; }

        public Create_EditModel(IWiskey context, IHtmlHelper htmlHelper, IGeneral general)
        {
            this.context = context;
            this.HtmlHelper = htmlHelper;
            this.general = general;
        }


        public async Task<IActionResult> OnGet(int? whiskeyId)
        {
            if (whiskeyId.HasValue)
            {
                Whiskey = await context.GetWhiskeyById(whiskeyId.Value);
                CountriesList = new SelectList(await general.GetAllCountriesAsync(), "Id", "Name", Whiskey.CountryOfOrigin.Id);
            }
            else
            {
                Whiskey = new WhiskeyBase();
                CountriesList = new SelectList(await general.GetAllCountriesAsync(), "Id", "Name");
            }
            if (Whiskey == null)
            {
                return RedirectToPage("./NotFound");
            }

            Types = HtmlHelper.GetEnumSelectList<WhiskeyType>();

            Images = new SelectList(new List<string> { Core.Helpers.ImageNames.Img1, Core.Helpers.ImageNames.Img2, Core.Helpers.ImageNames.Img3 });

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                Types = HtmlHelper.GetEnumSelectList<WhiskeyType>();

                CountriesList = new SelectList(await general.GetAllCountriesAsync(), "Id", "Name", Whiskey.CountryOfOrigin.Id);

                return Page();
            }
            if (Whiskey.Id > 0)
            {
                await context.UpdateWiskeyAsync(Whiskey, AddNewCountry, NewCountryName);
            }
            else
            {
                await context.AddWhiskeyAsync(Whiskey, AddNewCountry, NewCountryName);
            }

            await general.Commit();

            TempData["Message"] = "Whiskey saved!";
            return RedirectToPage("./Details", new { whiskeyId = Whiskey.Id });

        }
    }
}
