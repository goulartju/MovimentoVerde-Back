using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;
using Mov.Domain.Enums;

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
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Doacao> Doacoes { get; set; }
        public DbSet<Medalha> Medalhas { get; set; }

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

            // Configurar relacionamento entre Matricula e Aluno
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Aluno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relacionamento entre Matricula e Turma
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Turma)
                .WithMany(t => t.Matriculas)
                .HasForeignKey(m => m.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relacionamento entre Matricula e Calendario
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Calendario)
                .WithMany(c => c.Matriculas)
                .HasForeignKey(m => m.CalendarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relacionamento entre Doacao e Matricula
            modelBuilder.Entity<Doacao>()
                .HasOne(d => d.Matricula)
                .WithMany(m => m.Doacoes)
                .HasForeignKey(d => d.MatriculaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relacionamento entre Doacao e Escola
            modelBuilder.Entity<Doacao>()
                .HasOne(d => d.Escola)
                .WithMany(e => e.Doacoes)
                .HasForeignKey(d => d.EscolaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relacionamento entre Doacao e Calendario
            modelBuilder.Entity<Doacao>()
                .HasOne(d => d.Calendario)
                .WithMany()
                .HasForeignKey(d => d.CalendarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed do primeiro usuário admin
            var adminId = Guid.NewGuid();
            var senhaHashAdmin = BCrypt.Net.BCrypt.HashPassword("AdminSenha123");

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = adminId,
                    Nome = "Administrador",
                    Email = "admin@example.com",
                    SenhaHash = senhaHashAdmin,
                    DataNascimento = new DateTime(1990, 1, 1),
                    Cargo = "Gerente do Sistema",
                    Permissao = PermissaoEnum.Administrador,
                    Ativo = true,
                    CriadoEm = DateTime.UtcNow
                }
            );
        }
    }
}