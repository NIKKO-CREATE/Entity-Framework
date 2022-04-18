using EFCore.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repo
{
    public class HeroiContexto : DbContext
    {
        // public HeroiContexto(DbContextOptions<HeroiContexto> options) : base(options) {  }
        public HeroiContexto() { }
        public DbSet<Heroi> Herois { get; set; }
        public DbSet<Batalha> Batalhas { get; set; }
        public DbSet<Arma> Armas { get; set; }
        public DbSet<HeroiBatalha> HeroisBatalhas { get; set; }
        public DbSet<IdentidadeSecreta> IdentidadesSecretas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Password=Sandro123;Persist Security Info=True;User ID=SANDRO;Initial Catalog=Heroi;Data Source=POAGES1CJX053\SQLEXPRESS");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroiBatalha>(entity =>
            {
                entity.HasKey(e => new { e.BatalhaId, e.HeroiId });
            });
        }
    }
}
