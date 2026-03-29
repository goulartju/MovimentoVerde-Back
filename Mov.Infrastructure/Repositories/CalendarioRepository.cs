using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Infrastructure.Data;

namespace Mov.Infrastructure.Repositories;

public class CalendarioRepository : ICalendarioRepository
{
    private readonly AppDbContext _context;

    public CalendarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Calendario>> GetAllAsync()
    {
        return await _context.Calendarios.ToListAsync();
    }

    public async Task<Calendario?> GetByIdAsync(Guid id)
    {
        return await _context.Calendarios.FindAsync(id);
    }

    public async Task<Calendario> CreateAsync(Calendario calendario)
    {
        if (calendario.Id == Guid.Empty)
            calendario.Id = Guid.NewGuid();

        await _context.Calendarios.AddAsync(calendario);
        await _context.SaveChangesAsync();
        return calendario;
    }

    public async Task<Calendario> UpdateAsync(Calendario calendario)
    {
        _context.Entry(calendario).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return calendario;
    }

    public async Task DeleteAsync(Guid id)
    {
        var calendario = await _context.Calendarios.FindAsync(id);
        if (calendario != null)
        {
            _context.Calendarios.Remove(calendario);
            await _context.SaveChangesAsync();
        }
    }
}
