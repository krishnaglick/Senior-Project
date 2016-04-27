
using System;
using System.Collections.Generic;
using Models;

namespace DataAccess.TestData
{
    public class AddressGenerator : List<Address>
    {
        private Random _random;

        public AddressGenerator(int numberOfAddresses, Random random = null)
        {
            _random = random ?? new Random();
            for (int i = 0; i < numberOfAddresses; i++)
            {
                this.Add(GenerateAddress());
            }
        }

        public Address GenerateAddress()
        {
            return new Address
            {
                StreetAddress = GenerateStreetAddress(),
                City = CityNames[_random.Next(CityNames.Length)],
                State = StateNames[_random.Next(StateNames.Length)],
                Zip = GetRandomNumberSet(5)
            };
        }

        public string GenerateStreetAddress()
        {
            return GetRandomNumberSet() +
                   " " + StreetNames[_random.Next(StreetNames.Length)] +
                   " " + RoadTypes[_random.Next(RoadTypes.Length)];
        }

        public static string[] StreetNames = new string[]{"Devon", "Heritage", "William", "Sheffield", "Water", "Windsor", "Pheasant", "Argyle", "Martin Luther King", "Lakeshore"};
        public static string[] CityNames = new string[]{"Jacksonville", "Tampa", "Destin", "Orlando", "Miami", "St. Peterburg", "Tallahassee"};
        public static string[] StateNames = new string[] {"Florida", "Georgia", "South Carolina", "North Carolina", "Virginia", "Maryland", "Rhode Island", "New Hampshire"};
        public static string[] RoadTypes = new string[]{"Ct", "Dr", "St","Rd", "Blvd"};

        public string GetRandomNumberSet(int min = 2, int max = 5)
        {
            string output = string.Empty;
            for (int i = 0; i < _random.Next(min, max); i++)
            {
                var number = _random.Next(0, 9);
                if (i == 0 && number == 0)
                    number = int.Parse(GetRandomNumberSet(1, 1));

                output += number;
            }
            return output;
        }


    }
}
