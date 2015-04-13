namespace Constellation.Mvc.ActionFilters
{
	using StructureMap;
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	/// <summary>
	/// Replacement for the stock MVC <see cref="FilterAttributeProvider"/> that integrates
	/// StructureMap for property injection on filter attributes. (Very handy)
	/// </summary>
	public class FilterProvider : FilterAttributeFilterProvider
	{
		/// <summary>
		/// The container.
		/// </summary>
		private readonly Func<IContainer> container;

		/// <summary>
		/// Initializes a new instance of the <see cref="FilterProvider"/> class.
		/// </summary>
		/// <param name="container">
		/// The container.
		/// </param>
		public FilterProvider(Func<IContainer> container)
		{
			this.container = container;
		}

		/// <summary>
		/// The get filters.
		/// </summary>
		/// <param name="controllerContext">
		/// The controller context.
		/// </param>
		/// <param name="actionDescriptor">
		/// The action descriptor.
		/// </param>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var filters = base.GetFilters(controllerContext, actionDescriptor);

			var c = container();

			foreach (var filter in filters)
			{
				c.BuildUp(filter.Instance);
				yield return filter;
			}
		}
	}
}
