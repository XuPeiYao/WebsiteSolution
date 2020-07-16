using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Injectable
{
    /// <summary>
    /// 相依注入屬性項目擴充方法
    /// </summary>
    public static class InjectableDependencyInjectionExtension
    {
        /// <summary>
        /// 加入相依注入屬性支援
        /// </summary>
        /// <param name="serviceCollection">服務容器</param>
        /// <returns>服務容器</returns>
        public static IServiceCollection AddInjectable(this IServiceCollection serviceCollection)
        {
            var types = TypeUtility.GetAllTypes();

            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<InjectableAttribute>();
                if (attr == null) continue;

                serviceCollection.Add(new ServiceDescriptor(type, type, attr.LifeTime));
            }

            return serviceCollection;
        }
    }
}
