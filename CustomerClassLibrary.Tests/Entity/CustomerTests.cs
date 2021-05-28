using System.Collections.Generic;
using CustomerClassLibrary.Entity;
using Xunit;

namespace CustomerClassLibrary.Tests.Entity
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
			var firstName = "first name";
			var lastName = "last name";
			var addresses = new List<Address>();
			var phoneNumber = "phone number";
			var email = "e-mail";
			var notes = new List<string>();
			var totalPurchasesAmount = 2;

			Customer customer = new();
			customer.FirstName = firstName;
			customer.LastName = lastName;
			customer.Addresses = addresses;
			customer.PhoneNumber = phoneNumber;
			customer.Email = email;
			customer.Notes = notes;
			customer.TotalPurchasesAmount = totalPurchasesAmount;

			Assert.Equal(firstName, customer.FirstName);
			Assert.Equal(lastName, customer.LastName);
			Assert.Equal(addresses, customer.Addresses);
			Assert.Equal(phoneNumber, customer.PhoneNumber);
			Assert.Equal(email, customer.Email);
			Assert.Equal(notes, customer.Notes);
			Assert.Equal(totalPurchasesAmount, customer.TotalPurchasesAmount);
		}
	}
}
