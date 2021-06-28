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
    
    [TypeFilter(typeof(CustomAuthorizationFilterAttribute))]
    public class MedicosController : Controller
    {
        SessionHelper s = new SessionHelper();

        private readonly CheckLifeBDContext _context;

        private readonly ILogger<LoginController> _logger;

        public MedicosController(ILogger<LoginController> logger, CheckLifeBDContext context)
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
                Paciente Paciente = BuscarPaciente(DNI, BuscarID(s.GetUsuario(HttpContext.Session).ID));
                if (Paciente == null)
                    ViewBag.MsjError = "No se encontro el paciente con DNI: " + DNI + ". Es posible que no sea un paciente vinculado a su registro.";
                else {
                    return View("Paciente", await _context.Pacientes
                                                               .Include("Nacionalidad")
                                                               .Where(m => m.ID == Paciente.ID)
                                                               .ToListAsync());
                }
                   
            }
            return View();
        }

        //-------------------------------------------SECCION-------------------------------------------------------------------------------------------------
        [AllowAnonymous]
        [HttpPost]
        public IActionResult LogearUsuario(Login LoginActual) //Genera la seccion
        {
            LoginActual.Password = null;
            s.SetUsuario(HttpContext.Session, LoginActual);
            return RedirectToAction("Index");
        }


        //-------------------------------------------PERFIL MEDICO--------------------------------------------------------------------------------------------
        public IActionResult Perfil() //Perfil del Medico
        {
            Medico Medico = BuscarInfoMedico(s.GetUsuario(HttpContext.Session).ID);
            return View(Medico);
        }

        public async Task<IActionResult> Modificar(int? id) //Te permite la modificacion del perfil del medico logeado
        {
            if (id == null)
            {
                return NotFound();
            }

            var Medico = await _context.Medicos.FindAsync(id);
            Login LoginS = s.GetUsuario(HttpContext.Session);
            Medico.Login = LoginS;
            Medico.User = LoginS.User;
            Medico.Password = LoginS.Password;

            ViewBag.Nacionalidades = _context.Nacionalidades.ToList();

            return View(Medico);
        }

        //Para guardar en la db la info modificada del medico
        public async Task<IActionResult> Guardar(int id, [Bind("ID,DNI,Nombre,Apellido,Matricula,Edad,FechaNacimiento,NacionalidadID,Email,LoginID,User,Password,Telefono")] Medico medico)
        {
            if (id != medico.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();

                    if (medico.Password != null)
                    {
                        Login login = s.GetUsuario(HttpContext.Session);
                        login.Password = medico.Password;
                        _context.Update(login);
                        await _context.SaveChangesAsync();
                        //s.SetUsuario(HttpContext.Session, login); no modifica la seccion porque no es necesario actualizar solo la contra
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.ID))
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
            return RedirectToAction("Modificar", medico);
        }


        //-------------------------------------------PACIENTE/s VINCULADO---------------------------------------------------------------------------------------

        //public IActionResult InfoPaciente(int ID) //Vista InfoPaciente - Es algo parecida al perfil que ve el paciente, solo que tiene una opcion para consultar las vacunas.
        //{
        //    Paciente Paciente = BuscarInfoPaciente(ID);
        //    return View(Paciente);
        //}

        public async Task<IActionResult> Paciente(int ID) //Lista de pacientes que tiene relaccionado, lo filtra por el id del medico, ya que los pacientes guardan el id del medico de cabecera 
        {
            //int ID = BuscarID(s.GetUsuario(HttpContext.Session).ID);
            return View(await _context.Pacientes
                    .Include("Nacionalidad")
                    .Where(m => m.ID == ID)
                    .ToListAsync());
        }

        public async Task<IActionResult> Vacunas(int ID) //Lista de Vacunas que posee el paciente registrados, esten o no aplicadas
        {
            ViewBag.ID = ID;
            return View(await _context.Vacunas
                    .Include("Estado")
                    .Include("CalendarioVacuna")
                    .Where(m => m.PacienteID == ID)
                    .ToListAsync());
        }

        public IActionResult PedirVacuna(int ID) //Si es que el medico quiere enviarle una nueva vacuna al paciente
        {
            Vacuna Vacuna = new Vacuna();
            Vacuna.PacienteID = ID;
            Vacuna.EstadoID = 1; //Pendiente
            ViewBag.CalendarioVacuna = _context.CalendarioVacunas.ToList();
            return View(Vacuna);
        }

        public async Task<IActionResult> GuardarVacuna([Bind("PacienteID,CalendarioVacunaID,EstadoID")] Vacuna Vacuna) //guarda la vacuna en estado pendiente
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Vacuna.EstadoID = 1; // Pendiente
                    _context.Update(Vacuna);
                    await _context.SaveChangesAsync();

                    ViewBag.ID = Vacuna.PacienteID;
                    ViewBag.Msj = "Se guardo con exito la vacuna seleccionada.";

                    return View("Vacunas", await _context.Vacunas
                                            .Include("Estado")
                                            .Include("CalendarioVacuna")
                                            .Where(m => m.PacienteID == Vacuna.PacienteID)
                                            .ToListAsync());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(Vacuna.PacienteID))
                    {
                        ViewBag.MsjError = "No se encontro al paciente al momento de guardar la vacuna.";
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewBag.MsjError = "Ocurrio un error de Validacion";
            return View("PedirVacuna", Vacuna);
        }


        public async Task<IActionResult> Borrar(int id)
        {
            var vacuna = await _context.Vacunas.FindAsync(id);
            int PacienteID = vacuna.PacienteID;
            _context.Vacunas.Remove(vacuna);
            await _context.SaveChangesAsync();

            ViewBag.ID = PacienteID;
            ViewBag.Msj = "Se elimino con exito la vacuna seleccionada.";
            return View("Vacunas", await _context.Vacunas
                                                .Include("Estado")
                                                .Include("CalendarioVacuna")
                                                .Where(m => m.PacienteID == PacienteID)
                                                .ToListAsync());
        }


        //-------------------------------------------FUNCIONES----------------------------------------------------------------------------------------------------

        private Medico BuscarInfoMedico(int LoginID) //Busca al medico por el id del login
        {
            var medico = _context.Medicos
                .Include("Login")
                .Include("Nacionalidad")
                .FirstOrDefaultAsync(m => m.Login.ID == LoginID);

            return medico.Result;
        }

        private int BuscarID(int LoginID) //Busca el id del medico que esta logeado
        {
            var medico = _context.Medicos
                .FirstOrDefaultAsync(m => m.Login.ID == LoginID);

            return medico.Result.ID;
        }

        private bool MedicoExists(int id) //Verifica que exista el medico
        {
            return _context.Medicos.Any(e => e.ID == id);
        }

        private Paciente BuscarPaciente(int? DNI, int IDMedico) //Busca el paciente por su dni y el id del medico
        {
            var paciente = _context.Pacientes
                    .Include("Login")
                    .Include("Nacionalidad")
                    .FirstOrDefaultAsync(e => e.DNI == DNI && e.MedicoCabeceraID == IDMedico);

            return paciente.Result;
        }

        private Paciente BuscarInfoPaciente(int ID) //Busca la info del paciente, utilizado a la hora de mostrar el perfil del paciente
        {
            var paciente = _context.Pacientes
                .Include("Login")
                .Include("Nacionalidad")
                .FirstOrDefaultAsync(m => m.ID == ID);
            return paciente.Result;
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.ID == id);
        }

       
    }
}
