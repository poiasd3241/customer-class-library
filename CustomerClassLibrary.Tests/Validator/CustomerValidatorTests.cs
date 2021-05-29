using System.Collections.Generic;
using CustomerClassLibrary.Entity;
using CustomerClassLibrary.Validator;
using Xunit;

namespace CustomerClassLibrary.Tests.Validator
{
	public class CustomerValidatorTests
	{
		#region Private members

		private static readonly string _text_valid_optional_byNull = null;
		private static readonly string _text_valid_optional_byEmpty = "";
		private static readonly string _text_invalid_required_byNull = null;
		private static readonly string _text_invalid_required_byEmpty = "";
		private static readonly string _text_invalid_byWhitespace = " ";

		// Name
		private static readonly string _name_valid = "MyName";
		private static readonly string _firstName_valid_optional_byNull = _text_valid_optional_byNull;
		private static readonly string _firstName_valid_optional_byEmpty = _text_valid_optional_byEmpty;
		private static readonly string _name_invalid_byLength = new('a', 51);
		private static readonly string _name_invalid_byWhiteSpace = _text_invalid_byWhitespace;
		private static readonly string _lastName_invalid_required_byNull = _text_invalid_required_byNull;
		private static readonly string _lastName_invalid_required_byEmpty = _text_invalid_required_byEmpty;

		// Address
		private static readonly List<Address> _addresses_valid = new() { new() };
		private static readonly List<Address> _addresses_invalid_byCountMin = new() { };
		private static readonly List<Address> _addresses_invalid_byNull = null;

		// Phone Number
		private static readonly string _phoneNumber_valid = "+123456";
		private static readonly string _phoneNumber_valid_optional_byNull = _text_valid_optional_byNull;
		private static readonly string _phoneNumber_valid_optional_byEmpty = _text_valid_optional_byEmpty;
		private static readonly string _phoneNumber_invalid_byFormat = "(111)222";
		private static readonly string _phoneNumber_invalid_byWhiteSpace = _text_invalid_byWhitespace;

		// Email
		private static readonly string _email_valid = "my@email.com";
		private static readonly string _email_valid_optional_byNull = _text_valid_optional_byNull;
		private static readonly string _email_valid_optional_byEmpty = _text_valid_optional_byEmpty;
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
		public void ShouldValidateOptionalCustomerByNull()
		{
			// Given
			var validOptionalCustomer = new Customer()
			{
				FirstName = _firstName_valid_optional_byNull,
				LastName = _name_valid,
				Addresses = _addresses_valid,
				PhoneNumber = _phoneNumber_valid_optional_byNull,
				Email = _email_valid_optional_byNull,
				Notes = _notes_valid,
			};

			// When
			var result = CustomerValidator.Validate(validOptionalCustomer);

			// Then
			Assert.False(result.HasErrors);
		}

		[Fact]
		public void ShouldValidateOptionalCustomerByEmpty()
		{
			// Given
			var validOptionalCustomer = new Customer()
			{
				FirstName = _firstName_valid_optional_byEmpty,
				LastName = _name_valid,
				Addresses = _addresses_valid,
				PhoneNumber = _phoneNumber_valid_optional_byEmpty,
				Email = _email_valid_optional_byEmpty,
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
		public void ShouldInvalidateCustomerFirstNameWhitespace()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.FirstName = _name_invalid_byWhiteSpace;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("First name cannot consist of white-space characters.", error);
		}

		[Fact]
		public void ShouldInvalidateCustomerFirstNameTooLong()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.FirstName = _name_invalid_byLength;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("First name: max 50 characters.", error);
		}

		#endregion

		#region Last Name

		[Fact]
		public void ShouldInvalidateCustomerLastNameRequiredByNull()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.LastName = _lastName_invalid_required_byNull;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Last name is required.", error);
		}

		[Fact]
		public void ShouldInvalidateCustomerLastNameRequiredByEmpty()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.LastName = _lastName_invalid_required_byEmpty;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Last name is required.", error);
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
			var error = Assert.Single(errors);
			Assert.Equal("Last name cannot consist of white-space characters.", error);
		}

		[Fact]
		public void ShouldInvalidateCustomerLastNameTooLong()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.LastName = _name_invalid_byLength;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Last name: max 50 characters.", error);
		}

		#endregion

		#region Addresses

		[Fact]
		public void ShouldInvalidateCustomerAddressesRequiredbyNull()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Addresses = _addresses_invalid_byNull;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Addresses: at least 1 required.", error);
		}

		[Fact]
		public void ShouldInvalidateCustomerAddressesRequiredByCountMin()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Addresses = _addresses_invalid_byCountMin;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Addresses: at least 1 required.", error);
		}

		#endregion

		#region Phone Number

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
			var error = Assert.Single(errors);
			Assert.Equal("Phone number cannot contain white-space characters.", error);
		}

		[Fact]
		public void ShouldInvalidateCustomerPhoneNumberFormat()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.PhoneNumber = _phoneNumber_invalid_byFormat;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Phone number: must be in E.164 format.", error);
		}

		#endregion

		#region Email

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
			var error = Assert.Single(errors);
			Assert.Equal("Email cannot contain white-space characters.", error);
		}

		[Fact]
		public void ShouldInvalidateCustomerEmailFormat()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Email = _email_invalid_byFormat;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Invalid email.", error);
		}

		#endregion

		#region Notes

		[Fact]
		public void ShouldInvalidateCustomerNotesRequiredByNull()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Notes = _notes_invalid_byNull;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Notes: at least 1 required.", error);
		}

		[Fact]
		public void ShouldInvalidateCustomerNotesRequiredByCountMin()
		{
			// Given
			var invalidCustomer = GetValidFilledCustomer();
			invalidCustomer.Notes = _notes_invalid_byCountMin;

			// When
			var errors = CustomerValidator.Validate(invalidCustomer).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Notes: at least 1 required.", error);
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
			var error = Assert.Single(errors);
			Assert.Equal("Notes cannot be empty or consist of white-space characters.", error);
		}

		#endregion
	}
}
