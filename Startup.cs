using System.Collections.Immutable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.HttpOverrides;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectApi.Data;
using ProjectApi.Helpers;
using ProjectApi.Models;
using ProjectApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProjectApi.Helpers;
using Microsoft.OpenApi.Models;

namespace ProjectApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly IHttpContextAccessor _contextAccessor;


        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => {
                x.UseLazyLoadingProxies();
                x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });

            ConfigureServices(services);
        }

//IApplicationBuilder app,
        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => {
                x.UseLazyLoadingProxies();
                x.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });

            ConfigureServices(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {

            
  

            services.AddSwaggerGen(c =>{
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AB Project  API",
                Version = "v1"
            });
            });

            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
           // builder.AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                         ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime=true,
                        //

                         ValidIssuer = Configuration.GetSection("AppSettings:Issuer").Value,
                        ValidAudience = Configuration.GetSection("AppSettings:Issuer").Value,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Key").Value)),
                       
                    };
                
           
            //Audience = "...",
            //Authority = "...",
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {

                    // Add the access_token as a claim, as we may actually need it
                    var accessToken = context.SecurityToken as JwtSecurityToken;

                 
                    //ClaimsPrincipal currentUser = _contextAccessor.HttpContext.User ;  

                    //var jwt = "(the JTW here)";


                    var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(accessToken.RawData);

                    if (accessToken != null)
                    {
                               
                        ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                        if (identity != null)
                        {
                            //identity.AddClaim(new Claim("access_token", accessToken.RawData));

            
                            foreach (var claim in token.Claims)
                                    {
                                        if (!identity.HasClaim(claim.Type, claim.Value))
                                            identity.AddClaim(claim);
                                    }


                        }
                    }

                    return Task.CompletedTask;
                }
            
        };
        

    });

   
            

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
                options.AddPolicy("VipOnly", policy => policy.RequireRole("VIP"));
            });

            services.AddMvc(options => 
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
             services.Configure<ForwardedHeadersOptions>(options =>
             {
          

             options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            // Only loopback proxies are allowed by default.
            // Clear that restriction because forwarders are enabled by explicit 
            // configuration.
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
            
        });


            
            services.AddCors();
            services.AddAutoMapper(typeof(IRepositoryBase<>).Assembly);
            services.AddAutoMapper(typeof(IUserService).Assembly);


            services.AddHttpContextAccessor();
            //services.AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>(); 
            
            //services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomUserClaimsPrincipalFactory>();

            //services.AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>(); 
            //services.AddAutoMapper(typeof(DatingRepository).Assembly);
           //services.AddTransient<Seed>();


            //Newly Added by Ibrahim Zakariyau
           
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));


            services.AddScoped<IAccessLevelService, AccessLevelService>();
            services.AddScoped<ICommodityService, CommodityService>();




            services.AddScoped<IFarmerVarificationStatusService, FarmerVarificationStatusService>();
            services.AddScoped<IFarmOwnershipTypeService, FarmOwnershipTypeService>();


            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IIDTypeService, IDTypeService>();

            services.AddScoped<ILgaService, LgaService>();

            services.AddScoped<IMaritalStatusService, MaritalStatusService>();
            services.AddScoped<INationalityService, NationalityService>();


            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IUserService, UserService>();


            services.AddScoped<IFarmerService, FarmerService>();


            services.AddScoped<IEOPService, EOPService>();
            services.AddScoped<IEOPTypeService, EOPTypeService>();
            services.AddScoped<IEOPUnitService, EOPUnitService>();

            services.AddScoped<IInventoryService, InventoryService>();

  
  
            //TO be remove

            services.AddScoped<LogUserActivity>();
         }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            // using Microsoft.AspNetCore.HttpOverrides;

           // app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
              //  ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            //});

            app.UseForwardedHeaders();



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
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                // app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
            
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AB Project API V1");
                });

            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
           app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Fallback", action = "Index" }
                );
            });
        }
    }
}
