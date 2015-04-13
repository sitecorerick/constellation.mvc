namespace Constellation.Mvc.InjectionRegistration
{
	using Constellation.Mvc.ModelMetadata;
	using Constellation.Mvc.ModelMetadata.Modifiers;

	using StructureMap.Configuration.DSL;
	using StructureMap.Graph;

	/// <summary>
	/// Supplies StructureMap with the registration of <see cref="IModelMetadataModifier"/> implementations.
	/// </summary>
	public class ModelMetadataModifierRegistry : Registry
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ModelMetadataModifierRegistry"/> class.
		/// </summary>
		public ModelMetadataModifierRegistry()
		{
			For<System.Web.Mvc.ModelMetadataProvider>().Use<ModelMetadataProvider>();

			Scan(scan =>
			{
				scan.AssembliesFromApplicationBaseDirectory();
				scan.AddAllTypesOf<IModelMetadataModifier>();
			});
		}
	}
}
