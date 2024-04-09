using UserNotesSystem.Data;
using UserNotesSystem.Data.Context;
using UserNotesSystem.Authentication;
using UserNotesSystem.Server;
using UserNotesSystem.Server.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddData(builder.Configuration);
builder.Services.AddTokenAuthentication(builder.Configuration);

builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddScoped<CurrentUserAccessor>();

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

app.MapFallbackToFile("/index.html");

app.Run();

