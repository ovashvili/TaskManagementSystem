using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR.Extensions.FluentValidation.AspNetCore;
using System;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using TaskManagementSystem.Infrastructure.Configurations.Swagger;
using System.IO;
using MediatR;
using FluentValidation;
using System.Linq;
using TaskManagementSystem.Infrastructure.Middlewares;
using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Infrastructure.Services;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using TaskManagementSystem.Infrastructure.Models.Options;
using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Mappings;
using TaskManagementSystem.Infrastructure.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.User.Commands.AuthenticateUser;

namespace TaskManagementSystem.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection InstallInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services), "IServiceCollection is null");
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration), "IConfiguration is null");
            }

            _ = services.AddControllers(options => 
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
                .AddNewtonsoftJson(options => 
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            _ = services.AddDbContext<AppDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("TaskManagementSystem")));

            _ = services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            _ = services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VV";
                options.SubstituteApiVersionInUrl = true;
            });

            _ = services.AddEndpointsApiExplorer();

            _ = services.AddValidatorsFromAssembly(typeof(AuthenticateUserCommand).Assembly);

            // configure DI for application services

            _ = services.AddScoped<IUserService, UserService>();

            _ = services.AddScoped<ITaskService, TaskService>();

            _ = services.AddScoped<IRoleManagerService, RoleManagerService>();

            _ = services.AddTransient<ExceptionHandlingMiddleware>();

            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            _ = services.AddMediatR(typeof(AuthenticateUserCommand).Assembly);

            _ = services.Configure<JWTAuthOptions>(options => configuration.GetSection(JWTAuthOptions.SectionName).Bind(options));

            _ = services.Configure<DirectoriesPathsOptions>(options => configuration.GetSection(DirectoriesPathsOptions.SectionName).Bind(options));

            _ = services.AddHealthChecks();

            _ = services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            _ = services.AddAutoMapper(typeof(MappingProfile).Assembly);

            _ = services.AddIdentity<User, IdentityRole>()
                                .AddEntityFrameworkStores<AppDBContext>();

            _ = services.AddAuthorization(opts => {
                opts.AddPolicy("User", policy => {
                    policy.RequireRole("Basic", "Moderator", "Admin");
                });
            });

            _ = services.AddAuthorization(opts => {
                opts.AddPolicy("Moderator", policy => {
                    policy.RequireRole("Moderator", "Admin");
                });
            });

            _ = services.AddAuthorization(opts => {
                opts.AddPolicy("Admin", policy => {
                    policy.RequireRole("Admin");
                });
            });

            _ = services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection(JWTAuthOptions.SectionName).Get<JWTAuthOptions>().Secret)),
                    ValidIssuer = "jwt",
                    ValidateIssuer = true,
                    ValidAudience = "jwt-audience",
                    ValidateAudience = true,
                    RoleClaimType = "Role",
                };
            });

            _ = services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName!.Contains("TaskManagementSystem.API")).GetName().Name}.xml"));
                options.UseInlineDefinitionsForEnums();
            });

            _ = services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}
