namespace Constellation.Mvc
{
	using Constellation.Mvc.ActionResults.Alerts;
	using System.Collections.Generic;
	using System.Web.Mvc;

	/// <summary>
	/// The temp data dictionary extensions.
	/// </summary>
	public static class TempDataDictionaryExtensions
	{
		/// <summary>
		/// The alerts.
		/// </summary>
		private const string Alerts = "alerts";

		/// <summary>
		/// Gets the alerts from the view's Temp Data.
		/// </summary>
		/// <param name="tempData">
		/// The temp data.
		/// </param>
		/// <returns>
		/// The <see cref="List"/>.
		/// </returns>
		public static List<Alert> GetAlerts(this TempDataDictionary tempData)
		{
			if (!tempData.ContainsKey(Alerts))
			{
				tempData[Alerts] = new List<Alert>();
			}

			return (List<Alert>)tempData[Alerts];
		}
	}
}
