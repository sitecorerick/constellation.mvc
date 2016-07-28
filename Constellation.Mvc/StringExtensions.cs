namespace Constellation.Mvc
{
	using System.Text.RegularExpressions;


	public static class StringExtensions
	{
		public static string ConvertPascalToTitle(this string input)
		{
			return Regex.Replace(
					   input,
					   "(?<!^)" +
					   "(" +
					   "  [A-Z][a-z] |" +
					   "  (?<=[a-z])[A-Z] |" +
					   "  (?<![A-Z])[A-Z]$" +
					   ")",
					   " $1",
					   RegexOptions.IgnorePatternWhitespace);
		}
	}
}
