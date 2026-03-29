using FluentValidation;
using Mov.Application.Dtos.Calendario;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;

namespace Mov.Application.Services;

public interface ICalendarioService
{
    Task<IEnumerable<CalendarioDto>> GetAllAsync();
    Task<CalendarioDto?> GetByIdAsync(Guid id);
    Task<CalendarioDto> CreateAsync(CreateCalendarioDto dto);
    Task<CalendarioDto> UpdateAsync(UpdateCalendarioDto dto);
    Task DeleteAsync(Guid id);
}

public class CalendarioService : ICalendarioService
{
    private readonly ICalendarioRepository _repository;
    private readonly IValidator<CreateCalendarioDto> _createValidator;
    private readonly IValidator<UpdateCalendarioDto> _updateValidator;

    public CalendarioService(
        ICalendarioRepository repository,
        IValidator<CreateCalendarioDto> createValidator,
        IValidator<UpdateCalendarioDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<CalendarioDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<CalendarioDto?> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item != null ? MapToDto(item) : null;
    }

    public async Task<CalendarioDto> CreateAsync(CreateCalendarioDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var calendario = new Calendario
        {
            Ano = dto.Ano,
            DataInicio = dto.DataInicio,
            DataFim = dto.DataFim
        };

        var created = await _repository.CreateAsync(calendario);
        return MapToDto(created);
    }

    public async Task<CalendarioDto> UpdateAsync(UpdateCalendarioDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Calendário com ID {dto.Id} não encontrado");

        existing.Ano = dto.Ano;
        existing.DataInicio = dto.DataInicio;
        existing.DataFim = dto.DataFim;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated);
    }

    public async Task DeleteAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Calendário com ID {id} não encontrado");

        await _repository.DeleteAsync(id);
    }

    private static CalendarioDto MapToDto(Calendario calendario)
    {
        return new CalendarioDto
        {
            Id = calendario.Id,
            Ano = calendario.Ano,
            DataInicio = calendario.DataInicio,
            DataFim = calendario.DataFim
        };
    }
}
