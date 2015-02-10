using System;
using System.Reflection;
using System.Linq;
using Microsoft.AspNet.Mvc.ApplicationModels;

namespace WebApiResourceCentricService
{
    public class VerbControllerApplicationConvention : IApplicationModelConvention
    {
        private readonly Func<TypeInfo, bool> _predicate;

        public VerbControllerApplicationConvention(Type baseType)
            : this(t => baseType.GetTypeInfo().IsAssignableFrom(t))
        {
        }

        public VerbControllerApplicationConvention(Func<TypeInfo, bool> typePredicate)
        {
            _predicate = typePredicate;
        }

        public void Apply(ApplicationModel application)
        {
            var convention = new VerbControllerConventionAttribute();

            foreach (var controller in application.Controllers.Where(c => _predicate(c.ControllerType)))
            {
                convention.Apply(controller);
            }
        }
    }

    public class VerbControllerApplicationConvention<TControllerBaseType> : VerbControllerApplicationConvention
    {
        public VerbControllerApplicationConvention() : base(typeof(TControllerBaseType))
        {
        }
    }
}