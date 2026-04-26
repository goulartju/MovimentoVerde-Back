using FluentValidation;
using Mov.Domain.Dtos.Doacao;

namespace Mov.Application.Validators.Doacao;

public class CreateDoacaoValidator : AbstractValidator<CreateDoacaoDto>
{
    public CreateDoacaoValidator()
    {
        RuleFor(d => d.MatriculaId).NotEmpty().WithMessage("Matrícula é obrigatória");
        RuleFor(d => d.EscolaId).NotEmpty().WithMessage("EscolaId é obrigatório");
        RuleFor(d => d.CalendarioId).NotEmpty().WithMessage("CalendarioId é obrigatório");
        RuleFor(d => d.QldLacre).GreaterThanOrEqualTo(0).WithMessage("Quantidade de lacre não pode ser negativa");
        RuleFor(d => d.QldTampinha).GreaterThanOrEqualTo(0).WithMessage("Quantidade de tampinha não pode ser negativa");
        RuleFor(d => d.Data).NotEmpty().WithMessage("Data é obrigatória");
    }
}

public class UpdateDoacaoValidator : AbstractValidator<UpdateDoacaoDto>
{
    public UpdateDoacaoValidator()
    {
        RuleFor(d => d.Id).GreaterThan(0).WithMessage("Id deve ser maior que 0");
        RuleFor(d => d.MatriculaId).NotEmpty().WithMessage("MatriculaId é obrigatória");
        RuleFor(d => d.EscolaId).NotEmpty().WithMessage("EscolaId é obrigatório");
        RuleFor(d => d.CalendarioId).NotEmpty().WithMessage("CalendarioId é obrigatório");
        RuleFor(d => d.QldLacre).GreaterThanOrEqualTo(0).WithMessage("Quantidade de lacre não pode ser negativa");
        RuleFor(d => d.QldTampinha).GreaterThanOrEqualTo(0).WithMessage("Quantidade de tampinha não pode ser negativa");
        RuleFor(d => d.Data).NotEmpty().WithMessage("Data é obrigatória");
    }
}
