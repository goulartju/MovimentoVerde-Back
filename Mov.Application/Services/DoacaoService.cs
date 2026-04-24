using FluentValidation;
using Mov.Domain.Dtos.Doacao;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application.Services;

public class DoacaoService : IDoacaoService
{
    private readonly IDoacaoRepository _repository;
    private readonly IValidator<CreateDoacaoDto> _createValidator;
    private readonly IValidator<UpdateDoacaoDto> _updateValidator;

    public DoacaoService(
        IDoacaoRepository repository,
        IValidator<CreateDoacaoDto> createValidator,
        IValidator<UpdateDoacaoDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<DoacaoDto>> GetAllAsync()
    {
        var doacoes = await _repository.GetAllAsync();
        return doacoes.Select(MapToDto);
    }

    public async Task<DoacaoDto?> GetByIdAsync(int id)
    {
        var doacao = await _repository.GetByIdAsync(id);
        return doacao == null ? null : MapToDto(doacao);
    }

    public async Task<IEnumerable<DoacaoDto>> GetByMatriculaIdAsync(int matriculaId)
    {
        var doacoes = await _repository.GetByMatriculaIdAsync(matriculaId);
        return doacoes.Select(MapToDto);
    }

    public async Task<IEnumerable<DoacaoDto>> GetByEscolaIdAsync(Guid escolaId)
    {
        var doacoes = await _repository.GetByEscolaIdAsync(escolaId);
        return doacoes.Select(MapToDto);
    }

    public async Task<DoacaoDto> CreateAsync(CreateDoacaoDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var doacao = new Doacao
        {
            MatriculaId = dto.MatriculaId,
            EscolaId = dto.EscolaId,
            CalendarioId = dto.CalendarioId,
            QldLacre = dto.QldLacre,
            QldTampinha = dto.QldTampinha,
            Data = dto.Data
        };

        var created = await _repository.CreateAsync(doacao);
        
        // Recarregar com relacionamentos
        var result = await _repository.GetByIdAsync(created.Id);
        return MapToDto(result!);
    }

    public async Task<DoacaoDto> UpdateAsync(UpdateDoacaoDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Doação com ID {dto.Id} não encontrada");

        existing.MatriculaId = dto.MatriculaId;
        existing.EscolaId = dto.EscolaId;
        existing.CalendarioId = dto.CalendarioId;
        existing.QldLacre = dto.QldLacre;
        existing.QldTampinha = dto.QldTampinha;
        existing.Data = dto.Data;

        await _repository.UpdateAsync(existing);

        var result = await _repository.GetByIdAsync(existing.Id);
        return MapToDto(result!);
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Doação com ID {id} não encontrada");

        await _repository.DeleteAsync(id);
    }

    private static DoacaoDto MapToDto(Doacao doacao)
    {
        return new DoacaoDto
        {
            Id = doacao.Id,
            MatriculaId = doacao.MatriculaId,
            EscolaId = doacao.EscolaId,
            CalendarioId = doacao.CalendarioId,
            QldLacre = doacao.QldLacre,
            QldTampinha = doacao.QldTampinha,
            Data = doacao.Data,
            NomeAluno = doacao.Matricula?.Aluno?.Nome ?? string.Empty,
            NomeTurma = doacao.Matricula?.Turma?.Nome ?? string.Empty,
            NomeEscola = doacao.Escola?.Nome ?? string.Empty,
            NomeCalendario = $"{doacao.Calendario?.Ano} - {doacao.Calendario?.Ano}" ?? string.Empty,
            AnoEscolar = (int)doacao.Matricula?.Turma?.AnoEscolar
        };
    }
}
