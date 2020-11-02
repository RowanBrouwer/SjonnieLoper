using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SjonnieLoper.Core;
using SjonnieLoper.Data;
using SjonnieLoper.DataBase;

namespace SjonnieLoper.Pages.WiskeyPages
{
    public class IndexModel : PageModel
    {
        private readonly IWiskey context;

        [TempData]
        public string Message { get; set; }

        public IEnumerable<WhiskeyBase> whiskeys { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IndexModel(IWiskey context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            whiskeys = context.GetAllWhiskeys(SearchTerm);
        }
    }
}
