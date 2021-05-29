using CustomerClassLibrary.Validator;
using Xunit;

namespace CustomerClassLibrary.Tests.Validator
{
	public class TextValidatorBaseTests : TextValidatorBase
	{
		[Fact]
		public void ShouldConfirmWhitespaceText()
		{
			Assert.True(IsWhitespace(" "));
		}

		[Fact]
		public void ShouldConfirmNotWhitespaceText()
		{
			Assert.False(IsWhitespace(null));
			Assert.False(IsWhitespace("what"));
			Assert.False(IsWhitespace(" ever"));
		}
	}
}
