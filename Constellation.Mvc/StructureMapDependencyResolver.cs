namespace Constellation.Mvc
{
	using StructureMap;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;

	/// <summary>
	/// Replaces ASP.NET MVC 5's default dependency resolver with StructureMap.
	/// </summary>
	public class StructureMapDependencyResolver : IDependencyResolver
	{
		/// <summary>
		/// The base StructureMap factory.
		/// </summary>
		private readonly Func<IContainer> factory;

		/// <summary>
		/// Initializes a new instance of the <see cref="StructureMapDependencyResolver"/> class.
		/// </summary>
		/// <param name="factory">
		/// The factory.
		/// </param>
		public StructureMapDependencyResolver(Func<IContainer> factory)
		{
			this.factory = factory;
		}

		/// <summary>
		/// The get service.
		/// </summary>
		/// <param name="serviceType">
		/// The service type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		public object GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				return null;
			}

			var f = factory();

			return serviceType.IsAbstract || serviceType.IsInterface
					   ? f.TryGetInstance(serviceType)
					   : f.GetInstance(serviceType);
		}

		/// <summary>
		/// The get services.
		/// </summary>
		/// <param name="serviceType">
		/// The service type.
		/// </param>
		/// <returns>
		/// The list of maching services.
		/// </returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return factory().GetAllInstances(serviceType).Cast<object>();
		}
	}
}
