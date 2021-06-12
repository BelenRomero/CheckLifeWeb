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
    public class PacientesPruebaController : Controller
    {
        private readonly CheckLifeBDContext _context;

        public PacientesPruebaController(CheckLifeBDContext context)
        {
            _context = context;
        }

        // GET: PacientesPrueba
        public async Task<IActionResult> Index()
        {
            var checkLifeBDContext = _context.Pacientes.Include(p => p.Login).Include(p => p.MedicoCabecera).Include(p => p.Nacionalidad);
            return View(await checkLifeBDContext.ToListAsync());
        }

        // GET: PacientesPrueba/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Login)
                .Include(p => p.MedicoCabecera)
                .Include(p => p.Nacionalidad)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: PacientesPrueba/Create
        public IActionResult Create()
        {
            ViewData["LoginID"] = new SelectList(_context.Logins, "ID", "ID");
            ViewData["MedicoCabeceraID"] = new SelectList(_context.Medicos, "ID", "Apellido");
            ViewData["NacionalidadID"] = new SelectList(_context.Nacionalidades, "ID", "ID");
            return View();
        }

        // POST: PacientesPrueba/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DNI,Nombre,Apellido,Edad,FechaNacimiento,NacionalidadID,Email,LoginID,Telefono,MedicoCabeceraID")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoginID"] = new SelectList(_context.Logins, "ID", "ID", paciente.LoginID);
            ViewData["MedicoCabeceraID"] = new SelectList(_context.Medicos, "ID", "Apellido", paciente.MedicoCabeceraID);
            ViewData["NacionalidadID"] = new SelectList(_context.Nacionalidades, "ID", "ID", paciente.NacionalidadID);
            return View(paciente);
        }

        // GET: PacientesPrueba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewData["LoginID"] = new SelectList(_context.Logins, "ID", "ID", paciente.LoginID);
            ViewData["MedicoCabeceraID"] = new SelectList(_context.Medicos, "ID", "Apellido", paciente.MedicoCabeceraID);
            ViewData["NacionalidadID"] = new SelectList(_context.Nacionalidades, "ID", "ID", paciente.NacionalidadID);
            return View(paciente);
        }

        // POST: PacientesPrueba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DNI,Nombre,Apellido,Edad,FechaNacimiento,NacionalidadID,Email,LoginID,Telefono,MedicoCabeceraID")] Paciente paciente)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoginID"] = new SelectList(_context.Logins, "ID", "ID", paciente.LoginID);
            ViewData["MedicoCabeceraID"] = new SelectList(_context.Medicos, "ID", "Apellido", paciente.MedicoCabeceraID);
            ViewData["NacionalidadID"] = new SelectList(_context.Nacionalidades, "ID", "ID", paciente.NacionalidadID);
            return View(paciente);
        }

        // GET: PacientesPrueba/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Login)
                .Include(p => p.MedicoCabecera)
                .Include(p => p.Nacionalidad)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: PacientesPrueba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.ID == id);
        }
    }
}
