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
			var text = "a";
			var type = AddressType.Billing;

			Address address = new();

			address.Line = text;
			address.Line2 = text;
			address.Type = type;
			address.City = text;
			address.PostalCode = text;
			address.State = text;
			address.Country = text;

			Assert.Equal(text, address.Line);
			Assert.Equal(text, address.Line2);
			Assert.Equal(type, address.Type);
			Assert.Equal(text, address.City);
			Assert.Equal(text, address.PostalCode);
			Assert.Equal(text, address.State);
			Assert.Equal(text, address.Country);
		}
	}
}
