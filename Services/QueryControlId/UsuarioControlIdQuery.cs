using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using ControlIDMvc.Services.ControlId;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ControlIDMvc.Services.QueryControlId
{
    public class UsuarioControlIdQuery
    {
        public string ApiUrl { get; set; }
        public UsuarioControlIdQuery()
        {

        }
        public object CreateUser(List<Persona> personas)
        {
            usersCreate user = new usersCreate();
            foreach (var persona in personas)
            {
                user.name = persona.nombre;
                user.password = "";
                user.registration = "";
                user.salt = "";
            }
            List<usersCreate> users = new List<usersCreate>();
            users.Add(user);

           /*  JObject jsonObject = new JObject{
                ["object"]="users",
                ["values"]=JsonConvert.SerializeObject(users)
            }; */
            JObject jsonObject = new JObject{
                {"object","users"},
                {"values",JsonConvert.SerializeObject(users)}
            };
            return jsonObject;
        }
    }

}