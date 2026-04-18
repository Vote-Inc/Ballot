namespace Ballot.Application.Ballot.DTOs;

public record CandidateDTO(
    Guid CandidateId,
    string DisplayName,
    string Party,
    string ImgUrl
);
