namespace Ballot.Domain.ValueObjects;
public sealed partial class Party: ValueObject
{
    public string Value { get; }

    private Party(string value) => Value = value;

    public static Party Create(string value)
    {        
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        
        var violations = new List<string>();

        value = value.Trim();
        
        if (value.Length > 100)
            violations.Add("maximum 100 character");

        if (violations.Count > 0)
            throw new InvalidPartyException(violations);

        return new Party(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public override string ToString() => Value;
}