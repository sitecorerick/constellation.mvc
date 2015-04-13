namespace Constellation.Mvc.ActionResults
{
	using Constellation.Mvc.ActionResults.Alerts;
	using System.Web.Mvc;

	/// <summary>
	/// ActionResult extensions that center around Alerts
	/// </summary>
	public static class AlertActionResultExtensions
	{
		/// <summary>
		/// Adds a Success alert to the TempData.
		/// </summary>
		/// <param name="result">
		/// The result.
		/// </param>
		/// <param name="message">
		/// The message.
		/// </param>
		/// <returns>
		/// The <see cref="ActionResult"/>.
		/// </returns>
		public static ActionResult AddSuccessAlert(this ActionResult result, string message)
		{
			return new AlertDecoratedActionResult(result, new SuccessAlert(message));
		}

		/// <summary>
		/// The add info alert.
		/// </summary>
		/// <param name="result">
		/// The result.
		/// </param>
		/// <param name="message">
		/// The message.
		/// </param>
		/// <returns>
		/// The <see cref="ActionResult"/>.
		/// </returns>
		public static ActionResult AddInfoAlert(this ActionResult result, string message)
		{
			return new AlertDecoratedActionResult(result, new InfoAlert(message));
		}

		/// <summary>
		/// The add warning alert.
		/// </summary>
		/// <param name="result">
		/// The result.
		/// </param>
		/// <param name="message">
		/// The message.
		/// </param>
		/// <returns>
		/// The <see cref="ActionResult"/>.
		/// </returns>
		public static ActionResult AddWarningAlert(this ActionResult result, string message)
		{
			return new AlertDecoratedActionResult(result, new WarningAlert(message));
		}

		/// <summary>
		/// The add error alert.
		/// </summary>
		/// <param name="result">
		/// The result.
		/// </param>
		/// <param name="message">
		/// The message.
		/// </param>
		/// <returns>
		/// The <see cref="ActionResult"/>.
		/// </returns>
		public static ActionResult AddErrorAlert(this ActionResult result, string message)
		{
			return new AlertDecoratedActionResult(result, new ErrorAlert(message));
		}
	}
}
