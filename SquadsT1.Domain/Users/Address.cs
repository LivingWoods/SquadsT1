using Ardalis.GuardClauses;
using SquadsT1.Domain.Common;

namespace SquadsT1.Domain.Users;

public class Address : ValueObject
{
    public string AddressLine1 { get; }
    public string AddressLine2 { get; }
    public string ZipCode { get; }
    public string City { get; }

    public Address(string addressLine1, string addressLine2, string zipCode, string city)
    {
        Guard.Against.NullOrEmpty(addressLine1, nameof(addressLine1));
        Guard.Against.NullOrWhiteSpace(addressLine2, nameof(addressLine2));

        Guard.Against.NullOrEmpty(addressLine2, nameof(addressLine2));
        Guard.Against.NullOrWhiteSpace(addressLine2, nameof(addressLine2));

        Guard.Against.NullOrEmpty(zipCode, nameof(zipCode));
        Guard.Against.NullOrWhiteSpace(city, nameof(city));

        Guard.Against.NullOrEmpty(city, nameof(city));
        Guard.Against.NullOrWhiteSpace(city, nameof(city));

        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        ZipCode = zipCode;
        City = city;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return AddressLine1.ToLower();
        yield return AddressLine2.ToLower();
        yield return ZipCode.ToLower();
        yield return City.ToLower();
    }
}
