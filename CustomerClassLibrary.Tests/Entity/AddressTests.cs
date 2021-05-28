using CustomerClassLibrary.Entity;
using CustomerClassLibrary.Enum;
using Xunit;

namespace CustomerClassLibrary.Tests.Entity
{
	public class AddressTests
	{
		[Fact]
		public void ShouldCreateAddress()
		{
			Address address = new();

			Assert.Null(address.Line);
			Assert.Null(address.Line2);
			Assert.Equal(AddressType.Shipping, address.Type);
			Assert.Null(address.City);
			Assert.Null(address.PostalCode);
			Assert.Null(address.State);
			Assert.Null(address.Country);
		}

		[Fact]
		public void ShouldSetAddressProperties()
		{
			var line = "line one";
			var line2 = "line two";
			var type = AddressType.Billing;
			var city = "Seattle";
			var postalCode = "123456";
			var state = "Washington";
			var country = "USA";

			Address address = new();
			address.Line = line;
			address.Line2 = line2;
			address.Type = type;
			address.City = city;
			address.PostalCode = postalCode;
			address.State = state;
			address.Country = country;

			Assert.Equal(line, address.Line);
			Assert.Equal(line2, address.Line2);
			Assert.Equal(type, address.Type);
			Assert.Equal(city, address.City);
			Assert.Equal(postalCode, address.PostalCode);
			Assert.Equal(state, address.State);
			Assert.Equal(country, address.Country);
		}
	}
}
