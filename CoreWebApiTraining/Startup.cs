using AutoMapper;
using CoreWebApiTraining.BackgroundServices;
using CoreWebApiTraining.AppSettings;
using CoreWebApiTraining.Core.Caching;
using CoreWebApiTraining.Core.Caching.impl;
using CoreWebApiTraining.Data.Entities;
using CoreWebApiTraining.Data.Repositories;
using CoreWebApiTraining.Extensions;
using CoreWebApiTraining.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreWebApiTraining
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
            services.Configure<EagerCacheSettings>(Configuration.GetSection("EagerCacheSettings"));

            var connStr = Configuration.GetConnectionString("MySqlStr");
            services.AddNHibernate(connStr);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGenericRepository<Users>, GenericRepository<Users>>();

            services.AddScoped(typeof(ICachedObjectProvider<>), typeof(InmemoryCachedObjectProvider<>));
            services.AddSingleton(typeof(ICachedObjects<>), typeof(CachedObjects<>));
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHostedService<UserCountCacheService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreWebApiTraining", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWebApiTraining v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
