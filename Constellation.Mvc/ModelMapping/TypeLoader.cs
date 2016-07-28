namespace Constellation.Mvc.ModelMapping
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// Utility class to access all loadable types for use by Automapper initialization.
	/// </summary>
	public static class TypeLoader
	{
		private static List<Type> types;

		public static IEnumerable<Type> GetTypes()
		{
			if (types != null)
			{
				return types;
			}

			types = new List<Type>();

			var assemblies = AppDomain.CurrentDomain.GetAssemblies();

			foreach (var assembly in assemblies)
			{
				if (assembly.FullName.StartsWith("System", StringComparison.InvariantCultureIgnoreCase))
				{
					continue;
				}

				if (assembly.FullName.StartsWith("Microsoft", StringComparison.InvariantCultureIgnoreCase))
				{
					continue;
				}

				types.AddRange(GetLoadableTypes(assembly));
			}

			return types;
		}

		/// <summary>
		/// Gets types that can actually be loaded by reflection. Handles the case where a
		/// type prerequisite isn't in the currently running application.
		/// </summary>
		/// <remarks>
		/// See http://haacked.com/archive/2012/07/23/get-all-types-in-an-assembly.aspx for details.
		/// </remarks>
		/// <param name="assembly">The assembly to inspect.</param>
		/// <returns>A list of Types that can be loaded.</returns>
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Phil Haack is his name.")]
		private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}

			try
			{
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				return e.Types.Where(t => t != null);
			}
		}
	}
}
