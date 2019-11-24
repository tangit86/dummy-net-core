using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using WorkerProfileApi.Handlers;
using WorkerProfileApi.Mappers;
using WorkerProfileApi.Middleware;
using WorkerProfileApi.Models;
using WorkerProfileApi.Repositories;
using WorkerProfileApi.Services;

namespace WorkerProfileApi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        readonly string MyCorsPolicy = "_MyCorsPolicy";
        public IConfiguration Configuration { get; }

        public virtual void addAuthentication (IServiceCollection services) {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear ();

            services.AddAuthentication (options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (options => {
                options.Authority = Configuration.GetSection ("Auth:Authority").Value.ToString ();
                options.Audience = Configuration.GetSection ("Auth:Audience").Value.ToString ();

                options.TokenValidationParameters = new TokenValidationParameters {
                    NameClaimType = "Roles",
                    RoleClaimType = "http://my.roles/"
                };
                options.Events = new JwtBearerEvents {
                    OnTokenValidated = context => {
                        if (context.SecurityToken is JwtSecurityToken token) {
                            if (context.Principal.Identity is ClaimsIdentity identity) {
                                identity.AddClaim (new Claim ("access_token", token.RawData));
                            }
                        }

                        return Task.FromResult (0);
                    }
                };
            });

        }

        public virtual void addDatabaseContext (IServiceCollection services) {
            // for the model/migration stuff mostly
            services.AddDbContext<WorkerProfileApiContext> (options => options.UseSqlServer (Configuration.GetSection ("DatabaseConnection").Value.ToString (), x => x.UseNetTopologySuite ()));

            var ctxFactory = new MyDbContextFactory (Configuration.GetSection ("DatabaseConnection").Value.ToString ());

            using (var cnt = ctxFactory.Get ()) {
                cnt.Database.Migrate ();
            }
            services.AddSingleton<MyDbContextFactory> (g => ctxFactory);
            services.AddSingleton<GeometryFactory> (g => NtsGeometryServices.Instance.CreateGeometryFactory (4326));
        }

        public virtual void addAuthorization (IServiceCollection services) {
            services.AddAuthorization ();
            services.AddSingleton<IAuthorizationService, MyAuthService> ();
            services.AddSingleton<IAuthorizationHandler, ProfileAccessAuthorizationHandler> ();
            services.AddSingleton<IAuthorizationHandler, PersonalizedRouteAuthorizationHandler> ();

        }

        public virtual void addLocationService (IServiceCollection services) {
            services.AddSingleton<ILocationService> (l => new LocationService (Configuration.GetSection ("GoogleApiKey").Value.ToString ()));
        }

        public virtual void ConfigureServices (IServiceCollection services) {

            services.AddCors (options => {
                options.AddPolicy (MyCorsPolicy,
                    builder => {
                        builder.WithOrigins (Configuration.GetSection ("AllowedHosts").Value.ToString ())
                            .AllowAnyMethod ()
                            .AllowAnyHeader ();
                    });
            });

            addAuthentication (services);

            addAuthorization (services);

            addDatabaseContext (services);

            services.AddSingleton<IExceptionToApiErrorMapper, ExceptionToApiErrorMapper> ();

            services.AddSingleton<IProfileMapper, ProfileMapper> ();
            services.AddSingleton<ISkillMapper, SkillMapper> ();
            services.AddSingleton<IGeometryMapper, GeometryMapper> ();

            services.AddSingleton<ISkillsRepository, DefaultSkillsRepository> ();
            services.AddSingleton<IProfilesRepository, DefaultProfilesRepository> ();
            services.AddSingleton<IProfileService, ProfileService> ();
            services.AddSingleton<ISkillsService, SkillsService> ();

            addLocationService (services);

            services.AddControllers ();

            services.AddOpenApiDocument ();
        }

        public virtual void enableAuthentication (IApplicationBuilder application) {
            application.UseAuthentication ();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure (IApplicationBuilder app, IWebHostEnvironment env, MyDbContextFactory ctxFactory) {

            app.UseOpenApi ();
            app.UseSwaggerUi3 ();

            app.UseMiddleware<ApiExceptionsHandlingMiddleware> ();

            app.UseHsts ();
            app.UseCors (MyCorsPolicy);
            app.UseHttpsRedirection ();

            app.UseRouting ();

            enableAuthentication (app);

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });

        }
    }
}