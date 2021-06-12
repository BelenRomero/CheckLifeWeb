using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckLifeWeb.Models;

namespace CheckLifeWeb.Controllers
{
    public class PacientesController : Controller
    {
        private readonly CheckLifeBDContext _context;

        public PacientesController(CheckLifeBDContext context)
        {
            _context = context;
        }

        public IActionResult Index(Login LoginActual)
        {
            //Paciente PacienteActual = BuscarInfoPaciente(LoginActual.ID);
            return View(LoginActual);
        }

        public IActionResult Perfil(int ID)
        {
            Paciente Paciente = BuscarInfoPaciente(ID);
            return View(Paciente);
        }

        public async Task<IActionResult> Vacunas(int ID)
        {
            return View(await _context.Vacunas
                .FirstOrDefaultAsync(m => m.Paciente.ID == ID));
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
