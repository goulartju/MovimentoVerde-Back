using FluentValidation;
using Mov.Domain.Dtos.Medalha;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application.Services;

public class MedalhaService : IMedalhaService
{
    private readonly IMedalhaRepository _repository;
    private readonly IValidator<CreateMedalhaDto> _createValidator;
    private readonly IValidator<UpdateMedalhaDto> _updateValidator;

    public MedalhaService(
        IMedalhaRepository repository,
        IValidator<CreateMedalhaDto> createValidator,
        IValidator<UpdateMedalhaDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<MedalhaDto>> GetAllAsync()
    {
        var medalhas = await _repository.GetAllAsync();
        return medalhas.Select(MapToDto);
    }

    public async Task<MedalhaDto?> GetByIdAsync(int id)
    {
        var medalha = await _repository.GetByIdAsync(id);
        return medalha == null ? null : MapToDto(medalha);
    }

    public async Task<MedalhaDto> CreateAsync(CreateMedalhaDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var medalha = new Medalha
        {
            Nome = dto.Nome,
            Qtd = dto.Qtd
        };

        var created = await _repository.CreateAsync(medalha);
        return MapToDto(created);
    }

    public async Task<MedalhaDto> UpdateAsync(UpdateMedalhaDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Medalha com ID {dto.Id} não encontrada");

        existing.Nome = dto.Nome;
        existing.Qtd = dto.Qtd;

        await _repository.UpdateAsync(existing);
        var result = await _repository.GetByIdAsync(existing.Id);
        return MapToDto(result!);
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Medalha com ID {id} não encontrada");

        await _repository.DeleteAsync(id);
    }

    private static MedalhaDto MapToDto(Medalha medalha)
    {
        return new MedalhaDto
        {
            Id = medalha.Id,
            Nome = medalha.Nome,
            Qtd = medalha.Qtd,
            CriadoEm = medalha.CriadoEm,
            AtualizadoEm = medalha.AtualizadoEm
        };
    }
}