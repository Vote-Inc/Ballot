namespace Ballot.Application.Ballot.DTOs;

public record ElectionDetailDTO(
    Guid ElectionId,
    string Title,
    string? Description,
    string Status,
    List<CandidateDTO> Candidates,
    DateTime? OpensAt,
    DateTime? ClosesAt);
