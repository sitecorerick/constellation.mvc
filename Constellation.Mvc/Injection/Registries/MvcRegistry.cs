namespace Constellation.Mvc.InjectionRegistration
{
	using StructureMap;
	using System.Security.Principal;
	using System.Web;
	using System.Web.Routing;

	/// <summary>
	/// Registers a number of core MVC structures with StructureMap
	/// </summary>
	public class MvcRegistry : Registry
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MvcRegistry"/> class.
		/// </summary>
		public MvcRegistry()
		{
			For<RouteCollection>().Use(RouteTable.Routes);
			For<IIdentity>().Use(() => HttpContext.Current.User.Identity);
			For<HttpSessionStateBase>().Use(() => new HttpSessionStateWrapper(HttpContext.Current.Session));
			For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
			For<HttpServerUtilityBase>().Use(() => new HttpServerUtilityWrapper(HttpContext.Current.Server));
		}
	}
}
