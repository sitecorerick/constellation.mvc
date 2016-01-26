using System;
using System.Collections.Generic;
using System.Linq;

namespace Constellation.Mvc.ModelMapping
{
	using AutoMapper;
	using Constellation.Mvc.ApplicationTasks;
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection;

	public class InitializeAutoMapperTask : IOnApplicationInitTask
	{
		public void Execute()
		{
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

				var types = GetLoadableTypes(assembly);

				LoadStandardMappings(types);

				LoadReversibleMappings(types);

				LoadCustomMappings(types);
			}
		}

		private static void LoadCustomMappings(IEnumerable<Type> types)
		{
			var maps = (from t in types
						from i in t.GetInterfaces()
						where typeof(IMapFromCustomConfiguration).IsAssignableFrom(t) &&
							  !t.IsAbstract &&
							  !t.IsInterface
						select (IMapFromCustomConfiguration)Activator.CreateInstance(t)).ToArray();

			foreach (var map in maps)
			{
				map.CreateMapping(Mapper.Configuration);
			}
		}

		private static void LoadStandardMappings(IEnumerable<Type> types)
		{
			var maps = (from t in types
						from i in t.GetInterfaces()
						where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
							  !t.IsAbstract &&
							  !t.IsInterface
						select new
						{
							Source = i.GetGenericArguments()[0],
							Destination = t
						}).ToArray();

			foreach (var map in maps)
			{
				Mapper.CreateMap(map.Source, map.Destination);
			}
		}

		private static void LoadReversibleMappings(IEnumerable<Type> types)
		{
			var maps = (from t in types
						from i in t.GetInterfaces()
						where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IReversibleMapFrom<>) &&
							  !t.IsAbstract &&
							  !t.IsInterface
						select new
						{
							Source = i.GetGenericArguments()[0],
							Destination = t
						}).ToArray();

			foreach (var map in maps)
			{
				Mapper.CreateMap(map.Source, map.Destination);
				Mapper.CreateMap(map.Destination, map.Source);
			}
		}

		#region Helpers
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
		#endregion
	}
}
