namespace Ballot.Domain.Exceptions;

public sealed partial class InvalidElectionTitleException: Exception
{
    public IReadOnlyList<string> Violations { get; }

    public InvalidElectionTitleException(IReadOnlyList<string> violations)
        : base($"Election Title does not meet requirements: {string.Join(", ", violations)}.")
    {
        Violations = violations;
    }
}