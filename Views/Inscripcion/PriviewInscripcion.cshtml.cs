using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Views.Inscripcion
{
    public class PriviewInscripcion : PageModel
    {
        private readonly ILogger<PriviewInscripcion> _logger;

        public PriviewInscripcion(ILogger<PriviewInscripcion> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}