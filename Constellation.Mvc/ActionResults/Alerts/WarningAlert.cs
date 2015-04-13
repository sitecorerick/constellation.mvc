namespace Constellation.Mvc.ActionResults.Alerts
{
	/// <summary>
	/// The success alert.
	/// </summary>
	public class WarningAlert : Alert
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WarningAlert"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public WarningAlert(string message)
			: base(message)
		{
		}
	}
}
