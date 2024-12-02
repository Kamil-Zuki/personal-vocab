using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using personal_vocab.DAL.DataAccess;
using personal_vocab.Interfeces;
using personal_vocab.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
namespace personal_vocab;

public class Program()
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

#if RELEASE
        builder.WebHost.UseUrls("http://*:80");
#endif
        builder.Services.AddControllers().AddNewtonsoftJson();
        //builder.Services.AddScoped<IFlashCardService, FlashCardService>();
        builder.Services.AddScoped<IGroupSevice, GroupService>();
        //builder.Services.AddScoped<IDeckService, DeckService>();
        //builder.Services.AddScoped<ITermService, TermService>();

        builder.Services.AddSingleton(s => new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(Program).Assembly);
            cfg.AllowNullDestinationValues = false;
            cfg.AllowNullCollections = false;
        }).CreateMapper());

        var configuration = builder.Configuration;

        builder.Services.AddDbContext<DataContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Db");
            options.UseNpgsql(connectionString);
        });

        builder.Services.AddCors(c => c.AddPolicy("cors", opt =>
        {
            opt.AllowAnyHeader();
            //opt.AllowCredentials();
            opt.AllowAnyMethod();
            opt.AllowAnyOrigin();
            //opt.WithOrigins(builder.Configuration.GetSection("Cors:Urls").Get<string[]>()!);
        }));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Personal Vocabulary API", Version = "v1" });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
                ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Secret")!))
            };
        });


        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseCors("cors");

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseSwagger(c =>
        {
            c.RouteTemplate = "personal-vocab/swagger/{documentname}/swagger.json";
        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/personal-vocab/swagger/v1/swagger.json", "Personal Vocabulary API");
            c.RoutePrefix = "personal-vocab/swagger";
        });

        app.MapControllers();

        app.Run();
    }
}
