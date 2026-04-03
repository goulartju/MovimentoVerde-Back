using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;

namespace Mov.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Calendario> Calendarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<RepresentanteTurma> RepresentanteTurmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relacionamento entre Turma e RepresentanteTurma
            modelBuilder.Entity<RepresentanteTurma>()
                .HasOne(rt => rt.Turma)
                .WithMany(t => t.Representantes)
                .HasForeignKey(rt => rt.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RepresentanteTurma>()
                .HasOne(rt => rt.Usuario)
                .WithMany()
                .HasForeignKey(rt => rt.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}