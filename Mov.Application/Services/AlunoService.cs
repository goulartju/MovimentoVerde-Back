using FluentValidation;
using Mov.Domain.Dtos.Aluno;
using Mov.Domain.Dtos.Matricula;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _repository;
    private readonly IMatriculaRepository _matriculaRepository;
    private readonly IMatriculaService _matriculaService;
    private readonly ITurmaRepository _turmaRepository;
    private readonly IValidator<CreateAlunoDto> _createValidator;
    private readonly IValidator<UpdateAlunoDto> _updateValidator;

    public AlunoService(
        IAlunoRepository repository,
        IMatriculaRepository matriculaRepository,
        IMatriculaService matriculaService,
        ITurmaRepository turmaRepository,
        IValidator<CreateAlunoDto> createValidator,
        IValidator<UpdateAlunoDto> updateValidator)
    {
        _repository = repository;
        _matriculaRepository = matriculaRepository;
        _matriculaService = matriculaService;
        _turmaRepository = turmaRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<AlunoDto>> GetAllAsync()
    {
        var alunos = await _repository.GetAllAsync();
        return alunos.Select(MapToDto);
    }

    public async Task<AlunoDto?> GetByIdAsync(Guid id)
    {
        var aluno = await _repository.GetByIdAsync(id);
        return aluno == null ? null : MapToDto(aluno);
    }

    public async Task<AlunoDto> CreateAsync(CreateAlunoDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var aluno = new Aluno
        {
            Nome = dto.Nome,
            Ativo = dto.Ativo
        };

        var created = await _repository.CreateAsync(aluno);

        // Se turmaId foi fornecido, matricular o aluno
        if (dto.TurmaId.HasValue)
        {
            // Validar se turma existe
            var turma = await _turmaRepository.GetByIdAsync(dto.TurmaId.Value);
            if (turma == null)
                throw new KeyNotFoundException($"Turma com ID {dto.TurmaId} não encontrada");

            // Criar matrícula usando o calendário da turma
            var createMatriculaDto = new CreateMatriculaDto
            {
                AlunoId = created.Id,
                TurmaId = turma.Id,
                CalendarioId = turma.CalendarioId,
                Ativo = true
            };

            await _matriculaService.CreateAsync(createMatriculaDto);
        }

        return MapToDto(created);
    }

    public async Task<AlunoDto> UpdateAsync(Guid id, UpdateAlunoDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Aluno com ID {id} não encontrado");

        existing.Nome = dto.Nome;
        existing.Ativo = dto.Ativo;

        var updated = await _repository.UpdateAsync(existing);

        // Se turmaId foi fornecido, verificar e matricular o aluno na turma
        if (dto.TurmaId.HasValue)
        {
            // Validar se turma existe
            var turma = await _turmaRepository.GetByIdAsync(dto.TurmaId.Value);
            if (turma == null)
                throw new KeyNotFoundException($"Turma com ID {dto.TurmaId} não encontrada");

            // Verificar se já existe matrícula do aluno nesta turma
            var matriculaExistenteTurma = await _matriculaRepository.GetByAlunoIdAndTurmaIdAsync(updated.Id, turma.Id);
            if (matriculaExistenteTurma != null)
            {
                // Já existe matrícula nesta turma, não fazer nada
                return MapToDto(updated);
            }

            // Verificar se aluno já está matriculado em outra turma do mesmo calendário
            var matriculaCalendario = await _matriculaRepository.GetByAlunoIdAndCalendarioIdAsync(updated.Id, turma.CalendarioId);
            if (matriculaCalendario != null)
            {
                // atualiza matrícula 
                var updateMatriculaDto = new UpdateMatriculaDto
                {
                    AlunoId = existing.Id,
                    TurmaId = turma.Id,
                    CalendarioId = turma.CalendarioId,
                    Ativo = true
                };

                await _matriculaService.UpdateAsync(matriculaCalendario.Id, updateMatriculaDto);
                
                return MapToDto(updated);
            }
            else
            {
                // Criar matrícula usando o calendário da turma
                var createMatriculaDto = new CreateMatriculaDto
                {
                    AlunoId = updated.Id,
                    TurmaId = turma.Id,
                    CalendarioId = turma.CalendarioId,
                    Ativo = true
                };

                await _matriculaService.CreateAsync(createMatriculaDto);

            }

        }

        return MapToDto(updated);
    }

    public async Task DeleteAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Aluno com ID {id} não encontrado");

        await _repository.DeleteAsync(id);
    }

    private static AlunoDto MapToDto(Aluno aluno)
    {
        return new AlunoDto
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            DataNascimento = aluno.DataNascimento,
            Ativo = aluno.Ativo
        };
    }
}
