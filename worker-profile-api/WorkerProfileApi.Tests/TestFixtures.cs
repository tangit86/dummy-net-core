using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkerProfileApi.Services;

namespace WorkerProfileApi.Tests {

    public class WorkerProfilesApiTestFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class {
        protected override IWebHostBuilder CreateWebHostBuilder () {
            return WebHost.CreateDefaultBuilder (null)
                .UseStartup<TEntryPoint> ();
        }
    }

    public class TestingOneStartup : Startup {
        public TestingOneStartup (IConfiguration configuration) : base (configuration) { }

        public override void addAuthorization (IServiceCollection services) {
            //base.addAuthorization (services);
        }

        public override void addAuthentication (IServiceCollection services) {
            //base.addAuthorization (services);
        }

        public override void enableAuthentication (IApplicationBuilder app) {
            //base.addAuthorization (services);
        }
        public override void addDatabaseContext (IServiceCollection services) {
            base.addDatabaseContext (services);
        }

        public override void addLocationService (IServiceCollection services) {
            base.addLocationService (services);
        }

        public override void Configure (IApplicationBuilder app, IWebHostEnvironment env, MyDbContextFactory ctxFactory) {
            base.Configure (app, env, ctxFactory);
        }

        public override void ConfigureServices (IServiceCollection services) {
            base.ConfigureServices (services);
        }

        public override bool Equals (object obj) {
            return base.Equals (obj);
        }

        public override int GetHashCode () {
            return base.GetHashCode ();
        }

        public override string ToString () {
            return base.ToString ();
        }
    }
    class FakeUserFilter : IAsyncActionFilter {
        public async Task OnActionExecutionAsync (ActionExecutingContext context, ActionExecutionDelegate next) {
            context.HttpContext.User = new ClaimsPrincipal (new ClaimsIdentity (new List<Claim> {
                new Claim (ClaimTypes.NameIdentifier, "123"),
                new Claim (ClaimTypes.Name, "Test user"),
                new Claim (ClaimTypes.Email, "test @example.com"),
                new Claim (ClaimTypes.Role, "Admin")
            }));

            await next ();
        }
    }

}