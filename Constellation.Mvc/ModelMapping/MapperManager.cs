namespace Constellation.Mvc.ModelMapping
{
	using AutoMapper;
	using System;
	using System.Linq;

	/// <summary>
	/// Scans the Application Assemblies for Profiles and loads them into the configuration.
	/// </summary>
	public static class MapperManager
	{
		private static MapperConfiguration configuration;

		public static MapperConfiguration Configuration
		{
			get
			{
				if (configuration == null)
				{
					configuration = Initialize();
				}

				return configuration;
			}
		}

		public static IMapper Mapper
		{
			get
			{
				return Configuration.CreateMapper();
			}
		}

		/// <summary>
		/// Creates a Configuration instance, scans the application assemblies for Profile classes and
		/// loads them into the Configuration instance.
		/// </summary>
		/// <returns>The Configuration instance to assign to a static variable.</returns>
		public static MapperConfiguration Initialize()
		{
			var config = new MapperConfiguration(
				c =>
				{
					c.AddMemberConfiguration().AddMember<NameSplitMember>();
					var types = TypeLoader.GetTypes();
					var profiles = types.Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface).ToArray();

					foreach (var type in profiles)
					{
						if (type.GetConstructor(Type.EmptyTypes) == null)
						{
							continue;
						}

						var profile = Activator.CreateInstance(type) as Profile;

						if (profile != null)
						{
							c.AddProfile(profile);
						}
					}
				});

			return config;
		}
	}
}
