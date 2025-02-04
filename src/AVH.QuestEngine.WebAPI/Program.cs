using AVH.QuestEngine.Application.Constants;
using AVH.QuestEngine.Application.Extensions;
using AVH.QuestEngine.Application.Mappers;
using AVH.QuestEngine.Domain.Repositories;
using AVH.QuestEngine.Infrastructure.Data;
using AVH.QuestEngine.Infrastructure.Repositories;
using AVH.QuestEngine.WebAPI.SwaggerConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace AVH.QuestEngine.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;

            services.AddControllers();
            services.AddDbContext<QuestEngineDbContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlite(builder.Configuration.GetConnectionString(nameof(QuestEngineDbContext) ?? throw new InvalidOperationException($"Connection string {nameof(QuestEngineDbContext)} not found."))));
            
            services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<ExampleSchemaFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Quest Engine API",
                    Version = "v1",
                    Description = "Use ASP.NET Core Web API to track quest progress"
                });
                // Set the comments path for the Swagger JSON and UI base on comment of function in controller.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddAutoMapper(typeof(QuestEngineProfile));

            services.AddDependencyInjectionAutomatically();

            var isUseConfiguration = builder.Configuration.GetValue<bool>(ConfigConstant.IsUseConfigurationKey);
            if (isUseConfiguration)
            {
                services.AddTransient<IQuestRepository, QuestByConfigurationRepository>();
            }
            else
            {
                services.AddTransient<IQuestRepository, QuestRepository>();
            }

            services.AddMemoryCache();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", "Version 1.0");
            });

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.SeedDataAsync().Wait();

            app.MapControllers();

            app.Run();
        }
    }
}
