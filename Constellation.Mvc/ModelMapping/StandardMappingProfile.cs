namespace Constellation.Mvc.ModelMapping
{
	using AutoMapper;
	using System.Linq;


	public class StandardMappingProfile : Profile
	{
		/// <summary>
		/// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
		///             Avoid calling the <see cref="T:AutoMapper.Mapper"/> class from this method.
		/// </summary>
		protected override void Configure()
		{
			var types = TypeLoader.GetTypes();

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
				CreateMap(map.Source, map.Destination);
			}
		}
	}
}
