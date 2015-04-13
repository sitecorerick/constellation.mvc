namespace Constellation.Mvc.ActionResults.Alerts
{
	/// <summary>
	/// An alert that needs to be displayed to the user.
	/// </summary>
	public abstract class Alert
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Alert"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public Alert(string message)
		{
			Message = message;
		}

		/// <summary>
		/// Gets the message.
		/// </summary>
		public string Message { get; private set; }
	}
}
