using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Infrastructure.Data;

namespace Mov.Infrastructure.Repositories;

public class TurmaRepository : ITurmaRepository
{
    private readonly AppDbContext _context;

    public TurmaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Turma>> GetAllAsync()
    {
        return await _context.Turmas
            .Where(t => t.Ativo)
            .Include(t => t.Representantes)
            .ThenInclude(r => r.Usuario)
            .ToListAsync();
    }

    public async Task<Turma?> GetByIdAsync(Guid id)
    {
        return await _context.Turmas
            .Include(t => t.Representantes)
            .ThenInclude(r => r.Usuario)
            .FirstOrDefaultAsync(t => t.Id == id && t.Ativo);
    }

    public async Task<IEnumerable<Turma>> GetByEscolaIdAsync(Guid escolaId)
    {
        return await _context.Turmas
            .Where(t => t.EscolaId == escolaId && t.Ativo)
            .Include(t => t.Representantes)
            .ThenInclude(r => r.Usuario)
            .ToListAsync();
    }

    public async Task<Turma> CreateAsync(Turma turma)
    {
        turma.Id = Guid.NewGuid();
        turma.CriadoEm = DateTime.UtcNow;
        _context.Turmas.Add(turma);
        await _context.SaveChangesAsync();
        return turma;
    }

    public async Task<Turma> UpdateAsync(Turma turma)
    {
        turma.AtualizadoEm = DateTime.UtcNow;
        _context.Turmas.Update(turma);
        await _context.SaveChangesAsync();
        return turma;
    }

    public async Task DeleteAsync(Guid id)
    {
        var turma = await _context.Turmas.FindAsync(id);
        if (turma != null)
        {
            turma.Ativo = false;
            turma.AtualizadoEm = DateTime.UtcNow;
            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();
        }
    }
}

public class RepresentanteTurmaRepository : IRepresentanteTurmaRepository
{
    private readonly AppDbContext _context;

    public RepresentanteTurmaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RepresentanteTurma>> GetByTurmaIdAsync(Guid turmaId)
    {
        return await _context.RepresentanteTurmas
            .Where(rt => rt.TurmaId == turmaId)
            .Include(rt => rt.Usuario)
            .ToListAsync();
    }

    public async Task<RepresentanteTurma?> GetByIdAsync(Guid id)
    {
        return await _context.RepresentanteTurmas
            .Include(rt => rt.Usuario)
            .FirstOrDefaultAsync(rt => rt.Id == id);
    }

    public async Task<RepresentanteTurma> AddRepresentanteAsync(RepresentanteTurma representanteTurma)
    {
        representanteTurma.Id = Guid.NewGuid();
        representanteTurma.AtribuidoEm = DateTime.UtcNow;
        _context.RepresentanteTurmas.Add(representanteTurma);
        await _context.SaveChangesAsync();
        return representanteTurma;
    }

    public async Task RemoveRepresentanteAsync(Guid representanteTurmaId)
    {
        var representanteTurma = await _context.RepresentanteTurmas.FindAsync(representanteTurmaId);
        if (representanteTurma != null)
        {
            _context.RepresentanteTurmas.Remove(representanteTurma);
            await _context.SaveChangesAsync();
        }
    }
}
