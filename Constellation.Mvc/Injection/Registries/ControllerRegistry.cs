namespace Constellation.Mvc.InjectionRegistration
{

	using Constellation.Mvc.InjectionRegistration.Conventions;

	using StructureMap.Configuration.DSL;
	using StructureMap.Graph;

	/// <summary>
	/// Registers Controllers with StructureMap
	/// </summary>
	public class ControllerRegistry : Registry
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerRegistry"/> class.
		/// </summary>
		public ControllerRegistry()
		{
			Scan(scan =>
			{
				scan.AssembliesFromApplicationBaseDirectory();
				scan.ExcludeNamespace("Microsoft");
				scan.ExcludeNamespace("System");
				scan.With(new ControllerConvention());
			});
		}
	}
}
