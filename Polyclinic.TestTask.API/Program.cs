using Microsoft.EntityFrameworkCore;
using Polyclinic.TestTask.API.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<PolyclinicDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ���� �������� ��� ����������.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<PolyclinicDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
