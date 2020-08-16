using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Autofac
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DependencyAttribute: Attribute
    {
    }
}
