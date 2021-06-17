using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckLifeWeb.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace CheckLifeWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly CheckLifeBDContext _context;

        public LoginController(CheckLifeBDContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login LoginActual)
        {
            if (LoginActual.User != null && LoginActual.Password != null)
            {
                Login LoginBuscado = BuscarUsuario(LoginActual.User);
                if (LoginBuscado == null)
                {
                    ViewBag.MsjError = "No se encontro registrado al usuario " + LoginActual.User;
                }
                else
                {
                    if(!LoginActual.Password.Equals(LoginBuscado.Password))
                    {
                        ViewBag.MsjError = "La contraseña ingresada es incorrecta.";
                    }
                    else
                    {
                        switch (LoginBuscado.Rol.ID) //Mando el login buscado porque tiene el id de la bd
                        {
                            case 1: //Paciente
                                return RedirectToActionPreserveMethod("LogearUsuario", "Pacientes", LoginBuscado);
                            case 2: //Medico
                                return RedirectToActionPreserveMethod("Index", "Medicos", LoginBuscado);
                            case 3: //Vacunatorio
                                return RedirectToActionPreserveMethod("Index", "Vacunatorios", LoginBuscado);
                            default:
                                ViewBag.MsjError = "No se encontro un perfil para el usuario " + LoginActual.User;
                                break;
                        }
                    }
                }
            }
            else
            {
                ViewBag.MsjError = "Error al consultar usuario y contraseña";
            }
            return View();
        }

        public IActionResult Registrar()
        {
            ViewBag.Roles = _context.Rols.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Rol Rol)
        {
            ViewBag.Nacionalidades = _context.Nacionalidades.ToList();
            //Crear el usuario
            switch (Rol.ID)
            {
                case 1: //Paciente
                    return View("RegistrarPaciente");
                case 2: //Medico
                    return View("RegistrarMedico");
                case 3: //Vacunatorio
                    return View("RegistrarVacunatorio");
                default:
                    ViewBag.MsjError = "Seleccione una opcion para poder crear un Registro";
                    break;
            }

            ViewBag.Roles = _context.Rols.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarMedico(Medico Medico, Login LoginNuevo)
        {
            if (!MedicoExists(Medico.DNI))
            {
                if (!UserExists(LoginNuevo.User))
                {
                    int idLogin = CrearLogin(LoginNuevo, 2);
                    Medico.LoginID = idLogin;
                    _context.Medicos.Add(Medico);
                    _context.SaveChanges();
                    return View("RegistrarOK");
                }
                else
                {
                    ViewBag.MsjError = "El usuario " + LoginNuevo.User + " ya existe.";
                }
            }
            else
            {
                ViewBag.MsjError = "Ya se encuentra ingresado en el sistema un usuario con dni " + Medico.DNI +".";
            }

            ViewBag.Nacionalidades = _context.Nacionalidades.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarPaciente(Paciente Paciente, Login LoginNuevo)
        {
            if (!PacienteExists(Paciente.DNI))
            {
                if (!UserExists(LoginNuevo.User))
                {
                    int idLogin = CrearLogin(LoginNuevo, 1);
                    Paciente.LoginID = idLogin;
                    _context.Pacientes.Add(Paciente);
                    _context.SaveChanges();
                    return View("RegistrarOK");
                }
                else
                {
                    ViewBag.MsjError = "El usuario " + LoginNuevo.User + " ya existe.";
                }
            }
            else
            {
                ViewBag.MsjError = "Ya se encuentra ingresado en el sistema un usuario con dni " + Paciente.DNI + ".";
            }

            ViewBag.Nacionalidades = _context.Nacionalidades.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarVacunatorio(Vacunatorio CentroVacunacion, Login LoginNuevo)
        {
            if (!VacunatorioExists(CentroVacunacion.CUIT))
            {
                if (!UserExists(LoginNuevo.User))
                {
                    int idLogin = CrearLogin(LoginNuevo, 3);
                    CentroVacunacion.LoginID = idLogin;
                    _context.Vacunatorios.Add(CentroVacunacion);
                    _context.SaveChanges();
                    return View("RegistrarOK");
                }
                else
                {
                    ViewBag.MsjError = "El usuario " + LoginNuevo.User + " ya existe.";
                }
            }
            else
            {
                ViewBag.MsjError = "Ya se encuentra ingresado en el sistema un usuario con dni " + CentroVacunacion.CUIT + ".";
            }
            return View();
        }

        public IActionResult RegistrarOK()
        {
            return View();
        }

        private bool UserExists(string User)
        {
            return _context.Logins.Any(e => e.User == User);
        }

        private bool MedicoExists(int dni)
        {
            return _context.Medicos.Any(e => e.DNI == dni);
        }

        private bool PacienteExists(int dni)
        {
            return _context.Pacientes.Any(e => e.DNI == dni);
        }

        private bool VacunatorioExists(int cuit)
        {
            return _context.Vacunatorios.Any(e => e.CUIT == cuit);
        }

        private Login BuscarUsuario (string User)
        {
            var login = _context.Logins
                .Include("Rol")
                .FirstOrDefaultAsync(m => m.User == User);
            return login.Result;
        }

        private int CrearLogin(Login LoginNuevo, int RolID)
        {
            //LoginNuevo.Rol = new Rol();
            LoginNuevo.RolID = RolID;
            _context.Logins.Add(LoginNuevo);
            _context.SaveChanges();
            var login = _context.Logins.FirstOrDefaultAsync(m => m.User == LoginNuevo.User);
            return login.Result.ID;
        }
        
        


    }
}
