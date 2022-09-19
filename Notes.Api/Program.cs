using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Notes.Api.Configuration;
using Notes.Api.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();

builder.Services
    .AddSwaggerDocumentation()
    .AddNotesDatabase();

var application = builder.Build();

application
    .UseDeveloperExceptionPage()
    .UseNotesClientServer(application.Environment)
    .UseSwaggerDocumentation()
    .UseRouting()
    .UseEndpoints(endpoints => endpoints.MapControllers());

application
    .SeedNotesDatabase()
    .Run();
