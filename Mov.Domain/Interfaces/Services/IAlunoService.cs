using Mov.Domain.Dtos.Aluno;

namespace Mov.Domain.Interfaces.Services;

public interface IAlunoService
{
    Task<IEnumerable<AlunoDto>> GetAllAsync();
    Task<AlunoDto?> GetByIdAsync(int id);
    Task<AlunoDto> CreateAsync(CreateAlunoDto dto);
    Task<AlunoDto> UpdateAsync(UpdateAlunoDto dto);
    Task DeleteAsync(int id);
}
