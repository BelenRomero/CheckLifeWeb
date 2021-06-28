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

        //-------------------------------------------SECCION------------------------------------------------------------------------
        [AllowAnonymous]
        public IActionResult LogearUsuario(Login LoginActual)
        {
            LoginActual.Password = null;
            s.SetUsuario(HttpContext.Session, LoginActual);
            return RedirectToAction("Index");
        }
        //-------------------------------------------PERFIL PACIENTE-----------------------------------------------------------------
        public IActionResult Perfil()
        {
            Login LoginActual = s.GetUsuario(HttpContext.Session);
            Paciente Paciente = BuscarInfoPaciente(LoginActual.ID);
            return View(Paciente);
        }

        public async Task<IActionResult> Modificar(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Paciente = await _context.Pacientes.FindAsync(id);
            Login LoginS = s.GetUsuario(HttpContext.Session);
            Paciente.Login = LoginS;
            Paciente.User = LoginS.User;
            Paciente.Password = LoginS.Password;

            ViewBag.Nacionalidades = _context.Nacionalidades.ToList();
            ViewData["MedicoCabeceraID"] = new SelectList(_context.Medicos, "ID", "Apellido", Paciente.MedicoCabeceraID);

            return View(Paciente);
        }

        public async Task<IActionResult> Guardar(int id, [Bind("ID,DNI,Nombre,Apellido,Edad,FechaNacimiento,NacionalidadID,Email,LoginID,User,Password,Telefono,MedicoCabeceraID")] Paciente paciente)
        {
            if (id != paciente.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();

                    if (paciente.Password != null)
                    {
                        Login login = s.GetUsuario(HttpContext.Session);
                        login.Password = paciente.Password;
                        //login.User = paciente.User;
                        _context.Update(login);
                        await _context.SaveChangesAsync();
                        //s.SetUsuario(HttpContext.Session, login);
                    }


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Perfil");
            }
            ViewBag.MsjError = "Error al guardar, verificar los datos modificados";
            return RedirectToAction("Modificar", paciente);
        }

        //-------------------------------------------LISTA DE VACUNAS APLICADAS Y PENDIENTES-------------------------------------------
        public async Task<IActionResult> Vacunas()
        {
            int id = BuscarID(s.GetUsuario(HttpContext.Session).ID);
            return View(await _context.Vacunas
                                        .Include("Estado")
                                        .Include("CalendarioVacuna")
                                        .Where(m => m.PacienteID == id )
                                        .ToListAsync());
        }

        //-------------------------------------------FUNCIONES------------------------------------------------------------------------
        private Paciente BuscarInfoPaciente(int LoginID)
        {
            var paciente = _context.Pacientes
                .Include("Login")
                .Include("Nacionalidad")
                .Include("MedicoCabecera")
                .FirstOrDefaultAsync(m => m.Login.ID == LoginID);
            return paciente.Result;
        }

        private int BuscarID(int LoginID)
        {
            var paciente = _context.Pacientes
                .FirstOrDefaultAsync(m => m.Login.ID == LoginID);
            return paciente.Result.ID;
        }

        private Paciente BuscarInfoPacientePorID(int ID)
        {
            var paciente = _context.Pacientes.
                FirstOrDefaultAsync(m => m.ID == ID);
            return paciente.Result;
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.ID == id);
        }
    }
}
