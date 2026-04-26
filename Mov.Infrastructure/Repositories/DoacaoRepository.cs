using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Infrastructure.Data;

namespace Mov.Infrastructure.Repositories;

public class DoacaoRepository : IDoacaoRepository
{
    private readonly AppDbContext _context;

    public DoacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Doacao>> GetAllAsync()
    {
        return await _context.Doacoes
            .Include(d => d.Matricula)
                .ThenInclude(m => m!.Aluno)
            .Include(d => d.Matricula)
                .ThenInclude(m => m!.Turma)
            .Include(d => d.Escola)
            .Include(d => d.Calendario)
            .ToListAsync();
    }

    public async Task<Doacao?> GetByIdAsync(int id)
    {
        return await _context.Doacoes
            .Include(d => d.Matricula)
                .ThenInclude(m => m!.Aluno)
            .Include(d => d.Matricula)
                .ThenInclude(m => m!.Turma)
            .Include(d => d.Escola)
            .Include(d => d.Calendario)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Doacao>> GetByMatriculaIdAsync(Guid matriculaId)
    {
        return await _context.Doacoes
            .Where(d => d.MatriculaId == matriculaId)
            .Include(d => d.Matricula)
                .ThenInclude(m => m!.Aluno)
            .Include(d => d.Matricula)
                .ThenInclude(m => m!.Turma)
            .Include(d => d.Escola)
            .Include(d => d.Calendario)
            .ToListAsync();
    }

    public async Task<IEnumerable<Doacao>> GetByEscolaIdAsync(Guid escolaId)
    {
        return await _context.Doacoes
            .Where(d => d.EscolaId == escolaId)
            .Include(d => d.Matricula)
                .ThenInclude(m => m!.Aluno)
            .Include(d => d.Matricula)
                .ThenInclude(m => m!.Turma)
            .Include(d => d.Escola)
            .Include(d => d.Calendario)
            .ToListAsync();
    }

    public async Task<Doacao> CreateAsync(Doacao doacao)
    {
        doacao.CriadoEm = DateTime.UtcNow;
        _context.Doacoes.Add(doacao);
        await _context.SaveChangesAsync();
        return doacao;
    }

    public async Task<Doacao> UpdateAsync(Doacao doacao)
    {
        doacao.AtualizadoEm = DateTime.UtcNow;
        _context.Doacoes.Update(doacao);
        await _context.SaveChangesAsync();
        return doacao;
    }

    public async Task DeleteAsync(int id)
    {
        var doacao = await _context.Doacoes.FindAsync(id);
        if (doacao != null)
        {
            _context.Doacoes.Remove(doacao);
            await _context.SaveChangesAsync();
        }
    }
}
