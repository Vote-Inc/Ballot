namespace Ballot.Domain.Exceptions;

public sealed partial class InvalidPartyException : Exception
{
    public IReadOnlyList<string> Violations { get; }

    public InvalidPartyException(IReadOnlyList<string> violations)
        : base($"Party does not meet requirements: {string.Join(", ", violations)}.")
    {
        Violations = violations;
    }
}