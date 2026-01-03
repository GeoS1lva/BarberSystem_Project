using BarberSystem.Application.Interfaces;
using BarberSystem.Application.Interfaces.Queries;
using BarberSystem.Application.Interfaces.Security;
using BarberSystem.Application.Services;
using BarberSystem.Domain.Interface;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;
using BarberSystem.Domain.Service;
using BarberSystem.Infrastructure.Data;
using BarberSystem.Infrastructure.Data.Context;
using BarberSystem.Infrastructure.Data.Queries;
using BarberSystem.Infrastructure.Services.BackgroundJobs;
using BarberSystem.Infrastructure.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SqlServerDbContext>();
builder.Services.AddDbContext<SqlServerDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSql")));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt");
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!)),
            ClockSkew = TimeSpan.Zero,
            NameClaimType = System.Security.Claims.ClaimTypes.Name,
            RoleClaimType = System.Security.Claims.ClaimTypes.Role
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                if (ctx.Request.Cookies.TryGetValue("jwt", out var token))
                    ctx.Token = token;
                return Task.CompletedTask;
            }
        };
    });

builder.Services
    .AddAuthorization(options =>
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
    });

builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IIdentitySystemService, IdentitySystemService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IServiceProvidedService, ServiceProvidedService>();
builder.Services.AddScoped<ISchedulingS, SchedulingS>();

builder.Services.AddScoped<ISchedulingAppService, SchedulingAppService>();
builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IServiceProvidedAppService, ServiceProvidedAppService>();
builder.Services.AddScoped<IIdentitySystemAppService, IdentitySystemAppService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserQueries, UserQueries>();
builder.Services.AddScoped<ICustomerQueries, CustomerQueries>();
builder.Services.AddScoped<ISchedulingQueries, SchedulingQueries>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthCookieService, AuthCookieService>();

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey(nameof(UpdateSchedulingJob));

    q.AddJob<UpdateSchedulingJob>(options => options.WithIdentity(jobKey));

    q.AddTrigger(options => options
    .ForJob(jobKey)
    .WithIdentity($"{nameof(UpdateSchedulingJob)}--trigger")
    .StartNow()
    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMinutes(10)).RepeatForever()));
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
