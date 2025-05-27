using CloudinaryDotNet;
using Ecommerce.Backend.Data;
using Ecommerce.Backend.Repositories;
using Ecommerce.Backend.Services;
using Microsoft.EntityFrameworkCore;

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
            //services capa de negocios
            builder.Services.AddScoped<CategoriaService>();
            builder.Services.AddScoped<ProductoService>();
            builder.Services.AddScoped<IFilesService, FilesService>();

            //repositories capa de acceso a datos
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            //registrar el servicio de swagger
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            builder.Services.AddOpenApi();

            var cloudinaryConfig = builder.Configuration.GetSection("Cloudinary");
            var cloudinary = new Cloudinary(new Account(
               cloudinaryConfig["CloudName"],
               cloudinaryConfig["ApiKey"],
               cloudinaryConfig["ApiSecret"]
            ));
            builder.Services.AddSingleton(cloudinary);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                //mostrar swagger;
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}