namespace Constellation.Mvc.ModelMetadata.Modifiers
{
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	/// <summary>
	/// If a property is marked Read Only and the Data Type is not specified,
	/// specify a data type of Read Only.
	/// </summary>
	public class ProvideDefaultDataTypeForReadOnlyProperties : IModelMetadataModifier
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
			if (metadata.IsReadOnly && string.IsNullOrEmpty(metadata.DataTypeName))
			{
				metadata.DataTypeName = "ReadOnly";
			}
		}
	}
}
