using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.portalsActionUpdateDto
{
    public class portalsActionsDto
    {
        public int portal_id { get; set; }
        public int action_id { get; set; }
    }
    public class responsePortalsActionsDto
    {
        public List<portalsActionsDto> portal_actions { get; set; }
    }
}