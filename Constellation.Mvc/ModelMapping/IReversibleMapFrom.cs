namespace Constellation.Mvc.ModelMapping
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Flag contract that tells Auto Mapper about the source class for a mappable object.
	/// </summary>
	/// <typeparam name="TSourceType">The source type
	/// </typeparam>
	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
	public interface IReversibleMapFrom<TSourceType> : IMappable
	{
	}
}
