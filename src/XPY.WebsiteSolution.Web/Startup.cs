using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using LinqToDB;
using LinqToDB.DataProvider.SQLite;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using XPY.ToolKit.Utilities.Common;
using XPY.WebsiteSolution.Database;
using XPY.WebsiteSolution.Utilities.Token;

namespace XPY.WebsiteSolution.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            LinqToDB.DataProvider.SQLite.SQLiteTools.CreateDatabase("Sample");

            services.AddScoped(sp =>
            {
                return new SampleContext(
                    new LinqToDB.DataProvider.SQLite.SQLiteDataProvider(ProviderName.SQLiteClassic),
                    sp.GetRequiredService<IConfiguration>().GetConnectionString("Default"));
            });

            services.AddResponseCompression();

            services.AddResponseCaching();

            services.AddJwtHelper<DefaultJwtTokenModel>();

            services.AddMvc()
                .AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseResponseCompression();
            app.UseResponseCaching(); 
            
            app.UseResponseBuffering();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStatusCodePagesWithReExecute("/");
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
