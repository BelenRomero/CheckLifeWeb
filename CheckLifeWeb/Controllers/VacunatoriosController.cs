using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckLifeWeb.Models;
using CheckLifeWeb.SessionHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace CheckLifeWeb.Controllers
{
    [TypeFilter(typeof(CustomAuthorizationFilterAttribute))]
    public class VacunatoriosController : Controller
    {
        SessionHelper s = new SessionHelper();

        private readonly CheckLifeBDContext _context;

        private readonly ILogger<LoginController> _logger;

        public VacunatoriosController(ILogger<LoginController> logger, CheckLifeBDContext context)
        {
            _logger = logger;
            _context = context;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? DNI) //Esto me va a ayudar a buscar el dni del paciente que se indique, tiene validacion
        {
            ViewBag.MsjError = null;

            if (DNI == null)
            {
                ViewBag.MsjError = "Ingrese un documento para buscar un paciente";
            }
            else
            {
                Paciente Paciente = BuscarPaciente(DNI);
                if (Paciente == null)
                    ViewBag.MsjError = "No se encontro el paciente con DNI: " + DNI ;
                else
                {
                    return View("Paciente", await _context.Pacientes
                                                               .Include("Nacionalidad")
                                                               .Where(m => m.ID == Paciente.ID)
                                                               .ToListAsync());
                }

            }
            return View();
        }
        //-------------------------------------------SECCION-------------------------------------------------------------------------------
        [AllowAnonymous]
        public IActionResult LogearUsuario(Login LoginActual)
        {
            LoginActual.Password = null;
            s.SetUsuario(HttpContext.Session, LoginActual);
            return RedirectToAction("Index");
        }

        //-------------------------------------------LISTA DE CENTROS DE VACUNATORIOS DISPONIBLES-------------------------------------------
        [AllowAnonymous] //no se necesita que se este logeado para consultarlo
        public async Task<IActionResult> CentrosDeVacunacion()
        {
            return View(await _context.Vacunatorios.ToListAsync());
        }

        //-------------------------------------------PACIENTE------------------------------------------------------------------------------

        public async Task<IActionResult> Paciente(int ID)
        {
            return View(await _context.Pacientes
                                      .Include("Nacionalidad")
                                     .Where(m => m.ID == ID)
                                     .ToListAsync());
        }

        public async Task<IActionResult> Aplicadas(int ID) 
        {
            ViewBag.ID = ID;
            ViewBag.SinRegistro = "No se encontro vacunas aplicadas vacunas";
            ViewBag.Accion = "Aplicadas";
            return View("Vacunas", await _context.Vacunas
                    .Include("Estado")
                    .Include("CalendarioVacuna")
                    .Where(m => m.PacienteID == ID && m.EstadoID !=1)
                    .ToListAsync());
        }

        public async Task<IActionResult> Pendientes(int ID) 
        {
            ViewBag.ID = ID;
            ViewBag.SinRegistro = "No se encontro vacunas pendientes";
            ViewBag.Accion = "Pendientes";
            ViewBag.Aplicar = "Si";
            return View("Vacunas", await _context.Vacunas
                    .Include("CalendarioVacuna")
                    .Where(m => m.PacienteID == ID && m.EstadoID == 1)
                    .ToListAsync());
        }

        public IActionResult Aplicar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(BuscarVacuna(id));
        }

        [HttpPost]
        public async Task<IActionResult> Aplicar(int id, [Bind("ID,PacienteID,CalendarioVacunaID,Apellido,MarcaComercialLote,SelloFirma,MatriculaEnfermero")] Vacuna Vacuna)
        {
            if (id != Vacuna.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Vacuna.FechaAplicada = new DateTime();  ////verificar como poner fecha actual
                    Vacuna.EstadoID = 2; // estado aplicada
                    _context.Update(Vacuna);
                    await _context.SaveChangesAsync();
                    
                    ViewBag.ID = Vacuna.PacienteID;
                    ViewBag.SinRegistro = "No se encontro vacunas pendientes";
                    ViewBag.Aplicar = "Si";
                    ViewBag.Accion = "Pendientes";
                    return View("Vacunas", await _context.Vacunas
                            .Include("CalendarioVacuna")
                            .Where(m => m.PacienteID == Vacuna.PacienteID && m.EstadoID == 1)
                            .ToListAsync());

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacunaExists(Vacuna.ID))
                    {
                        ViewBag.MsjError = "No se encontro la vacuna a modificar";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Perfil");
            }
            ViewBag.MsjError = "Ocurrrio un error al guardar la vacuna";
            return View(Vacuna);
        }
        //-------------------------------------------PERFIL DEL VACUNATORIO-----------------------------------------------------------------
        public IActionResult Perfil()
        {
            Login LoginActual = s.GetUsuario(HttpContext.Session);
            Vacunatorio CentroVacunatorio = BuscarCentro(LoginActual.ID);
            
            return View(CentroVacunatorio);
        }

        public async Task<IActionResult> Modificar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Vacunatorio = await _context.Vacunatorios.FindAsync(id);
            Login LoginS = s.GetUsuario(HttpContext.Session);
            Vacunatorio.Login = LoginS;
            Vacunatorio.User = LoginS.User;
            Vacunatorio.Password = LoginS.Password;
            
            return View(Vacunatorio);
        }

        public async Task<IActionResult> Guardar(int id, [Bind("ID,DNI,Nombre,CUIT,,Direccion,Localidad,Email,LoginID,User,Password,Telefono")] Vacunatorio vacunatorio)
        {
            if (id != vacunatorio.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacunatorio);
                    await _context.SaveChangesAsync();
                    
                    if(vacunatorio.Password != null)
                    {
                        Login login = s.GetUsuario(HttpContext.Session);
                        login.Password = vacunatorio.Password;
                        _context.Update(login);
                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacunatorioExists(vacunatorio.ID))
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
            return RedirectToAction("Modificar", vacunatorio);
        }

        //-------------------------------------------FUNCIONES-----------------------------------------------------------------------------
        private Vacunatorio BuscarInfoVacunatorio(int LoginID)
        {
            var vacunatorio = _context.Vacunatorios.
                FirstOrDefaultAsync(m => m.Login.ID == LoginID);
            return vacunatorio.Result;
        }

        private Vacunatorio BuscarCentro(int LoginID)
        {
            var centro = _context.Vacunatorios
                .Include("Login")
                .FirstOrDefaultAsync(m => m.Login.ID == LoginID);

            return centro.Result;
        }

        private bool VacunatorioExists(int id)
        {
            return _context.Vacunatorios.Any(e => e.ID == id);
        }

        private bool VacunaExists(int id) //Verifica si existe la vacuna en la base
        {
            return _context.Vacunas.Any(e => e.ID == id);
        }

        private Paciente BuscarPaciente(int? DNI) 
        {
            var paciente = _context.Pacientes
                    .Include("Login")
                    .Include("Nacionalidad")
                    .FirstOrDefaultAsync(e => e.DNI == DNI);

            return paciente.Result;
        }

        private Vacuna BuscarVacuna(int? ID) //Busca el paciente por su dni y el id del medico
        {
            var vacuna = _context.Vacunas
                    .Include("CalendarioVacuna")
                    .FirstOrDefaultAsync(e => e.ID == ID);

            return vacuna.Result;
        }
    }
}
