namespace CustomerClassLibrary.Localization
{
	public static class ValidationRules
	{
		#region Person

		public const string PERSON_FIRST_NAME_WHITESPACE = "First name cannot consist of white-space characters.";
		public const string PERSON_LAST_NAME_WHITESPACE = "Last name cannot consist of white-space characters.";
		public const string PERSON_FIRST_NAME_MAX_LENGTH = "First name: max {0} characters.";
		public const string PERSON_LAST_NAME_MAX_LENGTH = "Last name: max {0} characters.";
		public const string PERSON_LAST_NAME_REQUIRED = "Last name is required.";

		#endregion

		#region Customer

		public const string CUSTOMER_ADDRESSES_COUNT_MIN = "Addresses: at least {0} required.";
		public const string CUSTOMER_PHONE_NUMBER_WHITESPACE = "Phone number cannot contain white-space characters.";
		public const string CUSTOMER_PHONE_NUMBER_FORMAT = "Phone number: must be in {0} format.";
		public const string CUSTOMER_EMAIL_FORMAT = "Invalid email.";
		public const string CUSTOMER_EMAIL_WHITESPACE = "Email cannot contain white-space characters.";
		public const string CUSTOMER_NOTES_COUNT_MIN = "Notes: at least {0} required.";
		public const string CUSTOMER_NOTES_TEXT_EMPTY_OR_WHITESPACE = "Notes cannot be empty or consist of white-space characters.";

		#endregion
	}
}
