using FluentValidation.AspNetCore;
using Masive.Domain.Interfaces;
using Masive.Infrastructure.Filter;
using Masive.Infrastructure.Repositories;
using MasiveApi.Api.Data;
using MasiveApp.Application;
using MasiveApp.Application.Interfaces_App;
using MasiveApp.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasiveApi.Api
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
            //Se agrega el fluen validation y controladores
            services
                .AddControllers(Option =>
                {
                    Option.Filters.Add<GlobalExceptionFilter>();
                })
                .AddFluentValidation(); 

            //Se registran las dependencias del proyecto Masive.Application
            services.AddApplicationServices();

            //Enlace de base de datos
            services.AddDbContext<MusicaContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("Musica")));

            //Se registran las dependencias de los Repositorios 
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IFacturaRepository, FacturaRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IProductoRepository, ProductoRepository>();
            services.AddTransient<IDetalleRepository, DetalleRepository>();

            //Se registran las dependencias de los servicios
            services.AddTransient<ICategoriaService, CategoriaService>();
            services.AddTransient <IUsuarioService, UsuarioService>();
            services.AddTransient<IFacturaService, FacturaService>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IProductoService, ProductoService>();
            services.AddTransient<IDetalleService, DetalleService>();

            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
