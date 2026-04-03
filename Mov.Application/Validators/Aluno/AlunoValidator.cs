using FluentValidation;
using Mov.Domain.Dtos.Aluno;

namespace Mov.Application.Validators.Aluno;

public class CreateAlunoValidator : AbstractValidator<CreateAlunoDto>
{
    public CreateAlunoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(10).WithMessage("Nome não pode exceder 10 caracteres");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("Data de nascimento é obrigatória")
            .LessThan(DateTime.Now).WithMessage("Data de nascimento deve ser no passado");
    }
}

public class UpdateAlunoValidator : AbstractValidator<UpdateAlunoDto>
{
    public UpdateAlunoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id deve ser maior que 0");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(10).WithMessage("Nome não pode exceder 10 caracteres");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("Data de nascimento é obrigatória")
            .LessThan(DateTime.Now).WithMessage("Data de nascimento deve ser no passado");
    }
}
