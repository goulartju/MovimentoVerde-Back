using Mov.Domain.Entities;

namespace Mov.Domain.Interfaces.Repositories;

public interface IMedalhaRepository
{
    Task<IEnumerable<Medalha>> GetAllAsync();
    Task<Medalha?> GetByIdAsync(int id);
    Task<Medalha> CreateAsync(Medalha medalha);
    Task<Medalha> UpdateAsync(Medalha medalha);
    Task DeleteAsync(int id);
}