namespace Ballot.Domain.Exceptions;

public sealed partial class InvalidSlugException : Exception
{
    public IReadOnlyList<string> Violations { get; }

    public InvalidSlugException(IReadOnlyList<string> violations)
        : base($"Slug does not meet requirements: {string.Join(", ", violations)}.")
    {
        Violations = violations;
    }
}