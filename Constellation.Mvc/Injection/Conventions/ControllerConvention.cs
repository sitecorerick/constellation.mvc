using System;

namespace Constellation.Mvc.InjectionRegistration.Conventions
{
	using StructureMap;
	using StructureMap.Graph;
	using StructureMap.Graph.Scanning;
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

		public void ScanTypes(TypeSet types, Registry registry)
		{
			var concretes = types.FindTypes(TypeClassification.Concretes | TypeClassification.Closed);

			foreach (var type in concretes)
			{
				Process(type, registry);
			}

		}
	}
}
