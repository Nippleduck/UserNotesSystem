using UserNotesSystem.Data;
using UserNotesSystem.Data.Context;
using UserNotesSystem.Authentication;
using UserNotesSystem.Server;
using UserNotesSystem.Server.Handlers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new()
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });
    options.AddSecurityRequirement(new() {
   {
     new()
     {
       Reference = new()
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      Array.Empty<string>()
    }
  });
});
builder.Services.AddData(builder.Configuration);
builder.Services.AddTokenAuthentication(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddScoped<CurrentUserAccessor>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.InitialiseDatabaseAsync();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseExceptionHandler(options => { });

app.UseAuthentication();
app.UseAuthorization();

string[] allowedOrigins = ["http://localhost:3000"];

app.UseCors(options => options
    .WithOrigins(allowedOrigins)
    .AllowCredentials()
    .AllowAnyMethod()
    .AllowAnyHeader());
    

app.MapFallbackToFile("/index.html");

app.Run();

