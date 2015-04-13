namespace Constellation.Mvc.ActionResults.Alerts
{
	/// <summary>
	/// The success alert.
	/// </summary>
	public class InfoAlert : Alert
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InfoAlert"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public InfoAlert(string message)
			: base(message)
		{
		}
	}
}
