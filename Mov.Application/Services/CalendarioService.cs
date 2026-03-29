using FluentValidation;
using Mov.Domain.Dtos.Calendario;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application.Services;


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

    public async Task<IEnumerable<Calendario>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.ToList();
    }

    public async Task<Calendario?> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);
       
        return item;
    }

    public async Task<Calendario> CreateAsync(CreateCalendarioDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var calendario = new Calendario
        {
            Ano = dto.Ano,
            DataInicio = dto.DataInicio,
            DataFim = dto.DataFim,
            Ativo = dto.Ativo
        };

        var created = await _repository.CreateAsync(calendario);
        return created;
    }

    public async Task<Calendario> UpdateAsync(UpdateCalendarioDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Calendário com ID {dto.Id} não encontrado");

        existing.Ano = dto.Ano;
        existing.DataInicio = dto.DataInicio;
        existing.DataFim = dto.DataFim;
        existing.Ativo = dto.Ativo;

        var updated = await _repository.UpdateAsync(existing);
        return updated;
    }

    public async Task DeleteAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Calendário com ID {id} não encontrado");

        await _repository.DeleteAsync(id);
    }

}
