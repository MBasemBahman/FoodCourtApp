using Dashboard.Resources;

namespace Dashboard.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.SetIsOriginAllowed(origin => true)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .WithExposedHeaders(HeadersConstants.AuthorizationToken,
                                               HeadersConstants.AppKey,
                                               HeadersConstants.SetRefresh);
                });
            });
        }

    
       
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
               services.AddDbContext<DbContext, DBContext>(options => options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
           
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<RepositoryManager>();
        }

        public static void ConfigureResponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching();
        }

        public static void ConfigureLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                CultureInfo[] supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ar")
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "ar");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddSingleton<LocalizationService>();
        }

        public static void ConfigureViews(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                   .AddViewLocalization()
                   .AddDataAnnotationsLocalization(options =>
                   {
                       options.DataAnnotationLocalizerProvider = (type, factory) =>
                       {
                           AssemblyName assemblyName = new(typeof(LocalizerResources).GetTypeInfo().Assembly.FullName);
                           return factory.Create(nameof(LocalizerResources), assemblyName.Name);
                       };
                   })
                   .AddSessionStateTempDataProvider()
                   .AddRazorRuntimeCompilation()
                   .AddNewtonsoftJson(options =>
                   {
                       options.SerializerSettings.Converters.Add(new StringEnumConverter());
                   });
        }

        public static void ConfigureSessionAndCookie(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.IOTimeout = TimeSpan.FromMinutes(5);

                options.Cookie.Name = ".dashboard.cookie";
                options.Cookie.MaxAge = TimeSpan.FromDays(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }


    }
}
