using CustomerClassLibrary.Enums;

namespace CustomerClassLibrary.Entities
{
	public class Address
	{
		public string Line { get; set; }
		public string Line2 { get; set; }
		public AddressType Type { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
	}
}
