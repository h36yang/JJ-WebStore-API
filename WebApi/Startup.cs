﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
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
    /// <summary>
    /// Application Startup Class
    /// </summary>
    public class Startup
    {
        public const string MyCorsPolicyName = "AllowAll";

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="configuration">Configuration Interface</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration Interface Getter
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service Collection Interface</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the Options
            services.AddOptions();

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

            // Inject DB Context and Run Migrations
            services.AddDbContext<WebStoreContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("WebStore"));
            });

            // Register Health Checks
            services
                .AddHealthChecks()
                .AddDbContextCheck<WebStoreContext>();

            // Configura API behavior for Unprocessable Entity error
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext = actionContext as ActionExecutingContext;

                    // if there are modelstate errors & all keys were correctly
                    // found/parsed we're dealing with validation errors
                    if (actionContext.ModelState.ErrorCount > 0 &&
                        actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    // if one of the keys wasn't correctly found / couldn't be parsed
                    // we're dealing with null/unparsable input
                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            // Register CORS Middleware
            services.AddCors(options =>
            {
                options.AddPolicy(MyCorsPolicyName, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Register Response Caching Middleware
            services.AddResponseCaching();

            // Register Response Compression Middleware
            services.AddResponseCompression();

            // Register MVC Middleware
            services
                .AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services
                .AddMvcCore()
                .AddApiExplorer()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ZC Tea Web Store API", Version = "v1" });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                options.IncludeXmlComments(xmlCommentsFullPath);

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>(true, JwtBearerDefaults.AuthenticationScheme);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application Builder Interface</param>
        /// <param name="env">Web Host Environment Interface</param>
        /// <param name="service">Service Provider Interface</param>
        /// <param name="autoMapper">AutoMapper Interface</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider service, IMapper autoMapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Configure AutoMapper
            autoMapper.ConfigurationProvider.AssertConfigurationIsValid();

            // Run EF Database Migrations
            service.GetService<WebStoreContext>().Database.Migrate();

            // Enable MVC Routing
            app.UseRouting();

            // Enable Global CORS Policy
            app.UseCors(MyCorsPolicyName);

            // Enable HTTPS Redirect
            app.UseHttpsRedirection();

            // Enable Standard Error Responses
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            // Enable Response Caching
            app.UseResponseCaching();

            // Enable Response Compression
            app.UseResponseCompression();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ZC Tea Web Store API v1");
                options.RoutePrefix = string.Empty;
            });

            // Enable Auth
            app.UseAuthentication();
            app.UseAuthorization();

            // Enable Controller Endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
