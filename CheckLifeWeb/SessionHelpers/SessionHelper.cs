using CheckLifeWeb.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace CheckLifeWeb.SessionHelpers
{
    internal class SessionHelper
    {

        public bool UsuarioLogueado(ISession session)
        {
            var value = session.GetString("usuario");
            //if (value == null)
            //    return false;

            return value == null;
        }

        public void SetUsuario(ISession session, Login LoginActual)
        {
            session.SetString("usuario", JsonConvert.SerializeObject(LoginActual));
        }

        public Login GetUsuario(ISession session)
        {
            var value = session.GetString("usuario");
            return value == null ? null : JsonConvert.DeserializeObject<Login>(value);
        }

        public String GetNombreUsuario(ISession session)
        {
            var value = session.GetString("usuario");
            if (value == null) 
                { return null; }

            Login login = JsonConvert.DeserializeObject<Login>(value);
            return login.User;
        }
    }
}