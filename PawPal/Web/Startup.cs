namespace Web;

public static class Startup
{
    public static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSignalR();
        services.AddRouting(options => options.LowercaseUrls = true);

        return services;
    }

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "PawPal API",
                Description = "API for PawPal web service",
                Contact = new OpenApiContact
                {
                    Name = "GitHub Repository",
                    Url = new Uri("https://github.com/diploma-nure/pawpal_be")
                },
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthConfig>(configuration.GetSection(AuthConfig.Auth));
        services.Configure<EmailConfig>(configuration.GetSection(EmailConfig.Email));
        services.Configure<LiveKitConfig>(configuration.GetSection(LiveKitConfig.LiveKit));
        services.Configure<StorageConfig>(configuration.GetSection(StorageConfig.Storage));

        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration);
        services.AddApplicationServices(configuration);

        services.AddHostedService<MeetingBackgroundService>();

        return services;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        return services;
    }
}
