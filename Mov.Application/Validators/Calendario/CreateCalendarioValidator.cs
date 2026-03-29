using FluentValidation;
using Mov.Application.Dtos.Calendario;

namespace Mov.Application.Validators.Calendario;

public class CreateCalendarioValidator : AbstractValidator<CreateCalendarioDto>
{
    public CreateCalendarioValidator()
    {
        RuleFor(x => x.Ano)
            .NotEmpty().WithMessage("Ano é obrigatório")
            .Length(4).WithMessage("Ano deve conter 4 dígitos");

        RuleFor(x => x.DataInicio)
            .NotEmpty().WithMessage("Data de início é obrigatória")
            .LessThan(x => x.DataFim).WithMessage("Data de início deve ser menor que data de fim");

        RuleFor(x => x.DataFim)
            .NotEmpty().WithMessage("Data de fim é obrigatória")
            .GreaterThan(x => x.DataInicio).WithMessage("Data de fim deve ser maior que data de início");
    }
}
