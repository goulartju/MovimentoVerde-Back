using FluentValidation;
using Mov.Domain.Dtos.Escola;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mov.Application.Validators.Escola;

public class UpdateEscolaValidator : AbstractValidator<UpdateEscolaDto>
{
    public UpdateEscolaValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Id é obrigatório");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório");

        RuleFor(x => x.Diretor)
            .NotEmpty().WithMessage("Diretor é obrigatório");

        RuleFor(x => x.Municipio)
            .NotNull().WithMessage("Município é obrigatório");

        RuleFor(x => x.Endereco)
            .NotNull().WithMessage("Endereço é obrigatório");
    }
}