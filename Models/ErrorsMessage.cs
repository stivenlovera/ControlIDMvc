using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models
{
    public class Errors{
        public List<ErrorsMessage> errors { get; set; }
    }
    public class ErrorsMessage
    {
        public string errorMessage { get; set; }
    }
}