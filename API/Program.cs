using API;
using API.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureRequestsLimit();
builder.Services.ConfigureCors();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureVersioning();
builder.Services.ConfigureResponseCaching();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureService();
builder.Services.ConfigureRepositoryManager();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.ConfigureSwagger();
builder.Services.AddControllers(opt =>
{
    _ = opt.Filters.Add(typeof(GlobalModelStateValidatorAttribute));
});
//builder.Services.ConfigureFirebase("AppSettings");

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.ConfigureStaticFiles();
app.UseFileServer();
app.UseCors();
app.ConfigureSwagger();
app.UseResponseCaching();

app.UseMiddleware<HeaderMiddleware>();
app.ConfigureExceptionHandler();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

app.Run();