using CustomerClassLibrary.Entities;
using CustomerClassLibrary.Validators;
using Xunit;

namespace CustomerClassLibrary.Tests.Validators
{
	public class CustomerValidatorTests
	{
		#region Private members

		private static readonly CustomerValidator _customerValidator = new();

		private static Customer GetValidCustomer()
		{
			return new Customer()
			{
				FirstName = "a",
				LastName = "a",
				Addresses = new() { new() },
				PhoneNumber = "+123",
				Email = "my@email.com",
				Notes = new() { "a" },
			};
		}

		#endregion

		#region Valid

		[Fact]
		public void ShouldValidateFilledCustomer()
		{
			// Given
			var validFilledCustomer = GetValidCustomer();

			// When
			var result = _customerValidator.Validate(validFilledCustomer);

			// Then
			Assert.True(result.IsValid);
		}

		[Fact]
		public void ShouldValidateOptionalCustomerByNull()
		{
			// Given
			var validOptionalCustomer = GetValidCustomer();

			validOptionalCustomer.FirstName = null;
			validOptionalCustomer.PhoneNumber = null;
			validOptionalCustomer.Email = null;

			// When
			var result = _customerValidator.Validate(validOptionalCustomer);

			// Then
			Assert.True(result.IsValid);
		}

		[Fact]
		public void ShouldValidateOptionalCustomerByEmpty()
		{
			// Given
			var empty = "";
			var validOptionalCustomer = GetValidCustomer();

			validOptionalCustomer.FirstName = empty;
			validOptionalCustomer.PhoneNumber = empty;
			validOptionalCustomer.Email = empty;

			// When
			var result = _customerValidator.Validate(validOptionalCustomer);

			// Then
			Assert.True(result.IsValid);
		}

		#endregion

		#region Invalid

		[Fact]
		public void ShouldInvalidateCustomerByNullProperties()
		{
			// Given
			var invalidCustomer = GetValidCustomer();

			invalidCustomer.LastName = null;
			invalidCustomer.Addresses = null;
			invalidCustomer.Notes = null;

			// When
			var errors = _customerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Equal(3, errors.Count);
			Assert.Equal("Last name is required.", errors[0].ErrorMessage);
			Assert.Equal("At least one address is required.", errors[1].ErrorMessage);
			Assert.Equal("At least one note is required.", errors[2].ErrorMessage);
		}

		[Fact]
		public void ShouldInvalidateCustomerByEmptyProperties()
		{
			// Given
			var invalidCustomer = GetValidCustomer();

			invalidCustomer.LastName = "";
			invalidCustomer.Addresses = new();
			invalidCustomer.Notes = new();

			// When
			var errors = _customerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Equal(3, errors.Count);
			Assert.Equal("Last name is required.", errors[0].ErrorMessage);
			Assert.Equal("At least one address is required.", errors[1].ErrorMessage);
			Assert.Equal("At least one note is required.", errors[2].ErrorMessage);
		}

		[Fact]
		public void ShouldInvalidateCustomerByWhitespaceProperties()
		{
			// Given
			var whitespace = " ";
			var invalidCustomer = GetValidCustomer();

			invalidCustomer.FirstName = whitespace;
			invalidCustomer.LastName = whitespace;
			invalidCustomer.PhoneNumber = whitespace;
			invalidCustomer.Email = whitespace;

			// When
			var errors = _customerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Equal(4, errors.Count);
			Assert.Equal("First name cannot consist of whitespace characters.", errors[0].ErrorMessage);
			Assert.Equal("Last name cannot consist of whitespace characters.", errors[1].ErrorMessage);
			Assert.Equal("Phone number cannot contain whitespace characters.", errors[2].ErrorMessage);
			Assert.Equal("Email cannot contain whitespace characters.", errors[3].ErrorMessage);
		}

		[Fact]
		public void ShouldInvalidateCustomerByLength()
		{
			// Given
			var invalidCustomer = GetValidCustomer();

			invalidCustomer.FirstName = new('a', 51);
			invalidCustomer.LastName = new('a', 51);

			// When
			var errors = _customerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Equal(2, errors.Count);
			Assert.Equal("First name: max 50 characters.", errors[0].ErrorMessage);
			Assert.Equal("Last name: max 50 characters.", errors[1].ErrorMessage);
		}

		[Fact]
		public void ShouldInvalidateCustomerByNoteTextNull()
		{
			// Given
			var invalidCustomer = GetValidCustomer();

			invalidCustomer.Notes = new() { null };

			// When
			var errors = _customerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Notes cannot be empty or consist of whitespace characters.", errors[0].ErrorMessage);
		}

		[Fact]
		public void ShouldInvalidateCustomerByNoteTextEmpty()
		{
			// Given
			var invalidCustomer = GetValidCustomer();

			invalidCustomer.Notes = new() { "" };

			// When
			var errors = _customerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Notes cannot be empty or consist of whitespace characters.", errors[0].ErrorMessage);
		}


		[Fact]
		public void ShouldInvalidateCustomerByNoteTextWhitespace()
		{
			// Given
			var invalidCustomer = GetValidCustomer();

			invalidCustomer.Notes = new() { " " };

			// When
			var errors = _customerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Notes cannot be empty or consist of whitespace characters.", errors[0].ErrorMessage);
		}

		#endregion
	}
}
