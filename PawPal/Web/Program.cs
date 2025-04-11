var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureControllers();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.ConfigureCors();

var app = builder.Build();

app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AuthMiddleware>();

app.MapGet("/api/health", () => "Healthy!");

app.Run();
