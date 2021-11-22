using FluentValidation.AspNetCore;
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
using System.Threading.Tasks;

namespace Desafio.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AdicionarRequisicoes(Configuration);
            AdicionarServicoSwagger(services);
        }

        private static void AdicionarServicoSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Desafio de avaliação",
                    Version = "beta",
                    Description = "API conceitual de um desafio",
                    Contact = new OpenApiContact
                    {
                        Name = "Alexandre Dórea",
                        Email = "alexandre.dorea@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/alexandredorea/")
                    },
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            UsarSwagger(app);
        }

        private static void UsarSwagger(IApplicationBuilder app)
        {
            app.UseSwagger(x =>
            {
                x.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(x =>
            {
                x.RoutePrefix = string.Empty;
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Interface padrão swagger");
            });
        }
    }
}