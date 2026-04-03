using FluentValidation;
using Mov.Domain.Dtos.Turma;

namespace Mov.Application.Validators.Turma;

public class CreateTurmaValidator : AbstractValidator<CreateTurmaDto>
{
    public CreateTurmaValidator()
    {
        RuleFor(x => x.EscolaId)
            .NotEmpty().WithMessage("EscolaId é obrigatório");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("Nome não pode ter mais de 100 caracteres");

        RuleFor(x => x.AnoEscolar)
            .NotEmpty().WithMessage("Ano escolar é obrigatório")
            .GreaterThan(0).WithMessage("Ano escolar deve ser maior que 0");

        RuleFor(x => x.Turno)
            .NotEmpty().WithMessage("Turno é obrigatório")
            .MaximumLength(30).WithMessage("Turno não pode ter mais de 30 caracteres");

        RuleFor(x => x.CalendarioId)
            .NotEmpty().WithMessage("CalendarioId é obrigatório");
    }
}

public class UpdateTurmaValidator : AbstractValidator<UpdateTurmaDto>
{
    public UpdateTurmaValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id é obrigatório");

        RuleFor(x => x.EscolaId)
            .NotEmpty().WithMessage("EscolaId é obrigatório");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("Nome não pode ter mais de 100 caracteres");

        RuleFor(x => x.AnoEscolar)
            .NotEmpty().WithMessage("Ano escolar é obrigatório")
            .GreaterThan(0).WithMessage("Ano escolar deve ser maior que 0");

        RuleFor(x => x.Turno)
            .NotEmpty().WithMessage("Turno é obrigatório")
            .MaximumLength(30).WithMessage("Turno não pode ter mais de 30 caracteres");

        RuleFor(x => x.CalendarioId)
            .NotEmpty().WithMessage("CalendarioId é obrigatório");
    }
}
