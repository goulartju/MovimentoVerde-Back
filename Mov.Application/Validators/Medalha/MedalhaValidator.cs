using FluentValidation;
using Mov.Domain.Dtos.Medalha;

namespace Mov.Application.Validators.Medalha;

public class CreateMedalhaValidator : AbstractValidator<CreateMedalhaDto>
{
    public CreateMedalhaValidator()
    {
        RuleFor(m => m.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(50).WithMessage("Nome não pode ter mais de 50 caracteres");
        
        RuleFor(m => m.Qtd)
            .GreaterThan(0).WithMessage("Quantidade deve ser maior que 0");
    }
}

public class UpdateMedalhaValidator : AbstractValidator<UpdateMedalhaDto>
{
    public UpdateMedalhaValidator()
    {
        RuleFor(m => m.Id)
            .GreaterThan(0).WithMessage("Id deve ser maior que 0");
        
        RuleFor(m => m.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(50).WithMessage("Nome não pode ter mais de 50 caracteres");
        
        RuleFor(m => m.Qtd)
            .GreaterThan(0).WithMessage("Quantidade deve ser maior que 0");
    }
}