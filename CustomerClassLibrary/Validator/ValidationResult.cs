using System.Collections.Generic;

namespace CustomerClassLibrary.Validator
{
	public class ValidationResult
	{
		public bool HasErrors => Errors?.Count > 0;
		public List<string> Errors { get; private set; } = null;

		public void AddError(string error)
		{
			if (Errors == null)
			{
				Errors = new List<string>();
			}

			Errors.Add(error);
		}
	}
}
