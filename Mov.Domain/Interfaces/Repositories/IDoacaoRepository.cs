using Mov.Domain.Entities;

namespace Mov.Domain.Interfaces.Repositories;

public interface IDoacaoRepository
{
    Task<IEnumerable<Doacao>> GetAllAsync();
    Task<Doacao?> GetByIdAsync(int id);
    Task<IEnumerable<Doacao>> GetByMatriculaIdAsync(Guid matriculaId);
    Task<IEnumerable<Doacao>> GetByEscolaIdAsync(Guid escolaId);
    Task<Doacao> CreateAsync(Doacao doacao);
    Task<Doacao> UpdateAsync(Doacao doacao);
    Task DeleteAsync(int id);
}
