using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
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

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                options.ApplicationModelConventions.Add(
                    new AttributeRouteApplicationModel<Values2Controller>("/api/MyValues/{id?}"));
            });
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IActionDescriptorsCollectionProvider adp)
        {
            loggerFactory.AddConsole();
            var logger = loggerFactory.Create("Startup");
            logger.WriteInformation("Process Id: " + Process.GetCurrentProcess().Id);

            foreach (var action in adp.ActionDescriptors.Items)
            {
                var constraint = action?.ActionConstraints?.OfType<HttpMethodConstraint>()?.SingleOrDefault();
                if (constraint != null)
                {
                    Console.WriteLine(action.DisplayName + ": " + constraint.HttpMethods.Single());
                }
                else
                {
                    Console.WriteLine(action.DisplayName + ": " + "AnyVerb");
                }
            }

            app.UseStaticFiles();
            app.UseMvc(route => { });
        }
    }
}
