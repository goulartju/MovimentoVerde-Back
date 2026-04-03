using Mov.Domain.Dtos.Medalha;

namespace Mov.Domain.Interfaces.Services;

public interface IMedalhaService
{
    Task<IEnumerable<MedalhaDto>> GetAllAsync();
    Task<MedalhaDto?> GetByIdAsync(int id);
    Task<MedalhaDto> CreateAsync(CreateMedalhaDto dto);
    Task<MedalhaDto> UpdateAsync(UpdateMedalhaDto dto);
    Task DeleteAsync(int id);
}