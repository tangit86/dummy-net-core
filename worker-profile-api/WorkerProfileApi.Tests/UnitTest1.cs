using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace WorkerProfileApi.Tests {
    public class UnitTest1 : IClassFixture<WorkerProfilesApiTestFactory<TestingOneStartup>> {

        private readonly WorkerProfilesApiTestFactory<TestingOneStartup> _factory;

        public UnitTest1 (WorkerProfilesApiTestFactory<TestingOneStartup> factory) {
            this._factory = factory;
        }

        [Fact]
        public async System.Threading.Tasks.Task Test1Async () {
            // Arrange
            var client = GetFactory (true).CreateClient ();

            // Act
            var response = await client.GetAsync ("/api/v1/skills");

            // Assert
            response.EnsureSuccessStatusCode (); // Status Code 200-299
            Assert.Equal ("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString ());

        }

        private WebApplicationFactory<TestingOneStartup> GetFactory (bool hasUser = false) {
            var projectDir = Directory.GetCurrentDirectory ();
            var configPath = Path.Combine (projectDir, "appsettings.test.json");

            return _factory.WithWebHostBuilder (builder => {

                builder.UseSolutionRelativeContentRoot ("WorkerProfileApi");

                builder.ConfigureTestServices (services => {
                    services.AddMvc (options => {
                            if (hasUser) {
                                options.Filters.Add (new AllowAnonymousFilter ());
                                options.Filters.Add (new FakeUserFilter ());
                            }
                        })
                        .AddApplicationPart (typeof (Startup).Assembly);
                });
            });
        }
    }
}