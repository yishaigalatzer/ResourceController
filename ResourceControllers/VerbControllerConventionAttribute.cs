using System;
using Microsoft.AspNet.Mvc.ApplicationModels;

namespace WebApiResourceCentricService
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class VerbControllerConventionAttribute : Attribute, IControllerModelConvention
    {
        private static readonly string[] SupportedHttpMethodConventions = new string[]
        {
            "GET",
            "PUT",
            "POST",
            "DELETE",
            "PATCH",
            "HEAD",
            "OPTIONS",
        };

        public void Apply(ControllerModel controller)
        {
            var controllerName = controller.ControllerName;

            foreach (var action in controller.Actions)
            {
                var actionName = action.ActionName;

                Apply(action);
            }
        }

        private void Apply(ActionModel action)
        {
            // If the HttpMethods are set from attributes, don't override it with the convention
            if (action.HttpMethods.Count > 0)
            {
                return;
            }

            // The Method name is used to infer verb constraints. Changing the action name has not impact.
            foreach (var verb in SupportedHttpMethodConventions)
            {
                if (action.ActionMethod.Name.StartsWith(verb, StringComparison.OrdinalIgnoreCase))
                {
                    action.HttpMethods.Add(verb);
                    return;
                }
            }

            // If no convention matches, then assume POST
            action.HttpMethods.Add("POST");
        }
    }
}