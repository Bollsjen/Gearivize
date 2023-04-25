using Gerivize.Managers;
using Gerivize.Repositories;
using Gerivize.Schedulers;
using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder => builder.WithOrigins("http://localhost:8081").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

builder.Services.AddHangfire(configuration => {
    configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseMemoryStorage();
});

builder.Services.AddHangfireServer();
GlobalConfiguration.Configuration.UseMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();
app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer();
SchedulerBaseClass.InittializeSchedulers();

app.Run();
