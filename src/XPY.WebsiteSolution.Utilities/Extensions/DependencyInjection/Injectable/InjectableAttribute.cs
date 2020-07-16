using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Injectable
{
    /// <summary>
    /// 相依注入項目
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class InjectableAttribute : Attribute
    {
        /// <summary>
        /// 生命週期
        /// </summary>
        public ServiceLifetime LifeTime { get; private set; }

        /// <summary>
        /// 初始化 <see cref="InjectableAttribute"/>
        /// </summary>
        /// <param name="lifetime">生命週期</param>
        public InjectableAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            LifeTime = lifetime;
        }
    }
}
