namespace Ballot.Domain.ValueObjects;

public sealed partial class Slug: ValueObject
{
    private static readonly Regex SlugRegex = MyRegex();

    public string Value { get; }

    private Slug(string value) => Value = value;

    public static Slug Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var violations = new List<string>();

        if (value.Length > 100)
            violations.Add("maximum 100 character");

        if (!SlugRegex.IsMatch(value))
            violations.Add("invalid slug format");
        
        if (violations.Count > 0)
            throw new InvalidSlugException(violations);

        return new Slug(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public override string ToString() => Value;

    public static implicit operator string(Slug slug) => slug.Value;

    [GeneratedRegex(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}