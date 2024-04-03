using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using personal_vocab.BLL.Interfeces;
using personal_vocab.BLL.Logic;
using personal_vocab.DAL.DataAccess;
using personal_vocab.Interfeces;
using personal_vocab.Services;
using System.Text;
namespace personal_vocab
{
    public class Program
    {
        private IConfiguration Configuration { get; }

        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.WebHost.UseUrls("http://*:80");
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IFlashCardService, FlashCardService>();
            builder.Services.AddScoped<IGroupSevice, GroupService>();
            builder.Services.AddScoped<IDeckService, DeckService>();
            builder.Services.AddScoped<ITermService, TermService>();
            //builder.Services.AddDbContext<RepetitionAlgorithmContext>(options =>
            //{
            //    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")!);
            //});
            var configuration = builder.Configuration; // Get the configuration from the builder

            //var connectionString = configuration.GetConnectionString("Db");
            //builder.Services.AddDbContext<DataContext>(opt =>
            //    opt.UseNpgsql(connectionString));

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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Personal Vocabulary API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                 {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                    new string[]{}
                }
                });

            });


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Configure the JwtBearer authentication options
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "https://localhost:5001",
                    ValidAudience = "https://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });


            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

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

            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();



            app.Run();
        }
    }
}