using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using sm_repetition_algorithm.BLL.Interfeces;
using sm_repetition_algorithm.BLL.Logic;
using sm_repetition_algorithm.DAL.DataAccess;
using sm_repetition_algorithm.Interfeces;
using sm_repetition_algorithm.Services;
using System.Text;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace sm_repetition_algorithm
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

            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IFlashCardSevice, FlashCardSevice>();
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Repetition Algorithm API", Version = "v1" });
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
                c.RouteTemplate = "repetition/swagger/{documentname}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/repetition/swagger/v1/swagger.json", "Repetition Algorithm API");
                c.RoutePrefix = "repetition/swagger";
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