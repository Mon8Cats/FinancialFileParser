using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace OrderFileParser.Models
{
    public class Address
    {
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }

        public Address()
        {
            
        }

        public Address(string addressLine1, string addressLine2, string city, string state, string zip)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            Zip = zip;
        }

        public string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("200");
            sb.Append(AddressLine1.PadRight(50));
            sb.Append(AddressLine2.PadRight(50));
            sb.Append(City.PadRight(50));
            sb.Append(State.PadRight(2));
            sb.Append(Zip.PadRight(10));
            return sb.ToString();
        }
    }
}