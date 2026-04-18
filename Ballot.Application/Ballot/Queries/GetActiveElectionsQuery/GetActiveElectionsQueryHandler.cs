namespace Ballot.Application.Ballot.Queries.GetActiveElectionsQuery;

public sealed class GetActiveElectionsQueryHandler(IElectionRepository repository)
{
    public async Task<Result<List<ElectionSummaryDTO>>> Handle(
        GetActiveElectionsQuery getActiveElectionsQuery,
        CancellationToken cancellationToken)
    {
        var elections = await repository.GetActiveAsync(cancellationToken);

        return Result<List<ElectionSummaryDTO>>.Success(
            elections.Select(e => e.ToSummaryDto()).ToList()
        );
    }
}
