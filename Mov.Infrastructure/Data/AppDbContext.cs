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

            // Configurar tamanhos de coluna para Escola (MySQL não permite default em TEXT)
            modelBuilder.Entity<Escola>(entity =>
            {
                entity.Property(e => e.Nome).HasMaxLength(200);
                entity.Property(e => e.Municipio).HasMaxLength(200);
                entity.Property(e => e.Contato).HasMaxLength(100);
                entity.Property(e => e.Diretor).HasMaxLength(200);
            });

            // Configurar relacionamento entre Turma e RepresentanteTurma (um para um)
            modelBuilder.Entity<RepresentanteTurma>()
                .HasOne(rt => rt.Turma)
                .WithOne(t => t.Representante)
                .HasForeignKey<RepresentanteTurma>(rt => rt.TurmaId)
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



            // criar índice único (one-to-one)
            modelBuilder.Entity<RepresentanteTurma>()
                .HasIndex(rt => rt.TurmaId)
                .IsUnique();


        }
    }
}