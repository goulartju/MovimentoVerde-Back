using Mov.Domain.Entities;

namespace Mov.Domain.Interfaces.Repositories;

public interface IAlunoRepository
{
    Task<IEnumerable<Aluno>> GetAllAsync();
    Task<Aluno?> GetByIdAsync(int id);
    Task<Aluno> CreateAsync(Aluno aluno);
    Task<Aluno> UpdateAsync(Aluno aluno);
    Task DeleteAsync(int id);
}
