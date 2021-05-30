using CustomerClassLibrary.Validators;
using Xunit;

namespace CustomerClassLibrary.Tests.Validators
{
	public class TextValidationHelperTests
	{
		[Fact]
		public void ShouldConfirmWhitespaceText()
		{
			Assert.True(TextValidationHelper.IsWhitespace(" "));
		}

		[Fact]
		public void ShouldConfirmNotWhitespaceText()
		{
			Assert.False(TextValidationHelper.IsWhitespace(null));
			Assert.False(TextValidationHelper.IsWhitespace(""));
			Assert.False(TextValidationHelper.IsWhitespace("what"));
			Assert.False(TextValidationHelper.IsWhitespace(" ever"));
		}
	}
}
