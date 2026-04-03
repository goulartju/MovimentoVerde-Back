using Mov.Domain.Entities;

namespace Mov.Domain.Interfaces.Repositories;

public interface IMatriculaRepository
{
    Task<IEnumerable<Matricula>> GetAllAsync();
    Task<Matricula?> GetByIdAsync(int id);
    Task<Matricula> CreateAsync(Matricula matricula);
    Task<Matricula> UpdateAsync(Matricula matricula);
    Task DeleteAsync(int id);
}
