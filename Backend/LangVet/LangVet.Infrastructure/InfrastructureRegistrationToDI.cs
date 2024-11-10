using LangVet.Application.Persistence;
using LangVet.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LangVet.Infrastructure
{
    public static class InfrastructureRegistrationToDI
    {
        //public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        //{
        //    services.AddDbContext<LangVetContext>(options =>
        //                                          options.UseNpgsql("Host=ep-late-scene-a57qwigc.us-east-2.aws.neon.tech;Port=5432;Database=LangVetEntities;Username=neondb_owner;Password=XFdJ35HMSkcP",
        //                                          builder =>
        //                                          builder.MigrationsAssembly(typeof(LangVetContext).Assembly.FullName)));
        //    services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        //    services.AddScoped<IHighlightedTermRepository, HighlightedTermRepository>();
        //    services.AddScoped<IInputRepository, InputRepository>();
        //    services.AddScoped<IMarkedDocumentRepository, MarkedDocumentRepository>();
        //    services.AddScoped<IOutputTermsRepository, OutputTermsRepository>();
        //    return services;
        //}
    }
}
