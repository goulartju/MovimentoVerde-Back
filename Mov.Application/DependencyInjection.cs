using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mov.Domain.Dtos.Calendario;
using Mov.Domain.Dtos.Usuario;
using Mov.Application.Validators.Calendario;
using Mov.Application.Validators.Usuario;
using Mov.Application.Services;
using Mov.Domain.Interfaces.Services;

namespace Mov.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            // Calendario validators
            services.AddScoped<IValidator<CreateCalendarioDto>, CreateCalendarioValidator>();
            services.AddScoped<IValidator<UpdateCalendarioDto>, UpdateCalendarioValidator>();

            // Usuario validators
            services.AddScoped<IValidator<CreateUsuarioDto>, CreateUsuarioValidator>();
            services.AddScoped<IValidator<UpdateUsuarioDto>, UpdateUsuarioValidator>();

            // Calendario services
            services.AddScoped<ICalendarioService, CalendarioService>();

            // Usuario services
            services.AddScoped<IUsuarioService, UsuarioService>();

            return services;
        }
    }
}