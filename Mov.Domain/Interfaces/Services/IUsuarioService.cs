using Mov.Domain.Entities;
using Mov.Domain.Dtos.Usuario;


namespace Mov.Domain.Interfaces.Services;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(Guid id);
    Task<Usuario> CreateAsync(CreateUsuarioDto dto);
    Task<Usuario> UpdateAsync(UpdateUsuarioDto dto);
    Task DeleteAsync(Guid id);
}
