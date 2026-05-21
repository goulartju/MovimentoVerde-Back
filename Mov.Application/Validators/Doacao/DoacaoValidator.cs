using FluentValidation;
using Mov.Domain.Dtos.Doacao;

namespace Mov.Application.Validators.Doacao;

public class DoacaoFilterValidator : AbstractValidator<DoacaoFilterDto>
{
    public DoacaoFilterValidator()
    {
        RuleFor(x => x.CalendarioId)
            .NotEmpty().WithMessage("CalendarioId é obrigatório");

        RuleFor(x => x.Data)
            .NotEmpty().WithMessage("Data é obrigatória");

        RuleFor(x => x.EscolaId)
            .NotEmpty().WithMessage("EscolaId é obrigatório");

        RuleFor(x => x.TurmaId)
            .NotEmpty().WithMessage("TurmaId é obrigatório");
    }
}

public class CreateDoacaoLoteValidator : AbstractValidator<CreateDoacaoLoteDto>
{
    public CreateDoacaoLoteValidator()
    {
        RuleFor(x => x.EscolaId)
            .NotEmpty().WithMessage("Escola é obrigatória");

        RuleFor(x => x.CalendarioId)
            .NotEmpty().WithMessage("Calendário é obrigatório");

        RuleFor(x => x.Data)
            .NotEmpty().WithMessage("Data é obrigatória");

        RuleFor(x => x.Doacoes)
            .NotEmpty().WithMessage("Lista de doações não pode ser vazia");

   
        RuleForEach(x => x.Doacoes)
            .SetValidator(new CreateDoacaoValidator());
    }
}

public class CreateDoacaoValidator : AbstractValidator<CreateDoacaoItemDto>
{
    public CreateDoacaoValidator()
    {
        RuleFor(d => d.MatriculaId)
            .NotEmpty().WithMessage("Matrícula é obrigatória");

        RuleFor(d => d.QtdLacre)
            .GreaterThanOrEqualTo(0).WithMessage("Quantidade de lacre não pode ser negativa");

        RuleFor(d => d.QtdTampinha)
            .GreaterThanOrEqualTo(0).WithMessage("Quantidade de tampinha não pode ser negativa");

    }
}

public class UpdateDoacaoLoteValidator : AbstractValidator<UpdateDoacaoLoteDto>
{
    public UpdateDoacaoLoteValidator()
    {
        RuleFor(d => d.Data)
            .NotEmpty().WithMessage("Data é obrigatória");

        RuleFor(d => d.EscolaId)
            .NotEmpty().WithMessage("EscolaId é obrigatório");

        RuleFor(d => d.CalendarioId)
            .NotEmpty().WithMessage("CalendarioId é obrigatório");

        RuleFor(x => x.Doacoes)
           .NotEmpty().WithMessage("Lista de doações não pode ser vazia");

        RuleForEach(x => x.Doacoes)
           .SetValidator(new UpdateDoacaoItemValidator());
    }
}

public class UpdateDoacaoItemValidator : AbstractValidator<UpdateDoacaoItemDto>
{
    public UpdateDoacaoItemValidator()
    {

        RuleFor(d => d.Id)
           .NotEmpty().WithMessage("Id é obrigatório");

        RuleFor(d => d.MatriculaId)
            .NotEmpty().WithMessage("MatriculaId é obrigatória");

        RuleFor(d => d.QtdLacre)
            .GreaterThanOrEqualTo(0).WithMessage("Quantidade de lacre não pode ser negativa");

        RuleFor(d => d.QtdTampinha)
            .GreaterThanOrEqualTo(0).WithMessage("Quantidade de tampinha não pode ser negativa");

    }
}
