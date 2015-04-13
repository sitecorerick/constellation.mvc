namespace Constellation.Mvc.ModelMetadata
{
	using Constellation.Mvc.ModelMetadata.Modifiers;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	/// <summary>
	/// Wraps the stock <see cref="DataAnnotationsModelMetadataProvider"/> with the ability to load
	/// modifier classes at runtime to further decorate the Metadata.
	/// </summary>
	public class ModelMetadataProvider : DataAnnotationsModelMetadataProvider
	{
		/// <summary>
		/// The modifiers.
		/// </summary>
		private readonly IModelMetadataModifier[] modifiers;

		/// <summary>
		/// Initializes a new instance of the <see cref="ModelMetadataProvider"/> class.
		/// </summary>
		/// <param name="metadataFilters">
		/// The metadata filters.
		/// </param>
		public ModelMetadataProvider(IModelMetadataModifier[] metadataFilters)
		{
			modifiers = metadataFilters;
		}

		/// <summary>
		/// The create metadata.
		/// </summary>
		/// <param name="attributes">
		/// The attributes.
		/// </param>
		/// <param name="containerType">
		/// The container type.
		/// </param>
		/// <param name="modelAccessor">
		/// The model accessor.
		/// </param>
		/// <param name="modelType">
		/// The model type.
		/// </param>
		/// <param name="propertyName">
		/// The property name.
		/// </param>
		/// <returns>
		/// The <see cref="ModelMetadata"/>.
		/// </returns>
		protected override System.Web.Mvc.ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			// ReSharper disable PossibleMultipleEnumeration
			var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

			foreach (var modifier in modifiers)
			{
				modifier.Transform(metadata, attributes);
			}
			// ReSharper restore PossibleMultipleEnumeration

			return metadata;
		}
	}
}
