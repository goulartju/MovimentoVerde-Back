using FluentValidation;
using Mov.Domain.Dtos.Matricula;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application.Services;

public class MatriculaService : IMatriculaService
{
    private readonly IMatriculaRepository _repository;
    private readonly IAlunoRepository _alunoRepository;
    private readonly ITurmaRepository _turmaRepository;
    private readonly ICalendarioRepository _calendarioRepository;
    private readonly IEscolaRepository _escolaRepository;
    private readonly IValidator<CreateMatriculaDto> _createValidator;
    private readonly IValidator<UpdateMatriculaDto> _updateValidator;

    public MatriculaService(
        IMatriculaRepository repository,
        IAlunoRepository alunoRepository,
        ITurmaRepository turmaRepository,
        ICalendarioRepository calendarioRepository,
        IEscolaRepository escolaRepository,
        IValidator<CreateMatriculaDto> createValidator,
        IValidator<UpdateMatriculaDto> updateValidator)
    {
        _repository = repository;
        _alunoRepository = alunoRepository;
        _turmaRepository = turmaRepository;
        _calendarioRepository = calendarioRepository;
        _escolaRepository = escolaRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<MatriculaDto>> GetAllAsync()
    {
        var matriculas = await _repository.GetAllAsync();
        return await Task.WhenAll(matriculas.Select(m => MapToDtoAsync(m)));
    }

    public async Task<MatriculaDto?> GetByIdAsync(Guid id)
    {
        var matricula = await _repository.GetByIdAsync(id);
        return matricula == null ? null : await MapToDtoAsync(matricula);
    }

    public async Task<MatriculaDto> CreateAsync(CreateMatriculaDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        // Validar se aluno existe
        var aluno = await _alunoRepository.GetByIdAsync(dto.AlunoId);
        if (aluno == null)
            throw new KeyNotFoundException($"Aluno com ID {dto.AlunoId} não encontrado");

        // Validar se turma existe
        var turma = await _turmaRepository.GetByIdAsync(dto.TurmaId);
        if (turma == null)
            throw new KeyNotFoundException($"Turma com ID {dto.TurmaId} não encontrada");

        // Validar se calendario existe
        var calendario = await _calendarioRepository.GetByIdAsync(dto.CalendarioId);
        if (calendario == null)
            throw new KeyNotFoundException($"Calendário com ID {dto.CalendarioId} não encontrado");

        // Validar se escola existe
        var escola = await _escolaRepository.GetByIdAsync(dto.EscolaId);
        if (escola == null)
            throw new KeyNotFoundException($"Escola com ID {dto.EscolaId} não encontrada");

        if (turma.EscolaId != dto.EscolaId)
            throw new InvalidOperationException("A Turma informada não pertence à Escola informada");

        if (calendario.EscolaId != dto.EscolaId)
            throw new InvalidOperationException("O Calendário informado não pertence à Escola informada");

        var matricula = new Matricula
        {
            AlunoId = dto.AlunoId,
            TurmaId = dto.TurmaId,
            CalendarioId = dto.CalendarioId,
            EscolaId = dto.EscolaId,
            Status = dto.Status
        };

        var created = await _repository.CreateAsync(matricula);
        return await MapToDtoAsync(created);
    }

    public async Task<MatriculaDto> UpdateAsync(Guid id, UpdateMatriculaDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Matrícula com ID {id} não encontrada");

        // Validar se aluno existe
        var aluno = await _alunoRepository.GetByIdAsync(dto.AlunoId);
        if (aluno == null)
            throw new KeyNotFoundException($"Aluno com ID {dto.AlunoId} não encontrado");

        // Validar se turma existe
        var turma = await _turmaRepository.GetByIdAsync(dto.TurmaId);
        if (turma == null)
            throw new KeyNotFoundException($"Turma com ID {dto.TurmaId} não encontrada");

        // Validar se calendario existe
        var calendario = await _calendarioRepository.GetByIdAsync(dto.CalendarioId);
        if (calendario == null)
            throw new KeyNotFoundException($"Calendário com ID {dto.CalendarioId} não encontrado");

        // Validar se escola existe
        var escola = await _escolaRepository.GetByIdAsync(dto.EscolaId);
        if (escola == null)
            throw new KeyNotFoundException($"Escola com ID {dto.EscolaId} não encontrada");

        if (turma.EscolaId != dto.EscolaId)
            throw new InvalidOperationException("A Turma informada não pertence à Escola informada");

        if (calendario.EscolaId != dto.EscolaId)
            throw new InvalidOperationException("O Calendário informado não pertence à Escola informada");

        existing.AlunoId = dto.AlunoId;
        existing.TurmaId = dto.TurmaId;
        existing.CalendarioId = dto.CalendarioId;
        existing.EscolaId = dto.EscolaId;
        existing.Status = dto.Status;

        var updated = await _repository.UpdateAsync(existing);
        return await MapToDtoAsync(updated);
    }

    public async Task DeleteAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Matrícula com ID {id} não encontrada");

        await _repository.DeleteAsync(id);
    }

    private async Task<MatriculaDto> MapToDtoAsync(Matricula matricula)
    {
        var escola = await _escolaRepository.GetByIdAsync(matricula.EscolaId);

        return new MatriculaDto
        {
            Id = matricula.Id,
            AlunoId = matricula.AlunoId,
            NomeAluno = matricula.Aluno?.Nome ?? string.Empty,
            TurmaId = matricula.TurmaId,
            NomeTurma = matricula.Turma?.Nome ?? string.Empty,
            AnoEscolar = (int)matricula.Turma?.AnoEscolar,
            CalendarioId = matricula.CalendarioId,
            NomeCalendario = matricula.Calendario != null ? $"Calendário {matricula.Calendario.Ano}" : string.Empty,
            EscolaId = matricula.EscolaId,
            NomeEscola = escola?.Nome ?? string.Empty,
            Status = matricula.Status,
            CriadoEm = matricula.CriadoEm,
            AtualizadoEm = matricula.AtualizadoEm
        };
    }
}
