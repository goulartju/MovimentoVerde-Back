using FluentValidation;
using Mov.Domain.Dtos.Doacao;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application.Services;

public class DoacaoService : IDoacaoService
{
    private readonly IDoacaoRepository _repository;
    private readonly IValidator<CreateDoacaoLoteDto> _createValidator;
    private readonly IValidator<UpdateDoacaoLoteDto> _updateValidator;

    public DoacaoService(
        IDoacaoRepository repository,
        IValidator<CreateDoacaoLoteDto> createValidator,
        IValidator<UpdateDoacaoLoteDto> updateValidator)
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

    public async Task<DoacaoDto?> GetByIdAsync(Guid id)
    {
        var doacao = await _repository.GetByIdAsync(id);
        return doacao == null ? null : MapToDto(doacao);
    }

    public async Task<IEnumerable<DoacaoDto>> GetByMatriculaIdAsync(Guid matriculaId)
    {
        var doacoes = await _repository.GetByMatriculaIdAsync(matriculaId);
        return doacoes.Select(MapToDto);
    }

    public async Task<IEnumerable<DoacaoDto>> GetByEscolaIdAsync(Guid escolaId)
    {
        var doacoes = await _repository.GetByEscolaIdAsync(escolaId);
        return doacoes.Select(MapToDto);
    }

    public async Task<IEnumerable<DoacaoDto>> CreateLoteAsync(CreateDoacaoLoteDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var doacoes = new List<Doacao>();

        foreach (var item in dto.Doacoes)
        {
            var doacao = new Doacao
            {
                MatriculaId = item.MatriculaId,
                EscolaId = dto.EscolaId,
                CalendarioId = dto.CalendarioId,
                QtdLacre = item.QtdLacre,
                QtdTampinha = item.QtdTampinha,
                Data = dto.Data,
                CriadoEm = DateTime.UtcNow
            };
            doacoes.Add(doacao);
           
        }
        var createdList = await _repository.CreateLoteAsync(doacoes);

        return createdList.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<DoacaoDto>> UpdateLoteAsync(UpdateDoacaoLoteDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);
        var doacoes = new List<Doacao>();

        foreach (var item in dto.Doacoes)
        {
            var existing = await _repository.GetByIdAsync(item.Id);

            if (existing == null)
                throw new KeyNotFoundException($"Doação com ID {item.Id} não encontrada");

            existing.MatriculaId = item.MatriculaId;
            existing.EscolaId = dto.EscolaId;
            existing.CalendarioId = dto.CalendarioId;
            existing.QtdLacre = item.QtdLacre;
            existing.QtdTampinha = item.QtdTampinha;
            existing.Data = dto.Data;
            existing.AtualizadoEm = DateTime.UtcNow;
            doacoes.Add(existing);
        }

        await _repository.UpdateLoteAsync(doacoes);

        var result = await _repository.GetByIdAsync(doacoes.Last().Id);
        return doacoes.Select(MapToDto).ToList();
    }

    public async Task DeleteAsync(Guid id)
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
            QtdLacre = doacao.QtdLacre,
            QtdTampinha = doacao.QtdTampinha,
            Data = doacao.Data,
            NomeAluno = doacao.Matricula?.Aluno?.Nome ?? string.Empty,
            NomeTurma = doacao.Matricula?.Turma?.Nome ?? string.Empty,
            NomeEscola = doacao.Escola?.Nome ?? string.Empty,
            NomeCalendario = $"{doacao.Calendario?.Ano} - {doacao.Calendario?.Ano}" ?? string.Empty,
            AnoEscolar = (int)(doacao.Matricula?.Turma?.AnoEscolar ?? 0)
        };
    }
}
