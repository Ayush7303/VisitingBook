using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace VisitingBook.Views.Login
{
    public class GetOTP : PageModel
    {
        private readonly ILogger<GetOTP> _logger;

        public GetOTP(ILogger<GetOTP> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}