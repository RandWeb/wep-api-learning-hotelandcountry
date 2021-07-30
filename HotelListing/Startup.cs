using AutoMapper;
using HotelListing.Configurations;
using HotelListing.Data;
using HotelListing.Repository;
using HotelListing.Services.Contracts;
using HotelListing.Services.Implementaions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing
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
            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("sql-server"))
            );

            services.AddMemoryCache();

            services.ConfigureRateLimiting();
            services.AddHttpContextAccessor();

            services.ConfigureHttpCacheHeaders();

            services.AddAuthentication();
            services.AddIdentity();
            services.AddJwt(Configuration);
            services.AddMvcCore(config =>
            {
                config.CacheProfiles.Add("120SecondDuration",
                new CacheProfile
                {
                    Duration = 120
                });
            }).AddApiExplorer(

                )
                .AddAuthorization()
                .AddJsonFormatters()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                                              builder.AllowAnyOrigin()
                                                     .AllowAnyMethod()
                                                     .AllowAnyHeader()
                                                );
            });
            services.AddAutoMapper(typeof(MapperIntializer));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthManager, AuthManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HotelListing", Version = "v1" });
            });
            services.ConfigurationVersioning();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));
            app.ConfifurationExceptionHandler();
            app.UseCors("AllowAll");
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseMvc();

        }
    }
}
