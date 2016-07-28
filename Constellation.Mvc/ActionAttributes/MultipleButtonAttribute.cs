namespace Constellation.Mvc.ActionAttributes
{
	using System;
	using System.Reflection;
	using System.Web.Mvc;

	/// <summary>
	/// Provides a way to specify which action will be used to handle each button on a multi-submit button form.
	/// </summary>
	/// <remarks>
	/// Based on the work o Maarten Balliaw: http://blog.maartenballiauw.be/post/2009/11/26/Supporting-multiple-submit-buttons-on-an-ASPNET-MVC-view.aspx
	/// And referenced here: http://stackoverflow.com/questions/442704/how-do-you-handle-multiple-submit-buttons-in-asp-net-mvc-framework
	/// 
	/// Your Razor needs to look like this:
	/// 
	/// <code>
	/// &lt;form action="" method="post"&gt;
	/// &lt;input type="submit" value="Save" name="action:Save" /&gt;
	/// &lt;input type="submit" value="Cancel" name="action:Cancel" /&gt;
	/// &lt;/form&gt;
	/// </code>
	/// 
	/// Annotate your Controller Actions like this:
	/// 
	/// <code>
	/// [HttpPost]
	/// [MultipleButton(Name = "action", Argument = "Save")]
	/// public ActionResult Save(MessageModel mm) { ... }
	/// 
	/// [HttpPost]
	/// [MultipleButton(Name = "action", Argument = "Cancel")]
	/// public ActionResult Cancel(MessageModel mm) { ... }
	/// </code>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class MultipleButtonAttribute : ActionNameSelectorAttribute
	{
		public string Name { get; set; }
		public string Argument { get; set; }

		public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
		{
			var isValidName = false;
			var keyValue = $"{Name}:{Argument}";
			var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

			if (value != null)
			{
				controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
				isValidName = true;
			}

			return isValidName;
		}
	}
}
