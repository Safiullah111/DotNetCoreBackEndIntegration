using API.Work.Application.Common;
using API.Work.Application.Contract.Localization;
using API.Work.Application.Services.Configurations.DependencyInjection.ServiceCollectionExtensions;
using API.Work.Application.Services.JwtSettings;
using API.Work.Controllers.DependencyInjection.ServiceCollectionExtensions;
using API.Work.EntityFrameWork.Configurations;
using API.Work.EntityFrameWork.Configurations.DependencyInjection.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<APIWorkDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("API.Work.DbMigrator")
    ));
builder.Services.AddLogging();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()                                // Optional: still log to console
    .WriteTo.File("Logs/log-.txt",                    // Log file path (auto-creates folder)
        rollingInterval: RollingInterval.Day,         // Creates new file each day
        retainedFileCountLimit: 7,                    // Keep last 7 days of logs
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
    .Enrich.FromLogContext()
    .CreateLogger();

// ✅ Replace built-in logger with Serilog
builder.Host.UseSerilog();
builder.Services.AddEntityFrameWorkModule(builder.Configuration);
builder.Services.AddApplicationModule(builder.Configuration);
builder.Services.AddControllerModule(builder.Configuration);


builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSetting:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(); // <--- add this


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication(); // authenticate first
app.UseAuthorization();  // then authorize

ApplicationGlobalConfigurator.ConfigureGlobals(app.Services);

app.Use(async (context, next) =>
{
    var langHeader = context.Request.Headers["Accept-Language"].FirstOrDefault();
    var langCode = "en";

    if (!string.IsNullOrEmpty(langHeader))
    {
        var primary = langHeader.Split(',')[0]; // en-US
        langCode = primary.Split('-')[0];       // en
    }

    L.SetCulture(langCode);

    await next();
});

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
Log.CloseAndFlush();
