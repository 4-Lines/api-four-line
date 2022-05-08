using IES300.API.Domain.Entities;
using IES300.API.Repository.Mapping;
using Microsoft.EntityFrameworkCore;

namespace IES300.API.Repository
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext() { }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Patrocinador> Patrocinador { get; set; }
        public DbSet<Tema> Tema { get; set; }
        public DbSet<Ficha> Ficha { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-7JPLA67;Database=quatroemlinha;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patrocinador>(new PatrocinadorMap().Configure);
            modelBuilder.Entity<Tema>(new TemaMap().Configure);
            modelBuilder.Entity<Ficha>(new FichaMap().Configure);

            base.OnModelCreating(modelBuilder);
        }
    }
}
