using Mov.Domain.Dtos.Matricula;

namespace Mov.Domain.Interfaces.Services;

public interface IMatriculaService
{
    Task<IEnumerable<MatriculaDto>> GetAllAsync();
    Task<MatriculaDto?> GetByIdAsync(int id);
    Task<MatriculaDto> CreateAsync(CreateMatriculaDto dto);
    Task<MatriculaDto> UpdateAsync(UpdateMatriculaDto dto);
    Task DeleteAsync(int id);
}
