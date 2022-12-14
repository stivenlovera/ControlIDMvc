using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI
{
    public class ApiRutas
    {
        /*objetos*/
        public string ApiUrlLogin = "login.fcgi";
        public string ApiUrlCreate = "create_objects.fcgi";
        public string ApiUrlMostrar = "load_objects.fcgi";
        public string ApiUrlUpdate = "modify_objects.fcgi";
        public string ApiUrlDelete = "destroy_objects.fcgi";
        /*sistema*/
        public string ApiUrlGetInfoSistema = "system_information.fcgi";

        /*detector de rostro*/
        public string apiUrlShowUserImagen ="user_get_image_list.fcgi";
        public string apiUrlCreateUserImagen ="user_set_image_list.fcgi";
        public string apiUrlDeleteUserImagen ="user_destroy_image.fcgi";
    }
}