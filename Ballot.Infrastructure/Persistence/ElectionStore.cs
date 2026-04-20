namespace Ballot.Infrastructure.Persistence;

public sealed class ElectionStore
{
    public IReadOnlyList<Election> Elections { get; } = Seed();

    private static List<Election> Seed()
    {
        var faker = new Faker();
        var elections = new List<Election>();

        var electionTitles = new[]
        {
            "Presidential Election", "Senate Race", "Mayoral Election",
            "City Council Election", "Governor Race"
        };

        var parties = new[] { "Labour", "Conservative", "Liberal", "Green", "Independent" };

        foreach (var titleText in electionTitles)
        {
            var slug = Slug.Create(titleText.ToLower().Replace(" ", "-"));
            var title = ElectionTitle.Create(titleText);
            var election = Election.Create(slug, title, faker.Lorem.Sentence());

            var candidateCount = faker.Random.Int(2, 6);
            for (var i = 0; i < candidateCount; i++)
            {
                var forename = Name.Create(faker.Name.FirstName());
                var surname = Name.Create(faker.Name.LastName());
                var party = Party.Create(faker.PickRandom(parties));
                var imgUrl = new Uri($"https://i.pravatar.cc/150?u={Guid.NewGuid()}");

                var candidate = Candidate.Create(
                    election.Id,
                    forename,
                    surname,
                    otherNames: null,
                    knownAs: null,
                    party,
                    imgUrl
                );

                election.AddCandidate(candidate);
            }
            
            elections.Add(election);
        }

        foreach (var election in elections)
        {
            var opensAt = faker.Date.Recent(days: 5);
            var closesAt = faker.Date.Soon(days: 14);
            election.Schedule(opensAt, closesAt);

            election.Open();
        }
        
        return elections;
    }
}
