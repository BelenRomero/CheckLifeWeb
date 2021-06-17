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
            if (value == null)
                return false;

            return true;
        }

        public void SetUsuario(ISession session, Login LoginActual)
        {
            //Login login = new Login();
            //login.User = value;
            session.SetString("usuario", JsonConvert.SerializeObject(LoginActual));
        }

        public Login GetUsuario(ISession session)
        {
            var value = session.GetString("usuario");
            if (value == null)
                return null;

            return JsonConvert.DeserializeObject<Login>(value);
        }
    }
}