using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMS_RepositoryPattern.Config;
using PMS_RepositoryPattern.Data;
using PMS_RepositoryPattern.Repository;
using PMS_RepositoryPattern.Service;
using Polly;
using Polly.Extensions.Http;
//using Polly;
//using Polly.Extensions.Http;

namespace PMS_RepositoryPattern
{
    public class Startup
    {
       // public ILifetimeScope AutofacContainer { get; private set; }
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
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserService, UserService>();
                //            var basicCircuitBreakerPolicy = Policy
                //.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                //.CircuitBreakerAsync(2, TimeSpan.FromSeconds(30), OnBreak, OnReset, OnHalfOpen);
                //            services.AddHttpClient<IUserService, UserService>()
                //    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
                //    .AddPolicyHandler(GetRetryPolicy());
                services.AddHttpClient("errorApi", r => { r.BaseAddress = new Uri("http://localhost:40001/"); });
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
        [Obsolete]
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

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }

        private void OnHalfOpen()
        {
            Console.WriteLine("Circuit in test mode, one request will be allowed.");
        }

        private void OnReset()
        {
            Console.WriteLine("Circuit closed, requests flow normally.");
        }

        private void OnBreak(DelegateResult<HttpResponseMessage> result, TimeSpan ts)
        {
            Console.WriteLine("Circuit cut, requests will not flow.");
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                            retryAttempt)));
        }
    }
}
