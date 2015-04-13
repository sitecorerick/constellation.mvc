namespace Constellation.Mvc.ApplicationTasks
{
	/// <summary>
	/// Flag indicating a task that should be run when the application encounters an error.
	/// </summary>
	public interface IOnErrorTask : IApplicationTask
	{
	}
}
