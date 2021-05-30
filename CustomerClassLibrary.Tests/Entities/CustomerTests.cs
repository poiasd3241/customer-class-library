using System.Collections.Generic;
using CustomerClassLibrary.Entities;
using Xunit;

namespace CustomerClassLibrary.Tests.Entities
{
	public class CustomerTests
	{
		[Fact]
		public void ShouldCreateCustomer()
		{
			Customer customer = new();

			Assert.Null(customer.FirstName);
			Assert.Null(customer.LastName);
			Assert.Null(customer.Addresses);
			Assert.Null(customer.PhoneNumber);
			Assert.Null(customer.Email);
			Assert.Null(customer.Notes);
			Assert.Null(customer.TotalPurchasesAmount);
		}

		[Fact]
		public void ShouldSetCustomerProperties()
		{
			var text = "a";
			var addresses = new List<Address>();
			var notes = new List<string>();
			var total = 2m;

			Customer customer = new();
			customer.FirstName = text;
			customer.LastName = text;
			customer.Addresses = addresses;
			customer.PhoneNumber = text;
			customer.Email = text;
			customer.Notes = notes;
			customer.TotalPurchasesAmount = total;

			Assert.Equal(text, customer.FirstName);
			Assert.Equal(text, customer.LastName);
			Assert.Equal(addresses, customer.Addresses);
			Assert.Equal(text, customer.PhoneNumber);
			Assert.Equal(text, customer.Email);
			Assert.Equal(notes, customer.Notes);
			Assert.Equal(total, customer.TotalPurchasesAmount);
		}
	}
}
