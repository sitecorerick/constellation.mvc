namespace Constellation.Mvc.ActionResults.Alerts
{
	/// <summary>
	/// The success alert.
	/// </summary>
	public class ErrorAlert : Alert
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorAlert"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public ErrorAlert(string message)
			: base(message)
		{
		}
	}
}
