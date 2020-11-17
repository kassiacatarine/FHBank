using FHBank.API.Application.Mutations.Account;
using FHBank.Application.Queries;
using FHBank.Domain.SeedWork;
using FHBank.Infrastructure;
using FHBank.Infrastructure.Repositories;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.Execution.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace FHBank.API
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
            services.AddControllers();

            services
                .AddCustomMongoDbContext(Configuration)
                .AddCustomMediatR()
                .AddCustomGraphQL();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL("/api")
                .UsePlayground(new PlaygroundOptions
                {
                    QueryPath = "/api",
                    Path = "/playground",
                });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomMongoDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSettings>(configuration.GetSection("QueriesDb"));
            var settings = configuration.GetSection("QueriesDb").Get<DbSettings>();
            services.AddSingleton<IMongoClient, MongoClient>(
                _ => new MongoClient(settings.ConnectionString));
            services.AddTransient<IQueriesContext, QueriesContext>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }

        public static IServiceCollection AddCustomGraphQL(this IServiceCollection services)
        {
            services
                .AddGraphQL(sp =>
                    SchemaBuilder.New()
                        .AddServices(sp)
                        .AddQueryType(d => d.Name("Query"))
                        .AddType<AccountQueries>()
                        .AddMutationType(d => d.Name("Mutation"))
                        .AddType<AccountMutations>()
                        .Create(),
                    new QueryExecutionOptions { ForceSerialExecution = true });
            return services;
        }

        public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            return services;
        }
    }
}
