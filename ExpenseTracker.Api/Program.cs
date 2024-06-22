using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Extensions;
using ExpenseTracker.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("dev", policy =>
    {
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        policy.AllowAnyOrigin();
    });
});


var connectionString = "Data Source=./sqlite.db";
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(connectionString));

builder.Services.AddScoped<IDbStore, DbStore>();


var app = builder.Build();

if (args.ToList().Contains("--RunMigrations"))
{
    using var scope = app.Services.CreateScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("dev");

    using var scope = app.Services.CreateScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!dbContext.ExpenseType.Any())
    {
        await dbContext.ExpenseType.AddRangeAsync(
            new List<ExpenseType>(){
                new ExpenseType { Type = "Airfare", Id = Guid.Parse("c35c7703-ca31-46b9-a526-1b70762c2856").ToString() },
            new ExpenseType { Type = "Lodging", Id = Guid.Parse("e1ec0702-9663-4bb8-bc1c-8011e20bf79d").ToString() },
            new ExpenseType { Type = "Meal", Id = Guid.Parse("67b6f173-c047-41c7-a922-416e00730485").ToString() },
            new ExpenseType { Type = "Other", Id = Guid.Parse("cee4db16-04e5-4f21-8be9-e2f59c585a8f").ToString() }
            }
        );
        await dbContext.SaveChangesAsync();
    }
}


app.MapExpenseEndpoints();
app.MapGet("api/expensetypes", async (AppDbContext context) =>
{
    return Results.Ok(await context.ExpenseType.ToListAsync());
});

app.Run();


