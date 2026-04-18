namespace Ballot.Application.Ballot.DTOs;

public static class ElectionMappingExtensions
{
    public static ElectionSummaryDTO ToSummaryDto(this Election election) =>
        new(
            ElectionId: election.Id,
            Title: election.Title.Value,
            Status: election.Status.ToString(),
            Candidates: election.Candidates.Select(c => c.ToDto()).ToList(),
            ClosesAt: election.ClosesAt
        );

    public static ElectionDetailDTO ToDetailDto(this Election election) =>
        new(
            ElectionId: election.Id,
            Title: election.Title.Value,
            Status: election.Status.ToString(),
            Candidates: election.Candidates.Select(c => c.ToDto()).ToList(),
            OpensAt: election.OpensAt,
            ClosesAt: election.ClosesAt
        );

    public static CandidateDTO ToDto(this Candidate candidate) =>
        new(
            CandidateId: candidate.Id,
            DisplayName: candidate.DisplayName,
            Party: candidate.Party.Value,
            ImgUrl: candidate.ImgUrl.ToString()
        );
}
