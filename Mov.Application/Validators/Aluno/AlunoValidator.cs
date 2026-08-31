using FluentValidation;
using Mov.Domain.Dtos.Aluno;

namespace Mov.Application.Validators.Aluno;

public class CreateAlunoValidator : AbstractValidator<CreateAlunoDto>
{
    public CreateAlunoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("Nome não pode exceder 10 caracteres");

    }
}

public class UpdateAlunoValidator : AbstractValidator<UpdateAlunoDto>
{
    public UpdateAlunoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("Nome não pode exceder 10 caracteres");

    }
}
