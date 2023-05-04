using Gerivize.Managers;
using Gerivize.Models;
using Gerivize.Repositories;
using Gerivize.Schedulers;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication("gearivise")
    .AddCookie("gearivise", options =>
    {
        options.Cookie.Name = "gearivize-session";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.LoginPath = "/api/Authentication";
        options.AccessDeniedPath = "/api/Authentication";

        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    }
);

builder.Services.AddSingleton<User>();

builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder => builder.WithOrigins("http://localhost:8081").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

builder.Services.AddHangfire(configuration => {
    configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseMemoryStorage();
});

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer();
SchedulerBaseClass.InittializeSchedulers();

app.Run();
