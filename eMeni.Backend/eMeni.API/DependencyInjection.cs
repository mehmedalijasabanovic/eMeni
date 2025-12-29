using eMeni.Application.Abstractions;
using eMeni.Application.Common.Services;
using eMeni.Infrastructure.Common;
using eMeni.Shared.Dtos;
using eMeni.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;
using System.Text;

namespace eMeni.API;

public static class DependencyInjection
{
    public static IServiceCollection AddAPI(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment env)
    {
        // Controllers + uniform BadRequest
        services.AddControllers()
            .ConfigureApiBehaviorOptions(opts =>
            {
                opts.InvalidModelStateResponseFactory = ctx =>
                {
                    var msg = string.Join("; ",
                        ctx.ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                                                 ? "Validation error"
                                                 : e.ErrorMessage));
                    return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(new ErrorDto
                    {
                        Code = "validation.failed",
                        Message = msg
                    });
                };
            });

        services.AddCors(options => 
        { 
            options.AddPolicy("AllowAngularDev", 
                policy => 
                { 
                    policy
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); 
                }); 
        });

        // Typed options + validation on startup
        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // JWT auth (reads from IOptions<JwtOptions>)
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer((o) =>
        {
            var jwt = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;

            o.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = jwt.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization(o =>
        {
            o.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        // Swagger with Bearer auth
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "eMeni API", Version = "v1" });
            var xml = Path.Combine(AppContext.BaseDirectory, "eMeni.API.xml");
            if (File.Exists(xml))
                c.IncludeXmlComments(xml, includeControllerXmlComments: true);

            var bearer = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Unesi JWT token. Format: **Bearer {token}**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            };
            c.AddSecurityDefinition("Bearer", bearer);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement { { bearer, Array.Empty<string>() } });
        });

        services.AddExceptionHandler<eMeniExceptionHandler>();
        services.AddProblemDetails();
        services.AddScoped<IAuthorizationHelper, AuthorizationHelper>();

        // Rate Limiting Configuration
        var rateLimitConfig = configuration.GetSection("RateLimiting");
        var globalLimit = rateLimitConfig.GetSection("Global").GetValue<int>("PermitLimit", 100);
        var globalWindow = rateLimitConfig.GetSection("Global").GetValue<int>("WindowMinutes", 1);
        var authLimit = rateLimitConfig.GetSection("Auth").GetValue<int>("PermitLimit", 5);
        var authWindow = rateLimitConfig.GetSection("Auth").GetValue<int>("WindowMinutes", 1);
        var apiLimit = rateLimitConfig.GetSection("Api").GetValue<int>("PermitLimit", 60);
        var apiWindow = rateLimitConfig.GetSection("Api").GetValue<int>("WindowMinutes", 1);

        services.AddRateLimiter(options =>
        {
            // Global rate limit policy - applies to all endpoints unless overridden
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                // Use IP address as the partition key
                var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: ipAddress,
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = globalLimit,
                        Window = TimeSpan.FromMinutes(globalWindow)
                    });
            });

            // Policy for authentication endpoints (more restrictive)
            options.AddPolicy("AuthPolicy", context =>
            {
                var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: ipAddress,
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = authLimit,
                        Window = TimeSpan.FromMinutes(authWindow)
                    });
            });

            // Policy for general API endpoints (moderate)
            options.AddPolicy("ApiPolicy", context =>
            {
                var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: ipAddress,
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = apiLimit,
                        Window = TimeSpan.FromMinutes(apiWindow)
                    });
            });

            // Rejection response when rate limit is exceeded
            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsJsonAsync(new ErrorDto
                {
                    Code = "rate_limit_exceeded",
                    Message = "Too many requests. Please try again later."
                }, cancellationToken);
            };
        });

        return services;
    }
}