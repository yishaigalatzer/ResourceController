using System;
using System.Reflection;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ApplicationModels;
using Microsoft.AspNet.Mvc.Routing;

namespace WebApiResourceCentricService
{
    public class AttributeRouteApplicationModel : IApplicationModelConvention
    {
        IRouteTemplateProvider Route { get; }
        private readonly Func<TypeInfo, bool> _predicate;

        public AttributeRouteApplicationModel(Type baseType, string routeTemplate) :
            this(t => baseType.GetTypeInfo().IsAssignableFrom(t), routeTemplate)
        {
        }

        public AttributeRouteApplicationModel(Type baseType, IRouteTemplateProvider route) :
            this(t => baseType.GetTypeInfo().IsAssignableFrom(t), route)
        {
        }

        public AttributeRouteApplicationModel(Func<TypeInfo, bool> typePredicate, string routeTemplate)
            :this(typePredicate, new RouteAttribute(routeTemplate))
        {
            Route = new RouteAttribute(routeTemplate);
            _predicate = typePredicate;
        }

        public AttributeRouteApplicationModel(Func<TypeInfo, bool> typePredicate, IRouteTemplateProvider route)
        {
            Route = route;
            _predicate = typePredicate;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                if (_predicate(controller.ControllerType))
                {
                    if (controller.AttributeRoutes.Count == 0)
                    {
                        controller.AttributeRoutes.Add(new AttributeRouteModel(Route));
                    }
                }
            }
        }
    }

    public class AttributeRouteApplicationModel<TControllerBaseType> : AttributeRouteApplicationModel
    {
        public AttributeRouteApplicationModel(string route) : base(typeof(TControllerBaseType), route)
        {
        }
    }
}