using System.Linq;

namespace CustomerClassLibrary.Validator
{
	public class TextValidationHelper
	{
		/// <summary>
		/// Returns <see langword="true"/> if the text consists of whitespace characters; 
		/// otherwise, <see langword="false"/>.
		/// <br/>
		/// Empty string ("") is not considered a whitespace.
		/// </summary>
		/// <param name="text">The text to check.</param>
		public static bool IsWhitespace(string text)
		{
			if (text == null || text == "")
			{
				return false;
			}

			return text.Any(c => char.IsWhiteSpace(c) == false) == false;
		}
	}
}
