namespace Constellation.Mvc.InjectionRegistration
{
	using Constellation.Mvc.ActionFilters;
	using StructureMap;
	using StructureMap.TypeRules;
	using System;
	using System.Web.Mvc;

	/// <summary>
	/// The action filter registry.
	/// </summary>
	public class ActionFilterRegistry : Registry
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionFilterRegistry"/> class.
		/// </summary>
		/// <param name="containerFactory">
		/// The container factory.
		/// </param>
		public ActionFilterRegistry(Func<IContainer> containerFactory)
		{
			For<IFilterProvider>().Use(new FilterProvider(containerFactory));

			var policy = new PoliciesExpression(this);

			policy.SetAllProperties(x =>
				x.Matching(p =>
					p.DeclaringType.CanBeCastTo(typeof(ActionFilterAttribute)) &&
					!p.PropertyType.IsPrimitive &&
					p.PropertyType != typeof(string)));
		}
	}
}
