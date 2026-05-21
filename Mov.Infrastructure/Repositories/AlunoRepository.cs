using Microsoft.EntityFrameworkCore;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Infrastructure.Data;

namespace Mov.Infrastructure.Repositories;

public class AlunoRepository : IAlunoRepository
{
    private readonly AppDbContext _context;

    public AlunoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Aluno>> GetAllAsync()
    {
        return await _context.Alunos.Where(a => a.Ativo).ToListAsync();
    }

    public async Task<Aluno?> GetByIdAsync(Guid id)
    {
        return await _context.Alunos.FirstOrDefaultAsync(a => a.Id == id && a.Ativo);
    }

    public async Task<Aluno> CreateAsync(Aluno aluno)
    {
        aluno.CriadoEm = DateTime.UtcNow;
        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();
        return aluno;
    }

    public async Task<Aluno> UpdateAsync(Aluno aluno)
    {
        aluno.AtualizadoEm = DateTime.UtcNow;
        _context.Entry(aluno).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return aluno;
    }

    public async Task DeleteAsync(Guid id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno != null)
        {
            aluno.Ativo = false;
            aluno.AtualizadoEm = DateTime.UtcNow;
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
