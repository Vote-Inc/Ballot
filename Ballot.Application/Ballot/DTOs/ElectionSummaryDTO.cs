namespace Ballot.Application.Ballot.DTOs;

public record ElectionSummaryDTO(
    Guid ElectionId,
    string Title,
    string Status,
    List<CandidateDTO> Candidates,
    DateTime? ClosesAt
);
