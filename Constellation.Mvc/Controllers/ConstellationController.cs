namespace Constellation.Mvc.Controllers
{
	using Constellation.Mvc.ActionResults;
	using Microsoft.Web.Mvc;
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Web.Mvc;

	public abstract class ConstellationController : Controller
	{
		protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
			where TController : Controller
		{
			return ControllerExtensions.RedirectToAction(this, action);
		}

		[Obsolete("Do not use the standard Json helpers to return JSON data to the client.  Use either JsonSuccess or JsonError instead.")]
		protected JsonResult Json<T>(T data)
		{
			throw new InvalidOperationException("Do not use the standard Json helpers to return JSON data to the client.  Use either JsonSuccess or JsonError instead.");
		}

		protected ConstellationJsonResult JsonValidationError()
		{
			var result = new ConstellationJsonResult();

			foreach (var validationError in ModelState.Values.SelectMany(v => v.Errors))
			{
				result.AddError(validationError.ErrorMessage);
			}
			return result;
		}

		protected ConstellationJsonResult JsonError(string errorMessage)
		{
			var result = new ConstellationJsonResult();

			result.AddError(errorMessage);

			return result;
		}

		protected ConstellationJsonResult<T> JsonSuccess<T>(T data)
		{
			return new ConstellationJsonResult<T> { Data = data };
		}
	}
}
