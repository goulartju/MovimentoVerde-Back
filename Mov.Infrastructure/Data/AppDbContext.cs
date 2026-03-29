using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;

namespace Mov.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Calendario> Calendarios { get; set; }
    }
}