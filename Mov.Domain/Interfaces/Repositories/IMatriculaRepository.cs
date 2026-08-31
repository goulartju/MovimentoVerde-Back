using Mov.Domain.Entities;

namespace Mov.Domain.Interfaces.Repositories;

public interface IMatriculaRepository
{
    Task<IEnumerable<Matricula>> GetAllAsync();
    Task<Matricula?> GetByIdAsync(Guid id);
    Task<IEnumerable<Matricula>> GetByTurmaIdAsync(Guid turmaId);
    Task<IEnumerable<Matricula>> GetByAlunoIdAsync(Guid alunoId);
    Task<Matricula?> GetByAlunoIdAndTurmaIdAsync(Guid alunoId, Guid turmaId);
    Task<Matricula?> GetByAlunoIdAndCalendarioIdAsync(Guid alunoId, Guid calendarioId);
    Task<Matricula> CreateAsync(Matricula matricula);
    Task<Matricula> UpdateAsync(Matricula matricula);
    Task DeleteAsync(Guid id);
}
