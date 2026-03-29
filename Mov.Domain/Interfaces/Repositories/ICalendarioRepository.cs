using Mov.Domain.Entities;

namespace Mov.Domain.Interfaces.Repositories;

public interface ICalendarioRepository
{
    Task<IEnumerable<Calendario>> GetAllAsync();
    Task<Calendario?> GetByIdAsync(Guid id);
    Task<Calendario> CreateAsync(Calendario calendario);
    Task<Calendario> UpdateAsync(Calendario calendario);
    Task DeleteAsync(Guid id);
}
