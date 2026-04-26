using FluentValidation;
using Mov.Application.Validators.Usuario;
using Mov.Domain.Dtos.Usuario;
using Mov.Domain.Entities;
using Mov.Domain.Enums;
using Mov.Domain.Interfaces.Repositories;
using Mov.Domain.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace Mov.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;
    private readonly IValidator<CreateUsuarioDto> _createValidator;
    private readonly IValidator<UpdateUsuarioDto> _updateValidator;

    public UsuarioService(
        IUsuarioRepository repository,
        IValidator<CreateUsuarioDto> createValidator,
        IValidator<UpdateUsuarioDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();
        return usuarios.Select(MapToDto);
    }

    public async Task<UsuarioDto?> GetByIdAsync(Guid id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        return MapToDto(usuario); 
    }

    public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var existente = await _repository.GetByEmailAsync(dto.Email);
        if (existente != null)
            throw new InvalidOperationException($"Email {dto.Email} já cadastrado");

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            DataNascimento = dto.DataNascimento,
            Cargo = dto.Cargo,
            Permissao = dto.Permissao,
            Ativo = dto.Ativo
        };

        var criado = await _repository.CreateAsync(usuario);
        return MapToDto(criado);
    }

    public async Task<UsuarioDto> UpdateAsync(Guid id,UpdateUsuarioDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null)
            throw new KeyNotFoundException($"Usuário com ID {id} não encontrado");

        // Validar email único (se mudou)
        if (usuario.Email != dto.Email)
        {
            var emailExistente = await _repository.GetByEmailAsync(dto.Email);
            if (emailExistente != null)
                throw new InvalidOperationException($"Email {dto.Email} já cadastrado");
        }

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        usuario.DataNascimento = dto.DataNascimento;
        usuario.Cargo = dto.Cargo;
        usuario.Permissao = dto.Permissao;
        usuario.Ativo = dto.Ativo;

        var atualizado = await _repository.UpdateAsync(usuario);
        return MapToDto(atualizado);
    }

    public async Task DeleteAsync(Guid id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null)
            throw new KeyNotFoundException($"Usuário com ID {id} não encontrado");

        await _repository.DeleteAsync(id);
    }

    public static UsuarioDto MapToDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            DataNascimento = usuario.DataNascimento,
            Email = usuario.Email,
            Cargo = usuario.Cargo,
            Permissao = (int)usuario.Permissao,
            Ativo = usuario.Ativo
        };
    }
}
