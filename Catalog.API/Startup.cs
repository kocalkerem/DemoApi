using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Demo.Infrastructure.Data;
using Demo.Core.Repositories.Base;
using Demo.Core.Repositories;
using Demo.Infrastructure.Repository;
using Demo.Infrastructure.Repository.Base;
using AutoMapper;
using Demo.Application.Interfaces;
using Demo.Application.Services;
using Demo.Core.UnitOfWork;
using Demo.Infrastructure.UnitOfWork;

namespace Catalog.API
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
             
            services.AddDbContext<ProductContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("ProductConnection")), ServiceLifetime.Singleton);

            services.AddScoped<IUnitOfWork, UnitOfWork>();//request lifecycle boyunca instance saklanýyor ve commit sýrasýnda istediðimiz datalar elimizde oluyor.

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));

            services.AddScoped<IProductService, ProductService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1" , new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Demo API" , Version = "v1" });
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

            //swagger middleware tanýmlama 
            //end point tanýmlama
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");
            });
        }
    }
}
