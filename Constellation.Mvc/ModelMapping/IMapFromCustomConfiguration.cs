namespace Constellation.Mvc.ModelMapping
{
	using AutoMapper;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Flag contract that allows AutoMapper to map using a provided configuration set.
	/// </summary>
	public interface IMapFromCustomConfiguration : IMappable
	{
		/// <summary>
		/// Creates the AutoMapper mapping rules for the Object from the provided configuration.
		/// </summary>
		/// <param name="config">
		/// The Automapper configuration.
		/// </param>
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
		void CreateMapping(IConfiguration config);
	}
}
