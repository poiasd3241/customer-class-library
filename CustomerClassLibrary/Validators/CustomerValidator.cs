using CustomerClassLibrary.Entities;
using CustomerClassLibrary.Localization;
using FluentValidation;

namespace CustomerClassLibrary.Validators
{
	/// <summary>
	/// The fluent validator of <see cref="Customer"/> objects.
	/// </summary>
	public class CustomerValidator : AbstractValidator<Customer>
	{
		private static readonly int _name_max_length = 50;
		private static readonly string _phoneNumber_format = "E.164";

		public CustomerValidator()
		{
			// Optional
			RuleFor(customer => customer.FirstName).Cascade(CascadeMode.Stop)
				.NotWhitespace().WithMessage(ValidationRules.PERSON_FIRST_NAME_WHITESPACE)
				.MaximumLength(_name_max_length).WithMessage(string.Format(ValidationRules.PERSON_FIRST_NAME_MAX_LENGTH, _name_max_length))
					.When(customer => string.IsNullOrEmpty(customer.FirstName) == false, ApplyConditionTo.CurrentValidator);

			RuleFor(customer => customer.LastName).Cascade(CascadeMode.Stop)
				.NotNullNorEmpty().WithMessage(ValidationRules.PERSON_LAST_NAME_REQUIRED)
				.NotWhitespace().WithMessage(ValidationRules.PERSON_LAST_NAME_WHITESPACE)
				.MaximumLength(_name_max_length).WithMessage(string.Format(ValidationRules.PERSON_LAST_NAME_MAX_LENGTH, _name_max_length));

			RuleFor(customer => customer.Addresses)
				.NotNullNorEmpty().WithMessage(ValidationRules.CUSTOMER_ADDRESSES_COUNT_MIN);

			// Optional
			RuleFor(customer => customer.PhoneNumber).Cascade(CascadeMode.Stop)
				.NotWhitespace().WithMessage(ValidationRules.CUSTOMER_PHONE_NUMBER_WHITESPACE)
				.PhoneNumberFormatE164().WithMessage(string.Format(ValidationRules.CUSTOMER_PHONE_NUMBER_FORMAT, _phoneNumber_format))
					.When(customer => string.IsNullOrEmpty(customer.PhoneNumber) == false, ApplyConditionTo.CurrentValidator);

			// Optional
			RuleFor(customer => customer.Email).Cascade(CascadeMode.Stop)
				.NotWhitespace().WithMessage(ValidationRules.CUSTOMER_EMAIL_WHITESPACE)
				.Email().WithMessage(ValidationRules.CUSTOMER_EMAIL_FORMAT)
					.When(customer => string.IsNullOrEmpty(customer.Email) == false, ApplyConditionTo.CurrentValidator);

			RuleFor(customer => customer.Notes).Cascade(CascadeMode.Stop)
				.NotNullNorEmpty().WithMessage(ValidationRules.CUSTOMER_NOTES_COUNT_MIN)
				.NoAnyNullOrEmptyOrWhitespaceElements().WithMessage(ValidationRules.CUSTOMER_NOTES_TEXT_NULL_EMPTY_OR_WHITESPACE);
		}
	}
}
