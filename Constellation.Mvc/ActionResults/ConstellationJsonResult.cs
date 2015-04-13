﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Constellation.Mvc.ActionResults
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;
	using Newtonsoft.Json.Serialization;
	using System.Web;
	using System.Web.Mvc;

	public class ConstellationJsonResult<T> : ConstellationJsonResult
	{
		public new T Data
		{
			get { return (T)base.Data; }
			set { base.Data = value; }
		}
	}

	public class ConstellationJsonResult : JsonResult
	{
		public IList<string> ErrorMessages { get; private set; }

		public ConstellationJsonResult()
		{
			ErrorMessages = new List<string>();
		}

		public void AddError(string errorMessage)
		{
			ErrorMessages.Add(errorMessage);
		}

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
				string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("GET access is not allowed.  Change the JsonRequestBehavior if you need GET access.");
			}

			var response = context.HttpContext.Response;
			response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

			if (ContentEncoding != null)
			{
				response.ContentEncoding = ContentEncoding;
			}

			SerializeData(response);
		}

		protected virtual void SerializeData(HttpResponseBase response)
		{
			if (ErrorMessages.Any())
			{
				var originalData = Data;
				Data = new
				{
					Success = false,
					OriginalData = originalData,
					ErrorMessage = string.Join("\n", ErrorMessages),
					ErrorMessages = ErrorMessages.ToArray()
				};

				response.StatusCode = 400;
			}

			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				Converters = new JsonConverter[]
				{
					new StringEnumConverter(), 
				},
			};

			response.Write(JsonConvert.SerializeObject(Data, settings));
		}
	}
}
