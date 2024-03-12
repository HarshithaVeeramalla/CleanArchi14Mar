using Infrastructure;
using Application;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
var worklogCorsPolicy = "WorklogAllowedCredentialsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(worklogCorsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                .WithMethods("GET", "POST")
                    .WithHeaders(HeaderNames.ContentType, "x-custom-header",
                        HeaderNames.Authorization, "true")
                            .AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(worklogCorsPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<WorklogDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager, builder.Configuration);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration.");
}

app.Run();
