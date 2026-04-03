using FluentValidation;
using Mov.Domain.Dtos.Auth;

namespace Mov.Application.Validators.Auth;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.SenhaAtual)
            .NotEmpty().WithMessage("Senha atual é obrigatória");

        RuleFor(x => x.NovaSenha)
            .NotEmpty().WithMessage("Nova senha é obrigatória")
            .MinimumLength(8).WithMessage("Nova senha deve ter no mínimo 8 caracteres")
            .Matches(@"[A-Z]").WithMessage("Nova senha deve conter pelo menos uma letra maiúscula")
            .Matches(@"[a-z]").WithMessage("Nova senha deve conter pelo menos uma letra minúscula")
            .Matches(@"[0-9]").WithMessage("Nova senha deve conter pelo menos um número");

        RuleFor(x => x.ConfirmarNovaSenha)
            .NotEmpty().WithMessage("Confirmação de senha é obrigatória")
            .Equal(x => x.NovaSenha).WithMessage("As senhas não conferem");

        RuleFor(x => x.NovaSenha)
            .NotEqual(x => x.SenhaAtual).WithMessage("Nova senha deve ser diferente da senha atual");
    }
}
