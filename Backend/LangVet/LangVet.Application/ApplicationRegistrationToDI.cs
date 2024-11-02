using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LangVet.Application
{
    public static class ApplicationRegistrationToDI
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR
                (
                    cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
                );
        }
    }
}
