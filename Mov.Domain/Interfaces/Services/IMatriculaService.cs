using Mov.Domain.Dtos.Matricula;

namespace Mov.Domain.Interfaces.Services;

public interface IMatriculaService
{
    Task<IEnumerable<MatriculaDto>> GetAllAsync();
    Task<MatriculaDto?> GetByIdAsync(Guid id);
    Task<MatriculaDto> CreateAsync(CreateMatriculaDto dto);
    Task<MatriculaDto> UpdateAsync(Guid id, UpdateMatriculaDto dto);
    Task DeleteAsync(Guid id);
}
