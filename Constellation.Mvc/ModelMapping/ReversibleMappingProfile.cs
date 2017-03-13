namespace Constellation.Mvc.ModelMapping
{
	using AutoMapper;
	using System.Linq;

	public class ReversibleMappingProfile : Profile
	{
		/// <summary>
		/// Override this constructor in a derived class and call the CreateMap method to associate that map with this profile.
		///             Avoid calling the <see cref="T:AutoMapper.Mapper"/> class from this method.
		/// </summary>
		public ReversibleMappingProfile()
		{
			var types = TypeLoader.GetTypes();
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
				CreateMap(map.Source, map.Destination);
				CreateMap(map.Destination, map.Source);
			}
		}
	}
}
