namespace Ballot.Domain.Repositories;

public interface IElectionRepository : IRepository<Election>
{
    Task<IReadOnlyList<Election>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<Election?> FindByIdAsync(Guid electionId, CancellationToken cancellationToken = default);
}
