namespace Constellation.Mvc.ActionResults.Alerts
{
	/// <summary>
	/// The success alert.
	/// </summary>
	public class SuccessAlert : Alert
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SuccessAlert"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public SuccessAlert(string message)
			: base(message)
		{
		}
	}
}
