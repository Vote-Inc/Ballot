namespace Ballot.Domain.Entities;

public sealed class Candidate : Entity
{

    public Guid ElectionId { get; private set; }
    public Name Forename { get; private set; } = null!;
    public Name Surname { get; private set; } = null!;
    public Name? OtherNames { get; private set; }
    public Name? KnownAs { get; private set; }
    public Party Party { get; private set; } = null!;
    public Uri ImgUrl { get; private set; } = null!;

    public string DisplayName => KnownAs is not null
        ? $"{KnownAs} {Surname}"
        : $"{Forename} {Surname}";

    public static Candidate Create(
        Guid electionId,
        Name forename,
        Name surname,
        Name? otherNames,
        Name? knownAs,
        Party party,
        Uri imgUrl)
    {
        return new Candidate
        {
            Id = Guid.NewGuid(),
            ElectionId = electionId,
            Forename = forename,
            Surname = surname,
            OtherNames = otherNames,
            KnownAs = knownAs,
            Party = party,
            ImgUrl = imgUrl
        };
    }
    
}