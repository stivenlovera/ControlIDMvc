using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ControlIDMvc.Views.Inscripcion
{
    public class Create : PageModel
    {
        private readonly ILogger<Create> _logger;
        public ControlIDMvc.Dtos.Inscripcion.InscripcionCreateDto inscripcionCreateDto=new ControlIDMvc.Dtos.Inscripcion.InscripcionCreateDto();
        public Create(ILogger<Create> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}