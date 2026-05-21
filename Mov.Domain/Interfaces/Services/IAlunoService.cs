using Mov.Domain.Dtos.Aluno;

namespace Mov.Domain.Interfaces.Services;

public interface IAlunoService
{
    Task<IEnumerable<AlunoDto>> GetAllAsync();
    Task<AlunoDto?> GetByIdAsync(Guid id);
    Task<AlunoDto> CreateAsync(CreateAlunoDto dto);
    Task<AlunoDto> UpdateAsync(Guid id, UpdateAlunoDto dto);
    Task DeleteAsync(Guid id);
}
