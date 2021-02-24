using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaymentApplication.Core.Application.Services;
using PaymentApplication.Core.Application.Services.Gateways;
using PaymentApplication.Core.Application.Services.Interfaces;
using PaymentApplication.Core.Domain.Repository;
using PaymentApplication.Persistence.Context;
using PaymentApplication.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication
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
            services.AddControllers();
            services.AddCors(c =>
            {
                c.AddPolicy("PaymentGatewayTestAPI", coo =>
                {
                    coo.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.AllowCredentials();
                });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ICheapPaymentGateway,CheapPaymentGateway>();
            services.AddScoped<IExpensivePaymentGateway,ExpensivePaymentGateway>();
            services.AddScoped<IPremiumPaymentGateway,PremiumPaymentGateway>();
            services.AddScoped<IPaymentLogRepository,PaymentLogRepository>();

            services.AddScoped<IPaymentDetailService, PaymentDetailService>();
            services.AddScoped<IPaymentLogRepository, PaymentLogRepository>();
            services.AddScoped<IPaymentDetailRepository,PaymentDetailRepository>();


            services.AddScoped<IPremiumPaymentGateway,PremiumPaymentGateway>();
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddDbContext<PaymentAppDbContext>(x => x.UseSqlite(Configuration.GetConnectionString("Default"))).AddEntityFrameworkSqlite();// ();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PaymentGatewayTestAPI",
                    Version = "v1",
                    Description = "A Simple WEB API to test Payment",
                });
                // c.AddSecurityDefinition(Bear)
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "PaymentGatewayTestAPI");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
