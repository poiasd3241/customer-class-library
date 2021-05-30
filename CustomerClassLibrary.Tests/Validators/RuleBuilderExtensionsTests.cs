using System.Collections.Generic;
using CustomerClassLibrary.Validators;
using FluentValidation;
using Xunit;

namespace CustomerClassLibrary.Tests.Validators
{
	public class RuleBuilderExtensionsTests
	{
		#region Private Members

		private static readonly TestModelValidator _testModelValidator = new();
		private class TestModelValidator : AbstractValidator<TestModel>
		{
			public TestModelValidator()
			{
				RuleFor(model => model.NotWhitespaceText)
					.NotWhitespace().WithMessage("bad NotWhitespaceText");
				RuleFor(model => model.NotNullNorEmptyText)
					.NotNullNorEmpty().WithMessage("bad NotNullNorEmptyText");
				RuleFor(model => model.PhoneNumberFormatE164Text)
					.PhoneNumberFormatE164().WithMessage("bad PhoneNumberFormatE164Text");
				RuleFor(model => model.EmailText)
					.Email().WithMessage("bad EmailText");
				RuleFor(model => model.NotNullNorEmptyList)
					.NotNullNorEmpty().WithMessage("bad NotNullNorEmptyList");
				RuleFor(model => model.NoAnyNullOrEmptyOrWhitespaceElementsList)
					.NoAnyNullOrEmptyOrWhitespaceElements().WithMessage("bad NoAnyNullOrEmptyOrWhitespaceElementsList");
			}
		}

		private class TestModel
		{
			public string NotWhitespaceText { get; set; }
			public string NotNullNorEmptyText { get; set; }
			public string PhoneNumberFormatE164Text { get; set; }
			public string EmailText { get; set; }
			public List<string> NotNullNorEmptyList { get; set; }
			public List<string> NoAnyNullOrEmptyOrWhitespaceElementsList { get; set; }
		}

		private static TestModel GetValidTestModel()
		{
			return new TestModel()
			{
				NotWhitespaceText = "a",
				NotNullNorEmptyText = "a",
				PhoneNumberFormatE164Text = "+123",
				EmailText = "my@email.com",
				NotNullNorEmptyList = new() { "a" },
				NoAnyNullOrEmptyOrWhitespaceElementsList = new() { "a" }
			};
		}

		#endregion

		#region Test model

		[Fact]
		public void ShouldCreateTestModel()
		{
			TestModel model = new();

			Assert.Null(model.NotWhitespaceText);
			Assert.Null(model.NotNullNorEmptyText);
			Assert.Null(model.PhoneNumberFormatE164Text);
			Assert.Null(model.EmailText);
			Assert.Null(model.NotNullNorEmptyList);
			Assert.Null(model.NoAnyNullOrEmptyOrWhitespaceElementsList);
		}

		[Fact]
		public void ShouldSetTestModelProperties()
		{
			var text = "a";
			var list = new List<string>() { text };

			TestModel model = new();

			model.NotWhitespaceText = text;
			model.NotNullNorEmptyText = text;
			model.PhoneNumberFormatE164Text = text;
			model.EmailText = text;
			model.NotNullNorEmptyList = list;
			model.NoAnyNullOrEmptyOrWhitespaceElementsList = list;

			Assert.Equal(text, model.NotWhitespaceText);
			Assert.Equal(text, model.NotNullNorEmptyText);
			Assert.Equal(text, model.PhoneNumberFormatE164Text);
			Assert.Equal(text, model.EmailText);
			Assert.Equal(list, model.NotNullNorEmptyList);
			Assert.Equal(list, model.NoAnyNullOrEmptyOrWhitespaceElementsList);
		}

		#endregion

		#region RuleBuilder Extensions

		[Fact]
		public void ShouldValidateTestModel()
		{
			// Given
			var validModel = GetValidTestModel();

			// When
			var result = _testModelValidator.Validate(validModel);

			// Then
			Assert.True(result.IsValid);
		}

		[Fact]
		public void ShouldInvalidateTestModelByFormat()
		{
			// Given
			var invalidModel = GetValidTestModel();
			invalidModel.PhoneNumberFormatE164Text = "1-23";
			invalidModel.EmailText = "@email";

			// When
			var errors = _testModelValidator.Validate(invalidModel).Errors;

			// Then
			Assert.Equal(2, errors.Count);
			Assert.Equal("bad PhoneNumberFormatE164Text", errors[0].ErrorMessage);
			Assert.Equal("bad EmailText", errors[1].ErrorMessage);
		}

		[Fact]
		public void ShouldInvalidateTestModelByNull()
		{
			// Given
			var invalidModel = GetValidTestModel();
			invalidModel.NotNullNorEmptyText = null;
			invalidModel.NotNullNorEmptyList = null;
			invalidModel.NoAnyNullOrEmptyOrWhitespaceElementsList = new() { null };

			// When
			var errors = _testModelValidator.Validate(invalidModel).Errors;

			// Then
			Assert.Equal(3, errors.Count);
			Assert.Equal("bad NotNullNorEmptyText", errors[0].ErrorMessage);
			Assert.Equal("bad NotNullNorEmptyList", errors[1].ErrorMessage);
			Assert.Equal("bad NoAnyNullOrEmptyOrWhitespaceElementsList", errors[2].ErrorMessage);
		}

		[Fact]
		public void ShouldInvalidateTestModelByEmpty()
		{
			// Given
			var empty = "";

			var invalidModel = GetValidTestModel();
			invalidModel.NotNullNorEmptyText = empty;
			invalidModel.NotNullNorEmptyList = new();
			invalidModel.NoAnyNullOrEmptyOrWhitespaceElementsList = new() { empty };

			// When
			var errors = _testModelValidator.Validate(invalidModel).Errors;

			// Then
			Assert.Equal(3, errors.Count);
			Assert.Equal("bad NotNullNorEmptyText", errors[0].ErrorMessage);
			Assert.Equal("bad NotNullNorEmptyList", errors[1].ErrorMessage);
			Assert.Equal("bad NoAnyNullOrEmptyOrWhitespaceElementsList", errors[2].ErrorMessage);
		}


		[Fact]
		public void ShouldInvalidateTestModelByWhiteSpace()
		{
			// Given
			var whitespace = " ";
			var invalidModel = GetValidTestModel();
			invalidModel.NotWhitespaceText = whitespace;
			invalidModel.NoAnyNullOrEmptyOrWhitespaceElementsList = new() { whitespace };

			// When
			var errors = _testModelValidator.Validate(invalidModel).Errors;

			// Then
			Assert.Equal(2, errors.Count);
			Assert.Equal("bad NotWhitespaceText", errors[0].ErrorMessage);
			Assert.Equal("bad NoAnyNullOrEmptyOrWhitespaceElementsList", errors[1].ErrorMessage);
		}

		#endregion
	}
}
