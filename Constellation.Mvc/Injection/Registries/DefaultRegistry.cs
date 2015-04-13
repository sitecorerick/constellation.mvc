namespace Constellation.Mvc.InjectionRegistration
{
	using StructureMap.Configuration.DSL;
	using StructureMap.Graph;

	/// <summary>
	/// Registers objects in the calling assembly to the Registry
	/// </summary>
	public class DefaultRegistry : Registry
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultRegistry"/> class.
		/// </summary>
		public DefaultRegistry()
		{
			Scan(scan =>
			{
				scan.AssembliesFromApplicationBaseDirectory();
				scan.WithDefaultConventions();
			});
		}
	}
}
