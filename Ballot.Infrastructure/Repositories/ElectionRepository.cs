namespace Ballot.Infrastructure.Repositories;

public sealed class ElectionRepository(ElectionStore store) : IElectionRepository
{
    public IUnitOfWork UnitOfWork => throw new NotSupportedException("In-memory store does not support unit of work.");

    public Task<IReadOnlyList<Election>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Election> result = store.Elections
            .Where(e => e.IsActive)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<Election?> FindByIdAsync(Guid electionId, CancellationToken cancellationToken = default)
    {
        var election = store.Elections.FirstOrDefault(e => e.Id == electionId);
        return Task.FromResult(election);
    }
}
