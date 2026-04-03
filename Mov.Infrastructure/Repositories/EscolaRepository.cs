using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Infrastructure.Data;

namespace Mov.Infrastructure.Repositories
{
    public class EscolaRepository : IEscolaRepository
    {
        private readonly AppDbContext _context;

        public EscolaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Escola>> GetAllAsync()
        {
            return await _context.Escolas.Where(e => e.Ativo).ToListAsync();
        }

        public async Task<Escola?> GetByIdAsync(Guid id)
        {
            return await _context.Escolas.FirstOrDefaultAsync(e => e.Id == id && e.Ativo);
        }

        public async Task<Escola> CreateAsync(Escola escola)
        {
            if (escola.Id == Guid.Empty)
                escola.Id = Guid.NewGuid();

            escola.CriadoEm = DateTime.UtcNow;
            await _context.Escolas.AddAsync(escola);
            await _context.SaveChangesAsync();
            return escola;
        }

        public async Task<Escola> UpdateAsync(Escola escola)
        {
            escola.AtualizadoEm = DateTime.UtcNow;
            _context.Entry(escola).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return escola;
        }

        public async Task DeleteAsync(Guid id)
        {
            var escola = await _context.Escolas.FindAsync(id);
            if (escola != null)
            {
                escola.Ativo = false;
                escola.AtualizadoEm = DateTime.UtcNow;
                _context.Escolas.Update(escola);
                await _context.SaveChangesAsync();
            }
        }
    }
}
