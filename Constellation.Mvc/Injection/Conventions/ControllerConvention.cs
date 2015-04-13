using System;

namespace Constellation.Mvc.InjectionRegistration.Conventions
{
	using StructureMap.Configuration.DSL;
	using StructureMap.Graph;
	using StructureMap.Pipeline;
	using StructureMap.TypeRules;
	using System.Web.Mvc;

	/// <summary>
	/// Identifies controllers for registration with StructureMap.
	/// </summary>
	public class ControllerConvention : IRegistrationConvention
	{
		/// <summary>
		/// The process.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <param name="registry">
		/// The registry.
		/// </param>
		public void Process(Type type, Registry registry)
		{
			if (type.CanBeCastTo(typeof(Controller)) && !type.IsAbstract)
			{
				registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
			}
		}
	}
}
