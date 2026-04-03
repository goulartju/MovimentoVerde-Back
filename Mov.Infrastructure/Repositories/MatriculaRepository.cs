using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Infrastructure.Data;

namespace Mov.Infrastructure.Repositories;

public class MatriculaRepository : IMatriculaRepository
{
    private readonly AppDbContext _context;

    public MatriculaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Matricula>> GetAllAsync()
    {
        return await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .Include(m => m.Calendario)
            .ToListAsync();
    }

    public async Task<Matricula?> GetByIdAsync(int id)
    {
        return await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .Include(m => m.Calendario)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Matricula> CreateAsync(Matricula matricula)
    {
        matricula.CriadoEm = DateTime.UtcNow;
        _context.Matriculas.Add(matricula);
        await _context.SaveChangesAsync();
        return matricula;
    }

    public async Task<Matricula> UpdateAsync(Matricula matricula)
    {
        matricula.AtualizadoEm = DateTime.UtcNow;
        _context.Entry(matricula).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return matricula;
    }

    public async Task DeleteAsync(int id)
    {
        var matricula = await _context.Matriculas.FindAsync(id);
        if (matricula != null)
        {
            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
        }
    }
}
