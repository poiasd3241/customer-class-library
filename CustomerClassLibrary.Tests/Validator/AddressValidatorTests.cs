using CustomerClassLibrary.Entity;
using CustomerClassLibrary.Validator;
using Xunit;

namespace CustomerClassLibrary.Tests.Validator
{
	public class AddressValidatorTests
	{
		#region Private members

		private static readonly string _text_valid_optional_byNull = null;
		private static readonly string _text_valid_optional_byEmpty = "";
		private static readonly string _text_invalid_required_byNull = null;
		private static readonly string _text_invalid_required_byEmpty = "";
		private static readonly string _text_invalid_byWhitespace = " ";

		// Line
		private static readonly string _line_valid = "Line one";
		private static readonly string _line_invalid_required_byNull = _text_invalid_required_byNull;
		private static readonly string _line_invalid_required_byEmpty = _text_invalid_required_byEmpty;
		private static readonly string _line_invalid_byWhiteSpace = _text_invalid_byWhitespace;
		private static readonly string _line_invalid_byLength = new('a', 101);

		// Line2
		private static readonly string _line2_valid = "Line two";
		private static readonly string _line2_valid_optional_byNull = _text_valid_optional_byNull;
		private static readonly string _line2_valid_optional_byEmpty = _text_valid_optional_byEmpty;
		private static readonly string _line2_invalid_byWhiteSpace = _text_invalid_byWhitespace;
		private static readonly string _line2_invalid_byLength = new('a', 101);

		// City
		private static readonly string _city_valid = "Seattle";
		private static readonly string _city_invalid_required_byNull = _text_invalid_required_byNull;
		private static readonly string _city_invalid_required_byEmpty = _text_invalid_required_byEmpty;
		private static readonly string _city_invalid_byWhiteSpace = _text_invalid_byWhitespace;
		private static readonly string _city_invalid_byLength = new('a', 51);

		// Postal code
		private static readonly string _postalCode_valid = "98177";
		private static readonly string _postalCode_invalid_required_byNull = _text_invalid_required_byNull;
		private static readonly string _postalCode_invalid_required_byEmpty = _text_invalid_required_byEmpty;
		private static readonly string _postalCode_invalid_byWhiteSpace = _text_invalid_byWhitespace;
		private static readonly string _postalCode_invalid_byLength = new('a', 7);

		// State
		private static readonly string _state_valid = "Washington";
		private static readonly string _state_invalid_required_byNull = _text_invalid_required_byNull;
		private static readonly string _state_invalid_required_byEmpty = _text_invalid_required_byEmpty;
		private static readonly string _state_invalid_byWhiteSpace = _text_invalid_byWhitespace;
		private static readonly string _state_invalid_byLength = new('a', 21);

		// Country
		private static readonly string _country_valid = "United States";
		private static readonly string _country_invalid_required_byNull = _text_invalid_required_byNull;
		private static readonly string _country_invalid_required_byEmpty = _text_invalid_required_byEmpty;
		private static readonly string _country_invalid_byWhiteSpace = _text_invalid_byWhitespace;
		private static readonly string _country_invalid_byAllowed = "Whatever";

		private static Address GetValidFilledAddress()
		{
			return new Address()
			{
				Line = _line_valid,
				Line2 = _line2_valid,
				City = _city_valid,
				PostalCode = _postalCode_valid,
				State = _state_valid,
				Country = _country_valid
			};
		}

		#endregion

		#region [Full Address validation]

		[Fact]
		public void ShouldValidateFilledAddress()
		{
			// Given
			var validFilledAddress = GetValidFilledAddress();

			// When
			var result = AddressValidator.Validate(validFilledAddress);

			// Then
			Assert.False(result.HasErrors);
		}

		[Fact]
		public void ShouldValidateOptionalAddressByNull()
		{
			// Given
			var validOptionalAddress = GetValidFilledAddress();
			validOptionalAddress.Line2 = _line2_valid_optional_byNull;

			// When
			var result = AddressValidator.Validate(validOptionalAddress);

			// Then
			Assert.False(result.HasErrors);
		}

		[Fact]
		public void ShouldValidateOptionalAddressByEmpty()
		{
			// Given
			var validOptionalAddress = GetValidFilledAddress();
			validOptionalAddress.Line2 = _line2_valid_optional_byEmpty;

			// When
			var result = AddressValidator.Validate(validOptionalAddress);

			// Then
			Assert.False(result.HasErrors);
		}

		[Fact]
		public void ShouldInvalidateAddressAllProperties()
		{
			// Given
			var invalidAddressAllProperties = new Address()
			{
				Line = _line_invalid_byLength,
				Line2 = _line2_invalid_byLength,
				City = _city_invalid_byLength,
				PostalCode = _postalCode_invalid_byLength,
				State = _state_invalid_byLength,
				Country = _country_invalid_byAllowed
			};

			// When
			var result = AddressValidator.Validate(invalidAddressAllProperties);

			// Then
			Assert.Equal(6, result.Errors.Count);
		}

		#endregion

		#region Line

		[Fact]
		public void ShouldInvalidateAddressLineRequiredByNull()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.Line = _line_invalid_required_byNull;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Address line is required.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressLineRequiredByEmpty()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.Line = _line_invalid_required_byEmpty;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Address line is required.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressLineWhitespace()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.Line = _line_invalid_byWhiteSpace;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Address line cannot consist of white-space characters.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressLineTooLong()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.Line2 = _line_invalid_byLength;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Address line2: max 100 characters.", error);
		}

		#endregion

		#region Line2

		[Fact]
		public void ShouldInvalidateAddressLine2Whitespace()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.Line2 = _line2_invalid_byWhiteSpace;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Address line2 cannot consist of white-space characters.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressLine2TooLong()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.Line2 = _line2_invalid_byLength;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Address line2: max 100 characters.", error);
		}

		#endregion

		#region City

		[Fact]
		public void ShouldInvalidateAddressCityRequiredByNull()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.City = _city_invalid_required_byNull;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("City is required.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressCityRequiredByEmpty()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.City = _city_invalid_required_byEmpty;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("City is required.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressCityWhitespace()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.City = _city_invalid_byWhiteSpace;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("City cannot consist of white-space characters.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressCityTooLong()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.City = _city_invalid_byLength;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("City: max 50 characters.", error);
		}

		#endregion

		#region Postal code

		[Fact]
		public void ShouldInvalidateAddressPostalCodeRequiredByNull()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.PostalCode = _postalCode_invalid_required_byNull;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Postal code is required.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressPostalCodeRequiredByEmpty()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.PostalCode = _postalCode_invalid_required_byEmpty;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Postal code is required.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressPostalCodeWhitespace()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.PostalCode = _postalCode_invalid_byWhiteSpace;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Postal code cannot consist of white-space characters.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressPostalCodeTooLong()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.PostalCode = _postalCode_invalid_byLength;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Postal code: max 6 characters.", error);
		}

		#endregion

		#region State

		[Fact]
		public void ShouldInvalidateAddressStateRequiredByNull()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.State = _state_invalid_required_byNull;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("State is required.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressStateRequiredByEmpty()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.State = _state_invalid_required_byEmpty;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("State is required.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressStateWhitespace()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.State = _state_invalid_byWhiteSpace;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("State cannot consist of white-space characters.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressStateTooLong()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.State = _state_invalid_byLength;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("State: max 20 characters.", error);
		}

		#endregion

		#region Country

		[Fact]
		public void ShouldInvalidateAddressCountryRequiredByNull()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.Country = _country_invalid_required_byNull;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Country is required.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressCountryRequiredByEmpty()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.Country = _country_invalid_required_byEmpty;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Country is required.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressCountryWhitespace()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.Country = _country_invalid_byWhiteSpace;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Country cannot consist of white-space characters.", error);
		}

		[Fact]
		public void ShouldInvalidateAddressCountryAllowed()
		{
			// Given
			var invalidAddress = GetValidFilledAddress();
			invalidAddress.Country = _country_invalid_byAllowed;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			var error = Assert.Single(errors);
			Assert.Equal("Country: allowed only United States, Canada.", error);
		}

		#endregion
	}
}
