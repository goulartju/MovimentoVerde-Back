using Mov.Domain.Entities;

namespace Mov.Domain.Interfaces.Repositories;

public interface IDoacaoRepository
{
    Task<IEnumerable<Doacao>> GetAllAsync();
    Task<Doacao?> GetByIdAsync(Guid id);
    Task<IEnumerable<Doacao>> GetByMatriculaIdAsync(Guid matriculaId);
    Task<IEnumerable<Doacao>> GetByEscolaIdAsync(Guid escolaId);
    Task<IEnumerable<Doacao>> GetByFilterAsync(Guid calendarioId, DateTime data, Guid escolaId, Guid turmaId);
    Task<IEnumerable<Doacao>> CreateLoteAsync(List<Doacao> doacao);
    Task<IEnumerable<Doacao>> UpdateLoteAsync(List<Doacao> doacao);
    Task DeleteAsync(Guid id);
}
