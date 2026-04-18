using Ballot.Application.Ballot.Queries.GetActiveElectionsQuery;
using Ballot.Application.Ballot.Queries.GetElectionQuery;
using Microsoft.Extensions.DependencyInjection;

namespace Ballot.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetActiveElectionsQueryHandler>();
        services.AddScoped<GetElectionQueryHandler>();

        return services;
    }
}
