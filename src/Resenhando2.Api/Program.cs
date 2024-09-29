using Resenhando2.Api.Extensions;
using Resenhando2.Core.Entities;
using Resenhando2.Core.Services;
using SpotifyAPI.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependencyInjections(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();