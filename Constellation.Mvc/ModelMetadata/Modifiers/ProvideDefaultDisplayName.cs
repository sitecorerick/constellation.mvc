namespace Constellation.Mvc.ModelMetadata.Modifiers
{
	using System;
	using System.Collections.Generic;
	using System.Text.RegularExpressions;
	using System.Web.Mvc;

	/// <summary>
	/// If the property does not already include a Display Name value, supply a workable
	/// English display name from the name of the Property.
	/// </summary>
	public class ProvideDefaultDisplayName : IModelMetadataModifier
	{
		/// <summary>
		/// The method that executes transformation of Model Metadata.
		/// </summary>
		/// <param name="metadata">
		/// The metadata.
		/// </param>
		/// <param name="attributes">
		/// The attributes.
		/// </param>
		public void Transform(ModelMetadata metadata, IEnumerable<Attribute> attributes)
		{
			if (string.IsNullOrEmpty(metadata.PropertyName))
			{
				return;
			}

			if (!string.IsNullOrEmpty(metadata.DisplayName))
			{
				return;
			}

			metadata.DisplayName = InsertSpacesIntoPascalCasedString(metadata.PropertyName);
		}

		/// <summary>
		/// Converts a Pascal-cased string into a series of words.
		/// </summary>
		/// <param name="propertyName">
		/// The original Property Name.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		private string InsertSpacesIntoPascalCasedString(string propertyName)
		{
			return Regex.Replace(
			   propertyName,
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
