using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckLifeWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using CheckLifeWeb.SessionHelpers;

namespace CheckLifeWeb.Controllers
{
    //[ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    [TypeFilter(typeof(CustomAuthorizationFilterAttribute))]
    public class PacientesController : Controller
    {
        SessionHelper s = new SessionHelper();

        private readonly CheckLifeBDContext _context;

        private readonly ILogger<LoginController> _logger;

        public PacientesController(ILogger<LoginController> logger, CheckLifeBDContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult LogearUsuario(Login LoginActual)
        {
            s.SetUsuario(HttpContext.Session, LoginActual);
            return RedirectToAction("Index");
        }

        public IActionResult Perfil()
        {
            Login LoginActual = s.GetUsuario(HttpContext.Session);
            Paciente Paciente = BuscarInfoPaciente(LoginActual.ID);
            return View(Paciente);
        }

        public async Task<IActionResult> Vacunas()
        {
            Login LoginActual = s.GetUsuario(HttpContext.Session);
            return View(await _context.Vacunas
                .FirstOrDefaultAsync(m => m.Paciente.ID == LoginActual.ID));
        }
       

        private Paciente BuscarInfoPaciente(int LoginID)
        {
            var paciente = _context.Pacientes
                .Include("Login")
                .FirstOrDefaultAsync(m => m.Login.ID == LoginID);
            return paciente.Result;
        }

        private Paciente BuscarInfoPacientePorID(int ID)
        {
            var paciente = _context.Pacientes.
                FirstOrDefaultAsync(m => m.ID == ID);
            return paciente.Result;
        }
    }
}
