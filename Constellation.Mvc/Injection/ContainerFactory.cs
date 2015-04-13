namespace Constellation.Mvc.Injection
{
	using StructureMap;
	using System;
	using System.Threading;

	/// <summary>
	/// The basic entry point for StructureMap
	/// </summary>
	public static class ContainerFactory
	{
		/// <summary>
		/// The container builder.
		/// </summary>
		private static readonly Lazy<Container> ContainerBuilder = new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

		/// <summary>
		/// Gets the default StructureMap <see cref="Container"/>
		/// </summary>
		public static IContainer Container
		{
			get { return ContainerBuilder.Value; }
		}

		/// <summary>
		/// The default container.
		/// </summary>
		/// <returns>
		/// The <see cref="Container"/>.
		/// </returns>
		private static Container DefaultContainer()
		{
			return new Container(x =>
			{
				// default config
			});
		}
	}
}
