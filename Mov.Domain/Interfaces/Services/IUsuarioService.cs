using Mov.Domain.Dtos.Usuario;


namespace Mov.Domain.Interfaces.Services;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDto>> GetAllAsync();
    Task<UsuarioDto?> GetByIdAsync(Guid id);
    Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto);
    Task<UsuarioDto> UpdateAsync(Guid id, UpdateUsuarioDto dto);
    Task DeleteAsync(Guid id);
}
