using API.Errors;
using API.Extensions;
using API.Middleware;
using Core.Contract.Repository;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Move all the services into AddApplicationServices class, which extends the IServiceCollection.
// Register this extended IServiceCollection to obtain all the neccessary services for the application.
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
/* Exception handling middleware should always be at the top of request pipeline,
 otherwize, it might casue unhandled exception problem in server application.
*/
app.UseMiddleware<ExceptionMiddleware>();

// If no exception being catched, redirect user to the ErrorController that returns the error message
// This demo is for 'no found endpoint' request where the request calls the endpoint that doen't exist.
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseSwagger();
app.UseSwaggerUI();

// Configure to serve static files
app.UseStaticFiles();

// Use middleware to setup CORS, CORS should be set above Authorization
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

// When restart the application, apply migrations and seed data if any json data being altered.
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
