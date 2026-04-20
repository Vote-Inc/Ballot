namespace Ballot.Application.Ballot.Queries.GetElectionQuery;

public sealed class GetElectionQueryHandler(IElectionRepository repository)
{
    public async Task<Result<ElectionDetailDTO>> Handle(
        GetElectionQuery getElectionQuery,
        CancellationToken cancellationToken)
    {
        var election = await repository.FindByIdAsync(getElectionQuery.ElectionId, cancellationToken);

        if (election is null)
            return Result<ElectionDetailDTO>.Failure(BallotErrors.ElectionNotFound);

        return Result<ElectionDetailDTO>.Success(election.ToDetailDto());
    }
}
