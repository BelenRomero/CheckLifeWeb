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
    public class MedicosController : Controller
    {
        private readonly CheckLifeBDContext _context;

        public MedicosController(CheckLifeBDContext context)
        {
            _context = context;
        }

        public IActionResult Index(Login LoginActual)
        {
            Medico MedicoActual = BuscarInfoMedico(LoginActual.ID);
            return View(MedicoActual);
        }

        
        public async Task<IActionResult> Pacientes(int ID)
        {
            return View(await _context.Pacientes
                .FirstOrDefaultAsync(m => m.MedicoCabeceraID == ID));
        }

        private Medico BuscarInfoMedico(int LoginID)
        {
            var medico = _context.Medicos.
                FirstOrDefaultAsync(m => m.Login.ID == LoginID);
            return medico.Result;
        }
      
        
    }
}
