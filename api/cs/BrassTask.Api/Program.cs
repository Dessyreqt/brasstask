using System.Reflection;
using System.Text;
using BrassTask.Api.Identity;
using BrassTask.Api.Infrastructure.Configuration;
using BrassTask.Api.Services.Data;
using BrassTask.Api.Services.Jwt;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Version = "v1",
                Title = "JobSearch API",
                Description = "This documentation provides information about the JobSearch API.",
                Contact = new OpenApiContact
                {
                    Name = "David Carroll",
                    Url = new Uri("https://www.dscarroll.com/")
                }
            });

        c.AddSecurityDefinition(
            "oauth2",
            new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

        c.OperationFilter<SecurityRequirementsOperationFilter>();

        // Locate the XML file being generated by ASP.NET...
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        // ... and tell Swagger to use those XML comments.
        c.IncludeXmlComments(xmlPath);
    });

builder.Services.AddFluentValidationRulesToSwagger();

// Project services
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Mssql"));
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<UserOptions>(builder.Configuration.GetSection("User"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<UserManager>();

// Add MediatR
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly); });

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
        var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        var key = Encoding.ASCII.GetBytes(tokenOptions.Key);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
