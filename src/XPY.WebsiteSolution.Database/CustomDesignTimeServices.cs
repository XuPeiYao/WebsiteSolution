using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Database
{
    public class CustomDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, CustomEntityTypeGenerator>();
            serviceCollection.AddSingleton<ICSharpDbContextGenerator, CustomDbContextGenerator>();
        }
    }
}
