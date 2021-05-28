using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CustomerClassLibrary.Entity;
using CustomerClassLibrary.Localization;

namespace CustomerClassLibrary.Validator
{
	public class CustomerValidator
	{
		private const int _name_max_length = 50;
		private const int _addresses_count_min = 1;
		private const int _notes_count_min = 1;
		private const string _phoneNumber_format = "E.164";

		public static ValidationResult Validate(Customer customer)
		{
			ValidationResult result = new();

			ValidateFirstName(customer.FirstName, result);
			ValidateLastName(customer.LastName, result);
			ValidateAddresses(customer.Addresses, result);
			ValidatePhoneNumber(customer.PhoneNumber, result);
			ValidateEmail(customer.Email, result);
			ValidateNotes(customer.Notes, result);

			return result;
		}

		#region Private Methods

		private static ValidationResult ValidateFirstName(string firstName, ValidationResult result)
		{
			if (string.IsNullOrEmpty(firstName))
			{
				// Optional.
				return result;
			}
			else if (IsWhitespace(firstName))
			{
				result.AddError(ValidationRules.PERSON_FIRST_NAME_WHITESPACE);
			}
			else if (firstName.Length > _name_max_length)
			{
				result.AddError(string.Format(ValidationRules.PERSON_FIRST_NAME_MAX_LENGTH, _name_max_length));
			}

			return result;
		}
		private static ValidationResult ValidateLastName(string lastName, ValidationResult result)
		{
			if (string.IsNullOrEmpty(lastName))
			{
				// Required.
				result.AddError(ValidationRules.PERSON_LAST_NAME_REQUIRED);
			}
			else if (IsWhitespace(lastName))
			{
				result.AddError(ValidationRules.PERSON_LAST_NAME_WHITESPACE);
			}
			else if (lastName.Length > _name_max_length)
			{
				result.AddError(string.Format(ValidationRules.PERSON_LAST_NAME_MAX_LENGTH, _name_max_length));
			}

			return result;
		}
		private static ValidationResult ValidateAddresses(List<Address> addresses, ValidationResult result)
		{
			if (addresses == null || addresses.Count < _addresses_count_min)
			{
				result.AddError(string.Format(ValidationRules.CUSTOMER_ADDRESSES_COUNT_MIN, _addresses_count_min));
			}

			return result;
		}
		private static ValidationResult ValidatePhoneNumber(string phoneNumber, ValidationResult result)
		{
			if (string.IsNullOrEmpty(phoneNumber))
			{
				// Optional.
				return result;
			}
			else if (IsWhitespace(phoneNumber))
			{
				result.AddError(ValidationRules.CUSTOMER_PHONE_NUMBER_WHITESPACE);
			}
			else
			{
				var phoneNumberPatternE164 = "^\\+?[1-9]\\d{1,14}$";
				if (Regex.IsMatch(phoneNumber, phoneNumberPatternE164) == false)
				{
					result.AddError(string.Format(ValidationRules.CUSTOMER_PHONE_NUMBER_FORMAT, _phoneNumber_format));
				}
			}

			return result;
		}
		private static ValidationResult ValidateEmail(string email, ValidationResult result)
		{
			if (string.IsNullOrEmpty(email))
			{
				// Optional.
				return result;
			}
			else if (IsWhitespace(email))
			{
				result.AddError(ValidationRules.CUSTOMER_EMAIL_WHITESPACE);
			}
			else
			{
				var emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
				if (Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase) == false)
				{
					result.AddError(ValidationRules.CUSTOMER_EMAIL_FORMAT);
				}
			}

			return result;
		}
		private static ValidationResult ValidateNotes(List<string> notes, ValidationResult result)
		{
			if (notes == null || notes.Count < _notes_count_min)
			{
				result.AddError(string.Format(ValidationRules.CUSTOMER_NOTES_COUNT_MIN, _notes_count_min));
			}
			else if (notes.Any(note => string.IsNullOrWhiteSpace(note)))
			{
				result.AddError(ValidationRules.CUSTOMER_NOTES_TEXT_EMPTY_OR_WHITESPACE);
			}

			return result;
		}

		/// <summary>
		/// Returns <see langword="true"/> if the text consists of white-space characters; 
		/// otherwise, <see langword="false"/>.
		/// </summary>
		/// <param name="text">The text to check.</param>
		private static bool IsWhitespace(string text)
		{
			return text?.Length > 0 && text.Trim().Length == 0;
		}

		#endregion
	}
}
