namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableInterceptor>();

        var connectionString = configuration.GetConnectionString(ApplicationDbContext.DefaultConnection);
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(dataSource);
            options.EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddHttpContextAccessor();

        services.AddScoped<IMediaService, MediaService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IMeetingService, MeetingService>();

        services.AddSingleton(sp => {
            var opts = sp.GetRequiredService<IOptions<LiveKitConfig>>().Value;
            return new RoomServiceClient(opts.Url, opts.ApiKey, opts.ApiSecret);
        });

        services.AddSingleton<IAmazonS3>(sp => {
            var opts = sp.GetRequiredService<IOptions<StorageConfig>>().Value;
            var credentials = new BasicAWSCredentials(opts.AccessKey, opts.Secret);
            var s3Config = new AmazonS3Config
            {
                ServiceURL = opts.Url,
                ForcePathStyle = true
            };
            return new AmazonS3Client(credentials, s3Config);
        });

        return services;
    }
}
