namespace Ballot.Domain.Entities;

public sealed class Election : Entity, IAggregateRoot
{
    private readonly List<Candidate> _candidates = [];

    public Slug Slug { get; private set; }
    public ElectionTitle Title { get; private set; }
    public string? Description { get; private set; }
    public ElectionStatus Status { get; private set; }
    public DateTime? OpensAt { get; private set; }
    public DateTime? ClosesAt { get; private set; }
    public bool IsActive => Status is ElectionStatus.Scheduled or ElectionStatus.Ongoing;
    public IReadOnlyList<Candidate> Candidates => _candidates.AsReadOnly();
    public int ValidVoters { get; private set; }
    public string? AuditHash { get; private set; }

    public static Election Create(Slug slug, ElectionTitle title, string? description)
    {
        var election = new Election
        {
            Slug = slug,
            Title = title,
            Description = description,
            Status = ElectionStatus.Draft
        };

        return election;
    }

    public void Schedule(DateTime opensAt, DateTime closesAt)
    {
        if (Status != ElectionStatus.Pending)
            throw new InvalidOperationException("Election must have at least two candidates before it can be scheduled.");

        if (closesAt <= opensAt)
            throw new InvalidOperationException("ClosesAt must be after OpensAt.");

        OpensAt = opensAt;
        ClosesAt = closesAt;
        Status = ElectionStatus.Scheduled;
    }

    public void Open()
    {
        if (Status != ElectionStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled elections can be opened.");

        Status = ElectionStatus.Ongoing;
    }

    public void AddCandidate(Candidate candidate)
    {
        if (Status != ElectionStatus.Draft && Status != ElectionStatus.Pending)
            throw new InvalidOperationException("Candidates can only be added before an election is scheduled.");

        if (_candidates.Any(c => c.Id == candidate.Id))
            return;

        _candidates.Add(candidate);
        if (_candidates.Count != 2) return;
        
        Status = ElectionStatus.Pending;
    }

    public void RemoveCandidate(Guid candidateId)
    {
        if (Status != ElectionStatus.Draft && Status != ElectionStatus.Pending)
            throw new InvalidOperationException("Candidates can only be removed before an election is scheduled.");

        var candidate = _candidates.FirstOrDefault(c => c.Id == candidateId);
        if (candidate is null)
            return;

        _candidates.Remove(candidate);

        if (_candidates.Count >= 2 || Status != ElectionStatus.Pending) return;
        
        Status = ElectionStatus.Draft;
    }
}