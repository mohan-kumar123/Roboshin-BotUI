
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using RoboschienWeb.Models.Entities;
using RoboschienWeb.Models.Entities.UI;
using RoboschienWeb.Security;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace RoboschienWebFF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ApiSettings config = new ApiSettings();
            Configuration.Bind("ApiSettings", config);

            services.Configure<ApiSettings>(options => Configuration.GetSection("ApiSettings").Bind(options));
            


            services.AddCors();
            services.AddMvc()
                .AddJsonOptions(opt => opt.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddDNTCaptcha(options =>
            //    // options.UseSessionStorageProvider()
            //    options.UseMemoryCacheStorageProvider()
            //    );

            services.AddSession();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Roboschien API", Version = "v1" });
            });

            services.AddDbContext<SickLeaveContext>(options =>
            {
                options.UseSqlServer(config.ConnectionStrings.SickLeaveConnection);
                
            });

            services.Configure<FormOptions>(options =>
            {
                options.MemoryBufferThreshold = Int32.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                app.UseExceptionHandler("/Error/500");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            /// Denying the X-Frame
            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("X-Frame-Options", "DENY");
            //    await next();
            //});

            app.UseMiddleware(typeof(CustomResponseHeaderMiddleware));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });


            app.MapWhen(r => !(r.Request.Path.Value.StartsWith("/swagger") || r.Request.Path.Value.StartsWith("/DNTCaptchaApi")), builder =>
            {
                builder.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501
                    spa.Options.StartupTimeout = new System.TimeSpan(0, 15, 0);
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            });

            app.UseReferrerPolicy(options => options.NoReferrer());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Roboschien API V1");
            });

           
        }
    }
}
