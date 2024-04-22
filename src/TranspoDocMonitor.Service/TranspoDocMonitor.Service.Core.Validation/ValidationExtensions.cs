using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TranspoDocMonitor.Service.Core.Validation.Validators.User;

namespace TranspoDocMonitor.Service.Core.Validation
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
            services.AddFluentValidationAutoValidation();
            return services;
        }

    }
}