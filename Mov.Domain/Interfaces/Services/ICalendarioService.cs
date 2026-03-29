using Mov.Domain.Entities;
using Mov.Domain.Dtos.Calendario;

namespace Mov.Domain.Interfaces.Services
{
    public interface ICalendarioService
    {
        Task<IEnumerable<Calendario>> GetAllAsync();
        Task<Calendario?> GetByIdAsync(Guid id);
        Task<Calendario> CreateAsync(CreateCalendarioDto dto);
        Task<Calendario> UpdateAsync(UpdateCalendarioDto dto);
        Task DeleteAsync(Guid id);
    }
}
