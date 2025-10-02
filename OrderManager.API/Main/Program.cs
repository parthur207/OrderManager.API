using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Application.Interfaces.IServices.ICommandsAdm;
using OrderManager.Application.Interfaces.IServices.ICommandsUserCommon;
using OrderManager.Application.Interfaces.IServices.ILogin;
using OrderManager.Application.Interfaces.IServices.IQueriesAdm;
using OrderManager.Application.Interfaces.IServices.IQueriesUserCommon;
using OrderManager.Application.Interfaces.IUseCase;
using OrderManager.Application.Mappers;
using OrderManager.Application.RepositoryInterface.Commands;
using OrderManager.Application.RepositoryInterface.Login;
using OrderManager.Application.RepositoryInterface.Queries;
using OrderManager.Application.Services.CommandsAdm;
using OrderManager.Application.Services.CommandsUserGeneric;
using OrderManager.Application.Services.Login;
using OrderManager.Application.Services.QueriesAdm;
using OrderManager.Application.Services.QueriesUserCommon;
using OrderManager.Application.UseCases;
using OrderManager.Domain.Models;
using OrderManager.Infrastructure.Auth.JWT;
using OrderManager.Infrastructure.Persistence;
using OrderManager.Infrastructure.Repository.Commands;
using OrderManager.Infrastructure.Repository.Login;
using OrderManager.Infrastructure.Repository.Queries;
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

            builder.Services.AddDbContext<DbContextOrderManager>(options =>
            options.UseInMemoryDatabase("DbContextInMemory")); //Utilização do Banco em memória

            #region Serviços - Injeções de dependencia (Tempo de vida)
            
            //Mappers
            builder.Services.AddTransient<IOrderMapperInterface, OrderMapper>();
            builder.Services.AddTransient<IUserMapperInterface, UserMapper>();
            builder.Services.AddTransient<IOccurrenceMapperInterface, OccurrencesMapper>();

            //Services
            builder.Services.AddScoped<IOccurrenceOrderCommandsAdmInterface, OccurrencesOrderAdmService>();
            builder.Services.AddScoped<IUserCommadsAdmInterface, UserCommandsAdmService>();
            builder.Services.AddScoped<IOrderCommandsUserCommonInterface, OrderCommandsUserCommonService>();
            builder.Services.AddScoped<IUserCommandsUserCommonInterface, UserCommandsUserCommonService>();
            builder.Services.AddScoped<ILoginInterface, LoginService>();
            builder.Services.AddScoped<IOrderQueriesAdmInterface, OrderQueriesAdmService>();
            builder.Services.AddScoped<IUserQueriesAdmInterface, UserQueriesAdmService>();
            builder.Services.AddScoped<IOrderQueriesUserCommonInterface, OrderQueriesUserCommonService>();

            //Service UseCase
            builder.Services.AddTransient<ICheckTimeOccurrenceOrderInterface, CheckTimeOccurrenceOrderService>();

            //jwt
            builder.Services.AddScoped<IJwtInterface, JwtService>();

            //repository
            builder.Services.AddScoped<IOccurrenceOrderCommandsRepository, OccurrencesOrderCommandsRepository>();
            builder.Services.AddScoped<IOrderCommandsRepository, OrderCommandsRepository>();
            builder.Services.AddScoped<IUserCommandsRepository, UserCommandsRepository>();
            builder.Services.AddScoped<IUserLoginDataRepository, UserLoginDataRepository>();
            builder.Services.AddScoped<IOrderQueriesRepository, OrderQueriesRepository>();
            builder.Services.AddScoped<IUserQueriesRepository, UserQueriesRepository>();
            builder.Services.AddScoped<IOrderQueriesRepository, OrderQueriesRepository>();

            //UseCase repository
            builder.Services.AddTransient<ICheckTimeOccurrenceOrderInterface, CheckTimeOccurrenceOrderService>();
            #endregion

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
