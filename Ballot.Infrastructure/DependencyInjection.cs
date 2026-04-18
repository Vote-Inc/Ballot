namespace Ballot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ElectionStore>();
        services.AddSingleton<IElectionRepository, ElectionRepository>();

        return services;
    }
}
