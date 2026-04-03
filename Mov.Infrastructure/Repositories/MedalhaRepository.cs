using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Infrastructure.Data;

namespace Mov.Infrastructure.Repositories;

public class MedalhaRepository : IMedalhaRepository
{
    private readonly AppDbContext _context;

    public MedalhaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Medalha>> GetAllAsync()
    {
        return await _context.Medalhas.ToListAsync();
    }

    public async Task<Medalha?> GetByIdAsync(int id)
    {
        return await _context.Medalhas.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Medalha> CreateAsync(Medalha medalha)
    {
        medalha.CriadoEm = DateTime.UtcNow;
        _context.Medalhas.Add(medalha);
        await _context.SaveChangesAsync();
        return medalha;
    }

    public async Task<Medalha> UpdateAsync(Medalha medalha)
    {
        medalha.AtualizadoEm = DateTime.UtcNow;
        _context.Medalhas.Update(medalha);
        await _context.SaveChangesAsync();
        return medalha;
    }

    public async Task DeleteAsync(int id)
    {
        var medalha = await _context.Medalhas.FindAsync(id);
        if (medalha != null)
        {
            _context.Medalhas.Remove(medalha);
            await _context.SaveChangesAsync();
        }
    }
}