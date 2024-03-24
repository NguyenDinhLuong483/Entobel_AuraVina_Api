using ENTOBEL_AURAVINA_API.Domains;
using ENTOBEL_AURAVINA_API.Domains.Presistances.Repositories;
using ENTOBEL_AURAVINA_API.Domains.Repositories;
using ENTOBEL_AURAVINA_API.Domains.Services;
using ENTOBEL_AURAVINA_API.Hubs;
using ENTOBEL_AURAVINA_API.Mapping;
using ENTOBEL_AURAVINA_API.MQTTClients;
using ENTOBEL_AURAVINA_API.Resources;
using ENTOBEL_AURAVINA_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ENTOBEL_AURAVINA_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSignalR();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .WithOrigins("localhost","http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            var configure = builder.Configuration;
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configure.GetConnectionString("DefaultConnection"));
            });

            builder.Services.Configure<MqttOptions>(configure.GetSection("MqttOptions"));
            builder.Services.AddSingleton<ManagedMqttClient>();
            builder.Services.AddHostedService<ScadaHost>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(ResourceToModelProfile));

            builder.Services.Configure<AppSetting>(configure.GetSection("AppSettings"));
            var secretKey = configure["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            var app = builder.Build();

            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<NotificationHub>("/NotificationHub");
            app.MapControllers();

            app.Run();
        }
    }
}
