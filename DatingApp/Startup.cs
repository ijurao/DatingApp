using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.ActionFilters;
using DatingApp.Data;
using DatingApp.Helpers;
using DatingApp.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("local")));
            ConfigureServices(services);

        }

        public void ConfigureProductiontServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("default")));
            ConfigureServices(services);

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers().AddNewtonsoftJson(op => {

                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudImages"));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("default")));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IPhotoRepository, CloudinaryPhotoRepository>();
            services.AddScoped<IDatingRepository, DatingRepository>();
            services.AddScoped<LogUserActivity>();
            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("Appsetings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                }
                );

//            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//.AddJwtBearer(options =>
//                 options.TokenValidationParameters = new TokenValidationParameters
//                 {
//                     ValidateIssuer = false,
//                     ValidateAudience = false,
//                     ValidateLifetime = true,
//                     ValidateIssuerSigningKey = true,
//                     IssuerSigningKey = new SymmetricSecurityKey(
//                    Encoding.UTF8.GetBytes(Configuration["Appsetings:Token"])),
//                     ClockSkew = TimeSpan.Zero
//                 });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.addAplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }

                    });
                });
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Pagination"));
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // siempre despues de routing 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fall");
            });

        }
    }
}
