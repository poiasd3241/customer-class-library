using CustomerClassLibrary.Entity;
using CustomerClassLibrary.Validator;
using Xunit;

namespace CustomerClassLibrary.Tests.Validator
{
	public class AddressValidatorTests
	{
		#region Private Members

		private static Address GetValidAddress()
		{
			return new Address()
			{
				Line = "line",
				Line2 = "line2",
				City = "city",
				PostalCode = "code",
				State = "state",
				Country = "United States"
			};
		}

		#endregion

		#region Valid

		[Fact]
		public void ShouldValidateAddress()
		{
			// Given
			var validAddress = GetValidAddress();

			// When
			var result = AddressValidator.Validate(validAddress);

			// Then
			Assert.False(result.HasErrors);
		}

		[Fact]
		public void ShouldValidateOptionalAddressByNull()
		{
			// Given
			var validOptionalAddress = GetValidAddress();
			validOptionalAddress.Line2 = null;

			// When
			var result = AddressValidator.Validate(validOptionalAddress);

			// Then
			Assert.False(result.HasErrors);
		}

		[Fact]
		public void ShouldValidateOptionalAddressByEmpty()
		{
			// Given
			var validOptionalAddress = GetValidAddress();
			validOptionalAddress.Line2 = "";

			// When
			var result = AddressValidator.Validate(validOptionalAddress);

			// Then
			Assert.False(result.HasErrors);
		}

		#endregion

		#region Invalid

		[Fact]
		public void ShouldInvalidateAddressByNull()
		{
			// Given
			var invalidAddress = GetValidAddress();

			invalidAddress.Line = null;
			invalidAddress.City = null;
			invalidAddress.PostalCode = null;
			invalidAddress.State = null;
			invalidAddress.Country = null;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			Assert.Equal(5, errors.Count);
			Assert.Equal("Address line is required.", errors[0]);
			Assert.Equal("City is required.", errors[1]);
			Assert.Equal("Postal code is required.", errors[2]);
			Assert.Equal("State is required.", errors[3]);
			Assert.Equal("Country is required.", errors[4]);
		}

		[Fact]
		public void ShouldInvalidateAddressByEmpty()
		{
			// Given
			var empty = "";
			var invalidAddress = GetValidAddress();

			invalidAddress.Line = empty;
			invalidAddress.City = empty;
			invalidAddress.PostalCode = empty;
			invalidAddress.State = empty;
			invalidAddress.Country = empty;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			Assert.Equal(5, errors.Count);
			Assert.Equal("Address line is required.", errors[0]);
			Assert.Equal("City is required.", errors[1]);
			Assert.Equal("Postal code is required.", errors[2]);
			Assert.Equal("State is required.", errors[3]);
			Assert.Equal("Country is required.", errors[4]);
		}

		[Fact]
		public void ShouldInvalidateAddressByWhitespace()
		{
			// Given
			var whitespace = " ";
			var invalidAddress = GetValidAddress();

			invalidAddress.Line = whitespace;
			invalidAddress.Line2 = whitespace;
			invalidAddress.City = whitespace;
			invalidAddress.PostalCode = whitespace;
			invalidAddress.State = whitespace;
			invalidAddress.Country = whitespace;

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			Assert.Equal(6, errors.Count);
			Assert.Equal("Address line cannot consist of whitespace characters.", errors[0]);
			Assert.Equal("Address line2 cannot consist of whitespace characters.", errors[1]);
			Assert.Equal("City cannot consist of whitespace characters.", errors[2]);
			Assert.Equal("Postal code cannot consist of whitespace characters.", errors[3]);
			Assert.Equal("State cannot consist of whitespace characters.", errors[4]);
			Assert.Equal("Country cannot consist of whitespace characters.", errors[5]);
		}

		[Fact]
		public void ShouldInvalidateAddressByLengthAndAllowed()
		{
			// Given
			var invalidAddress = GetValidAddress();

			invalidAddress.Line = new('a', 101);
			invalidAddress.Line2 = new('a', 101);
			invalidAddress.City = new('a', 51);
			invalidAddress.PostalCode = new('a', 7);
			invalidAddress.State = new('a', 21);
			invalidAddress.Country = "Japan";

			// When
			var errors = AddressValidator.Validate(invalidAddress).Errors;

			// Then
			Assert.Equal(6, errors.Count);
			Assert.Equal("Address line: max 100 characters.", errors[0]);
			Assert.Equal("Address line2: max 100 characters.", errors[1]);
			Assert.Equal("City: max 50 characters.", errors[2]);
			Assert.Equal("Postal code: max 6 characters.", errors[3]);
			Assert.Equal("State: max 20 characters.", errors[4]);
			Assert.Equal("Country: allowed only United States, Canada.", errors[5]);
		}

		#endregion
	}
}
