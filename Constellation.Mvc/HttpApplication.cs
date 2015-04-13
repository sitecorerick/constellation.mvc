namespace Constellation.Mvc
{
	using Constellation.Mvc.ApplicationTasks;
	using Constellation.Mvc.Injection;
	using Constellation.Mvc.InjectionRegistration;
	using StructureMap;
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Web;
	using System.Web.Mvc;

	/// <summary>
	/// Your Global.asax file should inherit from this class.
	/// </summary>
	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
	public abstract class HttpApplication : System.Web.HttpApplication
	{
		/// <summary>
		/// Gets or sets the StructureMap container for this request.
		/// </summary>
		public IContainer Container
		{
			get
			{
				return (IContainer)HttpContext.Current.Items["_Container"];
			}

			set
			{
				HttpContext.Current.Items["_Container"] = value;
			}
		}

		/// <summary>
		/// The application begin request.
		/// </summary>
		public void Application_BeginRequest()
		{
			Container = ContainerFactory.Container.GetNestedContainer();

			foreach (var task in Container.GetAllInstances<IOnRequestStartTask>())
			{
				task.Execute();
			}
		}

		/// <summary>
		/// The application error.
		/// </summary>
		public void Application_Error()
		{
			foreach (var task in Container.GetAllInstances<IOnErrorTask>())
			{
				task.Execute();
			}
		}

		/// <summary>
		/// The application end request.
		/// </summary>
		public void Application_EndRequest()
		{
			if (Container != null)
			{
				try
				{
					foreach (var task in
						Container.GetAllInstances<IOnRequestEndTask>())
					{
						task.Execute();
					}
				}
				finally
				{
					Container.Dispose();
					Container = null;
				}
			}
		}

		/// <summary>
		/// The application_ start.
		/// </summary>
		[Obsolete("To ensure things get called in the correct order, use Application_Start_RegisterConfigs, Application_Start_InitializeDatabase, Application_Start_Add_StructureMap_Registries")]
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			Application_Start_RegisterConfigs();
			Application_Start_InitializeDatabase();

			DependencyResolver.SetResolver(new StructureMapDependencyResolver(() => Container ?? ContainerFactory.Container));
			Application_Start_Add_StructureMap_Registries();

			using (var container = ContainerFactory.Container.GetNestedContainer())
			{
				foreach (var task in container.GetAllInstances<IOnApplicationInitTask>())
				{
					task.Execute();
				}

				foreach (var task in container.GetAllInstances<IOnApplicationStartTask>())
				{
					task.Execute();
				}
			}
		}

		/// <summary>
		/// Use this method to register the contents of your App_Start folder.
		/// </summary>
		// ReSharper disable InconsistentNaming
		protected virtual void Application_Start_RegisterConfigs()
		{
			// nothing to do in this assembly.
		}

		/// <summary>
		/// Use this method to register your ORM database solution.
		/// </summary>
		protected virtual void Application_Start_InitializeDatabase()
		{
			// nothing to do in this assembly.
		}

		/// <summary>
		/// Use this method to add registries to the StructureMap container. Note you may want to call the base class method
		/// before adding your own registries.
		/// </summary>
		protected virtual void Application_Start_Add_StructureMap_Registries()
		{
			ContainerFactory.Container.Configure(cfg =>
			{
				cfg.AddRegistry(new DefaultRegistry());
				cfg.AddRegistry(new ControllerRegistry());
				cfg.AddRegistry(new ActionFilterRegistry(() => Container ?? ContainerFactory.Container));
				cfg.AddRegistry(new MvcRegistry());
				cfg.AddRegistry(new ApplicationTaskRegistry());
				cfg.AddRegistry(new ModelMetadataModifierRegistry());
			});
		}
		// ReSharper restore InconsistentNaming
	}
}
