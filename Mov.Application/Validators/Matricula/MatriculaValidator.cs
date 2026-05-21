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

        RuleFor(x => x.EscolaId)
            .NotEmpty().WithMessage("EscolaId é obrigatório");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status é obrigatório")
            .MaximumLength(30).WithMessage("Status não pode exceder 30 caracteres");
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

        RuleFor(x => x.EscolaId)
            .NotEmpty().WithMessage("EscolaId é obrigatório");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status é obrigatório")
            .MaximumLength(30).WithMessage("Status não pode exceder 30 caracteres");
    }
}
