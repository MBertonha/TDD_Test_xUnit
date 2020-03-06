using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Dados.Contexto {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) {

        }

        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
        }

        public async Task Commit () {
            await SaveChangesAsync ();
        }
    }
}
