using Mov.Domain.Dtos.Doacao;

namespace Mov.Domain.Interfaces.Services;

public interface IDoacaoService
{
    Task<IEnumerable<DoacaoDto>> GetAllAsync();
    Task<DoacaoDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<DoacaoDto>> GetByMatriculaIdAsync(Guid matriculaId);
    Task<IEnumerable<DoacaoDto>> GetByEscolaIdAsync(Guid escolaId);
    Task<IEnumerable<DoacaoDto>> GetByFilterAsync(DoacaoFilterDto filter);
    Task<IEnumerable<DoacaoDto>> CreateByFilterAsync(DoacaoFilterDto filter);
    Task<IEnumerable<DoacaoDto>> CreateLoteAsync(CreateDoacaoLoteDto dto);
    Task<IEnumerable<DoacaoDto>> UpdateLoteAsync(UpdateDoacaoLoteDto dto);
    Task DeleteAsync(Guid id);
}
