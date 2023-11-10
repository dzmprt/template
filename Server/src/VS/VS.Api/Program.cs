using Common.Api;
using Common.Api.Middleware;
using Common.Application;
using Common.Application.Abstractions;
using Common.Application.Abstractions.Persistence;
using Common.Persistence;
using Serilog;
using Serilog.Events;
using VS.Api;
using VS.Application;

#region Logger

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.File("Logs/Log-Information-.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
    .WriteTo.File("Logs/Log-Error-.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
    .CreateLogger();

#endregion

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

RegisterServices(builder.Services, builder.Configuration);

#region Build app

WebApplication app;
try
{
    app = builder.Build();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
    throw;
}

#endregion

ConfigureApp(app);

app.Run();

void RegisterServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddVcApiServices(configuration);
    services.AddVcApplicationServices();

    services.AddCommonApiServices(configuration);
    services.AddCommonApplicationServices();
    services.AddCommonPersistenceServices(configuration);
}

void ConfigureApp(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
    dataContext.MigrateAsync(CancellationToken.None).Wait();
    app.UseHttpsRedirection();

    app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentname}/swagger.json"; });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });

    app.UseCommonExceptionHandler();
    
    app.UseResponseCompression();
    
    app.UseCors("AllowAll");
    
    RegisterApis(app);
}

void RegisterApis(WebApplication app)
{
    var apiTypeInterface = typeof(IApi);
    var apiTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(p => apiTypeInterface.IsAssignableFrom(p) && p.IsClass);
    foreach (var apiType in apiTypes)
    {
        var api = (IApi)Activator.CreateInstance(apiType)!;
        api.Register(app, app.Environment.IsProduction() ? "api" : "api");
    }
}