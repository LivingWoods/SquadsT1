using SquadsT1.Domain.Common;
using Ardalis.GuardClauses;

namespace SquadsT1.Domain.Users;

public class PhoneNumber : ValueObject
{
    private string _validPhoneNumberRegex = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

    public string Value { get; } = default!;

	public PhoneNumber(string number)
    {
        number = number.Trim();

        Guard.Against.NullOrEmpty(number, nameof(number));
        Guard.Against.NullOrWhiteSpace(number, nameof(number));
        Guard.Against.InvalidFormat(number, nameof(number), _validPhoneNumberRegex);

        Value = number.ToLower();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}