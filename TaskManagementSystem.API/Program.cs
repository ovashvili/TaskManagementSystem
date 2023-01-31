using Microsoft.AspNetCore.Mvc.ApiExplorer;
using TaskManagementSystem.Infrastructure.Extensions;
using NLog.Web;
using NLog;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    _ = builder.Services.InstallInfrastructure(builder.Configuration);

    _ = builder.Logging.ClearProviders();

    _ = builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    
    _ = builder.Host.UseNLog();

    var app = builder.Build();

    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseCustomExceptionMiddleware();

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("/APIHealthCheck");
    });

    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });

    await app.AutomateMigrationAndSeeding();

    app.Run();
}
catch (Exception ex)
{
    logger.Fatal(ex, "Application failed");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
