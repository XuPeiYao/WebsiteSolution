using Autofac.Core;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Autofac
{
    public class DependencyPropertySelector : IPropertySelector
    {
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            return propertyInfo.GetCustomAttribute<DependencyAttribute>() != null;
        }
    }
}
