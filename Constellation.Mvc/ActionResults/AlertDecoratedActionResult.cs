namespace Constellation.Mvc.ActionResults
{
	using Constellation.Mvc.ActionResults.Alerts;
	using System.Web.Mvc;

	/// <summary>
	/// The alert decorated action result.
	/// </summary>
	public class AlertDecoratedActionResult : ActionResult
	{
		#region Fields
		/// <summary>
		/// The alert.
		/// </summary>
		private readonly Alert alert;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AlertDecoratedActionResult"/> class.
		/// </summary>
		/// <param name="innerResult">
		/// The inner result.
		/// </param>
		/// <param name="alert">
		/// The alert.
		/// </param>
		public AlertDecoratedActionResult(ActionResult innerResult, Alert alert)
		{
			InnerResult = innerResult;
			this.alert = alert;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the inner result.
		/// </summary>
		public ActionResult InnerResult { get; set; }
		#endregion

		#region Methods
		/// <summary>
		/// The execute result.
		/// </summary>
		/// <param name="context">
		/// The context.
		/// </param>
		public override void ExecuteResult(ControllerContext context)
		{
			var alerts = context.Controller.TempData.GetAlerts();
			alerts.Add(alert);
			InnerResult.ExecuteResult(context);
		}
		#endregion
	}
}
