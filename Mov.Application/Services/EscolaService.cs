using FluentValidation;
using Mov.Domain.Dtos.Escola;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application.Services;

public class EscolaService : IEscolaService
{
    private readonly IEscolaRepository _repository;
    private readonly IValidator<CreateEscolaDto> _createValidator;
    private readonly IValidator<UpdateEscolaDto> _updateValidator;

    public EscolaService(
        IEscolaRepository repository,
        IValidator<CreateEscolaDto> createValidator,
        IValidator<UpdateEscolaDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<Escola>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.ToList();
    }

    public async Task<Escola?> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);

        return item;
    }

    public async Task<Escola> CreateAsync(CreateEscolaDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var Escola = new Escola
        {
            Nome = dto.Nome,
            Municipio = dto.Municipio,
            Endereco = dto.Endereco,
            Diretor = dto.Diretor,
            Ativo = dto.Ativo
        };

        var created = await _repository.CreateAsync(Escola);
        return created;
    }

    public async Task<Escola> UpdateAsync(UpdateEscolaDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Escola com ID {dto.Id} não encontrado");

        existing.Nome = dto.Nome;
        existing.Municipio = dto.Municipio;
        existing.Endereco = dto.Endereco;
        existing.Diretor = dto.Diretor;
        existing.Ativo = dto.Ativo;

        var updated = await _repository.UpdateAsync(existing);
        return updated;
    }

    public async Task DeleteAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Escola com ID {id} não encontrado");

        await _repository.DeleteAsync(id);
    }
}
