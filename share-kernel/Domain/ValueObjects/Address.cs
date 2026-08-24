using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public record Address
    {
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
        public string Country { get; init; }

        private Address(string street, string city, string state, string zipCode, string country)
        {
            if (string.IsNullOrEmpty(street)) throw new ArgumentNullException("Street can not be null or empty", nameof(street));
            if (string.IsNullOrEmpty(city)) throw new ArgumentNullException("City cannot be null or empty", nameof(city));
            if (string.IsNullOrEmpty(state)) throw new ArgumentNullException("State cannot be null or empty", nameof(state));
            if (string.IsNullOrEmpty(zipCode)) throw new ArgumentNullException("Zipcode cannot be null or empty", nameof(zipCode));
            if (string.IsNullOrEmpty(country)) throw new ArgumentNullException("Country cannot be null or empty", nameof(country));
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }
        public static Address Create(string street, string city, string state, string zipCode, string country)
        {
            return new Address(street, city, state, zipCode, country);
        }
        public override string ToString()
        {
            return $"{Street},{City},{State},{ZipCode},{Country}";
        }
    }
}
