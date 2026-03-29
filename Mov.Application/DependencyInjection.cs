using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mov.Application.Dtos.Calendario;
using Mov.Application.Services;
using Mov.Application.Validators.Calendario;

namespace Mov.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            // Register validators
            services.AddScoped<IValidator<CreateCalendarioDto>, CreateCalendarioValidator>();
            services.AddScoped<IValidator<UpdateCalendarioDto>, UpdateCalendarioValidator>();

            // Register services
            services.AddScoped<ICalendarioService, CalendarioService>();

            return services;
        }
    }
}