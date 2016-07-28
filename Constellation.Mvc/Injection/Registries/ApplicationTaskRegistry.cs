namespace Constellation.Mvc.InjectionRegistration
{
	using Constellation.Mvc.ApplicationTasks;

	using StructureMap;
	using StructureMap.Graph;

	/// <summary>
	/// The application task registry.
	/// </summary>
	public class ApplicationTaskRegistry : Registry
	{
		public ApplicationTaskRegistry()
		{
			Scan(scan =>
			{
				scan.AssembliesFromApplicationBaseDirectory();
				scan.ExcludeNamespace("Microsoft");
				scan.ExcludeNamespace("System");
				scan.AddAllTypesOf<IOnApplicationInitTask>();
				scan.AddAllTypesOf<IOnApplicationStartTask>();
				scan.AddAllTypesOf<IOnErrorTask>();
				scan.AddAllTypesOf<IOnRequestStartTask>();
				scan.AddAllTypesOf<IOnRequestEndTask>();
			});
		}
	}
}
