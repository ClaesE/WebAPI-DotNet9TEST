/*
 * Från en artikel Tim Deschryver: 
 * https://timdeschryver.dev/blog/what-about-my-api-documentation-now-that-swashbuckle-is-no-longer-a-dependency-in-aspnet-9
 * 
 * För att detta ska fungera krävs fyra Nuget-paket:
 *      Microsoft.AspNetCore.OpenApi
 *      Scalar.AspNetCore
 *      Swashbuckle.AspNetCore.ReDoc
 *      Swashbuckle.AspNetCore.SwaggerUI
 */

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Generate OpenAPI document
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Register an endpoint to access the OpenAPI document
    app.MapOpenApi();

    Console.WriteLine("The links below open Swagger, OpenAPI and Scalar\nUse CTRL+Click to open them\n");

    // Render the OpenAPI document using Swagger UI
    Console.WriteLine("Swagger at https://localhost:7196/swagger");
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
    });

    // Render the OpenAPI document using Redoc
    Console.WriteLine("OpenAPI at https://localhost:7196/api-docs");
    app.UseReDoc(options =>
    {
        options.SpecUrl("/openapi/v1.json");
    });

    // Render the OpenAPI document using Scalar
    Console.WriteLine("Scalar at https://localhost:7196/scalar/v1\n\n");
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
