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

    public async Task<EscolaResponse?> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item is null) return null;

        return ToResponse(item);
    }

    public async Task<EscolaResponse> CreateAsync(CreateEscolaDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var escola = new Escola
        {
            Nome = dto.Nome,
            Municipio = dto.Municipio,
            Contato = dto.Contato,
            Diretor = dto.Diretor,
            Ativo = dto.Ativo
        };

        var created = await _repository.CreateAsync(escola);
        return ToResponse(created);
    }

    public async Task<EscolaResponse> UpdateAsync(UpdateEscolaDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Escola com ID {dto.Id} não encontrado");

        existing.Nome = dto.Nome;
        existing.Municipio = dto.Municipio;
        existing.Contato = dto.Contato;
        existing.Diretor = dto.Diretor;
        existing.Ativo = dto.Ativo;
        existing.AtualizadoEm = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return ToResponse(updated);
    }

    public async Task DeleteAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Escola com ID {id} não encontrado");

        await _repository.DeleteAsync(id);
    }

    private static EscolaResponse ToResponse(Escola escola) => new()
    {
        Id = escola.Id,
        Nome = escola.Nome,
        Municipio = escola.Municipio,
        Contato = escola.Contato,
        Diretor = escola.Diretor,
        Ativo = escola.Ativo,
        CriadoEm = escola.CriadoEm,
        AtualizadoEm = escola.AtualizadoEm
    };
}
