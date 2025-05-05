using FluentValidation.AspNetCore;
using Pi.Application.UseCases.Login.Dto;
using Pi.Application.Validators;

namespace Pi.API.Configurations
{
    public static class FluentValidatorConfig
    {
        public static IServiceCollection AddFluentValidationSetup(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>();
                });
            return services;

        }
    }
}
