using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using GestaoProduto.Application.Mappers;
using GestaoProduto.Application.Services;
using GestaoProduto.Application.Services.Interfaces;
using GestaoProduto.Infrastructure.Context;
using GestaoProduto.Infrastructure.Repositories;
using GestaoProduto.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GestaoProduto.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IValidacaoService, ValidacaoService>();

            services.AddDbContext<ProdutoDbContext>(options => options.UseInMemoryDatabase("MyInMemoryDatabase"));

            services.AddAutoMapper(typeof(ProdutoProfile));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GestaoProduto.API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Priscilla Batista",
                        Email = "pri.batista.lima@gmail.com"
                    }
                });
                
                var xmlFile = "GestaoProduto.API.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestaoProduto.API v1"));
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