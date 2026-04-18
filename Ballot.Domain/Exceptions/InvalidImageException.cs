namespace Ballot.Domain.Exceptions;

public sealed partial class InvalidImageException: Exception
{
    public IReadOnlyList<string> Violations { get; }

    public InvalidImageException(IReadOnlyList<string> violations)
        : base($"Image file does not meet requirements: {string.Join(", ", violations)}.")
    {
        Violations = violations;
    }
}