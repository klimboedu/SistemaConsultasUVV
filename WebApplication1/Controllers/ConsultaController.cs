using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasConsultas.Data;
using SistemasConsultas.Models;
using SistemasConsultas.Models.ViewModels;

namespace SistemasConsultas.Controllers
{
    [Authorize]
    public class ConsultaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Consulta
        public async Task<IActionResult> Index()
        {
            int usuarioId = ObterUsuarioId();

            var consultas = await _context.Consultas
                .Where(c => c.UsuarioId == usuarioId)
                .OrderBy(c => c.DataHora)
                .ToListAsync();

            return View(consultas);
        }

        // GET: /Consulta/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var consulta = new Consulta
            {
                Especialidade = model.Especialidade,
                DataHora = model.DataHora,
                Descricao = model.Descricao,
                UsuarioId = ObterUsuarioId()
            };

            _context.Consultas.Add(consulta);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Consulta/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int usuarioId = ObterUsuarioId();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UsuarioId == usuarioId);

            if (consulta == null)
            {
                return NotFound();
            }

            var model = new ConsultaViewModel
            {
                Especialidade = consulta.Especialidade,
                DataHora = consulta.DataHora,
                Descricao = consulta.Descricao
            };

            return View(model);
        }

        // POST: /Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            ConsultaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int usuarioId = ObterUsuarioId();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UsuarioId == usuarioId);

            if (consulta == null)
            {
                return NotFound();
            }

            consulta.Especialidade = model.Especialidade;
            consulta.DataHora = model.DataHora;
            consulta.Descricao = model.Descricao;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Consulta/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int usuarioId = ObterUsuarioId();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UsuarioId == usuarioId);

            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: /Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int usuarioId = ObterUsuarioId();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UsuarioId == usuarioId);

            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consultas.Remove(consulta);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private int ObterUsuarioId()
        {
            string? id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.Parse(id!);
        }
    }
}