using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using sm_repetition_algorithm.BLL.Interfeces;
using sm_repetition_algorithm.BLL.Logic;
using sm_repetition_algorithm.DAL.DataAccess;

namespace sm_repetition_algorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<ISuperMemo2Algorithm, SuperMemo2Algorithm>();
            builder.Services.AddDbContext<RepetitionAlgorithmContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")!);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy
                                      .AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Repetition Algorithm API", Version = "v1" });

            });

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

            app.Run();
        }
    }
}