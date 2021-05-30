using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace CustomerClassLibrary.Validators
{
	/// <summary>
	/// Extensions for <see cref="IRuleBuilder{T, TProperty}"/>.
	/// </summary>
	public static class RuleBuilderExtensions
	{
		#region Text

		public static IRuleBuilderOptions<T, string> NotWhitespace<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Must(text => TextValidationHelper.IsWhitespace(text) == false);
		}

		public static IRuleBuilderOptions<T, string> NotNullNorEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Must(text => string.IsNullOrEmpty(text) == false);
		}

		public static IRuleBuilderOptions<T, string> PhoneNumberFormatE164<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Matches("^\\+?[1-9]\\d{1,14}$");
		}

		public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Matches(
				@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");
		}

		#endregion

		#region Lists

		public static IRuleBuilderOptions<T, IList<TElement>> NotNullNorEmpty<T, TElement>(
			this IRuleBuilder<T, IList<TElement>> ruleBuilder)
		{
			return ruleBuilder.Must(list => list?.Count > 0);
		}

		public static IRuleBuilderOptions<T, IList<string>> NoAnyNullOrEmptyOrWhitespaceElements<T>(
			this IRuleBuilder<T, IList<string>> ruleBuilder)
		{
			return ruleBuilder.Must(list => list.Any(element => string.IsNullOrWhiteSpace(element)) == false);
		}

		#endregion
	}
}
