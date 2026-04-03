using Mov.Domain.Entities;

namespace Mov.Domain.Interfaces.Repositories;

public interface ITurmaRepository
{
    Task<IEnumerable<Turma>> GetAllAsync();
    Task<Turma?> GetByIdAsync(Guid id);
    Task<IEnumerable<Turma>> GetByEscolaIdAsync(Guid escolaId);
    Task<Turma> CreateAsync(Turma turma);
    Task<Turma> UpdateAsync(Turma turma);
    Task DeleteAsync(Guid id);
}

public interface IRepresentanteTurmaRepository
{
    Task<IEnumerable<RepresentanteTurma>> GetByTurmaIdAsync(Guid turmaId);
    Task<RepresentanteTurma?> GetByIdAsync(Guid id);
    Task<RepresentanteTurma> AddRepresentanteAsync(RepresentanteTurma representanteTurma);
    Task RemoveRepresentanteAsync(Guid representanteTurmaId);
}
