using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mov.Domain.Dtos.Calendario;
using Mov.Domain.Dtos.Usuario;
using Mov.Domain.Dtos.Escola;
using Mov.Domain.Dtos.Turma;
using Mov.Domain.Dtos.Aluno;
using Mov.Domain.Dtos.Matricula;
using Mov.Domain.Dtos.Doacao;
using Mov.Domain.Dtos.Auth;
using Mov.Application.Validators.Calendario;
using Mov.Application.Validators.Usuario;
using Mov.Application.Validators.Escola;
using Mov.Application.Validators.Turma;
using Mov.Application.Validators.Aluno;
using Mov.Application.Validators.Matricula;
using Mov.Application.Validators.Doacao;
using Mov.Application.Validators.Auth;
using Mov.Application.Services;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            // validators
            services.AddScoped<IValidator<CreateCalendarioDto>, CreateCalendarioValidator>();
            services.AddScoped<IValidator<UpdateCalendarioDto>, UpdateCalendarioValidator>();
            services.AddScoped<IValidator<CreateUsuarioDto>, CreateUsuarioValidator>();
            services.AddScoped<IValidator<UpdateUsuarioDto>, UpdateUsuarioValidator>();
            services.AddScoped<IValidator<CreateEscolaDto>, CreateEscolaValidator>();
            services.AddScoped<IValidator<UpdateEscolaDto>, UpdateEscolaValidator>();
            services.AddScoped<IValidator<CreateTurmaDto>, CreateTurmaValidator>();
            services.AddScoped<IValidator<UpdateTurmaDto>, UpdateTurmaValidator>();
            services.AddScoped<IValidator<CreateAlunoDto>, CreateAlunoValidator>();
            services.AddScoped<IValidator<UpdateAlunoDto>, UpdateAlunoValidator>();
            services.AddScoped<IValidator<CreateMatriculaDto>, CreateMatriculaValidator>();
            services.AddScoped<IValidator<UpdateMatriculaDto>, UpdateMatriculaValidator>();
            services.AddScoped<IValidator<CreateDoacaoDto>, CreateDoacaoValidator>();
            services.AddScoped<IValidator<UpdateDoacaoDto>, UpdateDoacaoValidator>();
            services.AddScoped<IValidator<LoginDto>, LoginValidator>();
            services.AddScoped<IValidator<RegisterDto>, RegisterValidator>();
            services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
            services.AddScoped<IValidator<ChangePasswordDto>, ChangePasswordValidator>();

            // services
            services.AddScoped<ICalendarioService, CalendarioService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEscolaService, EscolaService>();
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<IMatriculaService, MatriculaService>();
            services.AddScoped<IDoacaoService, DoacaoService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}