using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Hugger_Application.Data.Repository.HugRepository;
using Hugger_Application.Data.Repository.MatchRepository;
using Hugger_Application.Data.Repository.UserCharRepository;
using Hugger_Application.Data.Repository.UserPrefRepository;
using Hugger_Application.Helpers;
using Hugger_Application.Models.Repository;
using Hugger_Application.Services;
using Hugger_Application.Services.HugService;
using Hugger_Web_Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Hugger_Application

{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingSection = Configuration.GetSection("AppSettings");


            services.AddDbContext<UserContext>();
            
            services.AddAutoMapper(typeof(Startup));
            
            services.AddControllers();

            services.AddCors();

            //AddTransient -> Transient lifetime services are created each time they are requested
            //Transient objects are always different; a new instance is provided to every controller and every service.

            //AddScoped -> Scoped lifetime services are created once per request
            //Scoped objects are the same within a request, but different across different requests.

            //AddSingleton -> Singleton objects are the same for every object and every request.

            //=============Intefaces and its implementation==============
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IHugService, HugService>();
            services.AddScoped<IHugRepository, HugRepository>();

            services.AddScoped<IMatchRepository, MatchRepository>();
            
            services.AddScoped<IUserPrefRepository, UserPrefRepository>();

            services.AddScoped<IUserCharRepository, UserCharRepository>();



            //=========ConfigureOfApp=======================================
            services.Configure<AppSettings>(appSettingSection);
            var appSettings = appSettingSection.Get<AppSettings>();

            //services.AddDbContext<UserContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("HuggerContext")));

            //==========JWT token==========================================
            
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //==========Swagger==========================================
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0.1", new OpenApiInfo
                {
                    Title = "Hugger API",
                    Version = "version0.1",
                    Description = "Project to show our skills after 1 year of learning in codecool.",
                    Contact = new OpenApiContact
                    {
                        Name = "Patryk Duda and Huber Haznar",
                        Email = string.Empty,
                    },

                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                        new OpenApiSecurityScheme
                        {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(x => x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v0.1/swagger.json", "Hugger API 0.1");
               c.RoutePrefix = string.Empty;
           });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
