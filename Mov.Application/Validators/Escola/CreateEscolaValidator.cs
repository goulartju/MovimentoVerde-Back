using FluentValidation;
using Mov.Domain.Dtos.Escola;


namespace Mov.Application.Validators.Escola;

public class CreateEscolaValidator : AbstractValidator<CreateEscolaDto>
{
    public CreateEscolaValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres")
            .MaximumLength(100).WithMessage("Nome não pode ter mais de 100 caracteres");

        RuleFor(x => x.Diretor)
            .NotEmpty().WithMessage("Diretor é obrigatório");

        RuleFor(x => x.Municipio)
            .NotNull().WithMessage("Município é obrigatório");

        RuleFor(x => x.Contato)
            .NotNull().WithMessage("Contato é obrigatório");
    }
}