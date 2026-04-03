using FluentValidation;
using Mov.Domain.Dtos.Aluno;
using Mov.Domain.Entities;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _repository;
    private readonly IValidator<CreateAlunoDto> _createValidator;
    private readonly IValidator<UpdateAlunoDto> _updateValidator;

    public AlunoService(
        IAlunoRepository repository,
        IValidator<CreateAlunoDto> createValidator,
        IValidator<UpdateAlunoDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<AlunoDto>> GetAllAsync()
    {
        var alunos = await _repository.GetAllAsync();
        return alunos.Select(MapToDto);
    }

    public async Task<AlunoDto?> GetByIdAsync(int id)
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
            DataNascimento = dto.DataNascimento,
            Ativo = dto.Ativo
        };

        var created = await _repository.CreateAsync(aluno);
        return MapToDto(created);
    }

    public async Task<AlunoDto> UpdateAsync(UpdateAlunoDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Aluno com ID {dto.Id} não encontrado");

        existing.Nome = dto.Nome;
        existing.DataNascimento = dto.DataNascimento;
        existing.Ativo = dto.Ativo;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated);
    }

    public async Task DeleteAsync(int id)
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
            Ativo = aluno.Ativo,
            CriadoEm = aluno.CriadoEm,
            AtualizadoEm = aluno.AtualizadoEm
        };
    }
}
