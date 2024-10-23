using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace VisitingBook.Views.Login
{
    public class SetPassword : PageModel
    {
        private readonly ILogger<SetPassword> _logger;

        public SetPassword(ILogger<SetPassword> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}