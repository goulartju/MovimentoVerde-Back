using FluentValidation;
using Mov.Domain.Dtos.Usuario;

namespace Mov.Application.Validators.Usuario;

public class UpdateUsuarioValidator : AbstractValidator<UpdateUsuarioDto>
{
    public UpdateUsuarioValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id é obrigatório");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres")
            .MaximumLength(100).WithMessage("Nome não pode ter mais de 100 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("Data de nascimento é obrigatória")
            .LessThan(DateTime.Now).WithMessage("Data de nascimento não pode ser no futuro");

        RuleFor(x => x.Cargo)
            .NotEmpty().WithMessage("Cargo é obrigatório")
            .MaximumLength(50).WithMessage("Cargo não pode ter mais de 50 caracteres");

        RuleFor(x => x.Permissao)
            .IsInEnum().WithMessage("Permissão inválida");
    }
}
