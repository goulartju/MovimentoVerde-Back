using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mov.Domain.Dtos.Calendario;
using Mov.Domain.Dtos.Usuario;
using Mov.Domain.Dtos.Escola;
using Mov.Domain.Dtos.Turma;
using Mov.Application.Validators.Calendario;
using Mov.Application.Validators.Usuario;
using Mov.Application.Validators.Escola;
using Mov.Application.Validators.Turma;
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

            // services
            services.AddScoped<ICalendarioService, CalendarioService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEscolaService, EscolaService>();
            services.AddScoped<ITurmaService, TurmaService>();

            return services;
        }
    }
}