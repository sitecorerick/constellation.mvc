namespace Constellation.Mvc.ApplicationTasks
{
	/// <summary>
	/// Flag indicating a task should be run at the end of each HTTP request.
	/// </summary>
	public interface IOnRequestEndTask : IApplicationTask
	{
	}
}
