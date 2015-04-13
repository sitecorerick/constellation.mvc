namespace Constellation.Mvc
{
	using Constellation.Mvc.Injection;

	using StructureMap;
	using System.Web;

	/// <summary>
	/// Adds support for a StructureMap container-per-request pattern.
	/// </summary>
	public static class HttpContextExtensionForStructureMapContainer
	{
		/// <summary>
		/// The get container.
		/// </summary>
		/// <param name="context">
		/// The context.
		/// </param>
		/// <returns>
		/// The <see cref="IContainer"/>.
		/// </returns>
		public static IContainer GetContainer(this HttpContextBase context)
		{
			return (IContainer)HttpContext.Current.Items["structuremap_container"] ?? ContainerFactory.Container;
		}
	}
}
