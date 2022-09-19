namespace Notes.Api.Configuration;

using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

public static class SwaggerExtensions
{
    private static readonly OpenApiInfo _apiInfo = new OpenApiInfo
    {
        Version = "v1",
        Title = "Notes API",
        Description = "An API for sticky notes.",
    };

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services) =>
        services
            .AddSwaggerGenNewtonsoftSupport()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc(_apiInfo.Version, _apiInfo);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder application) =>
        application
            .UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", _apiInfo.Title);
            });
}