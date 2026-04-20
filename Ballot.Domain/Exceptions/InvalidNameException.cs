namespace Ballot.Domain.Exceptions;

public sealed partial class InvalidNameException : Exception
{
    public IReadOnlyList<string> Violations { get; }

    public InvalidNameException(string fieldName, IReadOnlyList<string> violations)
        : base($"{fieldName} does not meet requirements: {string.Join(", ", violations)}.")
    {
        Violations = violations;
    }
}