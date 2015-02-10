using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using WebApiResourceCentricService.Controllers;
using Microsoft.Framework.Logging.Console;
using System.Diagnostics;
using System.Linq;

namespace WebApiResourceCentricService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                options.ApplicationModelConventions.Add(
                    new AttributeRouteApplicationModel<Values2Controller>("/api/MyValues/{id?}")); // apply attribute route to values2controller (and deriving types).

                options.ApplicationModelConventions.Add(
                    new VerbControllerApplicationConvention<ResourceController>()); // apply verb convention to all controllers deriving from ResourceController.
            });
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IActionDescriptorsCollectionProvider adp)
        {
            SetupLoggingAndLogStartupInfo(loggerFactory, adp);

            app.UseMvc(route => { }); // no conventional routes (this will become the default shortly)
        }

        private void SetupLoggingAndLogStartupInfo(ILoggerFactory loggerFactory, IActionDescriptorsCollectionProvider adp)
        {
            loggerFactory.AddConsole();

            var logger = loggerFactory.Create("Startup");

            // This is kinda nice for attaching a debugger for self hosting scenarios
            logger.WriteInformation("Process Id: " + Process.GetCurrentProcess().Id);

            // The code below will be added to MVC at some point in the future
            foreach (var action in adp.ActionDescriptors.Items)
            {
                var constraint = action?.ActionConstraints?.OfType<HttpMethodConstraint>()?.SingleOrDefault();
                if (constraint != null)
                {
                    logger.WriteInformation(action.DisplayName + ": " + constraint.HttpMethods.Single());
                }
                else
                {
                    logger.WriteInformation(action.DisplayName + ": " + "AnyVerb");
                }
            }
        }
    }
}
