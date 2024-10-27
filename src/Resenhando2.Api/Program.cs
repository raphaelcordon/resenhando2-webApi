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
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins()//"http://localhost:5173"
                .AllowAnyHeader()
                .AllowAnyMethod();
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
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();
app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();