using Microsoft.EntityFrameworkCore;
using SistemasConsultas.Models;

namespace SistemasConsultas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Consulta> Consultas { get; set; }
    }
}