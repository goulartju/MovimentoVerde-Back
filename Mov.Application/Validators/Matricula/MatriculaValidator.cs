using FluentValidation;
using Mov.Domain.Dtos.Matricula;

namespace Mov.Application.Validators.Matricula;

public class CreateMatriculaValidator : AbstractValidator<CreateMatriculaDto>
{
    public CreateMatriculaValidator()
    {
        RuleFor(x => x.TurmaId)
            .NotEmpty().WithMessage("TurmaId é obrigatório");

        RuleFor(x => x.CalendarioId)
            .NotEmpty().WithMessage("CalendarioId é obrigatório");
    }
}

public class UpdateMatriculaValidator : AbstractValidator<UpdateMatriculaDto>
{
    public UpdateMatriculaValidator()
    {
        RuleFor(x => x.TurmaId)
            .NotEmpty().WithMessage("TurmaId é obrigatório");

        RuleFor(x => x.CalendarioId)
            .NotEmpty().WithMessage("CalendarioId é obrigatório");
    }
}
