using Polyclinic.TestTask.API.DataAccess;
using Polyclinic.TestTask.API.Middlewares;
using Polyclinic.TestTask.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<PolyclinicDbContext>();
builder.Services.AddServices();

var app = builder.Build();

app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Авто миграции при разработке.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    await DatabaseInitializationHelper.InitializeDevelopment(scope);
}

app.UseAuthorization();
app.MapControllers();
app.Run();
