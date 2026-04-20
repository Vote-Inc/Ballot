namespace Ballot.Application.Ballot.Errors;

public static class BallotErrors
{
    public static readonly Error ElectionNotFound = new("ballot.election.not_found", "Election not found.");
}
