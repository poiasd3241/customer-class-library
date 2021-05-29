namespace CustomerClassLibrary.Localization
{
	public static class ValidationRules
	{
		#region Person

		public const string PERSON_FIRST_NAME_WHITESPACE = "First name cannot consist of white-space characters.";
		public const string PERSON_FIRST_NAME_MAX_LENGTH = "First name: max {0} characters.";

		public const string PERSON_LAST_NAME_REQUIRED = "Last name is required.";
		public const string PERSON_LAST_NAME_WHITESPACE = "Last name cannot consist of white-space characters.";
		public const string PERSON_LAST_NAME_MAX_LENGTH = "Last name: max {0} characters.";

		#endregion

		#region Customer

		public const string CUSTOMER_ADDRESSES_COUNT_MIN = "Addresses: at least {0} required.";

		public const string CUSTOMER_PHONE_NUMBER_WHITESPACE = "Phone number cannot contain white-space characters.";
		public const string CUSTOMER_PHONE_NUMBER_FORMAT = "Phone number: must be in {0} format.";

		public const string CUSTOMER_EMAIL_WHITESPACE = "Email cannot contain white-space characters.";
		public const string CUSTOMER_EMAIL_FORMAT = "Invalid email.";

		public const string CUSTOMER_NOTES_COUNT_MIN = "Notes: at least {0} required.";
		public const string CUSTOMER_NOTES_TEXT_EMPTY_OR_WHITESPACE = "Notes cannot be empty or consist of white-space characters.";

		#endregion

		#region Address

		public const string ADDRESS_LINE_REQUIRED = "Address line is required.";
		public const string ADDRESS_LINE_WHITESPACE = "Address line cannot consist of white-space characters.";
		public const string ADDRESS_LINE_MAX_LENGTH = "Address line: max {0} characters.";

		public const string ADDRESS_LINE2_WHITESPACE = "Address line2 cannot consist of white-space characters.";
		public const string ADDRESS_LINE2_MAX_LENGTH = "Address line2: max {0} characters.";

		public const string ADDRESS_CITY_REQUIRED = "City is required.";
		public const string ADDRESS_CITY_WHITESPACE = "City cannot consist of white-space characters.";
		public const string ADDRESS_CITY_MAX_LENGTH = "City: max {0} characters.";

		public const string ADDRESS_POSTAL_CODE_REQUIRED = "Postal code is required.";
		public const string ADDRESS_POSTAL_CODE_WHITESPACE = "Postal code cannot consist of white-space characters.";
		public const string ADDRESS_POSTAL_CODE_MAX_LENGTH = "Postal code: max {0} characters.";

		public const string ADDRESS_STATE_REQUIRED = "State is required.";
		public const string ADDRESS_STATE_WHITESPACE = "State cannot consist of white-space characters.";
		public const string ADDRESS_STATE_MAX_LENGTH = "State: max {0} characters.";

		public const string ADDRESS_COUNTRY_REQUIRED = "Country is required.";
		public const string ADDRESS_COUNTRY_WHITESPACE = "Country cannot consist of white-space characters.";
		public const string ADDRESS_COUNTRY_ALLOWED_LIST = "Country: allowed only {0}.";

		#endregion
	}
}
