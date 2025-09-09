namespace MarketApp.src.Domain.valueObjects;
using System;
using MarketNet.src.Domain.entities.Customer;

// Usamos 'record' para inmutabilidad y comparación por valor
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

        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    public static AddressSnapshot FromAddress(Address address)
    {
        if (address == null) return null;
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
