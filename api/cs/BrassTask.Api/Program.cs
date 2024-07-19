using System.Text;
using System.Text.Json.Serialization;
using BrassTask.Api.Identity;
using BrassTask.Api.Infrastructure.Configuration;
using BrassTask.Api.Infrastructure.Swagger;
using BrassTask.Api.Infrastructure.Validation;
using BrassTask.Api.Services.Crypto;
using BrassTask.Api.Services.Data;
using BrassTask.Api.Services.Jwt;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Filters;

[assembly: ApiConventionType(typeof(ApiResponseConventions))]

var builder = WebApplication.CreateBuilder(args);

// configure logging prior to services being added
Log.Logger = new LoggerConfiguration().MinimumLevel.Override("Microsoft", LogEventLevel.Warning).Enrich.FromLogContext().WriteTo.Console().CreateBootstrapLogger();

Log.Information("Starting up BrassTask API");

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc(
            "v1",
            new()
            {
                Version = "v1",
                Title = "BrassTask API",
                Description = "This documentation provides information about the BrassTask API.",
                Contact = new()
                {
                    Name = "David Carroll",
                    Url = new("https://www.dscarroll.com/")
                }
            });

        c.AddSecurityDefinition(
            "oauth2",
            new()
            {
                Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

        c.OperationFilter<SecurityRequirementsOperationFilter>();

        c.CustomSchemaIds(type => type.FullName);

        // Locate the XML files being generated by ASP.NET...
        foreach (var filePath in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
            // ... and tell Swagger to use those XML comments.
            c.IncludeXmlComments(filePath);
    });

// Options configuration
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.ConfigSection));
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection(TokenOptions.ConfigSection));
builder.Services.Configure<UserOptions>(builder.Configuration.GetSection(UserOptions.ConfigSection));

// Project services
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Facades
builder.Services.AddScoped<UserFacade>();

// Add MediatR
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly); });
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
builder.Services.AddScoped(typeof(IRequestPreProcessor<>), typeof(ValidationPreProcessor<>));

// Add Validations
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add Authentication
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(
    options =>
    {
        var tokenOptions = builder.Configuration.GetSection(TokenOptions.ConfigSection).Get<TokenOptions>() ?? new TokenOptions();
        var key = Encoding.ASCII.GetBytes(tokenOptions.Key);

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// create the final logger now that services are available
builder.Host.UseSerilog((context, services, config) => config.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Logger.LogInformation("BrassTask API started successfully.");

app.Run();
