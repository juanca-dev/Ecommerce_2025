
using Ecommerce.Backend.Data;
using Ecommerce.Backend.Repositories;
using Ecommerce.Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;


namespace Ecommerce.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));
            // services de capa de negocios
            builder.Services.AddScoped<CategoriaService>();
            // repositories capa  acceso a Datos
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            // registrar el  servicio de Swagger
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            { 
                options.AddPolicy("AllowAnyOrigins", policy =>

                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    }); // Configuración de CORS para permitir cualquier origen, método y encabezado       

                });

            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                //Entorno de desarrollo (mostrar Swagger);
                app.UseSwagger();
                app.UseSwaggerUI();

            }
            app.UseHttpsRedirection();
            app.UseCors("AllowAnyOrigins");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }

}




