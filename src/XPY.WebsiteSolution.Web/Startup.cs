using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using AspectCore.Extensions.Autofac;

using Autofac;

using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;

using XPY.WebsiteSolution.Database;
using XPY.WebsiteSolution.Models;
using XPY.WebsiteSolution.Services;
using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Injectable;
using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.OpenApi;
using XPY.WebsiteSolution.Utilities.Token;
using Autofac;
using RestSharp.Extensions;
using XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.Autofac;
using XPY.WebsiteSolution.Web.Controllers;
using Autofac.Extras.DynamicProxy;

namespace XPY.WebsiteSolution.Web
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddOptions();

            services.Configure<FormOptions>(config =>
            {
                config.MultipartBodyLengthLimit = long.MaxValue;
            });

            services.Configure<MinioOptions>(config =>
            {
                config.EndPoint = Configuration["Minio:EndPoint"];
                config.WithSSL = Configuration.GetValue<bool>("Minio:WithSSL");
                config.BucketName = Configuration["Minio:BucketName"];
                config.AccessKey = Configuration["Minio:AccessKey"];
                config.SecretKey = Configuration["Minio:SecretKey"]; 
            });

            services.AddHttpContextAccessor();

            services.AddLogging();

            services.AddDbContext<WebsiteSolutionContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Default"));
            });

            services.AddResponseCompression();

            services.AddResponseCaching();
            
            services.AddJwtHelper<DefaultJwtTokenModel>(
                issuer: Configuration["JWT:Issuer"],
                audience: Configuration["JWT:Audience"],
                secureKey: Configuration["JWT:SecureKey"]);
            services.AddAuthorization();

            services.AddInjectable();

            services.AddMvc()
                .AddControllersAsServices();

            //services.AddHealthChecks();

            services.AddAutoMapper(typeof(SampleUserModel));

            services.AddOpenApi();

            _services = services;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterDynamicProxy();
            foreach (var type in _services) {
                if (type.ServiceType.GetProperties().All(x => x.GetAttribute<DependencyAttribute>() == null))
                {
                    continue;
                }
                builder.RegisterType(type.ServiceType)
                    .PropertiesAutowired(
                        new DependencyPropertySelector(),
                        true
                    ); 
            } 
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
            app.UseAuthorization();
                        
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseStatusCodePagesWithReExecute("/");
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
