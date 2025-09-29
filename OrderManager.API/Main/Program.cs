using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderManager.Application.Interfaces.UseCasesInterface;
using OrderManager.Application.UseCases;
using OrderManager.Infrastructure.Auth.JWT;
using OrderManager.Infrastructure.Persistence;
using System.Text;

namespace OrderManager.API.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Configuração do Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gerenciador de pedidos",
                    Version = "v1",
                    Description = "Teste técnico Catalde",
                    Contact = new OpenApiContact
                    {
                        Name = "Paulo Andrade",
                        Email = "parthur207@gmail.com"
                    }
                });


                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Informe o token JWT: Bearer {seu token}",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

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
                        new string[] {}
                    }
                });
            });

            builder.Services.AddDbContext<DbContextInMemory>(options =>
            options.UseInMemoryDatabase("DbContextInMemory")); //Utilização do Banco em memória


            builder.Services.AddScoped<IJwtInterface, JwtService>();

            builder.Services.AddTransient<ICheckTimeOccurrenceOrderInterface, CheckTimeOccurrenceOrderService>(); : 


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            //Autorização
            builder.Services.AddAuthorization();

            var app = builder.Build();

            //pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerenciador de Pedidos API v1");
                });
            }


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
