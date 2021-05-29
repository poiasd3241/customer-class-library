using System.Collections.Generic;
using CustomerClassLibrary.Entity;
using CustomerClassLibrary.Localization;

namespace CustomerClassLibrary.Validator
{
	public class AddressValidator : TextValidatorBase
	{
		private static readonly int _line_max_length = 100;
		private static readonly int _city_max_length = 50;
		private static readonly int _postalCode_max_length = 6;
		private static readonly int _state_max_length = 20;
		private static readonly List<string> _country_allowed =
			new() { "United States", "Canada" };

		public static ValidationResult Validate(Address address)
		{
			ValidationResult result = new();

			ValidateLine(address.Line, result);
			ValidateLine2(address.Line2, result);
			ValidateCity(address.City, result);
			ValidatePostalCode(address.PostalCode, result);
			ValidateState(address.State, result);
			ValidateCountry(address.Country, result);

			return result;
		}

		#region Private Methods

		private static ValidationResult ValidateLine(string line, ValidationResult result)
		{
			if (string.IsNullOrEmpty(line))
			{
				// Required.
				result.AddError(ValidationRules.ADDRESS_LINE_REQUIRED);
			}
			else if (IsWhitespace(line))
			{
				result.AddError(ValidationRules.ADDRESS_LINE_WHITESPACE);
			}
			else if (line.Length > _line_max_length)
			{
				result.AddError(string.Format(ValidationRules.ADDRESS_LINE_MAX_LENGTH, _line_max_length));
			}

			return result;
		}

		private static ValidationResult ValidateLine2(string line2, ValidationResult result)
		{
			if (string.IsNullOrEmpty(line2))
			{
				// Optional.
				return result;
			}
			else if (IsWhitespace(line2))
			{
				result.AddError(ValidationRules.ADDRESS_LINE2_WHITESPACE);
			}
			else if (line2.Length > _line_max_length)
			{
				result.AddError(string.Format(ValidationRules.ADDRESS_LINE2_MAX_LENGTH, _line_max_length));
			}

			return result;
		}

		private static ValidationResult ValidateCity(string city, ValidationResult result)
		{
			if (string.IsNullOrEmpty(city))
			{
				// Required.
				result.AddError(ValidationRules.ADDRESS_CITY_REQUIRED);
			}
			else if (IsWhitespace(city))
			{
				result.AddError(ValidationRules.ADDRESS_CITY_WHITESPACE);
			}
			else if (city.Length > _city_max_length)
			{
				result.AddError(string.Format(ValidationRules.ADDRESS_CITY_MAX_LENGTH, _city_max_length));
			}

			return result;
		}
		private static ValidationResult ValidatePostalCode(string postalCode, ValidationResult result)
		{
			if (string.IsNullOrEmpty(postalCode))
			{
				// Required.
				result.AddError(ValidationRules.ADDRESS_POSTAL_CODE_REQUIRED);
			}
			else if (IsWhitespace(postalCode))
			{
				result.AddError(ValidationRules.ADDRESS_POSTAL_CODE_WHITESPACE);
			}
			else if (postalCode.Length > _postalCode_max_length)
			{
				result.AddError(string.Format(ValidationRules.ADDRESS_POSTAL_CODE_MAX_LENGTH, _postalCode_max_length));
			}

			return result;
		}
		private static ValidationResult ValidateState(string state, ValidationResult result)
		{
			if (string.IsNullOrEmpty(state))
			{
				// Required.
				result.AddError(ValidationRules.ADDRESS_STATE_REQUIRED);
			}
			else if (IsWhitespace(state))
			{
				result.AddError(ValidationRules.ADDRESS_STATE_WHITESPACE);
			}
			else if (state.Length > _state_max_length)
			{
				result.AddError(string.Format(ValidationRules.ADDRESS_STATE_MAX_LENGTH, _state_max_length));
			}

			return result;
		}
		private static ValidationResult ValidateCountry(string country, ValidationResult result)
		{
			if (string.IsNullOrEmpty(country))
			{
				// Required.
				result.AddError(ValidationRules.ADDRESS_COUNTRY_REQUIRED);
			}
			else if (IsWhitespace(country))
			{
				result.AddError(ValidationRules.ADDRESS_COUNTRY_WHITESPACE);
			}
			else if (_country_allowed.Contains(country) == false)
			{
				result.AddError(string.Format(ValidationRules.ADDRESS_COUNTRY_ALLOWED_LIST,
					string.Join(", ", _country_allowed)));
			}

			return result;
		}

		#endregion
	}
}
