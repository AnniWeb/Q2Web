using BusinessLogic.Abstractions.Service;
using BusinessLogic.Service;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class Registration
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddTransient<IKittenService, KittenService>();
            services.AddTransient<IClinicService, ClinicService>();
            services.AddTransient<IPersonService, PersonService>();
            return services;
        }
    }
}