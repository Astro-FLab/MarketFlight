using System;
using System.Data;
using System.Data.SqlClient;
using MarketFlight.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarketFlight
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services, IWebHostEnvironment env )
        {
            services.AddControllers();
            string dbConnectionString = Configuration.GetConnectionString( "dbConnection" );
            if( env.IsProduction() )
            {
                dbConnectionString = Environment.GetEnvironmentVariable( "dbConnection" )!;
            }
            Console.WriteLine( "connection string:"+dbConnectionString );
            Migration.DbSetup( dbConnectionString );
            // Inject IDbConnection, with implementation from SqlConnection class.
            services.AddTransient<IDbConnection>( ( sp ) => new SqlConnection( dbConnectionString ) );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors( a =>
                a.WithOrigins( "http://localhost:5000" )
                .WithOrigins( "localhost:5000" )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
             );

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers();
             } );
        }
    }
}
