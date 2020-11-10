using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Frameworks;
using SjonnieLoper.Core;
using SjonnieLoper.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;


namespace SjonnieLoper.Pages.WiskeyPages
{
    public class IndexModel : PageModel
    {
        private readonly IWiskey context;

        [TempData]
        public string Message { get; set; }

        public IEnumerable<WhiskeyBase> Whiskeys { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

  
        #region Search Properties
        #region DataProperties
        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchBrand { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchCountry { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime SearchDate1 { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime SearchDate2 { get; set; }

        [BindProperty(SupportsGet = true)]
        public WhiskeyType SearchType { get; set; }

        [BindProperty(SupportsGet = true)]
        public double SearchPercent1 { get; set; }

        [BindProperty(SupportsGet = true)]
        public double SearchPercent2 { get; set; }

        [BindProperty(SupportsGet = true)]
        public double SearchPrice1 { get; set; }

        [BindProperty(SupportsGet = true)]
        public double SearchPrice2 { get; set; }
        #endregion

        #region booleans
        [BindProperty(SupportsGet = true)]
        public bool DoAdvancedSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchForType { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchForDate { get; set; }
        #endregion
        #endregion

        public IndexModel(IWiskey context)
        {
            this.context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            if (DoAdvancedSearch)
                Whiskeys = await context.GetAllWhiskeysSearch(SearchName, SearchBrand, SearchCountry, SearchForType, SearchType);
            else
                Whiskeys = await context.GetAllWhiskeys(SearchTerm);
            return Page();
        }


    }
}
