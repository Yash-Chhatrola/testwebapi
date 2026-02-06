using apiexample.Data;
using apiexample.Middelware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("StudentPortal")));
//Options.UseSqlServer(builder.Configuration.GetConnectionString("StudentPortalForLaptop")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseMiddleware<MyCustomMiddelware>();
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api/Home"), appBuilder => { appBuilder.UseMiddleware<ApiKeyAuthMiddelware>(); });
//app.UseMiddleware<ApiKeyAuthMiddelware>();
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();
app.Run();
