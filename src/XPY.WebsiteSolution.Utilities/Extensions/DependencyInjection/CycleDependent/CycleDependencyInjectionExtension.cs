using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.CycleDependent
{
    /// <summary>
    /// 循環相依注入擴充方法
    /// </summary>
    public static class CycleDependencyInjectionExtension
    {
        /// <summary>
        /// 加入循環相依注入功能
        /// </summary>
        /// <param name="serviceCollection">服務容器</param>
        /// <returns>服務容器</returns>
        public static IServiceCollection AddCycleDI(this IServiceCollection serviceCollection)
        {
            foreach (var sd in serviceCollection.ToList())
            {
                var cycleType = typeof(Cycle<>).MakeGenericType(sd.ServiceType);

                Func<IServiceProvider, object> factory = (sp) => {
                    return cycleType
                        .GetConstructor(new Type[] { typeof(IServiceProvider) })
                        .Invoke(new object[] { sp });
                };

                var cycleSD = new ServiceDescriptor(cycleType, factory, sd.Lifetime);

                serviceCollection.Add(cycleSD);
            }

            return serviceCollection;
        }
    }
}
