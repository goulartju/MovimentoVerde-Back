using Mov.Domain.Dtos.Doacao;

namespace Mov.Domain.Interfaces.Services;

public interface IDoacaoService
{
    Task<IEnumerable<DoacaoDto>> GetAllAsync();
    Task<DoacaoDto?> GetByIdAsync(int id);
    Task<IEnumerable<DoacaoDto>> GetByMatriculaIdAsync(int matriculaId);
    Task<IEnumerable<DoacaoDto>> GetByEscolaIdAsync(Guid escolaId);
    Task<DoacaoDto> CreateAsync(CreateDoacaoDto dto);
    Task<DoacaoDto> UpdateAsync(UpdateDoacaoDto dto);
    Task DeleteAsync(int id);
}
