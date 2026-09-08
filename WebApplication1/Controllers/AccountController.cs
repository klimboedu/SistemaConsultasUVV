using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasConsultas.Data;
using SistemasConsultas.Models;
using SistemasConsultas.Models.ViewModels;
using System.Security.Claims;


namespace SistemasConsultas.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(CadastroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool emailExiste = await _context.Usuarios
                .AnyAsync(u => u.Email == model.Email);

            if (emailExiste)
            {
                ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
                return View(model);
            }

            var usuario = new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                DataCadastro = DateTime.Now
            };

            usuario.Senha = _passwordHasher.HashPassword(
                usuario,
                model.Senha
            );

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AcessoNegado()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (usuario == null)
            {
                ModelState.AddModelError("", "E-mail ou senha inválidos.");
                return View(model);
            }

            var resultado = _passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.Senha,
                model.Senha
            );

            if (resultado == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "E-mail ou senha inválidos.");
                return View(model);
            }

            var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
    new Claim(ClaimTypes.Name, usuario.Nome),
    new Claim(ClaimTypes.Email, usuario.Email)
};

            var identidade = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identidade);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return RedirectToAction("Index", "Home");
        }
    }
}