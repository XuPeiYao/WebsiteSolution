using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPY.WebsiteSolution.Utilities.Token
{
    public static class JwtHelperDependencyInjectionExtension
    {
        /// <summary>
        /// 加入JwtHelper
        /// </summary>
        /// <param name="serviceCollection">服務容器</param>
        /// <returns>服務容器</returns>
        public static IServiceCollection AddJwtHelper<TJwtTokenModel>(
            this IServiceCollection serviceCollection,
            Action<JwtOption> configureOptions = null
            )
        {
            if (configureOptions != null)
            { 
                serviceCollection.Configure<JwtOption>(configureOptions);
                serviceCollection.AddSingleton<JwtHelper<JwtOption>>();

                return serviceCollection;
            }

            serviceCollection.AddSingleton<JwtHelper<TJwtTokenModel>>( sp =>
            { 
                return new JwtHelper<TJwtTokenModel>(Options.Create<JwtOption>(new JwtOption()
                {
                    Issuer = sp.GetService<IConfiguration>()["JWT:Issuer"],
                    Audience = sp.GetService<IConfiguration>()["JWT:Audience"],
                    SecureKey = sp.GetService<IConfiguration>()["JWT:SecureKey"],
                }));
            });

            return serviceCollection;
        }
    }
}
