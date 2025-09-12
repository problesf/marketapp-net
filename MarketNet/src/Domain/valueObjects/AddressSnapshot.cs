namespace MarketNet.Domain.valueObjects;
using System;
using MarketNet.Domain.entities.Customers;
public record AddressSnapshot
{
    public string Line1 { get; init; }
    public string Line2 { get; init; } // Opcional
    public string City { get; init; }
    public string State { get; init; }
    public string PostalCode { get; init; }
    public string Country { get; init; }

    public AddressSnapshot(string line1, string city, string state, string postalCode, string country, string line2 = null)
    {
        if (string.IsNullOrWhiteSpace(line1)) throw new ArgumentException("Line1 cannot be empty.", nameof(line1));
        if (string.IsNullOrWhiteSpace(line2)) throw new ArgumentException("Line2 cannot be empty.", nameof(line2));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City cannot be empty.", nameof(city));
        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State cannot be empty.", nameof(state));
        if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("PostalCode cannot be empty.", nameof(postalCode));
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country cannot be empty.", nameof(Country));

        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    public static AddressSnapshot FromAddress(Address address)
    {
        if (address == null) throw new ArgumentNullException(nameof(address));
        return new AddressSnapshot(
            address.Line1,
            address.City,
            address.State,
            address.PostalCode,
            address.Country,
            address.Line2
        );
    }

 
}
