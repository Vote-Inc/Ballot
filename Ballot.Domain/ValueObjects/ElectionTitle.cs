namespace Ballot.Domain.ValueObjects;

public sealed partial class ElectionTitle: ValueObject
{
    public string Value { get; }

    private ElectionTitle(string value) => Value = value;

    public static ElectionTitle Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        
        var violations = new List<string>();

        value = value.Trim();

        if (value.Length > 200)
            violations.Add("maximum 200 characters");
        
        if (value.Length == 0)
            violations.Add("empty string");
        
        if (violations.Count > 0)
            throw new InvalidElectionTitleException(violations);

        return new ElectionTitle(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public override string ToString() => Value;
}