using Mov.Domain.Dtos.Turma;

namespace Mov.Domain.Interfaces.Services;

public interface ITurmaService
{
    Task<IEnumerable<TurmaDto>> GetAllAsync();
    Task<TurmaDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<TurmaDto>> GetByEscolaIdAsync(Guid escolaId);
    Task<TurmaDto> CreateAsync(CreateTurmaDto dto);
    Task<TurmaDto> UpdateAsync(Guid id, UpdateTurmaDto dto);
    Task DeleteAsync(Guid id);
}
