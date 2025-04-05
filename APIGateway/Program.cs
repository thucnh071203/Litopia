using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot(builder.Configuration.GetSection("Ocelot"));

var app = builder.Build();
app.UseOcelot().Wait();
app.MapGet("/", () => "Hello World!");

app.Run();
