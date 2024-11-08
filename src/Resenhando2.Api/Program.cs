using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Data;
using Resenhando2.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependencyInjections(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwtSupport(builder.Configuration);

// To deal with FE requisitions
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Allows requests from any origin
            .AllowAnyHeader() // Allows any headers
            .AllowAnyMethod(); // Allows any HTTP methods (GET, POST, etc.)
    });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.Migrate();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();