﻿using ABC.RepositoryManager.API.Middlewares;
using ABC.RepositoryManager.API.Setup;
using Microsoft.OpenApi.Models;

namespace ABC.RepositoryManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GitHub Repository Manager API",
                    Version = "v1",
                    Description = "API RESTful for searching and managing GitHub repositories with favorite functionality.",
                    Contact = new OpenApiContact
                    {
                        Name = "João de Deus Uchôa",
                        Url = new Uri("https://www.linkedin.com/in/joaouchoa1"),
                        Email = "joaoucchoa@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                // Comentários XML
                var xmlFile = "ABC.RepositoryManager.API.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            builder.Services.AddApiConfig(builder.Configuration);
            builder.Services.AddHttpClient();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
