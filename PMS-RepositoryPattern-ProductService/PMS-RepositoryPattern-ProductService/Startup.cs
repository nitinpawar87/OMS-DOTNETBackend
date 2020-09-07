using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PMS_RepositoryPattern.Config;
using PMS_RepositoryPattern.Data;
using PMS_RepositoryPattern.Repository;
using PMS_RepositoryPattern.Service;

namespace PMS_RepositoryPattern
{
    public class Startup
    {
        private ConfigurationSetting _configurationSetting;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            try
            {
                Configuration = configuration;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error while processing your request...");
            }
        }


        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                _configurationSetting = services.RegisterConfiguration(Configuration);
                services.AddConsulConfig(_configurationSetting);
                services.AddControllers();
                services.AddApiVersioning();
                services.AddDbContext<ProductDBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("ProductDB")));                
                services.AddScoped<IProductRepository, ProductRepository>();                
                services.AddScoped<IProductServiceV1, ProductServiceV1>();
                services.AddScoped<IProductServiceV2, ProductServiceV2>();            
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error while processing your request...");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="_context"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProductDBContext _context)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                app.UseHttpsRedirection();
                app.UseConsul(_configurationSetting);

                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

                _context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error while processing your request...");
            }

        }
    }
}
