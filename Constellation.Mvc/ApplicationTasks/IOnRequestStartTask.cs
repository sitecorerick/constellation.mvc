namespace Constellation.Mvc.ApplicationTasks
{
	/// <summary>
	/// Flag indicating a task that should be run at the start of an HTTP request.
	/// </summary>
	public interface IOnRequestStartTask : IApplicationTask
	{
	}
}
