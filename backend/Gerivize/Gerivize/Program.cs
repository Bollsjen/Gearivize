using System.Collections.Immutable;
using dotenv.net;
using Gerivize.Managers;
using Gerivize.Models;
using Gerivize.Repositories;
using Gerivize.Schedulers;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File($"logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog(dispose: true);
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<FileExplorerManager>();
builder.Services.AddTransient<InstrumentsManager>();
builder.Services.AddTransient<NotificationsManager>();

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

builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo("mykeys"));

builder.Services.AddSingleton<User>();

builder.Services.AddCors(options => 
    options.AddPolicy("AllowAll", builder => 
        builder.WithOrigins("https://192.168.150.70:5000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        )
    );

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

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseDefaultFiles(); 
app.UseFileServer();

app.Map("/api", apiApp =>
{
    // Configure API-specific middleware, routes, and controllers
    apiApp.UseRouting();
    apiApp.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
});

app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer();

app.Run(async (context) =>
{
    if (!context.Request.Path.StartsWithSegments("/api") && !context.Request.Path.StartsWithSegments("/swagger") && !context.Request.Path.StartsWithSegments("/hangfire"))
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync(Path.Combine(app.Environment.ContentRootPath, "wwwroot", "index.html"));
    }
});

app.UseCors("AllowAll");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

SchedulerBaseClass.InittializeSchedulers();

app.Run();
