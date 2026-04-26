using Mov.Domain.Entities;

namespace Mov.Domain.Interfaces.Repositories;

public interface IAlunoRepository
{
    Task<IEnumerable<Aluno>> GetAllAsync();
    Task<Aluno?> GetByIdAsync(Guid id);
    Task<Aluno> CreateAsync(Aluno aluno);
    Task<Aluno> UpdateAsync(Aluno aluno);
    Task DeleteAsync(Guid id);
}
