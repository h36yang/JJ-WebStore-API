using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using System.Text;
using WebApi.DataAccess;
using WebApi.DataAccess.Repositories;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Services.Interfaces;
using WebApi.ViewModelMappers;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the Options
            services.AddOptions();

            // Inject the repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductFunctionRepository, ProductFunctionRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

            // Inject application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProductService, ProductService>();

            // Register AutoMapper Profile
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // Register Health Checks
            services
                .AddHealthChecks()
                .AddDbContextCheck<WebStoreContext>();

            // Inject DB Context and Run Migrations
            services.AddDbContext<WebStoreContext>(options =>
            {
                options
                    .UseSqlServer(Configuration.GetConnectionString("WebStore"))
                    .ConfigureWarnings(warnings =>
                    {
                        warnings.Default(WarningBehavior.Ignore)
                                .Log(CoreEventId.IncludeIgnoredWarning)
                                .Throw(RelationalEventId.QueryClientEvaluationWarning);
                    });
            });
            services
                .BuildServiceProvider()
                .GetService<WebStoreContext>()
                .Database.Migrate();

            // Register CORS Middleware
            services.AddCors();

            // Register Response Caching Middleware
            services.AddResponseCaching();

            // Register Response Compression Middleware
            services.AddResponseCompression(options =>
            {
                // Add image/jpeg to the list of default mime types
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new string[] { "image/jpeg" });
            });

            // Register MVC Middleware
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "ZC Tea Web Store API", Version = "v1" });

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>(true, JwtBearerDefaults.AuthenticationScheme);
            });

            // Register and configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // Register and configure JWT Authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMapper autoMapper)
        {
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseHealthChecks("/health");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Response Caching
            app.UseResponseCaching();

            // Response Compression
            app.UseResponseCompression();

            // Configure AutoMapper
            autoMapper.ConfigurationProvider.AssertConfigurationIsValid();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ZC Tea Web Store API v1");
                options.RoutePrefix = string.Empty;
            });

            // global cors policy
            app.UseCors(cors =>
            {
                cors
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
