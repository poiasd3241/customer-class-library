using System.Linq;

namespace CustomerClassLibrary.Validator
{
	public class TextValidatorBase
	{
		/// <summary>
		/// Returns <see langword="true"/> if the text consists of white-space characters; 
		/// otherwise, <see langword="false"/>.
		/// </summary>
		/// <param name="text">The text to check.</param>
		protected static bool IsWhitespace(string text)
		{
			if (text == null)
			{
				return false;
			}

			return text.Any(c => char.IsWhiteSpace(c) == false) == false;
		}
	}
}
