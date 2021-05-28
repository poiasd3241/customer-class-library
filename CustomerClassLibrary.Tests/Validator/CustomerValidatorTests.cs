using System.Collections.Generic;
using CustomerClassLibrary.Entity;
using CustomerClassLibrary.Validator;
using Xunit;

namespace CustomerClassLibrary.Tests.Validator
{
	public class CustomerValidatorTests
	{
		#region Private members

		private static readonly string _text_invalid_byWhitespace = " ";
		private static readonly string _text_valid_optional = null;

		// Name
		private static readonly string _name_valid = "MyName";
		private static readonly string _firstName_valid_optional = _text_valid_optional;
		private static readonly string _name_invalid_byLength = new('a', 51);
		private static readonly string _name_invalid_byWhiteSpace = _text_invalid_byWhitespace;
		private static readonly string _lastName_invalid_byNullOrEmpty = null;

		// Address
		private static readonly List<Address> _addresses_valid = new() { new() };
		private static readonly List<Address> _addresses_invalid_byCountMin = new() { };
		private static readonly List<Address> _addresses_invalid_byNull = null;

		// Phone Number
		private static readonly string _phoneNumber_valid = "+123456";
		private static readonly string _phoneNumber_valid_optional = _text_valid_optional;
		private static readonly string _phoneNumber_invalid_byFormat = "(111)222";
		private static readonly string _phoneNumber_invalid_byWhiteSpace = _text_invalid_byWhitespace;

		// Email
		private static readonly string _email_valid = "my@email.com";
		private static readonly string _email_valid_optional = _text_valid_optional;
		private static readonly string _email_invalid_byFormat = "@my.email";
		private static readonly string _email_invalid_byWhiteSpace = _text_invalid_byWhitespace;

		// Notes
		private static readonly List<string> _notes_valid = new() { "my note" };
		private static readonly List<string> _notes_invalid_byCountMin = new() { };
		private static readonly List<string> _notes_invalid_byNull = null;
		private static readonly List<string> _notes_invalid_byEmptyOrWhitespace = new() { _text_invalid_byWhitespace };

		private static Customer GetValidFilledCustomer()
		{
			return new Customer()
			{
				FirstName = _name_valid,
				LastName = _name_valid,
				Addresses = _addresses_valid,
				PhoneNumber = _phoneNumber_valid,
				Email = _email_valid,
				Notes = _notes_valid,
			};
		}

		#endregion

		#region [Full Customer validation]

		[Fact]
		public void ShouldValidateFilledCustomer()
		{
			// Given
			var validFilledCustomer = GetValidFilledCustomer();

			// When
			var result = CustomerValidator.Validate(validFilledCustomer);

			// Then
			Assert.False(result.HasErrors);
		}

		[Fact]
		public void ShouldValidateOptionalCustomer()
		{
			// Given
			var validOptionalCustomer = new Customer()
			{
				FirstName = _firstName_valid_optional,
				LastName = _name_valid,
				Addresses = _addresses_valid,
				PhoneNumber = _phoneNumber_valid_optional,
				Email = _email_valid_optional,
				Notes = _notes_valid,
			};

			// When
			var result = CustomerValidator.Validate(validOptionalCustomer);

			// Then
			Assert.False(result.HasErrors);
		}

		[Fact]
		public void ShouldInvalidateCustomerAllProperties()
		{
			// Given
			var invalidCustomerAllProperties = new Customer()
			{
				FirstName = _name_invalid_byLength,
				LastName = _name_invalid_byWhiteSpace,
				Addresses = _addresses_invalid_byCountMin,
				PhoneNumber = _phoneNumber_invalid_byFormat,
				Email = _email_invalid_byFormat,
				Notes = _notes_invalid_byCountMin,
			};
			;

			// When
			var result = CustomerValidator.Validate(invalidCustomerAllProperties);

			// Then
			Assert.Equal(6, result.Errors.Count);
		}

		#endregion

		#region First Name

		[Fact]
		public void ShouldInvalidateCustomerFirstNameTooLong()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.FirstName = _name_invalid_byLength;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("First name: max 50 characters.", errors[0]);
		}

		[Fact]
		public void ShouldInvalidateCustomerFirstNameWhitespace()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.FirstName = _name_invalid_byWhiteSpace;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("First name cannot consist of white-space characters.", errors[0]);
		}

		#endregion

		#region Last Name

		[Fact]
		public void ShouldInvalidateCustomerLastNameTooLong()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.LastName = _name_invalid_byLength;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Last name: max 50 characters.", errors[0]);
		}

		[Fact]
		public void ShouldInvalidateCustomerLastNameNullOrEmpty()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.LastName = _lastName_invalid_byNullOrEmpty;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Last name is required.", errors[0]);
		}

		[Fact]
		public void ShouldInvalidateCustomerLastNameWhitespace()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.LastName = _name_invalid_byWhiteSpace;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Last name cannot consist of white-space characters.", errors[0]);
		}

		#endregion

		#region Addresses

		[Fact]
		public void ShouldInvalidateCustomerAddressesCountMin()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Addresses = _addresses_invalid_byCountMin;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Addresses: at least 1 required.", errors[0]);
		}

		[Fact]
		public void ShouldInvalidateCustomerAddressesNull()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Addresses = _addresses_invalid_byNull;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Addresses: at least 1 required.", errors[0]);
		}

		#endregion

		#region Phone Number

		[Fact]
		public void ShouldInvalidateCustomerPhoneNumberFormat()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.PhoneNumber = _phoneNumber_invalid_byFormat;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Phone number: must be in E.164 format.", errors[0]);
		}

		[Fact]
		public void ShouldInvalidateCustomerPhoneNumberWhitespace()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.PhoneNumber = _phoneNumber_invalid_byWhiteSpace;
			;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Phone number cannot contain white-space characters.", errors[0]);
		}

		#endregion

		#region Email

		[Fact]
		public void ShouldInvalidateCustomerEmailFormat()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Email = _email_invalid_byFormat;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Invalid email.", errors[0]);
		}

		[Fact]
		public void ShouldInvalidateCustomerEmailWhitespace()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Email = _email_invalid_byWhiteSpace;
			;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Email cannot contain white-space characters.", errors[0]);
		}

		#endregion

		#region Notes

		[Fact]
		public void ShouldInvalidateCustomerNotesCountMin()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Notes = _notes_invalid_byCountMin;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Notes: at least 1 required.", errors[0]);
		}

		[Fact]
		public void ShouldInvalidateCustomerNotesNull()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Notes = _notes_invalid_byNull;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Notes: at least 1 required.", errors[0]);
		}

		[Fact]
		public void ShouldInvalidateCustomerNotesEmptyOrWhitespace()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Notes = _notes_invalid_byEmptyOrWhitespace;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			Assert.Single(errors);
			Assert.Equal("Notes cannot be empty or consist of white-space characters.", errors[0]);
		}

		#endregion
	}
}
