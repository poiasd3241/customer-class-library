using CustomerClassLibrary.Validator;
using Xunit;

namespace CustomerClassLibrary.Tests.Validator
{
	public class ValidationResultTests
	{
		[Fact]
		public void ShouldCreateValidationResult()
		{
			ValidationResult result = new();

			Assert.False(result.HasErrors);
			Assert.Null(result.Errors);
		}

		[Fact]
		public void ShouldAddError()
		{
			var error = "oops!";
			ValidationResult result = new();
			result.AddError(error);

			Assert.True(result.HasErrors);
			var actualError = Assert.Single(result.Errors);
			Assert.Equal(error, actualError);
		}
	}
}
