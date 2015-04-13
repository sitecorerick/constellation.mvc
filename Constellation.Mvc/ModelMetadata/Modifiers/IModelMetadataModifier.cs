namespace Constellation.Mvc.ModelMetadata.Modifiers
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// The ModelMetadataModifier interface.
	/// </summary>
	public interface IModelMetadataModifier
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
		void Transform(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes);
	}
}
