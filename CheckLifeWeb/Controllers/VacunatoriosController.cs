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
    public class VacunatoriosController : Controller
    {
        private readonly CheckLifeBDContext _context;

        public VacunatoriosController(CheckLifeBDContext context)
        {
            _context = context;
        }

        public IActionResult Index(Login LoginActual)
        {
            Vacunatorio VacunatorioActual = BuscarInfoVacunatorio(LoginActual.ID);
            return View(VacunatorioActual);
        }

        //GET: Centro
        public async Task<IActionResult> CentrosDeVacunacion()
        {
            return View(await _context.Vacunatorios.ToListAsync());
        }

        private Vacunatorio BuscarInfoVacunatorio(int LoginID)
        {
            var vacunatorio = _context.Vacunatorios.
                FirstOrDefaultAsync(m => m.Login.ID == LoginID);
            return vacunatorio.Result;
        }

    }
}
