using DataLayer.Abstractions.Repository;
using DataLayer.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer
{
    public static class Registration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            services.AddTransient<IKittenRepository, KittensRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IClinicRepository, ClinicRepository>();
            return services;
        }
    }
}