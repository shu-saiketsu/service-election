using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Saiketsu.Service.Election.Application;
using Saiketsu.Service.Election.Application.Common;
using Saiketsu.Service.Election.Domain.IntegrationEvents.Candidates;
using Saiketsu.Service.Election.Domain.IntegrationEvents.Users;
using Saiketsu.Service.Election.Domain.Options;
using Saiketsu.Service.Election.Infrastructure;
using Saiketsu.Service.Election.Infrastructure.Persistence;
using Serilog;
using Serilog.Events;

const string serviceName = "Election";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("ServiceName", serviceName)
    .WriteTo.Console()
    .CreateBootstrapLogger();

static void PerformDataMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (app.Environment.IsDevelopment()) context.Database.Migrate();
}

static void InjectSerilog(WebApplicationBuilder builder)
{
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ServiceName", serviceName)
        .WriteTo.Console());
}

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddRouting(x => x.LowercaseUrls = true);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddHealthChecks();

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
    builder.Services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Election API",
            Description = ".NET Web API for managing Saiketsu elections."
        });

        options.EnableAnnotations();
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Saiketsu.Service.Election.Application.xml"));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Saiketsu.Service.Election.Domain.xml"));
    });

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                builder =>
                {
                    builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    builder.EnableRetryOnFailure();
                })
            .UseSnakeCaseNamingConvention();
    });

    builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection(RabbitMQOptions.Position));

    builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
    builder.Services.AddSingleton<IEventBus, RabbitEventBus>();
}

static void AddMiddleware(WebApplication app)
{
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();
    app.MapHealthChecks("/health");
}

static void SubscribeEventBus(IHost app)
{
    using var scope = app.Services.CreateScope();
    var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

    eventBus.Subscribe<UserCreatedIntegrationEvent>();
    eventBus.Subscribe<UserDeletedIntegrationEvent>();

    eventBus.Subscribe<CandidateCreatedIntegrationEvent>();
    eventBus.Subscribe<CandidateDeletedIntegrationEvent>();
}

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    InjectSerilog(builder);
    AddServices(builder);

    var app = builder.Build();

    AddMiddleware(app);
    SubscribeEventBus(app);
    PerformDataMigrations(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}