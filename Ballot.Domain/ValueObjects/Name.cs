namespace Ballot.Domain.ValueObjects;

public sealed partial class Name: ValueObject
{
    private static readonly Regex NameRegex = MyRegex();
    public string Value { get; }

    private Name(string value) => Value = value;

    public static Name Create(string value, string fieldName = "Name")
    {        
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        
        var violations = new List<string>();

        value = value.Trim();

        if (value.Length > 100)
            violations.Add("maximum 100 characters");

        if (!NameRegex.IsMatch(value))
            violations.Add($"invalid name format");
            
        if (violations.Count > 0)    
            throw new InvalidNameException(fieldName, violations);

        return new Name(value);
    }

    public override string ToString() => Value;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public static implicit operator string(Name name) => name.Value;

    [GeneratedRegex(@"^[\p{L}\s\-']+$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}