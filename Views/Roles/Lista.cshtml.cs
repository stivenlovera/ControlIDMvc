using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Views.Roles
{
    public class Lista : PageModel
    {
        private readonly ILogger<Lista> _logger;

        public Lista(ILogger<Lista> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}