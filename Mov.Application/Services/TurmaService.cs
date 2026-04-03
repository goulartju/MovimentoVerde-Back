using FluentValidation;
using Mov.Domain.Dtos.Turma;
using Mov.Domain.Entities;
using Mov.Domain.Enums;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application.Services;

public class TurmaService : ITurmaService
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IRepresentanteTurmaRepository _representanteTurmaRepository;
    private readonly IValidator<CreateTurmaDto> _createValidator;
    private readonly IValidator<UpdateTurmaDto> _updateValidator;

    public TurmaService(
        ITurmaRepository turmaRepository,
        IRepresentanteTurmaRepository representanteTurmaRepository,
        IValidator<CreateTurmaDto> createValidator,
        IValidator<UpdateTurmaDto> updateValidator)
    {
        _turmaRepository = turmaRepository;
        _representanteTurmaRepository = representanteTurmaRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<TurmaDto>> GetAllAsync()
    {
        var turmas = await _turmaRepository.GetAllAsync();
        return turmas.Select(MapToDto);
    }

    public async Task<TurmaDto?> GetByIdAsync(Guid id)
    {
        var turma = await _turmaRepository.GetByIdAsync(id);
        return turma == null ? null : MapToDto(turma);
    }

    public async Task<IEnumerable<TurmaDto>> GetByEscolaIdAsync(Guid escolaId)
    {
        var turmas = await _turmaRepository.GetByEscolaIdAsync(escolaId);
        return turmas.Select(MapToDto);
    }

    public async Task<TurmaDto> CreateAsync(CreateTurmaDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var turma = new Turma
        {
            EscolaId = dto.EscolaId,
            Nome = dto.Nome,
            AnoEscolar = dto.AnoEscolar,
            Turno = dto.Turno,
            CalendarioId = dto.CalendarioId,
            Ativo = dto.Ativo
        };

        var created = await _turmaRepository.CreateAsync(turma);

        // Adicionar representantes se fornecidos
        if (dto.Representantes != null && dto.Representantes.Any())
        {
            foreach (var rep in dto.Representantes)
            {
                var representanteTurma = new RepresentanteTurma
                {
                    TurmaId = created.Id,
                    UsuarioId = rep.UsuarioId,
     
                };
                await _representanteTurmaRepository.AddRepresentanteAsync(representanteTurma);
            }
            // Recarregar para incluir representantes
            created = await _turmaRepository.GetByIdAsync(created.Id);
        }

        return MapToDto(created!);
    }

    public async Task<TurmaDto> UpdateAsync(UpdateTurmaDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existing = await _turmaRepository.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Turma com ID {dto.Id} não encontrada");

        existing.EscolaId = dto.EscolaId;
        existing.Nome = dto.Nome;
        existing.AnoEscolar = dto.AnoEscolar;
        existing.Turno = dto.Turno;
        existing.CalendarioId = dto.CalendarioId;
        existing.Ativo = dto.Ativo;

        var updated = await _turmaRepository.UpdateAsync(existing);

        // Processar representantes se fornecidos
        if (dto.Representantes != null)
        {
            // Remover representantes existentes
            var existingReps = await _representanteTurmaRepository.GetByTurmaIdAsync(dto.Id);
            foreach (var rep in existingReps)
            {
                await _representanteTurmaRepository.RemoveRepresentanteAsync(rep.Id);
            }

            // Adicionar novos representantes
            foreach (var rep in dto.Representantes)
            {
                var representanteTurma = new RepresentanteTurma
                {
                    TurmaId = dto.Id,
                    UsuarioId = rep.UsuarioId
                };
                await _representanteTurmaRepository.AddRepresentanteAsync(representanteTurma);
            }

            // Recarregar para incluir representantes atualizados
            updated = await _turmaRepository.GetByIdAsync(dto.Id);
        }

        return MapToDto(updated!);
    }

    public async Task DeleteAsync(Guid id)
    {
        var existing = await _turmaRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Turma com ID {id} não encontrada");

        await _turmaRepository.DeleteAsync(id);
    }

    private static TurmaDto MapToDto(Turma turma)
    {
        return new TurmaDto
        {
            Id = turma.Id,
            EscolaId = turma.EscolaId,
            Nome = turma.Nome,
            AnoEscolar = turma.AnoEscolar,
            Turno = turma.Turno,
            CalendarioId = turma.CalendarioId,
            Ativo = turma.Ativo,
            Representantes = turma.Representantes.Select(r => new RepresentanteDto
            {
                UsuarioId = r.UsuarioId,
                NomeUsuario = r.Usuario?.Nome ?? string.Empty,
            }).ToList()
        };
    }
}
